namespace DcsBriefop.Map
{
	internal class XYZProviderOpenTopoMap : XYZProviderBase
	{
		public XYZProviderOpenTopoMap()
		{
			Name = "OpenTopoMap";
			UrlTemplate = "https://tile.opentopomap.org/{z}/{x}/{y}.png";
		}
	}
}
