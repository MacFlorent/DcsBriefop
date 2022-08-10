using System.Collections.Generic;

namespace DcsBriefop.DataBopCustom
{
	internal abstract class BopCustomAsset : BaseBopCustomWithDefault
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public string DisplayName { get; set; }
		public string Information { get; set; }

		public BopCustomAsset(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}

	internal class BopCustomGroup : BopCustomAsset
	{
		public bool WithMissionData { get; set; }
		public BopCustomGroup(int iId, string sCoalitionName) : base(iId, sCoalitionName) { }
	}

	internal class BopCustomUnit : BaseBopCustomWithDefault
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public bool Included { get; set; }

		public BopCustomUnit(int iId, string sCoalitionName)
		{
			Id = iId;
			CoalitionName = sCoalitionName;
		}
	}
}
