using DcsBriefop.Briefing;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		private static class GridColumn
		{
			public static readonly string AssetCategory = "AssetCategory";
			public static readonly string AssetMapDisplay = "AssetMapDisplay";
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}

		#region Properties
		public BriefingCoalition BriefingCoalition { get; private set; }
		#endregion

		#region CTOR
		public UcBriefingCoalition(UcMap ucMap, BriefingPack bp, BriefingCoalition bc) : base (ucMap, bp)
		{
			InitializeComponent();

			BriefingCoalition = bc;

			DgvAssets.ContextMenuStrip = new ContextMenuStrip();
			DgvAssets.ReadOnly = true;
			DgvAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			DgvAssets.AllowUserToResizeColumns = true;

			BuildMenu();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = BriefingCoalition.BullseyeCoordinates;
			TbBullseyeDescription.Text = BriefingCoalition.BullseyeDescription;
			TbTask.Text = BriefingCoalition.Task;

			DgvAssets.Columns.Add(GridColumn.AssetCategory, "Cat.");
			DgvAssets.Columns.Add(GridColumn.AssetMapDisplay, "Map");
			DgvAssets.Columns.Add(GridColumn.Id, "ID");
			DgvAssets.Columns.Add(GridColumn.Name, "Name");
			DgvAssets.Columns.Add(GridColumn.Task, "Task");
			DgvAssets.Columns.Add(GridColumn.Type, "Type");
			DgvAssets.Columns.Add(GridColumn.Radio, "Radio");
			DgvAssets.Columns.Add(GridColumn.Notes, "Notes");
			DgvAssets.Columns.Add(GridColumn.Data, "");

			DgvAssets.Columns[GridColumn.Data].Visible = false;

			foreach (Asset asset in BriefingCoalition.Assets)
			{
				RefreshGridRow(asset);
			}
		}

		public override void ScreenToData()
		{
			BriefingCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			BriefingCoalition.Task = TbTask.Text;
		}

		private void RefreshGridRow(Asset asset)
		{
			DataGridViewRow dgvr = null;
			foreach(DataGridViewRow existingRow in DgvAssets.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == asset)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvAssets.Rows.Add();
				dgvr = DgvAssets.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = asset;
			}

			dgvr.Cells[GridColumn.AssetCategory].Value = MasterDataRepository.GetById(MasterDataType.AssetCategory, (int)asset.Category)?.Label;
			dgvr.Cells[GridColumn.AssetMapDisplay].Value = MasterDataRepository.GetById(MasterDataType.AssetMapDisplay, (int)asset.MapDisplay)?.Label;
			dgvr.Cells[GridColumn.Id].Value = asset.Id;
			dgvr.Cells[GridColumn.Name].Value = asset.Name;
			dgvr.Cells[GridColumn.Task].Value = asset.Task;
			dgvr.Cells[GridColumn.Type].Value = asset.Type;
			dgvr.Cells[GridColumn.Radio].Value = asset.RadioString;
			dgvr.Cells[GridColumn.Notes].Value = asset.Information;
		}

		private void ShowDetail()
		{
			if (DgvAssets.SelectedRows.Count > 0)
			{
				object o = DgvAssets.SelectedRows[0].Cells[GridColumn.Data].Value;
				if (o is Asset asset)
				{
					FrmAssetDetail f = new FrmAssetDetail(asset);
					f.ShowDialog();
					RefreshGridRow(asset);
				}
			}
		}
		private void ShowMission()
		{
			if (DgvAssets.SelectedRows.Count > 0)
			{
				object o = DgvAssets.SelectedRows[0].Cells[GridColumn.Data].Value;
				if (o is AssetGroup asset && asset.Category == ElementAssetCategory.Mission)
				{
					FrmMissionDetail f = new FrmMissionDetail(asset);
					f.ShowDialog();
				}
			}
		}

		private void SetCategory(ElementAssetCategory category)
		{

		}

		private void SetMapDisplay(ElementAssetMapDisplay mapDisplay)
		{
			foreach (DataGridViewRow row in DgvAssets.SelectedRows)
			{
				if (row.Cells[GridColumn.Data].Value is Asset asset && asset.MapDisplay != mapDisplay)
				{
					asset.MapDisplay = mapDisplay;
					asset.InitializeMapOverlay();
					RefreshGridRow(asset);
				}
			}
		}
		#endregion

		#region Menus
		private enum MenuName
		{
			AssetDetail,
			AssetMission,
			AssetCategorySetExcluded,
			AssetCategorySetMission,
			AssetCategorySetSupport,
			AssetCategorySetBase,
			AssetMapDisplaySetNone,
			AssetMapDisplaySetPoint,
			AssetMapDisplaySetOrbit,
			AssetMapDisplaySetFullRoute,
		}

		private void BuildMenu()
		{
			DgvAssets.ContextMenuStrip.Items.Clear();
			DgvAssets.ContextMenuStrip.Items.Add(MenuItem("Details", MenuName.AssetDetail));
			DgvAssets.ContextMenuStrip.Items.Add(MenuItem("Mission", MenuName.AssetMission));
			DgvAssets.ContextMenuStrip.Items.Add(new ToolStripSeparator());

			int iIndex = DgvAssets.ContextMenuStrip.Items.Add(MenuItem("Category", null));
			ToolStripMenuItem tmsiParent = DgvAssets.ContextMenuStrip.Items[iIndex] as ToolStripMenuItem;
			tmsiParent.DropDownItems.Add(MenuItem("Excluded", MenuName.AssetCategorySetExcluded));
			tmsiParent.DropDownItems.Add(MenuItem("Mission", MenuName.AssetCategorySetMission));
			tmsiParent.DropDownItems.Add(MenuItem("Support", MenuName.AssetCategorySetSupport));
			tmsiParent.DropDownItems.Add(MenuItem("Base", MenuName.AssetCategorySetBase));
			
			iIndex = DgvAssets.ContextMenuStrip.Items.Add(MenuItem("Map display", null));
			tmsiParent = DgvAssets.ContextMenuStrip.Items[iIndex] as ToolStripMenuItem;
			tmsiParent.DropDownItems.Add(MenuItem("None", MenuName.AssetMapDisplaySetNone));
			tmsiParent.DropDownItems.Add(MenuItem("Point", MenuName.AssetMapDisplaySetPoint));
			tmsiParent.DropDownItems.Add(MenuItem("Orbit", MenuName.AssetMapDisplaySetOrbit));
			tmsiParent.DropDownItems.Add(MenuItem("Full route", MenuName.AssetMapDisplaySetFullRoute));
		}

		private ToolStripMenuItem MenuItem(string sLabel, MenuName? name)
		{
			return new ToolStripMenuItem(sLabel, null, new EventHandler(ToolStripItemClicked), name?.ToString());
		}

		private void ToolStripItemClicked(object sender, EventArgs e)
		{
			ToolStripItem tsi = sender as ToolStripItem;
			if (tsi == null)
				return;

			if (string.IsNullOrEmpty(tsi.Name))
				return;

			Enum.TryParse(tsi.Name, out MenuName clickedName);

			if (clickedName == MenuName.AssetDetail)
			{
				ShowDetail();
			}
			else if (clickedName == MenuName.AssetMission)
			{
				ShowMission();
			}
			// Category set
			else if (clickedName == MenuName.AssetCategorySetExcluded)
			{
				SetCategory(ElementAssetCategory.Excluded);
			}
			else if (clickedName == MenuName.AssetCategorySetMission)
			{
				SetCategory(ElementAssetCategory.Mission);
			}
			else if (clickedName == MenuName.AssetCategorySetSupport)
			{
				SetCategory(ElementAssetCategory.Support);
			}
			else if (clickedName == MenuName.AssetCategorySetBase)
			{
				SetCategory(ElementAssetCategory.Base);
			}
			// Map display set
			else if (clickedName == MenuName.AssetMapDisplaySetNone)
			{
				SetMapDisplay(ElementAssetMapDisplay.None);
			}
			else if (clickedName == MenuName.AssetMapDisplaySetPoint)
			{
				SetMapDisplay(ElementAssetMapDisplay.Point);
			}
			else if (clickedName == MenuName.AssetMapDisplaySetOrbit)
			{
				SetMapDisplay(ElementAssetMapDisplay.Orbit);
			}
			else if (clickedName == MenuName.AssetMapDisplaySetFullRoute)
			{
				SetMapDisplay(ElementAssetMapDisplay.FullRoute);
			}

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

		private void DgvFlights_SelectionChanged(object sender, System.EventArgs e)
		{
			//if (sender is DataGridView dgv && dgv.SelectedRows.Count > 0)
			//{
			//	object o = dgv.SelectedRows[0].Cells["_data"].Value;
			//	if (o is BriefingGroup bg)
			//	{
			//		UcMap.RemoveOverlay("group");
			//		UcMap.AddOverlay(bg.MapOverlay);
			//		UcMap.RefreshMap("group");
			//	}
			//}

			
		}

		private void TbBullseyeDescription_Validated(object sender, System.EventArgs e)
		{
			BriefingCoalition.BullseyeDescription = TbBullseyeDescription.Text;
		}
		#endregion
	}
}
