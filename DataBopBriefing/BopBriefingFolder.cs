using DcsBriefop.Data;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingFolder
	{
		public string Name { get; set; }
		public List<string> AircraftTypes { get; set; }
		BopBriefingOptions Options { get; set; }
		public List<BopBriefingPage> Pages { get; set; }

	}
}
