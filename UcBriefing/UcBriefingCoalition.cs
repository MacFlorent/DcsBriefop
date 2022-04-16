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
		#region Columns
		private static class GridColumn
		{
			public static readonly string Included = "Usage";
			public static readonly string MapDisplay = "MapDisplay";
			public static readonly string Mission = "Mission";
			public static readonly string Side = "Side";
			public static readonly string Function = "Function";
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}
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

			TabPage tp = new TabPage("Own assets");
			TcAssets.TabPages.Add(tp);
			DataGridView dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridOwnAsset(dgv);
			FillGridAsset(dgv, Coalition.OwnAssets);

			tp = new TabPage("Opposing assets");
			TcAssets.TabPages.Add(tp);
			dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridOpposingAsset(dgv);
			FillGridAsset(dgv, Coalition.OpposingAssets);

			tp = new TabPage("Airdromes");
			TcAssets.TabPages.Add(tp);
			dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridAirdrome(dgv);
			FillGridAsset(dgv, Coalition.Airdromes.OfType<Asset>().ToList());

			if (iSelectedIndex > 0)
				TcAssets.SelectedIndex = iSelectedIndex;
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

			dgv.CellEndEdit += DgvAssets_CellEndEdit;
			dgv.CellMouseUp += DgvAssets_CellMouseUp;
			dgv.CellDoubleClick += DgvAssets_CellDoubleClick;
			dgv.CellFormatting += DgvAssets_CellFormatting;
		}

		private void InitializeGridAsset(DataGridView dgv)
		{
			dgv.Columns.Clear();

			DataGridViewCheckBoxColumn colIncluded = new DataGridViewCheckBoxColumn() { Name = GridColumn.Included, HeaderText = "Included" };
			DataGridViewCheckBoxColumn colMission = new DataGridViewCheckBoxColumn() { Name = GridColumn.Mission, HeaderText = "Mission" };
			colIncluded.ValueType = colMission.ValueType = typeof(bool); ;

			dgv.Columns.Add(colIncluded);
			dgv.Columns.Add(colMission);
			dgv.Columns.Add(GridColumn.MapDisplay, "Map");
			dgv.Columns.Add(GridColumn.Id, "ID");
			dgv.Columns.Add(GridColumn.Function, "Function");
			dgv.Columns.Add(GridColumn.Name, "Description");
			dgv.Columns.Add(GridColumn.Task, "Task");
			dgv.Columns.Add(GridColumn.Type, "Type");
			dgv.Columns.Add(GridColumn.Radio, "Radio");
			dgv.Columns.Add(GridColumn.Notes, "Notes");
			dgv.Columns.Add(GridColumn.Data, "");

			dgv.Columns[GridColumn.MapDisplay].ReadOnly = true;
			dgv.Columns[GridColumn.Id].ReadOnly = true;
			dgv.Columns[GridColumn.Function].ReadOnly = true;
			dgv.Columns[GridColumn.Name].ReadOnly = true;
			dgv.Columns[GridColumn.Task].ReadOnly = true;
			dgv.Columns[GridColumn.Type].ReadOnly = true;
			dgv.Columns[GridColumn.Radio].ReadOnly = true;
			dgv.Columns[GridColumn.Notes].ReadOnly = true;
			dgv.Columns[GridColumn.Data].Visible = false;
		}

		private void InitializeGridOwnAsset(DataGridView dgv)
		{
			InitializeGridAsset(dgv);
			dgv.Columns[GridColumn.Included].ToolTipText = "Included in Operations page";
		}

		private void InitializeGridOpposingAsset(DataGridView dgv)
		{
			InitializeGridAsset(dgv);
			dgv.Columns[GridColumn.Included].ToolTipText = "Included in Opposition page";

			dgv.Columns[GridColumn.Mission].Visible = false;
			dgv.Columns[GridColumn.Radio].Visible = false;
		}

		private void InitializeGridAirdrome(DataGridView dgv)
		{
			InitializeGridAsset(dgv);

			dgv.Columns[GridColumn.Mission].Visible = false;
			dgv.Columns[GridColumn.Task].Visible = false;
			dgv.Columns[GridColumn.Type].Visible = false;
		}

		private void FillGridAsset(DataGridView dgv, List<Asset> assets)
		{
			dgv.Rows.Clear();

			foreach (Asset asset in assets)
			{
				if (asset is AssetFlight && !CkFilterFlights.Checked)
					continue;
				if (asset is AssetVehicle && !CkFilterVehicles.Checked)
					continue;
				if (asset is AssetShip && !CkFilterShips.Checked)
					continue;
				if (asset is AssetStatic && !CkFilterStatics.Checked)
					continue;
				if (!ToolsBriefop.AssetOrUnitIncluded(asset) && !((asset as AssetFlight)?.MissionData is object) && !CkFilterExcluded.Checked)
					continue;

				RefreshGridRow(dgv, asset);
			}
		}

		private void RefreshGridRow(DataGridView dgv, Asset asset)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in dgv.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == asset)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = dgv.Rows.Add();
				dgvr = dgv.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = asset;
			}

			RefreshGridRowContent(dgvr, GridColumn.Included, asset.Included);

			dgvr.Cells[GridColumn.Mission].ReadOnly = !(asset is AssetFlight);
			RefreshGridRowContent(dgvr, GridColumn.Mission, (asset as AssetFlight)?.MissionData is object);

			RefreshGridRowContent(dgvr, GridColumn.MapDisplay, MasterDataRepository.GetById(MasterDataType.AssetMapDisplay, (int)asset.MapDisplay)?.Label);
			RefreshGridRowContent(dgvr, GridColumn.Function, asset.Function);
			RefreshGridRowContent(dgvr, GridColumn.Id, asset.Id);
			RefreshGridRowContent(dgvr, GridColumn.Name, asset.Description);
			RefreshGridRowContent(dgvr, GridColumn.Task, asset.Task);
			RefreshGridRowContent(dgvr, GridColumn.Type, asset.Type);
			RefreshGridRowContent(dgvr, GridColumn.Radio, asset.GetRadioString());
			RefreshGridRowContent(dgvr, GridColumn.Notes, asset.Information);
		}

		private void RefreshGridRowContent(DataGridViewRow dgvr, string sColumn, object value)
		{
			if (dgvr.DataGridView.Columns.Contains(sColumn))
				dgvr.Cells[sColumn].Value = value;
		}

		private void ShowDetail(Asset asset, DataGridView dgv)
		{
			FrmAssetDetail f = new FrmAssetDetail(asset);
			f.ShowDialog();
			RefreshGridRow(dgv, asset);
		}

		private void ShowMission(AssetFlight asset, DataGridView dgv)
		{
			FrmMissionDetail f = new FrmMissionDetail(asset);
			f.ShowDialog();
		}

		private void SetIncluded(List<Asset> assets, bool bIncluded, DataGridView dgv)
		{
			foreach (Asset asset in assets)
			{
				if (asset.Included != bIncluded)
				{
					asset.Included = bIncluded;
					RefreshGridRow(dgv, asset);
				}
			}
		}

		private void SetMapDisplay(List<Asset> assets, ElementAssetMapDisplay mapDisplay, DataGridView dgv)
		{
			foreach (Asset asset in assets)
			{
				if (asset.MapDisplay != mapDisplay)
				{
					asset.MapDisplay = mapDisplay;
					asset.InitializeMapOverlay();
					RefreshGridRow(dgv, asset);
				}
			}
		}
		#endregion

		#region Menus
		private void InitializeContextMenu(DataGridView dgv)
		{
			dgv.ContextMenuStrip = new ContextMenuStrip();
			dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, dgv, e); };
		}

		private void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<Asset> selectedAssets = new List<Asset>();
			foreach (DataGridViewRow row in dgv.SelectedRows)
			{
				if (row.Cells[GridColumn.Data].Value is Asset asset)
				{
					selectedAssets.Add(asset);
				}
			}

			Asset singleSelectedAsset = selectedAssets.Count == 1 ? selectedAssets[0] as Asset : null;

			menu.Items.Clear();

			if (selectedAssets.Count > 0)
			{
				if (singleSelectedAsset is object)
					menu.Items.AddMenuItem("Details", (object _sender, EventArgs _e) => { ShowDetail(singleSelectedAsset, dgv); });
				if (singleSelectedAsset is AssetFlight assetFlight && assetFlight.MissionData is object)
					menu.Items.AddMenuItem("Mission", (object _sender, EventArgs _e) => { ShowMission(assetFlight, dgv); });

				menu.Items.AddMenuSeparator();

				ToolStripMenuItem tmsiUsage = menu.Items.AddMenuItem("Inclusion", null);
				tmsiUsage.DropDownItems.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, false, dgv); });
				tmsiUsage.DropDownItems.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedAssets, true, dgv); });

				ToolStripMenuItem tmsiMapDisplay = menu.Items.AddMenuItem("Map display", null);
				tmsiMapDisplay.DropDownItems.AddMenuItem("None", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.None, dgv); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Point", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.Point, dgv); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Orbit", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.Orbit, dgv); });
				tmsiMapDisplay.DropDownItems.AddMenuItem("Full route", (object _sender, EventArgs _e) => { SetMapDisplay(selectedAssets, ElementAssetMapDisplay.FullRoute, dgv); });
			}

			if (menu.Items.Count <= 0)
				e.Cancel = true;

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
			DataToScreenTabs();

		}
		private void BtComPresets_Click(object sender, EventArgs e)
		{

			FrmComs f = new FrmComs(Coalition);
			if (f.ShowDialog() == DialogResult.OK)
			{
				DataToScreen();
			}
		}

		private void DgvAssets_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
				asset.Included = (bool)cell.Value;
			}
			else if (column.Name == GridColumn.Mission)
			{
				if (asset is AssetFlight flight)
				{
					if ((bool)cell.Value && flight.MissionData is null)
					{
						flight.AddMissionData();
					}
					else if (!(bool)cell.Value && flight.MissionData is object)
					{
						flight.RemoveMissionData();
					}
				}
			}

		}

		private void DgvAssets_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Included || column.Name == GridColumn.Mission)
			{
				(sender as DataGridView).EndEdit();
			}
		}

		private void DgvAssets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			if (column.Name != GridColumn.Included && column.Name != GridColumn.Mission)
			{
				Asset asset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as Asset;
				ShowDetail(asset, dgv);
			}
		}

		private void DgvAssets_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			Asset asset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as Asset;
			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

			DataGridViewCellStyle cellStyle = dgvc.InheritedStyle;
			if (column.Name == GridColumn.Included)
			{
				if (ToolsBriefop.AssetOrUnitIncluded(asset))
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
