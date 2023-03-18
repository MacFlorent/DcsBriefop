using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionMaps : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private UcMap m_ucMap;
		#endregion

		#region CTOR
		public FrmMissionMaps(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
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
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmMissionMaps f = new FrmMissionMaps(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbMapProvider.SelectedValueChanged -= CbMapProvider_SelectedValueChanged;

			CbMapProvider.SelectedItem = GMapProviders.TryGetProvider(m_briefopManager.BopMission.PreferencesMap.ProviderName);

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
			if (sCoalition is object && m_briefopManager.BopMission.Coalitions.ContainsKey(sCoalition))
			{
				BopCoalition bopCoalition = m_briefopManager.BopMission.Coalitions[sCoalition];
				mapData = bopCoalition.MapData;
				staticOverlays.Add(bopCoalition.MapOverlay);
			}
			else
			{
				mapData = m_briefopManager.BopMission.MapData;
				staticOverlays.Add(m_briefopManager.BopMission.MapOverlay);
			}

			m_ucMap.MapData = mapData;
			m_ucMap.StaticOverlays = staticOverlays;
			m_ucMap.MapProviderName = m_briefopManager.BopMission.PreferencesMap.ProviderName;
			m_ucMap.RefreshMapData();
		}

		private void ScreenToData()
		{
			m_briefopManager.BopMission.PreferencesMap.ProviderName = (CbMapProvider.SelectedItem as GMapProvider)?.Name; ;
		}
		#endregion

		#region Events
		private void FrmMissionMaps_Shown(object sender, EventArgs e)
		{
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
	}
}
