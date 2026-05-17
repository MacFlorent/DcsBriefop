using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using System.Text;

namespace DcsBriefop.Map
{
	internal static class WMSProvider
	{
		#region Fields
		public static string CustomWMSURL = "http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE";
		public static string szWmsLayer = "LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR";
		#endregion

		#region Methods
		public static ITileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(), new WmsUrlBuilder(), "WMS DCS", null, null, null);
		}
		#endregion

		#region WmsUrlBuilder
		private class WmsUrlBuilder : IUrlBuilder
		{
			public Uri GetUrl(TileInfo tileInfo)
			{
				string sBbox = FormattableString.Invariant($"{tileInfo.Extent.MinX},{tileInfo.Extent.MinY},{tileInfo.Extent.MaxX},{tileInfo.Extent.MaxY}");
				StringBuilder sb = new(CustomWMSURL);
				sb.Append('&').Append(szWmsLayer);
				sb.Append("&FORMAT=image/png&TRANSPARENT=TRUE&ATTRIBUTION=Flappie");
				sb.Append("&MINZOOM=8&MAXZOOM=12");
				sb.Append("&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=");
				sb.Append("&SRS=EPSG:3857");
				sb.Append("&BBOX=").Append(sBbox);
				sb.Append("&WIDTH=256&HEIGHT=256");
				return new Uri(sb.ToString());
			}
		}
		#endregion
	}
}
