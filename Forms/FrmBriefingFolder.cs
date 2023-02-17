using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

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

			MasterDataRepository.FillCombo(MasterDataType.Coalition, CbCoalition, null);
			MasterDataRepository.FillCombo(MasterDataType.WeatherDisplay, CbWeatherDisplay, null);
			MasterDataRepository.FillCombo(MasterDataType.MeasurementSystem, CbMeasurementSystem, null);
			MasterDataRepository.FillListBox(MasterDataType.CoordinateDisplay, LstCoordinateDisplay);

			m_gridManagerBriefingPages = new GridManagerBriefingPages(DgvPages, m_bopBriefingFolder.Pages);
			m_gridManagerBriefingPages.SelectionChangedTyped += SelectionChangedTypedEvent;

			DataToScreen();
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
			m_gridManagerBriefingPages.SelectionChangedTyped -= SelectionChangedTypedEvent;

			TbName.Text = m_bopBriefingFolder.Name;
			CbCoalition.Text = m_bopBriefingFolder.CoalitionName;
			CbWeatherDisplay.SelectedValue = (int)m_bopBriefingFolder.WeatherDisplay;
			CbMeasurementSystem.SelectedValue = (int)m_bopBriefingFolder.MeasurementSystem;
			MasterDataRepository.SetFlagCheckedListbox((int)m_bopBriefingFolder.CoordinateDisplay, LstCoordinateDisplay);
			UcImageSize.SelectedSize = m_bopBriefingFolder.ImageSize;

			m_gridManagerBriefingPages.Initialize();
			DataToScreenDetail();

			m_gridManagerBriefingPages.SelectionChangedTyped += SelectionChangedTypedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopBriefingPage> selectedPages = m_gridManagerBriefingPages.GetSelected();
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

			ScreenToDataDetail();
		}

		private void ScreenToDataDetail()
		{
			m_ucBriefingPage?.ScreenToData();
		}
		#endregion

		#region Events
		private void FrmBriefingFolder_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToData();
		}

		private void SelectionChangedTypedEvent(object sender, GridManagerBriefingPages.EventArgsBopBriefingPages e)
		{
			ScreenToDataDetail();
			DataToScreenDetail();
		}
		#endregion
	}


}
