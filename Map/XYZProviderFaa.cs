namespace DcsBriefop.Map
{
	// ArcGIS tile URL order is {z}/{y}/{x} (level/row/col).
	// XyzUrlBuilder maps {y}→Row and {x}→Col, so the template below produces the correct path without any axis swap.

	internal class XYZProviderFaaVfrSectional : XYZProviderBase
	{
		public XYZProviderFaaVfrSectional()
		{
			Name = "FAA VFR Sectional (Nevada only)";
			UrlTemplate = "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/VFR_Sectional/MapServer/tile/{z}/{y}/{x}";
		}
	}

	internal class XYZProviderFaaVfrTerminal : XYZProviderBase
	{
		public XYZProviderFaaVfrTerminal()
		{
			Name = "FAA VFR Terminal (Nevada only)";
			UrlTemplate = "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/VFR_Terminal/MapServer/tile/{z}/{y}/{x}";
		}
	}

	internal class XYZProviderFaaIfrLow : XYZProviderBase
	{
		public XYZProviderFaaIfrLow()
		{
			Name = "FAA IFR Low (Nevada only)";
			UrlTemplate = "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/IFR_AreaLow/MapServer/tile/{z}/{y}/{x}";
		}
	}

	internal class XYZProviderFaaIfrHigh : XYZProviderBase
	{
		public XYZProviderFaaIfrHigh()
		{
			Name = "FAA IFR High (Nevada only)";
			UrlTemplate = "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/IFR_High/MapServer/tile/{z}/{y}/{x}";
		}
	}
}
