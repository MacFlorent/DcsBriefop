using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class DcsAirdrome
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public Tacan Tacan { get; set; }
		public List<Radio> Radios { get; set; }
	}
}
