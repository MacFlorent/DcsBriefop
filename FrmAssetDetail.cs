using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Text;
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
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.TextBoxSmall(TbLegend);

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
			Text = $"Asset detail : {m_asset.Description}";
			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;
			TbDescription.Text = m_asset.Description;

			AssetGroup group = m_asset as AssetGroup;

			if (group is object)
			{
				CkPlayable.Checked = group.Playable;
				CkLateActivation.Checked = group.LateActivation;
				TbRadio.Text = group.GetRadioString();
			}
			else if (m_asset is AssetAirdrome airdrome)
			{
				TbRadio.Text = airdrome.GetRadioString();
			}

			CbUsage.SelectedValue = (int)m_asset.Usage;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;

			TbInformation.Text = m_asset.Information;

			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Information is an additionnal text useful for describing the asset and its role. If customized it can be reset to its automatic value with {BtInformationReset.Text}");
			if (group is object && group.Playable)
				sb.AppendLine($"For playable units, displayed radio is the one that will be initially selected (which is also preset 1 or radio 1)");

			TbLegend.Text = sb.ToString();
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
