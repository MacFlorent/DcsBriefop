using DcsBriefop.MasterData;
using System.Collections.Generic;

namespace DcsBriefop.Briefing
{
	internal class CustomData
	{
		public bool? DisplayRed { get; set; }
		public bool? DisplayBlue { get; set; }
		public bool? DisplayNeutral { get; set; }

		public CustomDataCoalition Red { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Blue { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Neutral { get; set; } = new CustomDataCoalition();

		public List<CustomDataFlight> Flights = new List<CustomDataFlight>();

		public CustomData() { }

		public CustomDataCoalition GetCoalition(string sCoalitionCode)
		{
			if (sCoalitionCode == CoalitionCode.Red)
				return Red;
			else if (sCoalitionCode == CoalitionCode.Blue)
				return Blue;
			else if (sCoalitionCode == CoalitionCode.Neutral)
				return Neutral;
			else
				return null;
		}

	}

	internal class CustomDataCoalition
	{
		public string BullseyeDescription { get; set; }
	}

	internal class CustomDataFlight
	{
		public int Id { get; set; }
		public bool Included { get; set; }
	}
}
