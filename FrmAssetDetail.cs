using DcsBriefop.Data;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmAssetDetail : Form
	{
		#region Fields
		private Asset m_asset;
		#endregion

		#region CTOR
		internal FrmAssetDetail(Asset asset)
		{
			InitializeComponent();
			m_asset = asset;

			MasterDataRepository.FillCombo(MasterDataType.AssetUsage, CbUsage);
			MasterDataRepository.FillCombo(MasterDataType.AssetMapDisplay, CbMapDisplay);

			CkPlayable.Visible = CkLateActivation.Visible = (m_asset is AssetGroup);

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

			if (m_asset is AssetGroup group)
			{
				CkPlayable.Checked = group.Playable;
				CkLateActivation.Checked = group.LateActivation;
				TbRadio.Text = group.GetRadioString(); // TODO radio selection for flights
			}
			else if (m_asset is AssetAirdrome airdrome)
			{
				TbRadio.Text = airdrome.GetRadioString();
			}

			CbUsage.SelectedValue = (int)m_asset.Usage;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;

			TbInformation.Text = m_asset.Information;
		}

		private void ScreenToData()
		{
			ElementAssetUsage usage = (ElementAssetUsage)CbUsage.SelectedValue;
			if (usage != m_asset.Usage)
			{
				m_asset.Usage = usage;
				(m_asset as AssetFlight)?.SetMissionData();
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
		private void CbUsage_SelectionChangeCommitted(object sender, System.EventArgs e)
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
