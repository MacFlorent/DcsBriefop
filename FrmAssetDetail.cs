using DcsBriefop.Briefing;
using DcsBriefop.Data;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmAssetDetail : Form
	{
		#region Fields
		private AssetGroup m_asset;
		#endregion

		#region CTOR
		internal FrmAssetDetail(AssetGroup asset)
		{
			InitializeComponent();
			m_asset = asset;

			MasterDataRepository.FillCombo(MasterDataType.AssetCategory, CbCategory);
			MasterDataRepository.FillCombo(MasterDataType.AssetMapDisplay, CbMapDisplay);
			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			Text = $"Asset detail : {m_asset.Name}";
			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;
			CkPlayable.Checked = m_asset.Playable;
			CkLateActivation.Checked = m_asset.LateActivation;
			TbRadio.Text = m_asset.Radio;

			CbCategory.SelectedValue = (int)m_asset.Category;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;

			TbInformation.Text = m_asset.Information;
		}

		private void ScreenToData()
		{
			ElementAssetCategory category = (ElementAssetCategory)CbCategory.SelectedValue;
			if (category != m_asset.Category)
			{
				m_asset.Category = category;
				m_asset.InitializeMapDataMission();
			}

			ElementAssetMapDisplay mapDisplay = (ElementAssetMapDisplay)CbMapDisplay.SelectedValue;
			if (mapDisplay != m_asset.MapDisplay)
			{
				m_asset.MapDisplay = mapDisplay;
				m_asset.InitializeMapOverlay();
			}

			m_asset.Information = TbInformation.Text;
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

		private void BtInformationReset_Click(object sender, System.EventArgs e)
		{
			m_asset.Information = null;
			TbInformation.Text = m_asset.Information;
		}

		private void TbInformation_TextChanged(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
