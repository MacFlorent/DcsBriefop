using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class BriefopCustomMission
	{
		public string TypeName { get; set; }
		public string CoalitionName { get; set; }
		public string MissionInformation { get; set; }
		public List<int> OpposingAssetIds { get; set; } = new List<int>();
		public List<int> OpposingUnitIds { get; set; } = new List<int>();

		public BriefopCustomMap MapData { get; set; }

		public BriefopCustomMission(string sTypeName, string sCoalitionName)
		{
			TypeName = sTypeName;
			CoalitionName = sCoalitionName;
		}
	}
}
