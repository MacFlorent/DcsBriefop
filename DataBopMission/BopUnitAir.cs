using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnitAir : BopUnit
	{
		#region Fields
		#endregion

		#region Properties
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopUnitAir(Miz miz, Theatre theatre, MizUnit mizUnit, BopGroup bopGroup) : base(miz, theatre, mizUnit)
		{
			Tacan = bopGroup.GetTacanFromTaskAction(Id);
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		#endregion
	}
}
