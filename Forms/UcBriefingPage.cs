using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPage : UserControl
	{
		#region Fields
		private BopMission m_bopMission;
		private BopBriefingFolder m_bopBriefingFolder;
		private BopBriefingPage m_bopBriefingPage;

		private GridManagerBriefingParts m_gridManagerBriefingParts;
		private UcBriefingPartBase m_ucBriefingPart;
		private FrmBriefingFolder m_frmBriefingFolderParent;
		private UcMap m_ucMap;
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
		public UcBriefingPage(BopMission bopMission, BopBriefingFolder bopBriefingFolder, BopBriefingPage bopBriefingPage, FrmBriefingFolder frmBriefingFolderParent)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;
			m_bopBriefingPage = bopBriefingPage;
			m_frmBriefingFolderParent = frmBriefingFolderParent;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbHeader);
			ToolsStyle.LabelHeader(LbParts);
			ToolsStyle.ButtonOk(BtPartAdd);
			ToolsStyle.ButtonCancel(BtPartRemove);

			m_gridManagerBriefingParts = new GridManagerBriefingParts(DgvParts, m_bopBriefingPage.Parts);
			m_gridManagerBriefingParts.SelectionChanged += SelectionChangedEvent;
			DgvParts.FilterAndSortEnabled = false;
			DgvParts.MultiSelect = false;

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerBriefingParts.SelectionChanged -= SelectionChangedEvent;
			CkRenderHtml.CheckedChanged -= CkRender_CheckedChanged;
			CkRenderMap.CheckedChanged -= CkRender_CheckedChanged;

			TbTitle.Text = m_bopBriefingPage.Title;
			CkDisplayTitle.Checked = m_bopBriefingPage.DisplayTitle;
			CkRenderHtml.Checked = (m_bopBriefingPage.Render & ElementBriefingPageRender.Html) != 0;
			CkRenderMap.Checked = (m_bopBriefingPage.Render & ElementBriefingPageRender.Map) != 0;

			m_gridManagerBriefingParts.BriefingParts = m_bopBriefingPage.Parts;
			m_gridManagerBriefingParts.Initialize();

			DisplayCurrentRender();
			DataToScreenPart();
			DataToScreenMap();

			m_gridManagerBriefingParts.SelectionChanged += SelectionChangedEvent;
			CkRenderHtml.CheckedChanged += CkRender_CheckedChanged;
			CkRenderMap.CheckedChanged += CkRender_CheckedChanged;
		}

		private void DataToScreenPart()
		{
			TpPartDetail.Controls.Clear();
			m_ucBriefingPart = null;
			IEnumerable<BopBriefingPartBase> selecteds = m_gridManagerBriefingParts.GetSelectedElements();
			if (selecteds.Count() == 1)
			{
				BopBriefingPartBase selected = selecteds.First();
				if (selected is BopBriefingPartBullseye)
					m_ucBriefingPart = new UcBriefingPartBullseye(selected, m_bopMission, this);
				else if (selected is BopBriefingPartParagraph)
					m_ucBriefingPart = new UcBriefingPartParagraph(selected, m_bopMission, this);
				else if (selected is BopBriefingPartAirbases)
					m_ucBriefingPart = new UcBriefingPartAirbases(selected, m_bopMission, this);
				else if (selected is BopBriefingPartGroups)
					m_ucBriefingPart = new UcBriefingPartGroups(selected, m_bopMission, this);

				if (m_ucBriefingPart is object)
				{
					m_ucBriefingPart.Dock = DockStyle.Fill;
					TpPartDetail.Controls.Add(m_ucBriefingPart);
				}
			}
		}

		private void DataToScreenMap()
		{
			CkMapIncludeBaseOverlays.CheckedChanged -= CkMapIncludeBaseOverlays_CheckedChanged;

			CkMapIncludeBaseOverlays.Checked = m_bopBriefingPage.MapIncludeBaseOverlays;

			m_ucMap.MapData = m_bopBriefingPage.MapData;
			m_ucMap.MapProviderName = m_bopMission.PreferencesMap.ProviderName;
			DisplayCurrentMap();

			CkMapIncludeBaseOverlays.CheckedChanged += CkMapIncludeBaseOverlays_CheckedChanged;
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
			ScreenToDataMap();
		}

		public void ScreenToDataPart()
		{
			m_ucBriefingPart?.ScreenToData();
		}

		public void ScreenToDataMap()
		{
			m_bopBriefingPage.MapIncludeBaseOverlays = CkMapIncludeBaseOverlays.Checked;
		}

		private void ScreenToDataFromParent()
		{
			if (m_frmBriefingFolderParent is object)
				m_frmBriefingFolderParent.ScreenToData();
			else
				ScreenToData();
		}

		private async void RefreshHtmlPreview()
		{
			PbHtmlPreview.Image = await m_bopBriefingPage.BuildHtmlImage(m_bopMission, m_bopBriefingFolder);
			TbHtmlPreviewSource.Text = m_bopBriefingPage.BuildHtmlString(m_bopMission, m_bopBriefingFolder);
		}

		private void RefreshMapPreview()
		{
			PbMapPreview.Image = m_bopBriefingPage.BuildMapImage(m_bopMission, m_bopBriefingFolder);
		}

		private void DisplayCurrentRender()
		{
			TabPage tpSelected = TcDetail.SelectedTab;
			TcDetail.TabPages.Clear();
			TcDetail.TabPages.Add(TpPartDetail);
			if (CkRenderMap.Checked)
				TcDetail.TabPages.Add(TpMapDetail);
			if (CkRenderHtml.Checked)
				TcDetail.TabPages.Add(TpHtmlPreview);
			if (CkRenderMap.Checked)
				TcDetail.TabPages.Add(TpMapPreview);


			if (TcDetail.TabPages.Contains(tpSelected))
				TcDetail.SelectedTab = tpSelected;
		}

		public void DisplayCurrentMap()
		{
			m_ucMap.StaticOverlays = m_bopBriefingPage.GetMapAdditionalOverlays(m_bopMission, m_bopBriefingFolder);
			m_ucMap.RefreshMapData();
		}

		private void AddPart(ElementBriefingPartType partType)
		{
			BopBriefingPartBase newPart = m_bopBriefingPage.AddPart(partType);
			m_gridManagerBriefingParts.Initialize();
			if (m_bopBriefingPage.Parts.Count() == 1)
				DataToScreenPart();
			else
				m_gridManagerBriefingParts.SelectElement(newPart);
		}

		private void RemovePart()
		{
			foreach (BopBriefingPartBase partToRemove in m_gridManagerBriefingParts.GetSelectedElements())
			{
				m_bopBriefingPage.Parts.Remove(partToRemove);
			}
			m_gridManagerBriefingParts.Initialize();
			DataToScreenPart();
		}

		private void OrderPart(int iWay)
		{
			BopBriefingPartBase selectedElement = m_gridManagerBriefingParts.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				m_bopBriefingPage.OrderPart(selectedElement, iWay);
				m_gridManagerBriefingParts.Initialize();
				m_gridManagerBriefingParts.SelectElement(selectedElement);
			}
		}
		#endregion

		#region Events
		private void CkRender_CheckedChanged(object sender, EventArgs e)
		{
			DisplayCurrentRender();
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataPart();
			m_gridManagerBriefingParts.RefreshDataSourceRows();
			DataToScreenPart();
		}

		private void BtPartAdd_MouseDown(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Clear();

			foreach (MasterData partType in MasterDataRepository.GetMasterDataList(MasterDataType.BriefingPartType))
			{
				menu.Items.AddMenuItem(partType.Label, (object _sender, EventArgs _e) => { AddPart((ElementBriefingPartType)partType.Id); });
			}

			if (menu.Items.Count > 0)
			{
				menu.Show(BtPartAdd, new Point(0, BtPartAdd.Height));
			}
		}

		private void BtPartRemove_Click(object sender, EventArgs e)
		{
			RemovePart();
		}

		private void CkMapIncludeBaseOverlays_CheckedChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DisplayCurrentMap();
		}

		private void BtHtmlPreviewRefresh_Click(object sender, EventArgs e)
		{
			ScreenToDataFromParent();
			RefreshHtmlPreview();
		}

		private void BtMapPreviewRefresh_Click(object sender, EventArgs e)
		{
			ScreenToDataFromParent();
			RefreshMapPreview();
		}

		#endregion


		private void BtPartOrderUp_Click(object sender, EventArgs e)
		{
			OrderPart(-1);
		}

		private void BtPartOrderDown_Click(object sender, EventArgs e)
		{
			OrderPart(1);
		}
	}
}
