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
		public override ElementDcsObjectClass Class { get { return base.Class == ElementDcsObjectClass.None ? ElementDcsObjectClass.Ground : base.Class; } }
		#endregion

		#region CTOR
		public AssetVehicle(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupVehicle group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();
		}
		#endregion

		#region Methods
		#endregion
	}
}
