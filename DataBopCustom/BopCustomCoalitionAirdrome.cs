using DcsBriefop.Data;
using System.Collections.Generic;

namespace DcsBriefop.DataBopCustom
{
	internal class BopCustomCoalitionAirdrome : BaseBopCustomWithDefault
	{
		public string CoalitionName { get; set; }
		public int Id { get; set; }
		public bool MapDisplay { get; set; }
		public string MapColor { get; set; }
		public string Information { get; set; }
		public List<Radio> RadiosOverride { get; set; }
		public Tacan TacanOverride { get; set; }

		public BopCustomCoalitionAirdrome(int iId)
		{
			Id = iId;
		}
	}

}
