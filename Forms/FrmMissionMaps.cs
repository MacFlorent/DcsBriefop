using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionMaps : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private UcMap m_ucMap;
		#endregion

		#region CTOR
		public FrmMissionMaps(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			RbMapSelectionRed.Tag = ElementCoalition.Red;
			RbMapSelectionBlue.Tag = ElementCoalition.Blue;
			RbMapSelectionNeutral.Tag = ElementCoalition.Neutral;

			MapProviders.FillCombo(CbMapProvider, CbMapProvider_SelectedValueChanged);
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			using FrmMissionMaps f = new(briefopManager);
			f.ShowDialog(parentForm);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbMapProvider.SelectedValueChanged -= CbMapProvider_SelectedValueChanged;

			CbMapProvider.SelectedItem = MapProviders.TryGetProvider(m_briefopManager.BopMission.PreferencesMap.ProviderName);

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreenDetail();

			CbMapProvider.SelectedValueChanged += CbMapProvider_SelectedValueChanged;
		}

		private void DataToScreenDetail()
		{
			string sCoalition = PnMapSelection.Controls.OfType<RadioButton>().Where(_rb => _rb.Checked).FirstOrDefault()?.Tag as string ?? "global";

			MizBopMap mapData = null;
			List<GMapOverlay> staticOverlays = new List<GMapOverlay>();
			if (sCoalition is not null && m_briefopManager.BopMission.Coalitions.ContainsKey(sCoalition))
			{
				BopCoalition bopCoalition = m_briefopManager.BopMission.Coalitions[sCoalition];
				mapData = bopCoalition.MapData;
				staticOverlays.Add(bopCoalition.BuildStaticMapOverlay());
			}
			else
			{
				mapData = m_briefopManager.BopMission.MapData;
				staticOverlays.Add(m_briefopManager.BopMission.BuildStaticMapOverlay());
			}

			m_ucMap.MapData = mapData;
			m_ucMap.StaticOverlays = staticOverlays;
			m_ucMap.MapProviderName = m_briefopManager.BopMission.PreferencesMap.ProviderName;
			m_ucMap.DataToScreen();
		}

		private void ScreenToData()
		{
			m_briefopManager.BopMission.PreferencesMap.ProviderName = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
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
			DataToScreenDetail();
		}

		private void CbMapProvider_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DataToScreenDetail();
		}
		#endregion

		#region POC LOTATC drawings
		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = PreferencesManager.Preferences.Application.WorkingDirectory;
				ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string sJson = File.ReadAllText(ofd.FileName);
					ToolsLotatc.DrawingsJsonToMiz(sJson, m_briefopManager);
					DataToScreenDetail();
				}
			}
		}
		#endregion

	}
}
