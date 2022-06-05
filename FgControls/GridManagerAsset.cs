using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.FgControls
{
	internal class GridManagerAsset : GridManager
	{
		#region Columns
		public static class Column
		{
			public static readonly string Included = "Included";
			public static readonly string MapDisplay = "MapDisplay";
			public static readonly string Id = "Id";
			public static readonly string Side = "Side";
			public static readonly string Class = "Class";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string Localisation = "Localisation";
			public static readonly string Function = "Function";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}
		public static List<string> ColumnsDisplayedOwn = new List<string>() { Column.Included, Column.MapDisplay, Column.Id, Column.Function, Column.DisplayName, Column.Task, Column.Class, Column.Type, Column.Information };
		public static List<string> ColumnsDisplayedOpposing = new List<string>() { Column.Included, Column.MapDisplay, Column.Id, Column.DisplayName, Column.Task, Column.Class, Column.Type, Column.Information };
		public static List<string> ColumnsDisplayedAirdrome = new List<string>() { Column.Included, Column.MapDisplay, Column.Id, Column.DisplayName, Column.Radio, Column.Information };
		#endregion

		#region Fields
		private List<Asset> m_assets;
		private FlightMission m_flightMission;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAsset(FgDataGridView dgv, List<string> columnsDisplayed, List<Asset> assets, FlightMission flightMission) : base(dgv, columnsDisplayed)
		{
			m_assets = assets;
			m_flightMission = flightMission;

			m_dgv.CellDoubleClick += CellDoubleClickEvent;
		}

		public static GridManagerAsset CreateManager(FgDataGridView dgv, List<string> columnsDisplayed, List<Asset> assets, FlightMission flightMission)
		{
			//columnsDisplayed = ColumnsDisplayedOwn;
			GridManagerAsset gm = new GridManagerAsset(dgv, columnsDisplayed, assets, flightMission);
			gm.Initialize();
			return gm;
		}

		public static GridManagerAsset CreateManagerOwn(FgDataGridView dgv, List<Asset> assets, FlightMission flightMission)
		{
			return CreateManager(dgv, ColumnsDisplayedOwn, assets, flightMission);
		}
		public static GridManagerAsset CreateManagerOpposing(FgDataGridView dgv, List<Asset> assets, FlightMission flightMission)
		{
			return CreateManager(dgv, ColumnsDisplayedOpposing, assets, flightMission);
		}
		public static GridManagerAsset CreateManagerAirdrome(FgDataGridView dgv, List<Asset> assets, FlightMission flightMission)
		{
			return CreateManager(dgv, ColumnsDisplayedAirdrome, assets, flightMission);
		}
		#endregion

		#region Methods
		protected override void InitializeGridSource()
		{
			m_dtSource = new DataTable();

			m_dtSource.Columns.Add(Column.Included, typeof(bool));
			m_dtSource.Columns.Add(Column.MapDisplay, typeof(ElementAssetMapDisplay));
			m_dtSource.Columns.Add(Column.Function, typeof(string));
			m_dtSource.Columns.Add(Column.Id, typeof(int));
			m_dtSource.Columns.Add(Column.Side, typeof(ElementAssetSide));
			m_dtSource.Columns.Add(Column.Class, typeof(ElementDcsObjectClass));
			m_dtSource.Columns.Add(Column.DisplayName, typeof(string));
			m_dtSource.Columns.Add(Column.Localisation, typeof(string));
			m_dtSource.Columns.Add(Column.Type, typeof(string));
			m_dtSource.Columns.Add(Column.Task, typeof(string));
			m_dtSource.Columns.Add(Column.Radio, typeof(string));
			m_dtSource.Columns.Add(Column.Information, typeof(string));
			m_dtSource.Columns.Add(Column.Data, typeof(Asset));

			foreach (Asset asset in m_assets)
			{
				DataRow dr = m_dtSource.NewRow();
				dr.SetField(Column.Data, asset);
				ObjectToDataRow(dr);
				m_dtSource.Rows.Add(dr);
			}
		}

		public void ObjectToDataRow(DataRow dr)
		{
			Asset asset = dr.Field<Asset>(Column.Data);

			dr.SetField(Column.Included, asset.Included);
			dr.SetField(Column.MapDisplay, asset.MapDisplay);
			dr.SetField(Column.Function, asset.Function);
			dr.SetField(Column.Id, asset.Id);
			dr.SetField(Column.Side, asset.Side);
			dr.SetField(Column.Class, asset.Class);
			dr.SetField(Column.DisplayName, asset.DisplayName);
			dr.SetField(Column.Localisation, asset.GetLocalisation());
			dr.SetField(Column.Type, asset.Type);
			dr.SetField(Column.Task, asset.Task);
			dr.SetField(Column.Radio, asset.GetRadioString());
			dr.SetField(Column.Information, asset.Information);
		}

		protected override void InitializeGridColumns()
		{
			m_dgv.Columns.Clear();

			m_dgv.AddColumn<bool>(Column.Included, "Incl.");
			m_dgv.AddColumn<ElementAssetMapDisplay>(Column.MapDisplay, "Map").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Function, "Function").ReadOnly = true;
			m_dgv.AddColumn<int>(Column.Id, "Id").ReadOnly = true;
			m_dgv.AddColumn<ElementAssetSide>(Column.Side, "Side").ReadOnly = true;
			m_dgv.AddColumn<ElementDcsObjectClass>(Column.Class, "Class").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.DisplayName, "Name").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Localisation, "Localisation").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Type, "Type").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Task, "Task").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Radio, "Radio").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Information, "Information").ReadOnly = true;
		}

		private bool IncludedStrict(DataRow dr)
		{
			if (dr is null)
				return false;

			Asset asset = dr.Field<Asset>(Column.Data);

			if (m_flightMission is object)
			{
				return m_flightMission.OpposingAssetIds.Contains(asset.Id);
			}
			else
			{
				return asset.Included;
			}
		}

		private bool IncludedLoose(DataRow dr)
		{
			if (dr is null)
				return false;

			if (IncludedStrict(dr))
				return true;

			Asset asset = dr.Field<Asset>(Column.Data);
			if (m_flightMission is object)
			{
				return ((asset as AssetGroup)?.Units.Select(_u => _u.Id).Intersect(m_flightMission.OpposingUnitIds).Any()).GetValueOrDefault(false);
			}
			else
			{
				return ((asset as AssetGroup)?.Units.Where(_u => _u.Included).Any()).GetValueOrDefault(false);
			}
		}

		private void SetIncluded(DataRow dr, bool bIncluded)
		{
			if (dr is null)
				return;

			Asset asset = dr.Field<Asset>(Column.Data);

			if (m_flightMission is object)
			{
				m_flightMission.IncludeOpposingAsset(asset.Id, bIncluded);
			}
			else
			{
				asset.Included = bIncluded;
			}

			ObjectToDataRow(dr);
			(m_dgv.DataSource as BindingSource).EndEdit();
		}

		private void SetIncluded(IEnumerable<DataRow> drl, bool bIncluded)
		{
			foreach (DataRow dr in drl)
			{
				SetIncluded(dr, bIncluded);
			}
		}

		private void SetMapDisplay(IEnumerable<DataRow> drl, ElementAssetMapDisplay mapDisplay)
		{
			foreach (DataRow dr in drl)
			{
				Asset asset = dr.Field<Asset>(Column.Data);
				if (asset.MapDisplay != mapDisplay)
				{
					asset.MapDisplay = mapDisplay;
					asset.InitializeMapOverlay();
					ObjectToDataRow(dr);
				}
			}
			(m_dgv.DataSource as BindingSource).EndEdit();
		}

		private void ShowAssetDetail(DataRow dr)
		{
			Asset asset = dr.Field<Asset>(Column.Data);

			FrmAssetDetail f = new FrmAssetDetail(asset);
			f.ShowDialog();
			AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
			ObjectToDataRow(dr);
			(m_dgv.DataSource as BindingSource).EndEdit();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			DataRow dr = (dgvc.OwningRow.DataBoundItem as DataRowView)?.Row;
			if (column.DataPropertyName == Column.Included)
			{
				if (IncludedLoose(dr))
				{
					cellStyle.BackColor = Color.LightGreen;
				}
			}

			return cellStyle;
		}

		protected override void CellValueChanged(string sColumnName, DataRow dr)
		{
			Asset asset = dr.Field<Asset>(Column.Data);

			if (sColumnName == Column.Included)
			{
				SetIncluded(dr, dr.Field<bool>(Column.Included));
				AssetModified?.Invoke(this, new EventArgsAsset() { Asset = asset });
			}
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			IEnumerable<DataRow> selectedRows = m_dgv.GetSelectedDataRows();

			DataRow singleSelectedRow = selectedRows.Count() == 1 ? selectedRows.First() : null;
			Asset singleSelectedAsset = singleSelectedRow?.Field<Asset>(Column.Data);

			menu.Items.Clear();

			if (singleSelectedRow is object)
				menu.Items.AddMenuItem("Details", (object _sender, EventArgs _e) => { ShowAssetDetail(singleSelectedRow); });
			//if (m_missionData is null && singleSelectedRow is AssetFlight assetFlight && assetFlight.MissionData is object)
			//	menu.Items.AddMenuItem("Mission", (object _sender, EventArgs _e) => { ShowMission(assetFlight); });

			menu.Items.AddMenuSeparator();

			if (m_dgv.Columns[Column.Included].Visible && selectedRows.Count() > 0)
			{
				ToolStripMenuItem tmsiUsage = menu.Items.AddMenuItem("Inclusion", null);
				tmsiUsage.DropDownItems.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedRows, false); });
				tmsiUsage.DropDownItems.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedRows, true); });
			}

			if (m_dgv.Columns[Column.MapDisplay].Visible && selectedRows.Count() > 0)
			{
				ToolStripMenuItem tmsiMapDisplay = menu.Items.AddMenuItem("Map display", null);
				tmsiMapDisplay.DropDownItems.AddMenuItem("None", (object _sender, EventArgs _e) => { SetMapDisplay(selectedRows, ElementAssetMapDisplay.None); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Point", (object _sender, EventArgs _e) => { SetMapDisplay(selectedRows, ElementAssetMapDisplay.Point); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Orbit", (object _sender, EventArgs _e) => { SetMapDisplay(selectedRows, ElementAssetMapDisplay.Orbit); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Full route", (object _sender, EventArgs _e) => { SetMapDisplay(selectedRows, ElementAssetMapDisplay.FullRoute); });
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

		private void CellDoubleClickEvent(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataRow dr = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;

			if (column.ReadOnly)
			{
				if (dr is object)
					ShowAssetDetail(dr);
			}
		}
		#endregion
	}
}
