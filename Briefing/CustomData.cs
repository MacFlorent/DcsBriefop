using DcsBriefop.MasterData;
using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DcsBriefop.Briefing
{
	internal class CustomData
	{
		public bool? DisplayRed { get; set; }
		public bool? DisplayBlue { get; set; }
		public bool? DisplayNeutral { get; set; }

		public CustomDataMap MapData { get; set; }

		public CustomDataCoalition Red { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Blue { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Neutral { get; set; } = new CustomDataCoalition();

		public List<CustomDataGroup> Groups = new List<CustomDataGroup>();

		public CustomData() { }

		public static CustomData DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<CustomData>(sJson, new GMapOverlayJsonConverter(), new GMarkerBriefopJsonConverter());
		}

		public string SerializeToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented, new GMapOverlayJsonConverter(), new GMarkerBriefopJsonConverter());
		}

		public CustomDataCoalition GetCoalition(string sCoalitionCode)
		{
			if (sCoalitionCode == ElementCoalition.Red)
				return Red;
			else if (sCoalitionCode == ElementCoalition.Blue)
				return Blue;
			else if (sCoalitionCode == ElementCoalition.Neutral)
				return Neutral;
			else
				return null;
		}
	}

	internal class CustomDataMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }

		public GMapOverlay MapOverlayCustom { get; set; }
		[JsonIgnore]
		public List<GMapOverlay> AdditionalMapOverlays { get; private set; } = new List<GMapOverlay>();
	}

	internal class CustomDataCoalition
	{
		public string BullseyeDescription { get; set; }
		public CustomDataMap MapData { get; set; }
	}

	internal class CustomDataGroup
	{
		public int Id { get; set; }
		public int Category { get; set; }
		public int MapDisplay { get; set; }
		
		public CustomDataMap MapData { get; set; }

		public CustomDataGroup(int iId)
		{
			Id = iId;
		}
	}
}

