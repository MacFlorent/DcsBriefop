using System.Collections.Generic;

namespace DcsBriefop.Briefing
{
	internal class CustomDataMission : CustomDataBase
	{
		public int Id { get; set; }
		public string Coalition { get; set; }
		public string MissionInformation { get; set; }
		public List<int> TargetIds { get; set; } = new List<int>();
		public Dictionary<int, string> WaypointNotes { get; set; } = new Dictionary<int, string>();

		public CustomDataMap MapData { get; set; }

		public CustomDataMission(int iId, string sCoalition)
		{
			Id = iId;
			Coalition = sCoalition;
		}
	}
}
