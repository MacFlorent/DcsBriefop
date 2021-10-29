using DcsBriefop.Briefing;
using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class Airdrome
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Radio> Radios { get; set; }
	}
}
