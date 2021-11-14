using DcsBriefop.Data;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal abstract class Asset : BaseBriefing
	{
		#region Fields
		#endregion

		#region Properties
		protected virtual string MapMarker { get; set; } = MarkerBriefopType.dot.ToString();
		protected virtual Color Color { get; set; } = ElementCoalitionColor.Neutral;

		public BriefingCoalition BriefingCoalition { get; protected set; }
		public ElementAssetSide Side { get; set; }

		public abstract ElementAssetUsage Usage { get; set; }
		public abstract ElementAssetMapDisplay MapDisplay { get; set; }
		public abstract int Id { get; }
		public abstract string Name { get; }
		public abstract string Task { get; }
		public abstract string Type { get; }
		public abstract string RadioString { get; }

		public abstract string CustomInformation { get; set; }
		public string Information
		{
			get
			{
				if (CustomInformation is null)
					return GetDefaultInformation();
				else
					return CustomInformation;
			}
			set
			{
				CustomInformation = value;
			}
		}

		public List<AssetMapPoint> MapPoints { get; private set; } = new List<AssetMapPoint>();
		public GMapOverlay MapOverlayStatic { get; set; }
		#endregion

		#region CTOR
		public Asset(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side) : base(briefingPack)
		{
			BriefingCoalition = briefingCoalition;
			Side = side;
			
			if (Side == ElementAssetSide.Own)
			{
				Color = briefingCoalition.OwnColor;
			}
			else if (Side == ElementAssetSide.Opposing)
			{
				Color = briefingCoalition.OpposingColor;
			}

		}
		#endregion

		#region Initialize
		protected abstract void InitializeCustomData();
		protected abstract void InitializeMapPoints(BriefingPack briefingPack);
		
		protected void InitializeData(BriefingPack briefingPack)
		{
			InitializeCustomData();
			InitializeMapPoints(briefingPack);
			InitializeMapOverlay();
		}

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
			else if(MapDisplay == ElementAssetMapDisplay.FullRoute)
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
