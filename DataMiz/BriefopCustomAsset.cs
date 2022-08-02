using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal abstract class BriefopCustomAsset : BaseBriefopCustomWithDefault
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public bool Included { get; set; }
		public int MapDisplay { get; set; }
		public string Information { get; set; }

		public BriefopCustomAsset(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}

	internal class BriefopCustomAirdrome : BriefopCustomAsset
	{
		public BriefopCustomAirdrome(int iId, string sCoalitionName) : base(iId, sCoalitionName) { }
	}

	internal class BriefopCustomGroup : BriefopCustomAsset
	{
		public bool WithMissionData { get; set; }
		public BriefopCustomGroup(int iId, string sCoalitionName) : base(iId, sCoalitionName) { }
	}

	internal class BriefopCustomUnit : BaseBriefopCustomWithDefault
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public bool Included { get; set; }

		public BriefopCustomUnit(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}

	internal class BriefopCustomAssetFlightMission
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public string MissionInformation { get; set; }
		public List<int> ThreatUnitIds { get; set; } = new List<int>();
		public Dictionary<int, string> WaypointNotes { get; set; } = new Dictionary<int, string>();

		public BriefopCustomMap MapData { get; set; }

		public BriefopCustomAssetFlightMission(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}
}
