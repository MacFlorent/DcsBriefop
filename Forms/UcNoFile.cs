using DcsBriefop.Tools;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcNoFile : UserControl
	{
		#region Fields
		private FrmMain m_mainForm;
		#endregion

		#region CTOR
		public UcNoFile(FrmMain mainForm)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.SetBackgroundImage(this);
			TlRecentMiz.CenterInParent();

			m_mainForm = mainForm;
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			TlRecentMiz.Controls.Clear();
			TlRecentMiz.ColumnCount = 1;
			TlRecentMiz.RowCount = 0;
			TlRecentMiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

			AddLinkOpenMiz(null);

			if (PreferencesManager.Preferences.Application.RecentMiz.Count > 0)
			{
				Label lb = new Label();
				ToolsStyle.LabelTitle(lb);
				lb.Anchor = AnchorStyles.Bottom;
				lb.AutoSize = true;
				lb.Text = "Recent miz files";
				AddControl(lb);

				foreach (string sRecentMizFilePath in PreferencesManager.Preferences.Application.RecentMiz)
					AddLinkOpenMiz(sRecentMizFilePath);
			}
			
			TlRecentMiz.CenterInParent();
		}

		private void AddLinkOpenMiz(string sMizFilePath)
		{
			LinkLabel lkOpen = new LinkLabel();
			ToolsStyle.LabelDefault(lkOpen);
			lkOpen.Anchor = AnchorStyles.None;
			lkOpen.AutoSize = true;
			lkOpen.Padding = new Padding(10);

			if (string.IsNullOrEmpty(sMizFilePath))
			{
				lkOpen.Text = "Open miz file";
				lkOpen.LinkClicked += (object _sender, LinkLabelLinkClickedEventArgs _e) => { m_mainForm?.MizOpen(); };
			}
			else
			{
				lkOpen.Text = sMizFilePath;
				lkOpen.LinkClicked += (object _sender, LinkLabelLinkClickedEventArgs _e) => { m_mainForm?.MizOpen(sMizFilePath); };
			}

			AddControl(lkOpen);
		}

		private void AddControl(Control control)
		{
			TlRecentMiz.RowCount++;
			TlRecentMiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			TlRecentMiz.Controls.Add(control, 0, TlRecentMiz.RowCount - 1);
		}

		#endregion
	}
}
