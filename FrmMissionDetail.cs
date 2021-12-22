using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmMissionDetail : Form
	{
		private static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Selected = "Selected";
			public static readonly string Number = "Number";
			public static readonly string Asset = "Asset";
			public static readonly string Name = "Name";
			public static readonly string Action = "Action";
			public static readonly string Altitude = "Altitude";
			public static readonly string Type = "Type";
			public static readonly string Localisation = "Localisation";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}

		#region Fields
		private AssetFlight m_asset;
		private UcMap m_ucMap;
		#endregion

		#region CTOR
		internal FrmMissionDetail(AssetFlight asset)
		{
			InitializeComponent();
			m_asset = asset;

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMissionMap.Controls.Clear();
			PnMissionMap.Controls.Add(m_ucMap);

			ToolsMisc.SetDataGridViewProperties(DgvRoutePoints);
			ToolsMisc.SetDataGridViewProperties(DgvTargets);
			DgvRoutePoints.ReadOnly = true;

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
			TbAssetInformation.Text = m_asset.Information;
			TbInformation.Text = m_asset.MissionData.MissionInformation;

			DataToScreenRoutePoints();
			DataToScreenTargets();

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

		private void DataToScreenTargets()
		{
			DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn() { Name = GridColumn.Selected, HeaderText = "Selected" };
			DgvTargets.Columns.Add(col);
			DgvTargets.Columns.Add(GridColumn.Asset, "Asset");
			DgvTargets.Columns.Add(GridColumn.Id, "Id");
			DgvTargets.Columns.Add(GridColumn.Type, "Type");
			DgvTargets.Columns.Add(GridColumn.Localisation, "Localisation");
			DgvTargets.Columns.Add(GridColumn.Information, "Information");
			DgvTargets.Columns.Add(GridColumn.Data, "Data");

			DgvTargets.Columns[GridColumn.Selected].ValueType = typeof(bool);
			DgvTargets.Columns[GridColumn.Asset].ReadOnly = true;
			DgvTargets.Columns[GridColumn.Id].ReadOnly = true;
			DgvTargets.Columns[GridColumn.Type].ReadOnly = true;
			DgvTargets.Columns[GridColumn.Localisation].ReadOnly = true;
			DgvTargets.Columns[GridColumn.Information].ReadOnly = true;
			DgvTargets.Columns[GridColumn.Data].Visible = false;

			foreach (AssetGroup target in m_asset.Coalition.OpposingAssets.OfType<AssetGroup>())
			{
				foreach (AssetUnit unit in target.Units)
				{
					RefreshGridRowTarget(target, unit);
				}
			}
		}

		private void RefreshGridRowTarget(AssetGroup target, AssetUnit unit)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvTargets.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == unit)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvTargets.Rows.Add();
				dgvr = DgvTargets.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = unit;
			}

			dgvr.Cells[GridColumn.Selected].Value = m_asset.MissionData.IsTarget(unit.Id);
			dgvr.Cells[GridColumn.Asset].Value = target.Name;
			dgvr.Cells[GridColumn.Id].Value = unit.Id;
			dgvr.Cells[GridColumn.Type].Value = unit.Type;
			dgvr.Cells[GridColumn.Localisation].Value = unit.GetLocalisation();
			dgvr.Cells[GridColumn.Information].Value = unit.Information;
		}

		private void ScreenToData()
		{
			m_asset.MissionData.MissionInformation = TbInformation.Text;

			UpdateMapControl();
		}

		private void UpdateMapControl()
		{
			m_ucMap.SetMapData(m_asset.MissionData.MapData, "Mission map", false);
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
		#endregion

		private void TbInformation_TextChanged(object sender, System.EventArgs e)
		{
			ScreenToData();
		}

		private void DgvTargets_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView grid = (sender as DataGridView);
			if (grid is null)
				return;

			DataGridViewColumn column = grid.Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Selected)
			{
				DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
				AssetUnit unit = grid.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;
				m_asset.MissionData.SetTarget(unit.Id, (bool)cell.Value);
				m_asset.MissionData.InitializeMapData();
				UpdateMapControl();
			}

		}

		private void DgvTargets_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Selected)
			{
				(sender as DataGridView).EndEdit();
			}
		}
	}
}
