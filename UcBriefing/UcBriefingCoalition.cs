using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		#region Columns
		private static class GridColumn
		{
			public static readonly string Side = "Side";
			public static readonly string Usage = "Usage";
			public static readonly string MapDisplay = "MapDisplay";
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
			TbTask.Text = Coalition.Task;

			TcAssets.TabPages.Clear();

			TabPage tp = new TabPage("Own assets");
			TcAssets.TabPages.Add(tp);
			DataGridView dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridOwnAsset(dgv);
			foreach (Asset asset in Coalition.OwnAssets)
			{
				RefreshGridRow(dgv, asset);
			}

			tp = new TabPage("Opposing assets");
			TcAssets.TabPages.Add(tp);
			dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridOpposingAsset(dgv);
			foreach (Asset asset in Coalition.OpposingAssets)
			{
				RefreshGridRow(dgv, asset);
			}

			tp = new TabPage("Airdromes");
			TcAssets.TabPages.Add(tp);
			dgv = new DataGridView();
			SetGridProperties(dgv);
			InitializeContextMenu(dgv);
			tp.Controls.Add(dgv);
			InitializeGridAirdrome(dgv);
			foreach (Asset asset in Coalition.Airdromes)
			{
				RefreshGridRow(dgv, asset);
			}

			SetComPresetButton();
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
			dgv.ReadOnly = true;
			dgv.Dock = DockStyle.Fill;
		}

		private static void InitializeGridOwnAsset(DataGridView dgv)
		{
			dgv.Columns.Add(GridColumn.Usage, "Usage");
			dgv.Columns.Add(GridColumn.MapDisplay, "Map");
			dgv.Columns.Add(GridColumn.Id, "ID");
			dgv.Columns.Add(GridColumn.Name, "Description");
			dgv.Columns.Add(GridColumn.Task, "Task");
			dgv.Columns.Add(GridColumn.Type, "Type");
			dgv.Columns.Add(GridColumn.Radio, "Radio");
			dgv.Columns.Add(GridColumn.Notes, "Notes");
			dgv.Columns.Add(GridColumn.Data, "");

			dgv.Columns[GridColumn.Data].Visible = false;
		}

		private static void InitializeGridOpposingAsset(DataGridView dgv)
		{
			dgv.Columns.Add(GridColumn.Usage, "Usage");
			dgv.Columns.Add(GridColumn.MapDisplay, "Map");
			dgv.Columns.Add(GridColumn.Id, "ID");
			dgv.Columns.Add(GridColumn.Name, "Description");
			dgv.Columns.Add(GridColumn.Task, "Task");
			dgv.Columns.Add(GridColumn.Type, "Type");
			dgv.Columns.Add(GridColumn.Notes, "Notes");
			dgv.Columns.Add(GridColumn.Data, "");

			dgv.Columns[GridColumn.Data].Visible = false;
		}

		private static void InitializeGridAirdrome(DataGridView dgv)
		{
			dgv.Columns.Add(GridColumn.Usage, "Usage");
			dgv.Columns.Add(GridColumn.MapDisplay, "Map");
			dgv.Columns.Add(GridColumn.Id, "ID");
			dgv.Columns.Add(GridColumn.Name, "Description");
			dgv.Columns.Add(GridColumn.Radio, "Radio");
			dgv.Columns.Add(GridColumn.Notes, "Notes");
			dgv.Columns.Add(GridColumn.Data, "");

			dgv.Columns[GridColumn.Data].Visible = false;
		}

		public override void ScreenToData()
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.Task = TbTask.Text;
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

			RefreshGridRowContent(dgvr, GridColumn.Usage, MasterDataRepository.GetById(MasterDataType.AssetUsage, (int)asset.Usage)?.Label);
			RefreshGridRowContent(dgvr, GridColumn.MapDisplay, MasterDataRepository.GetById(MasterDataType.AssetMapDisplay, (int)asset.MapDisplay)?.Label);
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

		private void SetUsage(List<Asset> assets, ElementAssetUsage usage, DataGridView dgv)
		{
			foreach (Asset asset in assets)
			{
				if (asset.Usage != usage)
				{
					asset.Usage = usage;
					(asset as AssetFlight)?.SetMissionData();
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

				ToolStripMenuItem tmsiUsage = menu.Items.AddMenuItem("Usage", null);
				tmsiUsage.DropDownItems.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetUsage(selectedAssets, ElementAssetUsage.Excluded, dgv); });
				tmsiUsage.DropDownItems.AddMenuItem("Mission with detail", (object _sender, EventArgs _e) => { SetUsage(selectedAssets, ElementAssetUsage.MissionWithDetail, dgv); });
				tmsiUsage.DropDownItems.AddMenuItem("Mission", (object _sender, EventArgs _e) => { SetUsage(selectedAssets, ElementAssetUsage.Mission, dgv); });
				tmsiUsage.DropDownItems.AddMenuItem("Support",  (object _sender, EventArgs _e) => { SetUsage(selectedAssets, ElementAssetUsage.Support, dgv); });
				tmsiUsage.DropDownItems.AddMenuItem("Base", (object _sender, EventArgs _e) => { SetUsage(selectedAssets, ElementAssetUsage.Base, dgv); });

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
		private void DgvFlights_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//MessageBox.Show("Flight detail - choose included, preset frequencies, additional info, targets for briefing");
			//UcMap.Overlays.Clear();
			//if (sender is DataGridView dgv)
			//{
			//	object o = dgv.Rows[e.RowIndex].Cells["_data"].Value;
			//	if (o is BriefingGroup bg)
			//	{
			//		UcMap.Overlays.Add(bg.MapOverlay);
			//		UcMap.Refresh();
			//	}
			//}
		}

		private void TbBullseyeDescription_Validated(object sender, System.EventArgs e)
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.ResetBullseyeMarkerDescription();
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
