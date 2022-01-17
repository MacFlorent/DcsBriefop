using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class AssetVehicle : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		private MizGroupVehicle GroupVehicle { get { return m_mizGroup as MizGroupVehicle; } }
		#endregion

		#region CTOR
		public AssetVehicle(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupVehicle group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();
			MapMarker = MarkerBriefopType.ground.ToString();

			Type = GroupVehicle.Units.FirstOrDefault()?.Type;
		}
		#endregion

		#region Methods
		#endregion
	}
}
