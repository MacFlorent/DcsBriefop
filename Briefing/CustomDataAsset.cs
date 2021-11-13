namespace DcsBriefop.Briefing
{
	internal abstract class CustomDataAsset : CustomDataWithDefault
	{
		public int Id { get; set; }
		public string Coalition { get; set; }
		public int Usage { get; set; }
		public int MapDisplay { get; set; }
		public string Information { get; set; }

		public CustomDataAsset(int iId, string sCoalition)
		{
			Id = iId;
			Coalition = sCoalition;
		}
	}

	internal class CustomDataAssetAirdrome : CustomDataAsset
	{
		public CustomDataAssetAirdrome(int iId, string sCoalition) : base(iId, sCoalition) { }
	}

	internal class CustomDataAssetGroup : CustomDataAsset
	{
		public CustomDataAssetGroup(int iId, string sCoalition) : base(iId, sCoalition) { }
	}
}
