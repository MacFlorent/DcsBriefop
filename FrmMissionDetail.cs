using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System.Drawing;
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
			GridAssetManager gam = new GridAssetManager(DgvThreats, m_asset.Coalition.OpposingAssets.Where(_a => _a is AssetGroup).ToList(), m_asset.MissionData);
			gam.ColumnsDisplayed = GridAssetManager.ColumnsDisplayedUnit;
			gam.DisplayFilters = GridAssetManager.DisplayFilterAllClassesAndExcluded | GridAssetManager.DisplayFilter.Units;
			gam.Initialize();
		}

		//private void RefreshGridRowThreat(AssetGroup group, AssetUnit unit)
		//{
		//	DataGridViewRow dgvr = null;
		//	foreach (DataGridViewRow existingRow in DgvThreats.Rows)
		//	{
		//		if (existingRow.Cells[GridColumn.Data].Value == unit)
		//		{
		//			dgvr = existingRow;
		//			break;
		//		}
		//	}
		//	if (dgvr is null)
		//	{
		//		int iNewRowIndex = DgvThreats.Rows.Add();
		//		dgvr = DgvThreats.Rows[iNewRowIndex];
		//		dgvr.Cells[GridColumn.Data].Value = unit;
		//	}

		//	dgvr.Cells[GridColumn.Included].Value = m_asset.MissionData.IsThreatIncluded(unit.Id);
		//	dgvr.Cells[GridColumn.Id].Value = unit.Id;
		//	dgvr.Cells[GridColumn.Asset].Value = group.Description;			
		//	dgvr.Cells[GridColumn.Unit].Value = unit.Description;
		//	dgvr.Cells[GridColumn.Localisation].Value = unit.GetLocalisation();
		//	dgvr.Cells[GridColumn.Information].Value = unit.Information;
		//}

		private void ScreenToData()
		{
			m_asset.MissionData.MissionInformation = TbInformation.Text;
			UpdateMapControl();
		}

		private void UpdateMapControl()
		{
			m_ucMap.SetMapData(m_asset.MissionData.MapData, m_asset.Core.Theatre.Name, "Mission map", false);
		}
		#endregion

		#region Events
		private void DgvTargets_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			//if (e.RowIndex < 0)
			//	return;

			//DataGridView grid = (sender as DataGridView);
			//if (grid is null)
			//	return;

			//DataGridViewColumn column = grid.Columns[e.ColumnIndex];
			//if (column.Name == GridColumn.Included)
			//{
			//	DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
			//	AssetUnit unit = grid.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;
			//	m_asset.MissionData.IncludeThreat(unit.Id, (bool)cell.Value);
			//	m_asset.MissionData.InitializeMapData();
			//	UpdateMapControl();
			//}
		}

		private void DgvTargets_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			//if (e.RowIndex < 0)
			//	return;

			//DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			//if (column.Name == GridColumn.Included)
			//{
			//	(sender as DataGridView).EndEdit();
			//}
		}

		private void FrmMissionDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
