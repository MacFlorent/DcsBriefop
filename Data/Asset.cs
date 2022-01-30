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
		protected static readonly string m_sBullsPointName = "BULLS";
		#endregion

		#region Properties
		public BriefingCoalition Coalition { get; protected set; }
		public ElementAssetSide Side { get; set; }


		public string MapMarker { get; set; }
		public Color Color { get; set; }
		public ElementAssetUsage Usage { get; set; }
		public ElementAssetMapDisplay MapDisplay { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
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
			MapMarker = ElementMapTemplateMarker.Mark;

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
			GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName (p, MapMarker, Color, Name, 1, 0);
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
					GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
			}

			if (points.Count > 1)
			{
				GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, Name, ElementMapTemplateRoute.DashDot, Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}

		public List<PointLatLng> InitializeMapDataFullRoute(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint mapPoint in MapPoints.Where(_mp => _mp.Name != m_sBullsPointName))
			{
				PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
				GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, $"{mapPoint.Number}:{mapPoint.Name}", 1, 0);
				staticOverlay.Markers.Add(marker);
				points.Add(p);
			}

			if (points.Count > 1)
			{
				GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, Name, ElementMapTemplateRoute.DashDot, Color, 2);
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
