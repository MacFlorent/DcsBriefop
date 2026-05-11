using Mapsui.Styles;

namespace DcsBriefop.Map
{
	internal class BriefopMarkerStyle(BriefopMarker marker) : BaseStyle
	{
		public BriefopMarker Marker { get; } = marker;
	}
}
