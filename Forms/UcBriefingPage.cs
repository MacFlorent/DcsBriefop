﻿using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPage : UserControl
	{
		#region Fields
		private BriefopManager m_bopManager;
		private BopBriefingFolder m_bopBriefingFolder;
		private BopBriefingPage m_bopBriefingPage;

		private GridManagerBriefingParts m_gridManagerBriefingParts;
		private UcBriefingPartBase m_ucBriefingPart;
		private FrmBriefingFolder m_frmBriefingFolderParent;
		private UcMap m_ucMap;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcBriefingPage(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder, BopBriefingPage bopBriefingPage, FrmBriefingFolder frmBriefingFolderParent)
		{
			m_bopManager = bopManager;
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
			NudHtmlFontSize.Value = m_bopBriefingPage.HtmlFontSize;

			m_gridManagerBriefingParts.Elements = m_bopBriefingPage.Parts;
			m_gridManagerBriefingParts.Refresh();

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
			IEnumerable<BaseBopBriefingPart> selecteds = m_gridManagerBriefingParts.GetSelectedElements();
			if (selecteds.Count() == 1)
			{
				BaseBopBriefingPart selected = selecteds.First();
				if (selected is BopBriefingPartBullseye)
					m_ucBriefingPart = new UcBriefingPartBullseye(selected, m_bopManager, this);
				else if (selected is BopBriefingPartParagraph)
					m_ucBriefingPart = new UcBriefingPartParagraph(selected, m_bopManager, this);
				else if (selected is BopBriefingPartAirbases)
					m_ucBriefingPart = new UcBriefingPartAirbases(selected, m_bopManager, this);
				else if (selected is BopBriefingPartGroups)
					m_ucBriefingPart = new UcBriefingPartGroups(selected, m_bopManager, this);
				else if (selected is BopBriefingPartImage)
					m_ucBriefingPart = new UcBriefingPartImage(selected, m_bopManager, this);
				else if (selected is BopBriefingPartWaypoints)
					m_ucBriefingPart = new UcBriefingPartWaypoints(selected, m_bopManager, this);
				else if (selected is BopBriefingPartTableText)
					m_ucBriefingPart = new UcBriefingPartTableText(selected, m_bopManager, this);

				if (m_ucBriefingPart is not null)
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
			m_ucMap.MapProviderName = m_bopManager.BopMission.PreferencesMap.ProviderName;
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

			m_bopBriefingPage.HtmlFontSize = (int)NudHtmlFontSize.Value;

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
			using (new WaitDialog(ParentForm))
			{
				PbHtmlPreview.Image = await m_bopBriefingPage.BuildHtmlImage(m_bopManager, m_bopBriefingFolder);
			}
		}

		private void RefreshMapPreview()
		{
			PbMapPreview.Image = m_bopBriefingPage.BuildMapImage(m_bopManager, m_bopBriefingFolder);
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
			m_ucMap.StaticOverlays = m_bopBriefingPage.GetMapAdditionalOverlays(m_bopManager, m_bopBriefingFolder);
			m_ucMap.DataToScreen();
		}

		private void AddPart(ElementBriefingPartType partType)
		{
			BaseBopBriefingPart newPart = m_bopBriefingPage.AddPart(partType);
			m_gridManagerBriefingParts.Refresh();
			if (m_bopBriefingPage.Parts.Count() == 1)
				DataToScreenPart();
			else
				m_gridManagerBriefingParts.SelectRow(newPart);
		}

		private void RemovePart()
		{
			foreach (BaseBopBriefingPart partToRemove in m_gridManagerBriefingParts.GetSelectedElements())
			{
				m_bopBriefingPage.Parts.Remove(partToRemove);
			}
			m_gridManagerBriefingParts.Refresh();
			DataToScreenPart();
		}

		private void OrderPart(int iWay)
		{
			BaseBopBriefingPart selectedElement = m_gridManagerBriefingParts.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				m_bopBriefingPage.OrderPart(selectedElement, iWay);
				m_gridManagerBriefingParts.Refresh();
				m_gridManagerBriefingParts.SelectRow(selectedElement);
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
			ContextMenuStrip menu = new();
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

		private void BtPartOrderUp_Click(object sender, EventArgs e)
		{
			OrderPart(-1);
		}

		private void BtPartOrderDown_Click(object sender, EventArgs e)
		{
			OrderPart(1);
		}

		private void CkMapIncludeBaseOverlays_CheckedChanged(object sender, EventArgs e)
		{
			ScreenToDataMap();
			DisplayCurrentMap();
		}

		private void BtHtmlPreviewRefresh_Click(object sender, EventArgs e)
		{
			ScreenToDataFromParent();
			RefreshHtmlPreview();
		}

		private void BtHtmlPreviewRefresh_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				string sHtml = m_bopBriefingPage.BuildHtmlString(m_bopManager, m_bopBriefingFolder);
				Clipboard.SetText(sHtml);
			}
		}

		private void BtMapPreviewRefresh_Click(object sender, EventArgs e)
		{
			ScreenToDataFromParent();
			RefreshMapPreview();
		}
		#endregion
	}
}
