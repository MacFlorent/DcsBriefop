using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal abstract class Asset : BaseBriefing
	{
		#region Fields
		protected BriefingCoalition m_briefingCoalition;
		protected CustomDataAsset m_customData;
		#endregion

		#region Properties
		protected virtual string DefaultMarker { get; set; } = MarkerBriefopType.dot.ToString();

		public ElementAssetCategory Category
		{
			get { return (ElementAssetCategory)m_customData.Category; }
			set { m_customData.Category = (int)value; }
		}

		public ElementAssetMapDisplay MapDisplay
		{
			get { return (ElementAssetMapDisplay)m_customData.MapDisplay; }
			set { m_customData.MapDisplay = (int)value; }
		}

		public abstract int Id { get; set; }
		public abstract string Name { get; set; }

		public CustomDataMap MapData
		{
			get { return m_customData.MapData; }
			set { m_customData.MapData = value; }
		}

		public List<AssetMapPoint> MapPoints { get; private set; } = new List<AssetMapPoint>();
		#endregion

		#region CTOR
		public Asset(BriefingPack briefingPack, BriefingCoalition briefingCoalition) : base(briefingPack)
		{
			m_briefingCoalition = briefingCoalition;
		}
		#endregion

		#region Methods
		protected abstract void InitializeCustomData();
		protected abstract void InitializeMapPoints(BriefingPack briefingPack);

		protected void InitializeData(BriefingPack briefingPack)
		{
			m_customData = RootCustom.Assets?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (m_customData is null)
			{
				m_customData = new CustomDataAsset(Id);
				RootCustom.Assets.Add(m_customData);

				InitializeCustomData();
			}

			InitializeMapPoints(briefingPack);
			InitializeMapData();
		}

		public void InitializeMapData()
		{
			GMapOverlay staticOverlay = GetStaticMapOverlay();
			if (staticOverlay is null)
				staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

			staticOverlay.Clear();
			List<PointLatLng> points = null;
			
			if (MapDisplay == ElementAssetMapDisplay.Point)
			{
				points = InitializeMapDataPoint(staticOverlay);
			}
			else if (MapDisplay == ElementAssetMapDisplay.Orbit)
			{
				points = InitializeMapDataOrbit(staticOverlay);
			}
			else if(MapDisplay == ElementAssetMapDisplay.FullRoute)
			{
				points = InitializeMapDataFullRoute(staticOverlay);
			}

			if (MapData is null)
			{
				MapData = new CustomDataMap();

				PointLatLng? centerPoint = ToolsMap.GetPointsCenter(points);
				if (centerPoint is object)
				{
					MapData.CenterLatitude = centerPoint.Value.Lat;
					MapData.CenterLongitude = centerPoint.Value.Lng;
				}
				else
				{
					MapData.CenterLatitude = m_briefingCoalition.Bullseye.Latitude.DecimalDegree;
					MapData.CenterLongitude = m_briefingCoalition.Bullseye.Longitude.DecimalDegree;
				}
				MapData.Zoom = ElementMapValue.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
			MapData.AdditionalMapOverlays.Add(m_briefingCoalition.MapData.MapOverlayCustom);
		}

		private List<PointLatLng> InitializeMapDataPoint(GMapOverlay staticOverlay)
		{
			PointLatLng p = new PointLatLng(MapPoints[0].Coordinate.Latitude.DecimalDegree, MapPoints[0].Coordinate.Longitude.DecimalDegree);
			GMarkerBriefop marker = new GMarkerBriefop(p, DefaultMarker, m_briefingCoalition.Color, Name);
			staticOverlay.Markers.Add(marker);

			return new List<PointLatLng>() { p };
		}

		private List<PointLatLng> InitializeMapDataOrbit(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint routePoint in MapPoints)
			{
				if (points.Count > 1)
					break;
				else if (points.Count <= 0 && routePoint.IsOrbitStart())
				{
					PointLatLng p = new PointLatLng(routePoint.Coordinate.Latitude.DecimalDegree, routePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), m_briefingCoalition.Color, Name);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(routePoint.Coordinate.Latitude.DecimalDegree, routePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), m_briefingCoalition.Color, null);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(m_briefingCoalition.Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}

		private List<PointLatLng> InitializeMapDataFullRoute(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint routePoint in MapPoints)
			{
				PointLatLng p = new PointLatLng(routePoint.Coordinate.Latitude.DecimalDegree, routePoint.Coordinate.Longitude.DecimalDegree);
				GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), m_briefingCoalition.Color, null);
				staticOverlay.Markers.Add(marker);
				points.Add(p);
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(m_briefingCoalition.Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}

		public GMapOverlay GetStaticMapOverlay()
		{
			return MapData?.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault();
		}
		#endregion
	}
}
