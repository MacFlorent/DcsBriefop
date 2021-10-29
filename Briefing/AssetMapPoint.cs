using CoordinateSharp;

namespace DcsBriefop.Briefing
{
	internal class AssetMapPoint : BaseBriefing
	{
		#region Properties
		public virtual string Name { get; set; }
		public virtual Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public AssetMapPoint(BriefingPack briefingPack) : base(briefingPack) { }
		#endregion

		#region Methods
		public virtual bool IsOrbitStart()
		{
			return false;
		}
		#endregion
	}
}