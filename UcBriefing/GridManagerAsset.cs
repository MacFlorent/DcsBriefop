using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class GridManagerAsset : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Included = "Included";
			public static readonly string Mission = "Mission";
			public static readonly string MapDisplay = "MapDisplay";
			public static readonly string Id = "Id";
			public static readonly string Side = "Side";
			public static readonly string Class = "Class";
			public static readonly string Description = "Description";
			public static readonly string Localisation = "Localisation";
			public static readonly string Function = "Function";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}
		public static List<string> ColumnsDisplayedOwn = new List<string>() { GridColumn.Included, GridColumn.Mission, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Function, GridColumn.Description, GridColumn.Task, GridColumn.Type, GridColumn.Radio, GridColumn.Notes };
		public static List<string> ColumnsDisplayedOpposing = new List<string>() { GridColumn.Included, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Function, GridColumn.Description, GridColumn.Task, GridColumn.Type, GridColumn.Notes };
		public static List<string> ColumnsDisplayedAirdrome = new List<string>() { GridColumn.Included, GridColumn.MapDisplay, GridColumn.Id, GridColumn.Description, GridColumn.Radio, GridColumn.Notes };
		#endregion

		#region Fields
		private List<Asset> m_assets;
		private AssetFlightMission m_missionData;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAsset(DataGridView dgv, List<Asset> assets, AssetFlightMission missionData) : base(dgv)
		{
			m_assets = assets;
			m_missionData = missionData;

			m_dgv.CellEndEdit += CellEndEdit;
			m_dgv.CellMouseUp += CellMouseUp;
			m_dgv.CellDoubleClick += CellDoubleClick;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Included, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Mission, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.MapDisplay, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Side, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Class, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Description, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Localisation, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Task, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Radio, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Notes, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(Asset));

			foreach (Asset asset in m_assets)
				RefreshDataSourceRow(asset);
		}

		private void RefreshDataSourceRow(Asset asset)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<Asset>(GridColumn.Data) == asset).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, asset);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Included, asset.Included);
			dr.SetField(GridColumn.Mission, (asset as AssetFlight)?.MissionData is object);
			dr.SetField(GridColumn.MapDisplay, MasterDataRepository.GetById(MasterDataType.AssetMapDisplay, (int)asset.MapDisplay)?.Label);
			dr.SetField(GridColumn.Id, asset.Id);
			dr.SetField(GridColumn.Side, asset.Side);
			dr.SetField(GridColumn.Class, asset.Class);
			dr.SetField(GridColumn.Description, asset.Description);
			dr.SetField(GridColumn.Localisation, asset.GetLocalisation());
			dr.SetField(GridColumn.Type, asset.Type);
			dr.SetField(GridColumn.Task, asset.Task);
			dr.SetField(GridColumn.Radio, asset.GetRadioString());
			dr.SetField(GridColumn.Notes, asset.Information);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Included].ReadOnly = false;
			m_dgv.Columns[GridColumn.Mission].ReadOnly = false;
			m_dgv.Columns[GridColumn.Data].Visible = false;
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

		private void SetIncluded(Asset asset, bool bIncluded)
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
					RefreshDataSourceRow(asset);
					(m_dgv.DataSource as BindingSource).EndEdit();
				}
			}
		}

		private void SetIncluded(List<Asset> assets, bool bIncluded)
		{
			foreach (Asset asset in assets)
			{
				SetIncluded(asset, bIncluded);
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
					RefreshDataSourceRow(asset);
					(m_dgv.DataSource as BindingSource).EndEdit();
				}
			}
		}

		private void ShowAssetDetail(Asset asset)
		{
			FrmAssetDetail f = new FrmAssetDetail(asset);
			f.ShowDialog();
			AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
			RefreshDataSourceRow(asset);
			(m_dgv.DataSource as BindingSource).EndEdit();
		}

		private void ShowMission(AssetFlight asset)
		{
			FrmMissionDetail f = new FrmMissionDetail(asset);
			f.ShowDialog();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			Asset asset = dgvc.DataGridView.Rows[dgvc.RowIndex].Cells[GridColumn.Data].Value as Asset;

			if (column.DataPropertyName == GridColumn.Included)
			{
				if ((asset is object && AssetOrUnitIncluded(asset)))
				{
					cellStyle.BackColor = Color.LightGreen;
				}
			}
			else if (column.DataPropertyName == GridColumn.Mission)
			{
				if (asset is AssetFlight flight && flight.MissionData is object)
				{
					cellStyle.BackColor = Color.LightGreen;
				}
			}

			return cellStyle;
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<Asset> selectedAssets = new List<Asset>();
			foreach (DataGridViewRow row in GetSelectedRows())
			{
				if (row.Cells[GridColumn.Data].Value is Asset asset)
				{
					selectedAssets.Add(asset);
				}
			}

			Asset singleSelectedAsset = selectedAssets.Count == 1 ? selectedAssets[0] : null;

			menu.Items.Clear();

			if (m_missionData is null && singleSelectedAsset is object)
				menu.Items.AddMenuItem("Details", (object _sender, EventArgs _e) => { ShowAssetDetail(singleSelectedAsset); });
			if (m_missionData is null && singleSelectedAsset is AssetFlight assetFlight && assetFlight.MissionData is object)
				menu.Items.AddMenuItem("Mission", (object _sender, EventArgs _e) => { ShowMission(assetFlight); });

			menu.Items.AddMenuSeparator();

			if (m_dgv.Columns[GridColumn.Included].Visible && selectedAssets.Count > 0)
			{
				ToolStripMenuItem tmsiUsage = menu.Items.AddMenuItem("Inclusion", null);
				tmsiUsage.DropDownItems.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, false); });
				tmsiUsage.DropDownItems.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, true); });
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
		public event EventHandler<EventArgsAsset> AssetModified;

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

			if (column.Name == GridColumn.Included)
			{
				if (asset is object)
				{
					SetIncluded(asset, (bool)cell.Value);
					AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
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
		#endregion
	}
}
