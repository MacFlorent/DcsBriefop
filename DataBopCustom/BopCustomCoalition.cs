using DcsBriefop.DataBop;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBopCustom
{
	internal class BopCustomCoalition
	{
		#region Properties
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public BopCustomMap MapData { get; set; }
		public List<BopCustomCoalitionAirdrome> BopCustomCoalitionAirdromes { get; set; }
		#endregion

		#region Methods
		public BopCustomCoalitionAirdrome GetAirdrome(int iId)
		{
			return BopCustomCoalitionAirdromes.Where(_a => _a.Id == iId).FirstOrDefault();
		}
		#endregion
	}
}
