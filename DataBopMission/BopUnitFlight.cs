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
		public BopUnitFlight(Miz miz, Theatre theatre, MizUnit mizUnit, BopGroup bopGroup) : base(miz, theatre, mizUnit, bopGroup)
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
