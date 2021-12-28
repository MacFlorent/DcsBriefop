using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Data
{
	internal abstract class Asset : BaseBriefing
	{
		#region Fields
		#endregion

		#region Properties
		public BriefingCoalition Coalition { get; protected set; }
		public ElementAssetSide Side { get; set; }


		protected string MapMarker { get; set; }
		protected Color Color { get; set; }
		public ElementAssetUsage Usage { get; set; }
		public ElementAssetMapDisplay MapDisplay { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Task { get; set; }
		public string Type { get; set; }

		protected string m_customInformation { get; set; }
		public string Information
		{
			get
			{
				if (m_customInformation is null)
					return GetDefaultInformation();
				else
					return m_customInformation;
			}
			set
			{
				m_customInformation = value;
			}
		}

		public List<AssetMapPoint> MapPoints { get; private set; } = new List<AssetMapPoint>();
		public GMapOverlay MapOverlayStatic { get; set; }
		#endregion

		#region CTOR
		public Asset(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side) : base(core)
		{
			Coalition = coalition;
			Side = side;
		}
		#endregion

		#region Initialize
		protected void Initialize()
		{
			InitializeData();
			InitializeDataCustom();
			InitializeMapPoints();
			InitializeMapOverlay();
		}

		protected virtual void InitializeData()
		{
			MapMarker = MarkerBriefopType.dot.ToString();
			
			if (Side == ElementAssetSide.Own)
			{
				Color = Coalition.OwnColor;
			}
			else if (Side == ElementAssetSide.Opposing)
			{
				Color = Coalition.OpposingColor;
			}
			else
			{
				Color = ElementCoalitionColor.Neutral; 
			}
		}

		protected virtual void InitializeDataCustom()
		{
			Usage = ElementAssetUsage.Excluded;
			MapDisplay = ElementAssetMapDisplay.None;
		}

		protected abstract void InitializeMapPoints();

		public void InitializeMapOverlay()
		{
			if (MapOverlayStatic is null)
				MapOverlayStatic = new GMapOverlay(ElementMapValue.OverlayStatic);

			MapOverlayStatic.Clear();

			if (MapDisplay == ElementAssetMapDisplay.Point)
			{
				InitializeMapDataPoint(MapOverlayStatic);
			}
			else if (MapDisplay == ElementAssetMapDisplay.Orbit)
			{
				InitializeMapDataOrbit(MapOverlayStatic);
			}
			else if (MapDisplay == ElementAssetMapDisplay.FullRoute)
			{
				InitializeMapDataFullRoute(MapOverlayStatic);
			}
		}

		public List<PointLatLng> InitializeMapDataPoint(GMapOverlay staticOverlay)
		{
			PointLatLng p = new PointLatLng(MapPoints[0].Coordinate.Latitude.DecimalDegree, MapPoints[0].Coordinate.Longitude.DecimalDegree);
			GMarkerBriefop marker = new GMarkerBriefop(p, MapMarker, Color, Name);
			staticOverlay.Markers.Add(marker);

			return new List<PointLatLng>() { p };
		}

		public List<PointLatLng> InitializeMapDataOrbit(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint mapPoint in MapPoints)
			{
				if (points.Count > 1)
					break;
				else if (points.Count <= 0 && mapPoint.IsOrbitStart())
				{
					PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), Color, Name);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), Color, null);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}

		public List<PointLatLng> InitializeMapDataFullRoute(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint mapPoint in MapPoints)
			{
				PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
				GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), Color, $"{mapPoint.Number}:{mapPoint.Name}");
				staticOverlay.Markers.Add(marker);
				points.Add(p);
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}
		#endregion

		#region Methods
		protected virtual string GetDefaultInformation()
		{
			return "";
		}

		public virtual string GetRadioString()
		{
			return "";
		}

		public virtual string GetLocalisation()
		{
			string sLocalisation = "";
			AssetMapPoint point = MapPoints.FirstOrDefault();
			if (point is object)
			{
				sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			}

			return sLocalisation;
		}
		#endregion
	}
}
