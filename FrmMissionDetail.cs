using DcsBriefop.Briefing;
using DcsBriefop.Data;
using DcsBriefop.UcBriefing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmMissionDetail : Form
	{
		private static class GridRouteWaypoint
		{
			public static readonly string Number = "Number";
			public static readonly string Name = "Name";
			public static readonly string Action = "Action";
			public static readonly string Altitude = "Altitude";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}

		private static class GridColumnTarget
		{
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Altitude = "Altitude";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}

		#region Fields
		private AssetGroup m_asset;
		private UcMap m_ucMap;
		#endregion

		#region CTOR
		internal FrmMissionDetail(AssetGroup asset)
		{
			InitializeComponent();
			m_asset = asset;
			m_asset.InitializeMapDataMission();

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMissionMap.Controls.Clear();
			PnMissionMap.Controls.Add(m_ucMap);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			Text = $"Mission detail : {m_asset.Name}";
			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;
			TbInformation.Text = m_asset.MissionInformation;

			DataToScreenRoutePoints();

			UpdateMapControl();
		}

		private void DataToScreenRoutePoints()
		{
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Number, "#");
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Name, "Name");
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Action, "Action");
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Altitude, "Altitude");
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Notes, "Notes");
			DgvRoutepoints.Columns.Add(GridRouteWaypoint.Data, "#");

			DgvRoutepoints.Columns[GridRouteWaypoint.Data].Visible = false;

			foreach (AssetRoutePoint routePoint in m_asset.MapPoints.OfType<AssetRoutePoint>())
			{
				RefreshGridRowRoutePoint(routePoint);
			}
		}

		private void RefreshGridRowRoutePoint(AssetRoutePoint routePoint)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvRoutepoints.Rows)
			{
				if (existingRow.Cells[GridRouteWaypoint.Data].Value == routePoint)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvRoutepoints.Rows.Add();
				dgvr = DgvRoutepoints.Rows[iNewRowIndex];
				dgvr.Cells[GridRouteWaypoint.Data].Value = routePoint;
			}

			dgvr.Cells[GridRouteWaypoint.Number].Value = routePoint.Number;
			dgvr.Cells[GridRouteWaypoint.Name].Value = routePoint.Name;
			dgvr.Cells[GridRouteWaypoint.Action].Value = routePoint.Action;
			dgvr.Cells[GridRouteWaypoint.Altitude].Value = routePoint.Altitude;
			dgvr.Cells[GridRouteWaypoint.Notes].Value = routePoint.Notes;
		}

		private void ScreenToData()
		{
			m_asset.MissionInformation = TbInformation.Text;

			UpdateMapControl();
		}

		private void UpdateMapControl()
		{
			m_ucMap.SetMapData(m_asset.MapDataMission, "Mission map", false);
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

		private void TbInformation_TextChanged(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
	}
}
