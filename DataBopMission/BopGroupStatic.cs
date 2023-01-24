using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupStatic : BopGroup
	{
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public BopGroupStatic(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, mizGroup)
		{
			DcsObject = "Static";
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		#endregion
	}
}
