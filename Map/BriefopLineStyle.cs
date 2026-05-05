using Mapsui.Styles;

namespace DcsBriefop.Map
{
	internal class BriefopLineStyle : BaseStyle
	{
		public BriefopLine Line { get; }

		public BriefopLineStyle(BriefopLine line)
		{
			Line = line;
		}
	}
}
