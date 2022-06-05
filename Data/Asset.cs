using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.WindowsForms;
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
		public virtual ElementDcsObjectClass Class { get; protected set; } = ElementDcsObjectClass.None;

		public BriefingCoalition Coalition { get; protected set; }
		public ElementAssetSide Side { get; set; }
		public ElementAssetFunction Function { get; set; }


		public string MapMarker { get; set; }
		public Color Color { get; set; }
		public bool Included { get; set; }
		public ElementAssetMapDisplay MapDisplay { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
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
			Function = ElementAssetFunction.Other;

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
			Included = false;
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
			GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName (p, MapMarker, Color, DisplayName, 1, 0);
			staticOverlay.Markers.Add(marker);

			return new List<PointLatLng>() { p };
		}

		public List<PointLatLng> InitializeMapDataOrbit(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();
			bool bDone = false;
			int iCount = 0;
			foreach (AssetMapPoint mapPoint in MapPoints)
			{
				iCount++;

				if (bDone)
					break;
				else if (points.Count <= 0 && mapPoint.GetOrbitPattern() is string sPattern)
				{
					PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker;
					if (sPattern == "Circle" || iCount == MapPoints.Count)
					{
						marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Circle, Color, DisplayName, 2, 0);
						bDone = true;
					}
					else
					{
						marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
					}
					
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
					bDone = true;
				}
			}

			if (points.Count > 1)
			{
				GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, DisplayName, ElementMapTemplateRoute.DashDot, Color, 2);
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
				GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, DisplayName, ElementMapTemplateRoute.DashDot, Color, 2);
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
				//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
				sLocalisation = point.GetLocalisationString();
			}

			return sLocalisation;
		}
		#endregion
	}
}
