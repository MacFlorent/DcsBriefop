using DcsBriefop.Briefing;
using DcsBriefop.Data;
using System;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		private static class GridColumn
		{
			public static readonly string AssetCategory = "AssetCategory";
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
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
			DgvAssets.Columns.Add(GridColumn.Id, "ID");
			DgvAssets.Columns.Add(GridColumn.Name, "Name");
			DgvAssets.Columns.Add(GridColumn.Type, "Type");
			DgvAssets.Columns.Add(GridColumn.Task, "Task");
			DgvAssets.Columns.Add(GridColumn.Radio, "Radio");
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
			dgvr.Cells[GridColumn.Id].Value = asset.Id;
			dgvr.Cells[GridColumn.Name].Value = asset.Name;

			if (asset is AssetFlight flight)
			{
				dgvr.Cells[GridColumn.Type].Value = flight.Type;
				dgvr.Cells[GridColumn.Task].Value = flight.Task;
				dgvr.Cells[GridColumn.Radio].Value = flight.GetRadioString();
			}
			else if (asset is AssetShip ship)
			{
				dgvr.Cells[GridColumn.Type].Value = ship.Type;
				dgvr.Cells[GridColumn.Radio].Value = ship.GetRadioString();
			}
		}

		private void ShowDetail()
		{
			if (DgvAssets.SelectedRows.Count > 0)
			{
				object o = DgvAssets.SelectedRows[0].Cells[GridColumn.Data].Value;
				if (o is AssetGroup group)
				{
					FrmAssetDetail f = new FrmAssetDetail(group);
					f.ShowDialog();
					RefreshGridRow(group);
				}
			}
		}
		#endregion

		#region Menus
		private class MenuName
		{
			public static readonly string Detail = "Detail";
		}

		private void BuildMenu()
		{
			DgvAssets.ContextMenuStrip.Items.Clear();
			DgvAssets.ContextMenuStrip.Items.Add(MenuItem("Details", MenuName.Detail));
		}

		private ToolStripMenuItem MenuItem(string sLabel, string sName)
		{
			return new ToolStripMenuItem(sLabel, null, new EventHandler(ToolStripItemClicked), sName);
		}

		private void ToolStripItemClicked(object sender, EventArgs e)
		{
			ToolStripItem tsi = sender as ToolStripItem;
			if (tsi == null)
				return;

			if (tsi.Name == MenuName.Detail)
			{
				ShowDetail();
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
