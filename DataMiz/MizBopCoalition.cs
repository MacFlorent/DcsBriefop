using DcsBriefop.Data;

namespace DcsBriefop.DataMiz
{
	internal class MizBopCoalition : BaseMizBopSerializable
	{
		#region Properties
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public ElementBullseyeWaypoint BullseyeWaypoint { get; set; }
		public MizBopMap MapData { get; set; }
		#endregion

		#region Methods
		#endregion
	}
}
