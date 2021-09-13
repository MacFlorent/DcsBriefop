using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		public BriefingCoalition BriefingCoalition { get; private set; }

		public UcBriefingCoalition(GMapControl map, BriefingPack bp, BriefingCoalition bc) : base (map, bp)
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

			foreach (BriefingFlight bf in BriefingCoalition.Flights)
			{
				DgvFlights.Rows.Add(bf.Included, bf.Id, bf.Callsign, bf.Name, bf.UnitTypes, bf.Task, bf.RadioString, bf);
			}
		}

		public override void ScreenToData()
		{
			BriefingCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			BriefingCoalition.Task = TbTask.Text;
		}

		private void DtpDate_Validated(object sender, System.EventArgs e)
		{
			//ScreenToData();
		}

		private void TbSituation_Validated(object sender, System.EventArgs e)
		{
			//ScreenToData();
		}

		private void TbTask_Validated(object sender, System.EventArgs e)
		{
			//ScreenToData();
		}

		private void DgvFlights_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			MessageBox.Show("Flight detail - choose included, preset frequencies, additional info, targets for briefing");
		}

		private void TbBullseyeCoordinates_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Map.Position = new GMap.NET.PointLatLng(BriefingCoalition.Bullseye.Latitude.Degrees, BriefingCoalition.Bullseye.Longitude.Degrees);
		}
	}
}
