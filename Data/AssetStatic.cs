using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class AssetStatic : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		private MizGroupStatic GroupStatic { get { return m_mizGroup as MizGroupStatic; } }
		#endregion

		#region CTOR
		public AssetStatic(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupVehicle group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();

			MapMarker = GetMarkerFromUnit();

			Type = GroupStatic.Units.FirstOrDefault()?.Type;
		}
		#endregion

		#region Methods
		#endregion
	}
}
