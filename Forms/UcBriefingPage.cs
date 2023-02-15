using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPage : UserControl
	{
		#region Fields
		private BopBriefingPage m_bopBriefingPage;
		#endregion

		#region Properties
		public BopBriefingPage BopBriefingPage
		{
			private get { return m_bopBriefingPage; }
			set
			{
				m_bopBriefingPage = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcBriefingPage(BopBriefingPage bopBriefingPage)
		{
			m_bopBriefingPage = bopBriefingPage;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			//MapControl.InitializeMapControl();
			//MapControl.MapProvider = GMapProviders.TryGetProvider(m_briefopManager.BopMission.Miz.MizBopCustom.PreferencesMap.ProviderName);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			LbTitle.Text = m_bopBriefingPage.Title;
			CkDisplayTitle.Checked= m_bopBriefingPage.DisplayTitle;

			DataToScreenMap();
		}

		private void DataToScreenMap()
		{
			//UcGroupBase activeTabControl = null;
			//if (TcDetails.SelectedTab is object && TcDetails.SelectedTab.Controls.Count > 0)
			//	activeTabControl = TcDetails.SelectedTab.Controls[0] as UcGroupBase;

			//activeTabControl?.DataToScreenMap();
		}

		public void ScreenToData()
		{
			m_bopBriefingPage.Title = LbTitle.Text;
			m_bopBriefingPage.DisplayTitle = CkDisplayTitle.Checked;
		}

		private async void RefreshHtml()
		{
			PbHtml.Image = await m_bopBriefingPage.BuildHtmlImage();
		}

		#endregion

		private void BtRefresh_Click(object sender, EventArgs e)
		{
			RefreshHtml();


		}
	}
}
