using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui.Layers;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionMaps : Form
	{
		#region Fields
		private readonly BriefopManager m_bopManager;
		private UcMap m_ucMap;
		#endregion

		#region CTOR
		public FrmMissionMaps(BriefopManager briefopManager)
		{
			m_bopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			RbMapSelectionRed.Tag = ElementCoalition.Red;
			RbMapSelectionBlue.Tag = ElementCoalition.Blue;
			RbMapSelectionNeutral.Tag = ElementCoalition.Neutral;

			MapTileSourceManager.FillComboBasemaps(CbMapProvider, CbMapProvider_SelectedValueChanged);
			MapTileSourceManager.FillCheckDropDownOverlays(CddMapOverlays);
		}

		public static void CreateModal(BriefopManager bopManager, Form parentForm)
		{
			using FrmMissionMaps f = new(bopManager);
			f.ShowDialog(parentForm);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbMapProvider.SelectedValueChanged -= CbMapProvider_SelectedValueChanged;

			CbMapProvider.SelectedItem = MapTileSourceManager.TryGetBasemapOrDefault(m_bopManager.BopMission.PreferencesMap.ProviderName);
			CddMapOverlays.CheckedItemTexts = m_bopManager.BopMission.PreferencesMap.OverlayNames;
			DisplayCurrentOverlays();

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreenMap();

			CbMapProvider.SelectedValueChanged += CbMapProvider_SelectedValueChanged;
		}

		private void DataToScreenMap()
		{
			string sCoalition = PnMapSelection.Controls.OfType<RadioButton>().Where(_rb => _rb.Checked).FirstOrDefault()?.Tag as string ?? "global";

			MizBopMap mapData = null;
			List<ILayer> staticLayers = [];
			if (sCoalition is not null && m_bopManager.BopMission.Coalitions.TryGetValue(sCoalition, out BopCoalition bopCoalition))
			{
				mapData = bopCoalition.MapData;
				staticLayers.Add(bopCoalition.BuildStaticMapLayer());
			}
			else
			{
				mapData = m_bopManager.BopMission.MapData;
				staticLayers.Add(m_bopManager.BopMission.BuildStaticMapLayer());
			}

			m_ucMap.MapData = mapData;
			m_ucMap.StaticLayers = staticLayers;
			m_ucMap.MapProviderName = m_bopManager.BopMission.PreferencesMap.ProviderName;
			m_ucMap.MapOverlayNames = m_bopManager.BopMission.PreferencesMap.OverlayNames;
			m_ucMap.DataToScreen();
		}

		private void ScreenToData()
		{
			m_bopManager.BopMission.PreferencesMap.ProviderName = (CbMapProvider.SelectedItem as MapTileSource)?.Name;
			m_bopManager.BopMission.PreferencesMap.OverlayNames = [.. CddMapOverlays.CheckedItemTexts];
		}

		private void DisplayCurrentOverlays()
		{
			LnkMapOpenAipAttribution.Visible = CddMapOverlays.CheckedItems.OfType<MapTileSourceOpenAip>().Any();
		}
		#endregion

		#region Events
		private void FrmMissionMaps_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void FrmMissionMaps_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToData();
		}

		private void RbMapSelection_CheckedChanged(object sender, EventArgs e)
		{
			DataToScreenMap();
		}

		private void CbMapProvider_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DataToScreenMap();
		}

		private void CddMapOverlays_ItemCheckedChanged(object sender, EventArgs e)
		{
			DisplayCurrentOverlays();
			ScreenToData();
			DataToScreenMap();
		}

		private void LnkMapOpenAipAttribution_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MapTileSourceOpenAip.OpenAttributionLink();
		}
		#endregion

		#region POC LOTATC drawings
		private void BtImportDrawingsFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = PreferencesManager.Preferences.Application.WorkingDirectory;
				ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					try
					{
						using (new WaitDialog(this))
						{
							string sJson = File.ReadAllText(ofd.FileName);
							ToolsLotatc.DrawingsFileJsonToMiz(sJson, m_bopManager);
							DataToScreenMap();
						}
					}
					catch (Exception ex)
					{
						ToolsControls.ShowMessageBoxAndLogException("Failed to import lotatc drawings file.", ex);
					}
				}
			}
		}
		#endregion

	}
}
