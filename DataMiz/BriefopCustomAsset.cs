namespace DcsBriefop.DataMiz
{
	internal abstract class BriefopCustomAsset : BaseBriefopCustomWithDefault
	{
		public int Id { get; set; }
		public string CoalitionName { get; set; }
		public int Usage { get; set; }
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
		public BriefopCustomGroup(int iId, string sCoalitionName) : base(iId, sCoalitionName) { }
	}
}
