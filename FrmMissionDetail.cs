using DcsBriefop.Briefing;
using DcsBriefop.Data;
using DcsBriefop.UcBriefing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmMissionDetail : Form
	{
		private static class GridColumnRoutePoint
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
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Number, "#");
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Name, "Name");
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Action, "Action");
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Altitude, "Altitude");
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Notes, "Notes");
			DgvRoutePoints.Columns.Add(GridColumnRoutePoint.Data, "#");

			DgvRoutePoints.Columns[GridColumnRoutePoint.Data].Visible = false;

			foreach (AssetRoutePoint routePoint in m_asset.MapPoints.OfType<AssetRoutePoint>())
			{
				RefreshGridRowRoutePoint(routePoint);
			}
		}

		private void RefreshGridRowRoutePoint(AssetRoutePoint routePoint)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvRoutePoints.Rows)
			{
				if (existingRow.Cells[GridColumnRoutePoint.Data].Value == routePoint)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvRoutePoints.Rows.Add();
				dgvr = DgvRoutePoints.Rows[iNewRowIndex];
				dgvr.Cells[GridColumnRoutePoint.Data].Value = routePoint;
			}

			dgvr.Cells[GridColumnRoutePoint.Number].Value = routePoint.Number;
			dgvr.Cells[GridColumnRoutePoint.Number].ReadOnly = true;
			dgvr.Cells[GridColumnRoutePoint.Name].Value = routePoint.Name;
			dgvr.Cells[GridColumnRoutePoint.Name].ReadOnly = true;
			dgvr.Cells[GridColumnRoutePoint.Action].Value = routePoint.Action;
			dgvr.Cells[GridColumnRoutePoint.Action].ReadOnly = true;
			dgvr.Cells[GridColumnRoutePoint.Altitude].Value = routePoint.AltitudeFeet;
			dgvr.Cells[GridColumnRoutePoint.Altitude].ReadOnly = true;
			dgvr.Cells[GridColumnRoutePoint.Notes].Value = routePoint.Notes;
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

		private void DgvRoutePoints_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			AssetRoutePoint routePoint = DgvRoutePoints.Rows[e.RowIndex].Cells[GridColumnRoutePoint.Data].Value as AssetRoutePoint;
			object value = DgvRoutePoints.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			string sColumnName = DgvRoutePoints.Columns[e.ColumnIndex].Name;
			
			if (sColumnName == GridColumnRoutePoint.Notes)
			{
				routePoint.Notes = value as string;
			}
		}
	}
}
