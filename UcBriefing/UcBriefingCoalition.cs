using DcsBriefop.Briefing;
using DcsBriefop.MasterData;
using System;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		#region Properties
		public BriefingCoalition BriefingCoalition { get; private set; }
		#endregion

		#region CTOR
		public UcBriefingCoalition(UcMap ucMap, BriefingPack bp, BriefingCoalition bc) : base (ucMap, bp)
		{
			InitializeComponent();

			BriefingCoalition = bc;

			DgvGroups.ContextMenuStrip = new ContextMenuStrip();
			DgvGroups.ReadOnly = true;
			BuildMenu();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = BriefingCoalition.BullseyeCoordinates;
			TbBullseyeDescription.Text = BriefingCoalition.BullseyeDescription;
			TbTask.Text = BriefingCoalition.Task;

			DgvGroups.Columns.Add("status", "Status");
			DgvGroups.Columns.Add("id", "ID");
			DgvGroups.Columns.Add("name", "Name");
			DgvGroups.Columns.Add("callsign", "Callsign");
			DgvGroups.Columns.Add("type", "Type");
			DgvGroups.Columns.Add("task", "Task");
			DgvGroups.Columns.Add("radio", "Radio");
			DgvGroups.Columns.Add("_data", "");

			DgvGroups.Columns["_data"].Visible = false;

			foreach (BriefingFlight flight in BriefingCoalition.GroupFlights)
			{
				RefreshGridRow(flight);
				//DgvGroups.Rows.Add(bga.BriefingStatus, bga.Id, bga.GetCallsign(), bga.Name, bga.Type, bga.Task, bga.GetRadioString(), bga);
			}
			foreach (BriefingShip ship in BriefingCoalition.GroupShips)
			{
				RefreshGridRow(ship);
				//DgvGroups.Rows.Add(bgs.BriefingStatus, bgs.Id, bgs.UnitName, bgs.Name, bgs.Type, "", bgs.GetRadioString(), bgs);
			}
		}

		public override void ScreenToData()
		{
			BriefingCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			BriefingCoalition.Task = TbTask.Text;
		}

		private void RefreshGridRow(BriefingGroup group)
		{
			DataGridViewRow dgvr = null;
			foreach(DataGridViewRow existingRow in DgvGroups.Rows)
			{
				if (existingRow.Cells["_data"].Value == group)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvGroups.Rows.Add();
				dgvr = DgvGroups.Rows[iNewRowIndex];
				dgvr.Cells["_data"].Value = group;
			}

			dgvr.Cells["status"].Value = GroupStatus.GetById (group.BriefingStatus)?.Label;
			dgvr.Cells["id"].Value = group.Id;
			dgvr.Cells["name"].Value = group.Name;

			if (group is BriefingFlight flight)
			{
				dgvr.Cells["callsign"].Value = flight.GetCallsign();
				dgvr.Cells["type"].Value = flight.Type;
				dgvr.Cells["task"].Value = flight.Task;
				dgvr.Cells["radio"].Value = flight.GetRadioString();
			}
			else if (group is BriefingShip ship)
			{
				dgvr.Cells["callsign"].Value = ship.UnitName;
				dgvr.Cells["type"].Value = ship.Type;
				dgvr.Cells["task"].Value = "";
				dgvr.Cells["radio"].Value = ship.GetRadioString();
			}
		}

		private void ShowDetail()
		{
			if (DgvGroups.SelectedRows.Count > 0)
			{
				object o = DgvGroups.SelectedRows[0].Cells["_data"].Value;
				if (o is BriefingGroup group)
				{
					FrmGroupDetail f = new FrmGroupDetail(group);
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
			DgvGroups.ContextMenuStrip.Items.Clear();
			DgvGroups.ContextMenuStrip.Items.Add(MenuItem("Details", MenuName.Detail));
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
