using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		#region Fields
		private List<GridAssetManager> m_gridAssetManagers = new List<GridAssetManager>();
		#endregion

		#region Properties
		public BriefingCoalition Coalition { get; private set; }
		#endregion

		#region CTOR
		public UcBriefingCoalition(UcMap ucMap, BriefingContainer briefingContainer, BriefingCoalition briefingCoalition) : base(ucMap, briefingContainer)
		{
			InitializeComponent();

			Coalition = briefingCoalition;
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = Coalition.GetBullseyeCoordinatesString();
			TbBullseyeDescription.Text = Coalition.BullseyeDescription;
			CkBullseyeWaypoint.Checked = Coalition.BullseyeWaypoint;
			TbTask.Text = Coalition.Task.Replace("\\\n", Environment.NewLine);

			DataToScreenTabs();
			SetComPresetButton();
		}

		private void DataToScreenTabs()
		{
			int iSelectedIndex = -1;
			if (TcAssets.TabPages.Count > 0)
			{
				iSelectedIndex = TcAssets.SelectedIndex;
				TcAssets.TabPages.Clear();
			}

			m_gridAssetManagers.Clear();
			DataToScreenAddGridTab("Own assets", Coalition.OwnAssets, GridAssetManager.ColumnsDisplayedOwn);
			DataToScreenAddGridTab("Opposing assets", Coalition.OpposingAssets, GridAssetManager.ColumnsDisplayedOpposing);
			DataToScreenAddGridTab("Airdromes", Coalition.Airdromes.OfType<Asset>().ToList(), GridAssetManager.ColumnsDisplayedAirdrome);

			if (iSelectedIndex > 0)
				TcAssets.SelectedIndex = iSelectedIndex;
		}

		private void DataToScreenAddGridTab(string sTabText, List<Asset> assets, List<string> columnsDisplayed)
		{
			TabPage tp = new TabPage(sTabText);
			TcAssets.TabPages.Add(tp);
			DataGridView dgv = new DataGridView();
			SetGridProperties(dgv);
			tp.Controls.Add(dgv);

			GridAssetManager gam = new GridAssetManager(dgv, assets, null);
			gam.ColumnsDisplayed = columnsDisplayed;
			gam.DisplayFilters = GetDisplayFilter();
			gam.Initialize();

			m_gridAssetManagers.Add(gam);
		}

		public override void ScreenToData()
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.Task = TbTask.Text.Replace(Environment.NewLine, "\\\n"); ;
			Coalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
		}

		private void SetComPresetButton()
		{
			if (Coalition.ComPresets is object && Coalition.ComPresets.Count > 0)
			{
				BtComPresets.BackColor = Color.LightGreen;
			}
			else
			{
				BtComPresets.BackColor = Color.LightGray;
			}
		}

		private void SetGridProperties(DataGridView dgv)
		{
			ToolsMisc.SetDataGridViewProperties(dgv);
			dgv.ReadOnly = false;
			dgv.Dock = DockStyle.Fill;
		}

		private GridAssetManager.DisplayFilter GetDisplayFilter()
		{
			GridAssetManager.DisplayFilter filter = GridAssetManager.DisplayFilter.Assets | GridAssetManager.DisplayFilter.Airdromes;

			if (CkFilterFlights.Checked)
				filter |= GridAssetManager.DisplayFilter.Flights;
			if (CkFilterVehicles.Checked)
				filter |= GridAssetManager.DisplayFilter.Vehicles;
			if (CkFilterShips.Checked)
				filter |= GridAssetManager.DisplayFilter.Ships;
			if (CkFilterStatics.Checked)
				filter |= GridAssetManager.DisplayFilter.Statics;
			if (CkFilterExcluded.Checked)
				filter |= GridAssetManager.DisplayFilter.Excluded;

			return filter;
		}
		#endregion

		#region Events
		private void TbBullseyeDescription_Validated(object sender, System.EventArgs e)
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.ResetBullseyeMarkerDescription();
		}

		private void CkBullseyeWaypoint_CheckedChanged(object sender, EventArgs e)
		{
			Coalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
			Coalition.InitializeBullseyeWaypoints();
		}

		private void CkAssetFilter_CheckedChanged(object sender, EventArgs e)
		{
			foreach(GridAssetManager gam in m_gridAssetManagers)
			{
				gam.DisplayFilters = GetDisplayFilter();
			}
		}

		private void BtComPresets_Click(object sender, EventArgs e)
		{

			FrmComs f = new FrmComs(Coalition);
			if (f.ShowDialog() == DialogResult.OK)
			{
				DataToScreen();
			}
		}
		#endregion
	}
}
