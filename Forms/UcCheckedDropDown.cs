using System.Collections;
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
		private string m_sLabel = string.Empty;
		#endregion

		#region Properties
		[DefaultValue("")]
		public string Label
		{
			get => m_sLabel;
			set { m_sLabel = value ?? string.Empty; UpdateButton(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<object> CheckedItems
		{
			get => m_checkedList.CheckedItems.Cast<object>();
			set
			{
				m_checkedList.ItemCheck -= CheckedList_ItemCheck;
				HashSet<object> checkedSet = value?.ToHashSet() ?? [];
				for (int i = 0; i < m_checkedList.Items.Count; i++)
					m_checkedList.SetItemChecked(i, checkedSet.Contains(m_checkedList.Items[i]));

				UpdateButton();
				m_checkedList.ItemCheck += CheckedList_ItemCheck;
			}
		}

		public event EventHandler ItemCheckedChanged;
		#endregion

		#region CTOR
		public UcCheckedDropDown()
		{
			m_checkedList = new CheckedListBox { CheckOnClick = true, BorderStyle = BorderStyle.None };
			m_checkedList.ItemCheck += CheckedList_ItemCheck;

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
		private void UpdateButton()
		{
			int iCount = m_checkedList.CheckedItems.Count;
			m_button.Text = iCount > 0 ? $"{m_sLabel}({iCount}) ▾" : $"{m_sLabel}▾";

			if (iCount > 0)
			{
				m_button.BackColor = s_colorCheckedBack;
				m_button.ForeColor = s_colorCheckedFore;
				m_toolTip.SetToolTip(m_button, string.Join(Environment.NewLine, CheckedItems.Select(_ci => m_checkedList.GetItemText(_ci))));
			}
			else
			{
				m_button.BackColor = SystemColors.Control;
				m_button.ForeColor = SystemColors.ControlText;
				m_toolTip.SetToolTip(m_button, null);
			}
		}

		public void SetDataSource(IEnumerable dataSource, string sDisplayMember)
		{
			m_checkedList.ItemCheck -= CheckedList_ItemCheck;

			m_checkedList.Items.Clear();
			m_checkedList.DisplayMember = sDisplayMember;
			if (dataSource is not null)
				foreach (object item in dataSource)
					m_checkedList.Items.Add(item);

			UpdateButton();

			m_checkedList.ItemCheck += CheckedList_ItemCheck;
		}
		#endregion

		#region Events
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
