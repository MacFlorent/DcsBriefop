using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnitFlight : BopUnit
	{
		#region Fields
		#endregion

		#region Properties
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopUnitFlight(Miz miz, Theatre theatre, BopGroup bopGroup, MizUnit mizUnit) : base(miz, theatre, bopGroup, mizUnit)
		{
			Tacan = bopGroup.GetTacanFromRouteTaskAction(Id);
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		#endregion
	}
}
