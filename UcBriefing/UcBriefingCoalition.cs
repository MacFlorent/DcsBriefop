using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		public BriefingCoalition BriefingCoalition { get; private set; }

		public UcBriefingCoalition(UcMap ucMap, BriefingPack bp, BriefingCoalition bc) : base (ucMap, bp)
		{
			InitializeComponent();

			BriefingCoalition = bc;
		}

		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = BriefingCoalition.BullseyeCoordinates;
			TbBullseyeDescription.Text = BriefingCoalition.BullseyeDescription;
			TbTask.Text = BriefingCoalition.Task;

			DgvFlights.Columns.Add("included", "Included");
			DgvFlights.Columns.Add("id", "ID");
			DgvFlights.Columns.Add("callsign", "Callsign");
			DgvFlights.Columns.Add("name", "Name");
			DgvFlights.Columns.Add("type", "Type");
			DgvFlights.Columns.Add("task", "Task");
			DgvFlights.Columns.Add("radio", "Radio");
			DgvFlights.Columns.Add("_data", "");

			DgvFlights.Columns["_data"].Visible = false;

			foreach (BriefingFlight bga in BriefingCoalition.GroupAirs)
			{
				DgvFlights.Rows.Add(bga.Included, bga.Id, bga.GetCallsign(), bga.Name, bga.Type, bga.Task, bga.GetRadioString(), bga);
			}
			foreach (BriefingShip bgs in BriefingCoalition.GroupShips)
			{
				DgvFlights.Rows.Add(bgs.Included, bgs.Id, bgs.UnitName, bgs.Name, bgs.Type, "", bgs.GetRadioString(), bgs);
			}
		}

		public override void ScreenToData()
		{
			BriefingCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			BriefingCoalition.Task = TbTask.Text;
		}

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
	}
}
