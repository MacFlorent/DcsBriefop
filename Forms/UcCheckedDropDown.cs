using System.ComponentModel;

namespace DcsBriefop.Forms
{
	internal class UcCheckedDropDown : UserControl
	{
		#region Fields
		private static readonly Color s_colorCheckedBack = Color.FromArgb(204, 228, 247);
		private static readonly Color s_colorCheckedFore = Color.FromArgb(0, 84, 153);

		private readonly Button m_button;
		private readonly ToolStripDropDown m_dropDown;
		private readonly CheckedListBox m_checkedList;
		private readonly ToolTip m_toolTip = new();
		private string m_label = string.Empty;
		#endregion

		#region Properties
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Label
		{
			get => m_label;
			set { m_label = value; UpdateButton(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckedListBox CheckedListBox => m_checkedList;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<string> CheckedNames =>
			m_checkedList.CheckedItems.Cast<object>().Select(_o => m_checkedList.GetItemText(_o));
		#endregion

		#region CTOR
		public UcCheckedDropDown()
		{
			m_checkedList = new CheckedListBox { CheckOnClick = true, BorderStyle = BorderStyle.None };
			m_checkedList.ItemCheck += CheckedList_ItemCheck;
			m_checkedList.DataSourceChanged += (_, _) => BeginInvoke(UpdateButton);

			ToolStripControlHost host = new(m_checkedList) { Margin = Padding.Empty, Padding = Padding.Empty };
			m_dropDown = new ToolStripDropDown { Padding = Padding.Empty };
			m_dropDown.Items.Add(host);

			m_button = new Button { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
			m_button.Click += Button_Click;
			Controls.Add(m_button);
			Height = 23;
			UpdateButton();
		}
		#endregion

		#region Methods
		public void UpdateButton()
		{
			int iCount = m_checkedList.CheckedItems.Count;
			string sPrefix = string.IsNullOrEmpty(m_label) ? string.Empty : $"{m_label} ";
			m_button.Text = iCount > 0 ? $"{sPrefix}({iCount}) ▾" : $"{sPrefix}▾";

			if (iCount > 0)
			{
				m_button.BackColor = s_colorCheckedBack;
				m_button.ForeColor = s_colorCheckedFore;
				m_toolTip.SetToolTip(m_button, string.Join(Environment.NewLine, CheckedNames));
			}
			else
			{
				m_button.BackColor = SystemColors.Control;
				m_button.ForeColor = SystemColors.ControlText;
				m_toolTip.SetToolTip(m_button, null);
			}
		}
		#endregion

		#region Events
		public event EventHandler ItemCheckedChanged;

		private void Button_Click(object sender, EventArgs e)
		{
			int iItemHeight = m_checkedList.ItemHeight;
			int iCount = m_checkedList.Items.Count;
			int iListHeight = Math.Clamp(iCount * iItemHeight + 4, iItemHeight + 4, iItemHeight * 10 + 4);
			m_checkedList.Width = Math.Max(Width, 150);
			m_checkedList.Height = iListHeight;
			m_dropDown.Show(m_button, new Point(0, m_button.Height));
		}

		private void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			BeginInvoke(() =>
			{
				UpdateButton();
				ItemCheckedChanged?.Invoke(this, EventArgs.Empty);
			});
		}
		#endregion
	}
}
