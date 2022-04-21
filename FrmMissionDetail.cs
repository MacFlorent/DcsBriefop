using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmMissionDetail : Form
	{
		private static class GridColumn
		{
			public static readonly string Number = "Number";
			public static readonly string Name = "Name";
			public static readonly string Action = "Action";
			public static readonly string Altitude = "Altitude";
			public static readonly string Data = "Data";
		}

		#region Fields
		private AssetFlight m_asset;
		private UcMap m_ucMap;
		private GridAssetManager m_gamThreats;
		#endregion

		#region CTOR
		internal FrmMissionDetail(AssetFlight asset)
		{
			InitializeComponent();

			m_asset = asset;

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			SplitContainer.Panel2.Controls.Clear();
			SplitContainer.Panel2.Controls.Add(m_ucMap);

			ToolsMisc.SetDataGridViewProperties(DgvRoutePoints);
			ToolsMisc.SetDataGridViewProperties(DgvThreats);
			DgvRoutePoints.ReadOnly = true;

			DataToScreen();
			ToolsStyle.ApplyStyle(this);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			Text = $"Mission detail : {m_asset.Name}";
			TbId.Text = m_asset.Id.ToString();
			TbDescription.Text = m_asset.Description;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;
			TbAssetInformation.Text = m_asset.Information;
			TbInformation.Text = m_asset.MissionData.MissionInformation;

			DataToScreenRoutePoints();
			DataToScreenThreats();

			UpdateMapControl();
		}

		private void DataToScreenRoutePoints()
		{
			DgvRoutePoints.Columns.Add(GridColumn.Number, "#");
			DgvRoutePoints.Columns.Add(GridColumn.Name, "Name");
			DgvRoutePoints.Columns.Add(GridColumn.Action, "Action");
			DgvRoutePoints.Columns.Add(GridColumn.Altitude, "Altitude");
			DgvRoutePoints.Columns.Add(GridColumn.Data, "Data");

			DgvRoutePoints.Columns[GridColumn.Data].Visible = false;

			foreach (AssetRoutePoint routePoint in m_asset.MapPoints.OfType<AssetRoutePoint>())
			{
				RefreshGridRowRoutePoint(routePoint);
			}
		}

		private void RefreshGridRowRoutePoint(AssetRoutePoint missionPoint)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvRoutePoints.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == missionPoint)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvRoutePoints.Rows.Add();
				dgvr = DgvRoutePoints.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = missionPoint;
			}

			dgvr.Cells[GridColumn.Number].Value = missionPoint.Number;
			dgvr.Cells[GridColumn.Name].Value = missionPoint.Name;
			dgvr.Cells[GridColumn.Action].Value = missionPoint.Action;
			dgvr.Cells[GridColumn.Altitude].Value = missionPoint.AltitudeFeet;
		}

		private void DataToScreenThreats()
		{
			m_gamThreats = new GridAssetManager(DgvThreats, m_asset.Coalition.OpposingAssets.Where(_a => _a is AssetGroup).ToList(), m_asset.MissionData);
			m_gamThreats.ColumnsDisplayed = GridAssetManager.ColumnsDisplayedUnit;
			m_gamThreats.DisplayFilters = GetThreatDisplayFilter();
			m_gamThreats.UnitModified += (o, e) => { UpdateMapControl(); };
			m_gamThreats.Initialize();
		}

		private void ScreenToData()
		{
			m_asset.MissionData.MissionInformation = TbInformation.Text;
		}

		private void UpdateMapControl()
		{
			m_ucMap.SetMapData(m_asset.MissionData.MapData, m_asset.Core.Theatre.Name, "Mission map", false);
		}

		private GridAssetManager.DisplayFilter GetThreatDisplayFilter()
		{
			GridAssetManager.DisplayFilter filter = GridAssetManager.DisplayFilter.Units;

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
		private void CkThreatFilter_CheckedChanged(object sender, EventArgs e)
		{
			m_gamThreats.DisplayFilters = GetThreatDisplayFilter();
		}

		private void FrmMissionDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
