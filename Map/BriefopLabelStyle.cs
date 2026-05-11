using Mapsui.Styles;

namespace DcsBriefop.Map
{
	internal class BriefopLabelStyle(BriefopLabel label) : BaseStyle
	{
		public BriefopLabel Label { get; } = label;
	}
}
