using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class BriefopCustomMission
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public string MissionInformation { get; set; }
		public List<int> TargetIds { get; set; } = new List<int>();
		public Dictionary<int, string> WaypointNotes { get; set; } = new Dictionary<int, string>();

		public BriefopCustomMap MapData { get; set; }

		public BriefopCustomMission(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}
}
