using DcsBriefop.Briefing;
using DcsBriefop.MasterData;
using DcsBriefop.UcBriefing;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmAssetDetail : Form
	{
		#region Fields
		private Asset m_asset;
		private UcMap m_ucMap;
		#endregion

		#region CTOR
		internal FrmAssetDetail(Asset asset)
		{
			InitializeComponent();
			m_asset = asset;

			MasterDataRepository.FillCombo(MasterDataType.AssetCategory, CbCategory);
			MasterDataRepository.FillCombo(MasterDataType.AssetMapDisplay, CbMapDisplay);

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
			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbType.Text = m_asset.GetUnitTypes();
			CkPlayable.Checked = m_asset.Playable;
			CkLateActivation.Checked = m_asset.LateActivation;

			if (m_asset is AssetFlight flight)
			{
				TbTask.Text = flight.Task;
				TbBase.Text = flight.GetAirdromeNames();
			}

			CbCategory.SelectedValue = (int)m_asset.Category;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;

			m_ucMap.SetMapData(m_asset.MapData);
		}

		private void ScreenToData()
		{
			m_asset.Category = (ElementAssetCategory)CbCategory.SelectedValue;

			int iMapDisplay = (int)CbMapDisplay.SelectedValue;
			if (iMapDisplay != (int)m_asset.MapDisplay)
			{
				m_asset.MapDisplay = (ElementAssetMapDisplay)iMapDisplay;
				m_asset.InitializeMapData();
			}
		}
		#endregion

		#region Events
		private void CbCategory_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			ScreenToData();
		}

		private void CbMapDisplay_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
