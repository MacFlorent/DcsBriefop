namespace DcsBriefop.DataMiz
{
	internal class MizBopCoalition : BaseMizBopWithDefault
	{
		#region Properties
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public MizBopMap MapData { get; set; }
		#endregion

		#region Methods
		public override bool IsDefaultData()
		{
			return false;
		}
		#endregion
	}
}
