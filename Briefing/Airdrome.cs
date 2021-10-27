using System.Collections.Generic;

namespace DcsBriefop.Briefing
{
	internal class Airdrome
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Radio> Radios { get; set; }
	}
}
