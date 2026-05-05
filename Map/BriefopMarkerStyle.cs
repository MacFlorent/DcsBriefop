using Mapsui.Styles;

namespace DcsBriefop.Map
{
	internal class BriefopMarkerStyle : BaseStyle
	{
		public BriefopMarker Marker { get; }

		public BriefopMarkerStyle(BriefopMarker marker)
		{
			Marker = marker;
		}
	}
}
