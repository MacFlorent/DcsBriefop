using DcsBriefop.Data;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizBopAirbase : BaseMizBopWithDefault
	{
		public int Id { get; set; }
		public ElementAirbaseType AirbaseType { get; set; }
		public List<MizBopAirbaseRadio> Radios { get; set; } = new List<MizBopAirbaseRadio>();
		public Tacan Tacan { get; set; }
		public string MapMarker { get; set; }
	}

	internal class MizBopAirbaseRadio
	{
		public Radio Radio { get; set; }
		public string Label { get; set; }
		public bool Used { get; set; }
	}
}
