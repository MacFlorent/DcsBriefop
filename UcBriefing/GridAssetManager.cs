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
	internal class GridAssetManager
	{
		public static class GridColumn
		{
			public static readonly string Included = "Included";
			public static readonly string Mission = "Mission";
			public static readonly string MapDisplay = "MapDisplay";
			public static readonly string Id = "Id";
			public static readonly string Description = "Description";
			public static readonly string Localisation = "Localisation";
			public static readonly string Side = "Side";
			public static readonly string Class = "Class";
			public static readonly string Function = "Function";
			public static readonly string Asset = "Asset";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}

		public static List<string> ColumnsDisplayedOwn = new List<string>() { GridColumn.Included, GridColumn.Mission, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Function, GridColumn.Description, GridColumn.Task, GridColumn.Type, GridColumn.Radio, GridColumn.Notes };
		public static List<string> ColumnsDisplayedOpposing = new List<string>() { GridColumn.Included, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Function, GridColumn.Description, GridColumn.Task, GridColumn.Type, GridColumn.Notes };
		public static List<string> ColumnsDisplayedAirdrome = new List<string>() { GridColumn.Included, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Description, GridColumn.Radio, GridColumn.Notes };
		public static List<string> ColumnsDisplayedUnit = new List<string>() { GridColumn.Included, GridColumn.Id, GridColumn.Asset, GridColumn.Description, GridColumn.Localisation, GridColumn.Notes };

		[Flags]
		public enum DisplayFilter
		{
			Assets = 1,
			Units = 2,
			Flights = 4,
			Vehicles = 8,
			Ships = 16,
			Statics = 32,
			Airdromes = 64,
			Excluded = 128,
		}
		public static DisplayFilter DisplayFilterAllClasses = (DisplayFilter.Flights | DisplayFilter.Vehicles | DisplayFilter.Ships | DisplayFilter.Statics | DisplayFilter.Airdromes);
		public static DisplayFilter DisplayFilterAllClassesAndExcluded = (DisplayFilterAllClasses | DisplayFilter.Excluded);

		#region Fields
		private DataGridView m_dgv;
		private List<Asset> m_assets;
		private AssetFlightMission m_missionData;
		#endregion

		#region Properties
		private DisplayFilter m_displayFilters = (DisplayFilter.Assets & DisplayFilter.Units & DisplayFilterAllClassesAndExcluded);
		public DisplayFilter DisplayFilters
		{
			get => m_displayFilters;
			set { m_displayFilters = value; Fill(); }
		}

		public List<string> ColumnsDisplayed { get; set; } = null;

		#endregion

		#region CTOR
		public GridAssetManager(DataGridView dgvAsset, List<Asset> assets, AssetFlightMission missionData)
		{
			m_dgv = dgvAsset;
			m_assets = assets;
			m_missionData = missionData;

			m_dgv.CellEndEdit += CellEndEdit;
			m_dgv.CellMouseUp += CellMouseUp;
			m_dgv.CellDoubleClick += CellDoubleClick;
			m_dgv.CellFormatting += CellFormatting;
		}
		#endregion

		#region Methods
		public void Initialize()
		{
			InitializeColumns();
			RefreshColumnsDisplayed();
			InitializeContextMenu();
			Fill();
		}

		public void RefreshRows()
		{
			foreach (DataGridViewRow dgvr in m_dgv.Rows)
				RefreshRow(dgvr);
		}

		private void InitializeColumns()
		{
			m_dgv.Columns.Clear();

			DataGridViewCheckBoxColumn colIncluded = new DataGridViewCheckBoxColumn() { Name = GridColumn.Included, HeaderText = "Included" };
			DataGridViewCheckBoxColumn colMission = new DataGridViewCheckBoxColumn() { Name = GridColumn.Mission, HeaderText = "Mission" };
			colIncluded.ValueType = colMission.ValueType = typeof(bool);

			m_dgv.Columns.Add(colIncluded);
			m_dgv.Columns.Add(colMission);
			m_dgv.Columns.Add(GridColumn.MapDisplay, "Map");
			m_dgv.Columns.Add(GridColumn.Id, "ID");
			m_dgv.Columns.Add(GridColumn.Side, "Side");
			m_dgv.Columns.Add(GridColumn.Class, "Class");
			m_dgv.Columns.Add(GridColumn.Function, "Function");
			m_dgv.Columns.Add(GridColumn.Asset, "Asset");
			m_dgv.Columns.Add(GridColumn.Description, "Description");
			m_dgv.Columns.Add(GridColumn.Localisation, "Localisation");
			m_dgv.Columns.Add(GridColumn.Type, "Type");
			m_dgv.Columns.Add(GridColumn.Task, "Task");
			m_dgv.Columns.Add(GridColumn.Radio, "Radio");
			m_dgv.Columns.Add(GridColumn.Notes, "Notes");
			m_dgv.Columns.Add(GridColumn.Data, "");

			foreach (DataGridViewColumn column in m_dgv.Columns)
				column.ReadOnly = true;

			m_dgv.Columns[GridColumn.Included].ReadOnly = false;
			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.Localisation].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

			m_dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}

		private void Fill()
		{
			if (m_dgv.Columns.Count <= 0)
				return;

			m_dgv.Rows.Clear();

			foreach (Asset asset in m_assets)
			{
				if (asset is AssetFlight && (DisplayFilters & DisplayFilter.Flights) == 0)
					continue;
				if (asset is AssetVehicle && (DisplayFilters & DisplayFilter.Vehicles) == 0)
					continue;
				if (asset is AssetShip && (DisplayFilters & DisplayFilter.Ships) == 0)
					continue;
				if (asset is AssetStatic && (DisplayFilters & DisplayFilter.Statics) == 0)
					continue;
				if (asset is AssetAirdrome && (DisplayFilters & DisplayFilter.Airdromes) == 0)
					continue;

				bool bFilterExcluded = ((DisplayFilters & DisplayFilter.Excluded) == 0);
				bool bAssetIncluded = AssetOrUnitIncluded(asset);
				bool bWithMission = m_missionData is null && (asset as AssetFlight)?.MissionData is object;

				if ((DisplayFilters & DisplayFilter.Assets) != 0)
				{
					if (!bFilterExcluded || bAssetIncluded || bWithMission)
						RefreshAsset(asset);
				}

				if ((DisplayFilters & DisplayFilter.Units) != 0 && asset is AssetGroup group)
				{
					foreach (AssetUnit unit in group.Units)
					{
						if (!bFilterExcluded || UnitIncluded(unit))
							RefreshUnit(unit);
					}
				}
			}
		}

		private void RefreshColumnsDisplayed()
		{
			foreach (DataGridViewColumn dgvc in m_dgv.Columns)
			{
				if (ColumnsDisplayed is object && ColumnsDisplayed.Contains(dgvc.Name))
					dgvc.Visible = true;
				else
					dgvc.Visible = false;
			}
		}

		private void RefreshRow(DataGridViewRow dgvr)
		{
			Asset asset = dgvr.Cells[GridColumn.Data].Value as Asset;
			AssetUnit unit = dgvr.Cells[GridColumn.Data].Value as AssetUnit;

			if (asset is object)
				RefreshRowAsset(dgvr, asset);
			else if (unit is object)
				RefreshRowUnit(dgvr, unit);
		}

		private void RefreshAsset(Asset asset)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in m_dgv.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == asset)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = m_dgv.Rows.Add();
				dgvr = m_dgv.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = asset;
			}

			RefreshRowAsset(dgvr, asset);
		}

		private void RefreshUnit(AssetUnit unit)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in m_dgv.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == unit)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = m_dgv.Rows.Add();
				dgvr = m_dgv.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = unit;
			}

			RefreshRowUnit(dgvr, unit);
		}

		private void RefreshRowAsset(DataGridViewRow dgvr, Asset asset)
		{
			dgvr.Cells[GridColumn.Mission].ReadOnly = !(asset is AssetFlight);

			SetCellValue(dgvr, GridColumn.Included, AssetIncluded(asset));
			SetCellValue(dgvr, GridColumn.Mission, (asset as AssetFlight)?.MissionData is object);
			SetCellValue(dgvr, GridColumn.MapDisplay, MasterDataRepository.GetById(MasterDataType.AssetMapDisplay, (int)asset.MapDisplay)?.Label);
			SetCellValue(dgvr, GridColumn.Id, asset.Id);
			SetCellValue(dgvr, GridColumn.Side, asset.Side);
			SetCellValue(dgvr, GridColumn.Class, asset.Class);
			SetCellValue(dgvr, GridColumn.Function, asset.Function);
			SetCellValue(dgvr, GridColumn.Asset, asset.Description);
			SetCellValue(dgvr, GridColumn.Description, asset.Description);
			SetCellValue(dgvr, GridColumn.Localisation, asset.GetLocalisation());
			SetCellValue(dgvr, GridColumn.Type, asset.Type);
			SetCellValue(dgvr, GridColumn.Task, asset.Task);
			SetCellValue(dgvr, GridColumn.Radio, asset.GetRadioString());
			SetCellValue(dgvr, GridColumn.Notes, asset.Information);
		}

		private void RefreshRowUnit(DataGridViewRow dgvr, AssetUnit unit)
		{
			dgvr.Cells[GridColumn.Mission].ReadOnly = true;

			SetCellValue(dgvr, GridColumn.Included, UnitIncluded(unit));
			SetCellValue(dgvr, GridColumn.Mission, false);
			SetCellValue(dgvr, GridColumn.MapDisplay, ElementAssetMapDisplay.None);
			SetCellValue(dgvr, GridColumn.Id, unit.Id);
			SetCellValue(dgvr, GridColumn.Side, unit.AssetGroup.Side);
			SetCellValue(dgvr, GridColumn.Class, $"{unit.AssetGroup.Class}/unit");
			SetCellValue(dgvr, GridColumn.Function, unit.AssetGroup.Function);
			SetCellValue(dgvr, GridColumn.Asset, unit.AssetGroup.Description);
			SetCellValue(dgvr, GridColumn.Description, unit.Description);
			SetCellValue(dgvr, GridColumn.Localisation, unit.GetLocalisation());
			SetCellValue(dgvr, GridColumn.Type, unit.Type);
			SetCellValue(dgvr, GridColumn.Task, unit.AssetGroup.Task);
			SetCellValue(dgvr, GridColumn.Radio, unit.AssetGroup.GetRadioString());
			SetCellValue(dgvr, GridColumn.Notes, unit.Information);
		}


		private void SetCellValue(DataGridViewRow dgvr, string sColumn, object value)
		{
			if (dgvr.DataGridView.Columns.Contains(sColumn))
				dgvr.Cells[sColumn].Value = value;
		}

		private bool AssetIncluded(Asset asset)
		{
			if (m_missionData is object)
			{
				return false; // for now no full asset inclusion is possible in mission data
			}
			else
			{
				return asset.Included;
			}
		}

		private bool UnitIncluded(AssetUnit unit)
		{
			if (m_missionData is object)
			{
				return m_missionData.IsThreatIncluded(unit.Id);
			}
			else
			{
				return unit.Included;
			}
		}

		private bool AssetOrUnitIncluded(Asset asset)
		{
			if (m_missionData is object)
			{
				return false; // for now no full asset inclusion is possible in mission data
			}
			else
			{
				return ToolsBriefop.AssetOrUnitIncluded(asset);
			}
		}

		private void SetIncludedAsset(Asset asset, bool bIncluded)
		{
			if (m_missionData is object)
			{
				// for now no full asset inclusion is possible in mission data
			}
			else
			{
				if (asset.Included != bIncluded)
				{
					asset.Included = bIncluded;
					RefreshAsset(asset);
				}
			}
		}

		private void SetIncludedUnit(AssetUnit unit, bool bIncluded)
		{
			if (m_missionData is object)
			{
				if (m_missionData.IsThreatIncluded(unit.Id) != bIncluded)
				{
					m_missionData.IncludeThreat(unit.Id, bIncluded);
					RefreshUnit(unit);
				}
			}
			else
			{
				if (unit.Included != bIncluded)
				{
					unit.Included = bIncluded;
					RefreshUnit(unit);
				}
			}
		}

		private void SetIncluded(List<Asset> assets, List<AssetUnit> units, bool bIncluded)
		{
			foreach (Asset asset in assets)
			{
				SetIncludedAsset(asset, bIncluded);
			}
			foreach (AssetUnit unit in units)
			{
				SetIncludedUnit(unit, bIncluded);
			}
		}

		private void SetMapDisplay(List<Asset> assets, ElementAssetMapDisplay mapDisplay)
		{
			foreach (Asset asset in assets)
			{
				if (asset.MapDisplay != mapDisplay)
				{
					asset.MapDisplay = mapDisplay;
					asset.InitializeMapOverlay();
					RefreshAsset(asset);
				}
			}
		}

		private void ShowAssetDetail(Asset asset)
		{
			FrmAssetDetail f = new FrmAssetDetail(asset);
			f.ShowDialog();
			AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
			RefreshAsset(asset);
		}

		private void ShowMission(AssetFlight asset)
		{
			FrmMissionDetail f = new FrmMissionDetail(asset);
			f.ShowDialog();
		}
		#endregion

		#region Menus
		private void InitializeContextMenu()
		{
			m_dgv.ContextMenuStrip = new ContextMenuStrip();
			m_dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, m_dgv, e); };
		}

		private void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<Asset> selectedAssets = new List<Asset>();
			List<AssetUnit> selectedUnits = new List<AssetUnit>();
			foreach (DataGridViewRow row in dgv.SelectedRows)
			{
				if (row.Cells[GridColumn.Data].Value is Asset asset)
				{
					selectedAssets.Add(asset);
				}
				else if (row.Cells[GridColumn.Data].Value is AssetUnit unit)
				{
					selectedUnits.Add(unit);
				}
			}

			Asset singleSelectedAsset = selectedAssets.Count == 1 ? selectedAssets[0] : null;
			AssetUnit singleSelectedUnit = selectedUnits.Count == 1 ? selectedUnits[0] : null;

			menu.Items.Clear();

			if (m_missionData is null && singleSelectedAsset is object)
				menu.Items.AddMenuItem("Details", (object _sender, EventArgs _e) => { ShowAssetDetail(singleSelectedAsset); });
			if (m_missionData is null && singleSelectedAsset is AssetFlight assetFlight && assetFlight.MissionData is object)
				menu.Items.AddMenuItem("Mission", (object _sender, EventArgs _e) => { ShowMission(assetFlight); });

			menu.Items.AddMenuSeparator();

			if (m_dgv.Columns[GridColumn.Included].Visible && (selectedAssets.Count > 0 || selectedUnits.Count > 0))
			{
				ToolStripMenuItem tmsiUsage = menu.Items.AddMenuItem("Inclusion", null);
				tmsiUsage.DropDownItems.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, selectedUnits, false); });
				tmsiUsage.DropDownItems.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, selectedUnits, true); });
			}

			if (m_dgv.Columns[GridColumn.MapDisplay].Visible && selectedAssets.Count > 0)
			{
				ToolStripMenuItem tmsiMapDisplay = menu.Items.AddMenuItem("Map display", null);
				tmsiMapDisplay.DropDownItems.AddMenuItem("None", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.None); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Point", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.Point); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Orbit", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.Orbit); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Full route", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.FullRoute); });
			}

			if (menu.Items.Count <= 0)
				e.Cancel = true;

		}
		#endregion

		#region Events
		public class EventArgsAsset : EventArgs
		{
			public Asset Asset { get; set; }
		}
		public class EventArgsUnit : EventArgs
		{
			public AssetUnit Unit { get; set; }
		}

		public event EventHandler<EventArgsAsset> AssetModified;
		public event EventHandler<EventArgsUnit> UnitModified;

		private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			Asset asset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as Asset;
			AssetUnit unit = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

			if (column.Name == GridColumn.Included)
			{
				if (asset is object)
				{
					SetIncludedAsset(asset, (bool)cell.Value);
					AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
				}
				else if (unit is object)
				{
					SetIncludedUnit(unit, (bool)cell.Value);
					UnitModified?.Invoke(this, new EventArgsUnit() { Unit = unit });
				}
			}
			else if (column.Name == GridColumn.Mission)
			{
				if (asset is AssetFlight flight)
				{
					if ((bool)cell.Value && flight.MissionData is null)
					{
						flight.AddMissionData();
						AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
					}
					else if (!(bool)cell.Value && flight.MissionData is object)
					{
						flight.RemoveMissionData();
						AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
					}
				}
			}
		}

		private void CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Included || column.Name == GridColumn.Mission)
			{
				(sender as DataGridView).EndEdit();
			}
		}

		private void CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			if (column.ReadOnly)
			{
				Asset asset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as Asset;
				if (asset is object)
					ShowAssetDetail(asset);
			}
		}

		private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			Asset asset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as Asset;
			AssetUnit unit = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

			DataGridViewCellStyle cellStyle = dgvc.InheritedStyle;
			if (column.Name == GridColumn.Included)
			{
				if ((asset is object && AssetOrUnitIncluded(asset)) || (unit is object && UnitIncluded(unit)))
				{
					cellStyle.BackColor = Color.LightGreen;
					cellStyle.SelectionBackColor = ToolsImage.Lerp(cellStyle.BackColor, dgv.DefaultCellStyle.SelectionBackColor, 0.2f);
				}
			}
			else if (column.Name == GridColumn.Mission)
			{
				if (asset is AssetFlight flight && flight.MissionData is object)
				{
					cellStyle.BackColor = Color.LightGreen;
					cellStyle.SelectionBackColor = ToolsImage.Lerp(cellStyle.BackColor, dgv.DefaultCellStyle.SelectionBackColor, 0.2f);
				}
			}

			e.CellStyle = cellStyle;
		}
		#endregion
	}
}
