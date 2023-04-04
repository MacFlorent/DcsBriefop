using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;
using PuppeteerSharp;
using System.Drawing;
using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace DcsBriefop.Map
{


	/// <summary>
	/// WMS Custom
	/// </summary>
	public class WMSProvider : GMapProvider
	{
		public static readonly WMSProvider Instance;

		WMSProvider()
		{
			MaxZoom = 24;
		}

		static WMSProvider()
		{
			Instance = new WMSProvider();

			Type mytype = typeof(GMapProviders);
			FieldInfo field = mytype.GetField("DbHash", BindingFlags.Static | BindingFlags.NonPublic);
			Dictionary<int, GMapProvider> list = (Dictionary<int, GMapProvider>)field.GetValue(Instance);

			list.Add(Instance.DbId, Instance);
		}

		#region GMapProvider Members

		readonly Guid id = new Guid("4574218D-B552-4CAF-89AE-F20951BBDB2B");

		public override Guid Id
		{
			get { return id; }
		}

		readonly string name = "WMS Custom";

		public override string Name
		{
			get { return name; }
		}

		GMapProvider[] overlays;

		public override GMapProvider[] Overlays
		{
			get
			{
				if (overlays == null)
				{
					overlays = new GMapProvider[] { this };
				}
				return overlays;
			}
		}

		public override PureProjection Projection
		{
			get { return MercatorProjection.Instance; }
		}

		public override PureImage GetTileImage(GPoint pos, int zoom)
		{
			string url = MakeTileImageUrl(pos, zoom);

			return GetTileImageUsingHttp(url);
		}

		#endregion

		string MakeTileImageUrl(GPoint pos, int zoom)
		{
			var px1 = Projection.FromTileXYToPixel(pos);
			var px2 = px1;

			px1.Offset(0, Projection.TileSize.Height);
			PointLatLng p1 = Projection.FromPixelToLatLng(px1, zoom);

			px2.Offset(Projection.TileSize.Width, 0);
			PointLatLng p2 = Projection.FromPixelToLatLng(px2, zoom);

			StringBuilder sb = new StringBuilder(CustomWMSURL);
			sb.AppendWithSeparator(szWmsLayer, "&");
			sb.AppendWithSeparator("FORMAT=image/png", "&");
			sb.AppendWithSeparator("TRANSPARENT=TRUE", "&");
			sb.AppendWithSeparator("ATTRIBUTION=Flappie", "&");
			sb.AppendWithSeparator("MINZOOM=8", "&");
			sb.AppendWithSeparator("MAXZOOM=12", "&");
			sb.AppendWithSeparator("SERVICE=WMS", "&");
			sb.AppendWithSeparator("VERSION=1.1.1", "&");
			sb.AppendWithSeparator("REQUEST=GetMap", "&");
			sb.AppendWithSeparator("STYLES=", "&");
			sb.AppendWithSeparator("SRS=EPSG:3857", "&");
			//sb.AppendWithSeparator($"BBOX={p1.Lng},{p1.Lat},{p2.Lng},{p2.Lat}", "&");
			sb.AppendWithSeparator($"BBOX={px1.X},{px1.Y},{px2.X},{px2.Y}", "&");
			sb.AppendWithSeparator($"WIDTH={Projection.TileSize.Width}", "&");
			sb.AppendWithSeparator($"HEIGHT={Projection.TileSize.Height}", "&");

			return sb.ToString();
		}

		public static string szWmsLayer = "LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR";

		//public static string CustomWMSURL = "http://mapbender.wheregroup.com/cgi-bin/mapserv?map=/data/umn/osm/osm_basic.map";
		public static string CustomWMSURL = "http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE";
	}
}
/*
< img class= "olTileImage" style = "visibility: inherit; opacity: 1; position: absolute; left: 239%; top: -128%; width: 2560%; height: 438%;"
src="http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE&amp;LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR&amp;FORMAT=image/png&amp;TRANSPARENT=TRUE&amp;ATTRIBUTION=Flappie&amp;MINZOOM=8&amp;MAXZOOM=12&amp;SERVICE=WMS&amp;VERSION=1.1.1&amp;REQUEST=GetMap&amp;STYLES=&amp;SRS=EPSG%3A3857&amp;BBOX=4215743.6100442,5321460.9067695,4998458.7795754,5455378.5803065&amp;WIDTH=2560&amp;HEIGHT=438" >

http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE&LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR&FORMAT=image/png&TRANSPARENT=TRUE&ATTRIBUTION=Flappie&MINZOOM=8&MAXZOOM=12&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=&SRS=EPSG%3A3857&BBOX=4215743.6100442,5321460.9067695,4998458.7795754,5455378.5803065&WIDTH=2560&HEIGHT=438

 
http://dcsmaps.com/cgi-bin/mapserv
?map=CAUCASUS_MAPFILE
&LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR
&FORMAT=image/png
&TRANSPARENT=TRUE
&ATTRIBUTION=Flappie
&MINZOOM=8
&MAXZOOM=12
&SERVICE=WMS
&VERSION=1.1.1
&REQUEST=GetMap
&STYLES=
&SRS=EPSG:3857
&BBOX=4215743.6100442,5321460.9067695,4998458.7795754,5455378.5803065
&WIDTH=2560
&HEIGHT=438
 */
