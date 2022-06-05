using DcsBriefop.DataMiz;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class AssetStatic : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		private MizGroupStatic MizGroupStatic { get { return m_mizGroup as MizGroupStatic; } }
		#endregion

		#region CTOR
		public AssetStatic(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupVehicle group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();

			Type = MizGroupStatic.Units.FirstOrDefault()?.Type;
		}
		#endregion

		#region Methods
		#endregion
	}
}
