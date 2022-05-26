using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmAssetDetail : Form
	{
		private static class GridColumn
		{
			public static readonly string Included = "Included";
			public static readonly string Id = "Id";
			public static readonly string Description = "Description";
			public static readonly string Type = "Type";
			public static readonly string Localisation = "Localisation";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}

		#region Fields
		private Asset m_asset;
		#endregion

		#region CTOR
		internal FrmAssetDetail(Asset asset)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_asset = asset;

			MasterDataRepository.FillCombo(MasterDataType.AssetMapDisplay, CbMapDisplay);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			Text = $"Asset detail : {m_asset.Description}";

			TbDescription.Text = m_asset.Description;
			TbSide.Text = m_asset.Side.ToString();

			TbAssetClass.Text = m_asset.Class;
			TbFunction.Text = m_asset.Function.ToString();
			CkIncluded.Checked = m_asset.Included;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;
			TbInformation.Text = m_asset.Information;

			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;

			CkPlayable.Visible = CkLateActivation.Visible = (m_asset is AssetGroup);
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

			DataToScreenUnits();
		}

		private void DataToScreenUnits()
		{
			if (m_asset is AssetGroup group)
			{
				GridManagerUnit gamUnits = new GridManagerUnit(AdgvUnits, group.Units, null);
				gamUnits.ColumnsDisplayed = GridManagerUnit.ColumnsDisplayedUnit;
				gamUnits.Initialize();
			}
		}

		private void ScreenToData()
		{
			m_asset.Included = CkIncluded.Checked;

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
		private void BtInformationReset_Click(object sender, System.EventArgs e)
		{
			m_asset.Information = null;
			TbInformation.Text = m_asset.Information;
		}

		private void FrmAssetDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			ScreenToData();
		}
		#endregion

	}
}
