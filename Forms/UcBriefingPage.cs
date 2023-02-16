using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPage : UserControl
	{
		#region Fields
		private BopBriefingPage m_bopBriefingPage;

		private GridManagerBriefingParts m_gridManagerBriefingParts;
		private UcBriefingPartBase m_ucBriefingPart;
		private FrmBriefingFolder m_frmBriefingFolderParent;
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
		public UcBriefingPage(BopBriefingPage bopBriefingPage, FrmBriefingFolder frmBriefingFolderParent)
		{
			m_bopBriefingPage = bopBriefingPage;
			m_frmBriefingFolderParent = frmBriefingFolderParent;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbHeader);
			ToolsStyle.LabelHeader(LbParts);


			m_gridManagerBriefingParts = new GridManagerBriefingParts(DgvParts, m_bopBriefingPage.Parts);
			m_gridManagerBriefingParts.SelectionChangedTyped += SelectionChangedTypedEvent;

			//MapControl.InitializeMapControl();
			//MapControl.MapProvider = GMapProviders.TryGetProvider(m_briefopManager.BopMission.Miz.MizBopCustom.PreferencesMap.ProviderName);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerBriefingParts.SelectionChangedTyped -= SelectionChangedTypedEvent;

			TbTitle.Text = m_bopBriefingPage.Title;
			CkDisplayTitle.Checked= m_bopBriefingPage.DisplayTitle;
			CkRenderHtml.Checked = (m_bopBriefingPage.Render & Data.ElementBriefingPageRender.Html) != 0;
			CkRenderMap.Checked = (m_bopBriefingPage.Render & Data.ElementBriefingPageRender.Map) != 0;

			m_gridManagerBriefingParts.Initialize();
			DataToScreenPart();

			DataToScreenMap();

			m_gridManagerBriefingParts.SelectionChangedTyped += SelectionChangedTypedEvent;
		}

		private void DataToScreenPart()
		{
			TpPartDetail.Controls.Clear();
			m_ucBriefingPart = null;
			IEnumerable<BopBriefingPart> selecteds = m_gridManagerBriefingParts.GetSelected();
			if (selecteds.Count() == 1)
			{
				BopBriefingPart selected = selecteds.First();
				if (selected is BopBriefingPartBullseye partTyped)
				{
					m_ucBriefingPart = new UcBriefingPartBullseye(partTyped);
				}

				if (m_ucBriefingPart is object)
				{
					m_ucBriefingPart.Dock= DockStyle.Fill;
					TpPartDetail.Controls.Add(m_ucBriefingPart);
				}
			}
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
			m_bopBriefingPage.Title = TbTitle.Text;
			m_bopBriefingPage.DisplayTitle = CkDisplayTitle.Checked;

			m_bopBriefingPage.Render = ElementBriefingPageRender.None;
			if (CkRenderHtml.Checked)
				m_bopBriefingPage.Render |= ElementBriefingPageRender.Html;
			if (CkRenderMap.Checked)
				m_bopBriefingPage.Render |= ElementBriefingPageRender.Map;

			ScreenToDataPart();
		}

		public void ScreenToDataPart()
		{
			m_ucBriefingPart?.ScreenToData();
		}

		private async void RefreshHtml()
		{
			PbHtml.Image = await m_bopBriefingPage.BuildHtmlImage();
			TbHtmlSource.Text = m_bopBriefingPage.BuildHtmlString();
		}

		#endregion

		private void SelectionChangedTypedEvent(object sender, GridManagerBriefingParts.EventArgsBopBriefingParts e)
		{
			ScreenToDataPart();
			DataToScreenPart();
		}

		private void BtRefresh_Click(object sender, EventArgs e)
		{
			if (m_frmBriefingFolderParent is object)
				m_frmBriefingFolderParent.ScreenToData();
			else
				ScreenToData();

			RefreshHtml();
		}
	}
}
