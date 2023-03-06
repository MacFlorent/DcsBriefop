using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Linq;

namespace DcsBriefop.Forms
{
	internal partial class FrmBriefingFolder : FrmWithWaitDialog
	{
		#region Fields
		private BopMission m_bopMission;
		private BopBriefingFolder m_bopBriefingFolder;

		private GridManagerBriefingPages m_gridManagerBriefingPages;
		private UcBriefingPage m_ucBriefingPage;
		#endregion

		#region CTOR
		public FrmBriefingFolder(BopMission bopMission, BopBriefingFolder bopBriefingFolder, WaitDialog waitDialog) : base(waitDialog)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbHeader);
			ToolsStyle.LabelHeader(LbPages);
			ToolsStyle.ButtonOk(BtPageAdd);
			ToolsStyle.ButtonCancel(BtPageRemove);

			MasterDataRepository.FillCombo(MasterDataType.Coalition, CbCoalition, CbCoalition_SelectedValueChanged);
			MasterDataRepository.FillCombo(MasterDataType.WeatherDisplay, CbWeatherDisplay, null);
			MasterDataRepository.FillCombo(MasterDataType.MeasurementSystem, CbMeasurementSystem, null);
			MasterDataRepository.FillCheckedListBox(MasterDataType.CoordinateDisplay, LstCoordinateDisplay);

			m_gridManagerBriefingPages = new GridManagerBriefingPages(DgvPages, m_bopBriefingFolder.Pages);
			m_gridManagerBriefingPages.SelectionChanged += SelectionChangedEvent;
		}

		public static void CreateModal(BopMission bopMission, BopBriefingFolder bopBriefingFolder, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmBriefingFolder f = new FrmBriefingFolder(bopMission, bopBriefingFolder, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbCoalition.SelectedValueChanged -= CbCoalition_SelectedValueChanged;

			TbName.Text = m_bopBriefingFolder.Name;
			CbCoalition.Text = m_bopBriefingFolder.CoalitionName;
			CbWeatherDisplay.SelectedValue = (int)m_bopBriefingFolder.WeatherDisplay;
			CbMeasurementSystem.SelectedValue = (int)m_bopBriefingFolder.MeasurementSystem;
			MasterDataRepository.SetFlagCheckedListbox((int)m_bopBriefingFolder.CoordinateDisplay, LstCoordinateDisplay);
			UcImageSize.SelectedSize = m_bopBriefingFolder.ImageSize;

			DataToScreenUnitTypes();
			DataToScreenGridPages();
			DataToScreenDetail();

			CbCoalition.SelectedValueChanged += CbCoalition_SelectedValueChanged;
		}

		private void DataToScreenUnitTypes()
		{
			LstKneeboards.DataSource = m_bopMission.GetKneeboards(m_bopBriefingFolder.CoalitionName).OrderBy(_k => _k).ToList();
			for (int i = 0; i < LstKneeboards.Items.Count; i++)
			{
				LstKneeboards.SetItemChecked(i, m_bopBriefingFolder.Kneeboards.Contains(LstKneeboards.Items[i]));
			}

		}
		private void DataToScreenGridPages()
		{
			m_gridManagerBriefingPages.SelectionChanged -= SelectionChangedEvent;
			m_gridManagerBriefingPages.Initialize();
			m_gridManagerBriefingPages.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopBriefingPage> selectedPages = m_gridManagerBriefingPages.GetSelectedElements();
			if (selectedPages.Count() == 1)
			{
				BopBriefingPage selectedPage = selectedPages.First();
				if (ScMain.Panel2.Controls.Count > 0 && !(ScMain.Panel2.Controls[0] is UcBriefingPage))
				{
					ScMain.Panel2.Controls.Clear();
				}
				if (m_ucBriefingPage is null)
				{
					m_ucBriefingPage = new UcBriefingPage(m_bopMission, m_bopBriefingFolder, selectedPage, this);
				}
				else
				{
					m_ucBriefingPage.BopBriefingPage = selectedPage;
				}

				if (ScMain.Panel2.Controls.Count == 0)
				{
					ScMain.Panel2.Controls.Add(m_ucBriefingPage);
					m_ucBriefingPage.Dock = DockStyle.Fill;
				}
			}
			else
			{
				ScMain.Panel2.Controls.Clear();
			}
		}

		public void ScreenToData()
		{
			m_bopBriefingFolder.Name = TbName.Text;
			m_bopBriefingFolder.CoalitionName = CbCoalition.Text;
			m_bopBriefingFolder.WeatherDisplay = (ElementWeatherDisplay)CbWeatherDisplay.SelectedValue;
			m_bopBriefingFolder.MeasurementSystem = (ElementMeasurementSystem)CbMeasurementSystem.SelectedValue;
			m_bopBriefingFolder.CoordinateDisplay = (ElementCoordinateDisplay)MasterDataRepository.GetFlagCheckedListbox(LstCoordinateDisplay);
			m_bopBriefingFolder.ImageSize = UcImageSize.SelectedSize;

			m_bopBriefingFolder.Kneeboards.Clear();
			m_bopBriefingFolder.Kneeboards.AddRange(LstKneeboards.CheckedItems.OfType<string>());

			ScreenToDataDetail();
		}

		private void ScreenToDataDetail()
		{
			m_ucBriefingPage?.ScreenToData();
		}

		private void AddPage()
		{
			BopBriefingPage bopBriefingPage = m_bopBriefingFolder.AddPage(m_bopMission);
			DataToScreenGridPages();
			if (m_bopBriefingFolder.Pages.Count() == 1)
				DataToScreenDetail();
			else
				m_gridManagerBriefingPages.SelectElement(bopBriefingPage);
		}


		private void RemovePage()
		{
			m_bopBriefingFolder.Pages.Remove(m_gridManagerBriefingPages.GetSelectedElements().FirstOrDefault());
			DataToScreenGridPages();
			DataToScreenDetail();
		}

		private void OrderPage(int iWay)
		{
			BopBriefingPage selectedElement = m_gridManagerBriefingPages.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				m_bopBriefingFolder.OrderPage(selectedElement, iWay);
				m_gridManagerBriefingPages.Initialize();
				m_gridManagerBriefingPages.SelectElement(selectedElement);
			}
		}
		#endregion

		#region Events
		private void FrmBriefingFolder_Shown(object sender, EventArgs e)
		{
			DataToScreen();
		}

		private void FrmBriefingFolder_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToData();
		}

		private void CbCoalition_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DataToScreenUnitTypes();
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataDetail();
			m_gridManagerBriefingPages.RefreshDataSourceRows();
			DataToScreenDetail();
		}

		private void BtPageAdd_Click(object sender, EventArgs e)
		{
			AddPage();
		}

		private void BtPageRemove_Click(object sender, EventArgs e)
		{
			RemovePage();
		}

		private void BtPageOrderUp_Click(object sender, EventArgs e)
		{
			OrderPage(-1);
		}

		private void BtPageOrderDown_Click(object sender, EventArgs e)
		{
			OrderPage(1);
		}
		#endregion


	}


}
