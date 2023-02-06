namespace DcsBriefop.DataMiz
{
	internal class MizBopCoalition : BaseMizBopWithDefault
	{
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public MizBopMap MapData { get; set; }
	}
}
