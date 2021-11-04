using CoordinateSharp;

namespace DcsBriefop.Briefing
{
	internal class AssetMapPoint : BaseBriefing
	{
		#region Fields
		#endregion

		#region Properties
		public int Number { get; set; }
		public string Name { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public AssetMapPoint(BriefingPack briefingPack, int iNumber) : base(briefingPack)
		{
			Number = iNumber;
		}

		public AssetMapPoint(BriefingPack briefingPack, int iNumber, string sName, Coordinate coordinate) : this(briefingPack, iNumber)
		{
			Name = sName;
			Coordinate = coordinate;
		}
		#endregion

		#region Methods
		public virtual bool IsOrbitStart()
		{
			return false;
		}
		#endregion
	}
}