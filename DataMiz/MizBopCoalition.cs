namespace DcsBriefop.DataMiz
{
	internal class MizBopCoalition : BaseMizBopWithDefault
	{
		#region Properties
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public MizBopMap MapData { get; set; }
		//public List<BopCustomCoalitionAirdrome> BopCustomCoalitionAirdromes { get; set; }
		#endregion

		#region Methods
		//public BopCustomCoalitionAirdrome GetAirdrome(int iId)
		//{
		//	return BopCustomCoalitionAirdromes.Where(_a => _a.Id == iId).FirstOrDefault();
		//}
		#endregion
	}
}
