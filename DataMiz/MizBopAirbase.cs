using DcsBriefop.Data;

namespace DcsBriefop.DataMiz
{
	internal class MizBopAirbase : BaseMizBopWithDefault
	{
		#region Properties
		public int Id { get; set; }
		public ElementAirbaseType AirbaseType { get; set; }
		public List<MizBopAirbaseRadio> Radios { get; set; } = new List<MizBopAirbaseRadio>();
		public Tacan Tacan { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region Methods
		public override bool IsDefaultData()
		{
			if (Tacan is object)
				return false;
			if (!string.IsNullOrEmpty(MapMarker))
				return false;
			if (Radios is object && Radios.Count > 0)
				return false;

			return true;
		}
		#endregion
	}

	internal class MizBopAirbaseRadio
	{
		public Radio Radio { get; set; }
		public string Label { get; set; }
		public bool Used { get; set; }
	}
}
