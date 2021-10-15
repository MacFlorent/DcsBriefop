using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingGroup : BaseBriefing
	{
		#region Fields
		protected Group m_group;
		protected BriefingCoalition m_briefingCoalition;
		protected CustomDataGroup m_customDataGroup;
		#endregion

		#region Properties
		protected virtual string DefaultMarker { get; set; } = MarkerBriefopType.dot.ToString();
		public virtual string Category { get { return "Group"; } }

		public int BriefingStatus
		{
			get { return m_customDataGroup.BriefingCategory; }
			set { m_customDataGroup.BriefingCategory = value; }
		}

		public int Id
		{
			get { return m_group.Id; }
		}
		public string Name
		{
			get { return m_group.Name; }
		}

		public bool Playable
		{
			get { return m_group.Units.Where(u => u.Skill == ElementSkill.Player || u.Skill == ElementSkill.Client).Any(); }
		}

		public bool LateActivation
		{
			get { return m_group.LateActivation; }
		}

		public List<BriefingRoutePoint> RoutePoints { get; private set; } = new List<BriefingRoutePoint>();

		public CustomDataMap MapData
		{
			get { return m_customDataGroup.MapData; }
			set { m_customDataGroup.MapData = value; }
		}
		#endregion

		#region CTOR
		public BriefingGroup(BriefingPack bp, Group g, BriefingCoalition bc) : base(bp)
		{
			m_group = g;
			m_briefingCoalition = bc;

			m_customDataGroup = RootCustom.Groups?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (m_customDataGroup is null)
			{
				m_customDataGroup = new CustomDataGroup(Id);
				RootCustom.Groups.Add(m_customDataGroup);
			}

			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				RoutePoints.Add(new BriefingRoutePoint(bp, rp));
			}
		}
		#endregion

		#region Methods
		public void InitializeMapData()
		{
			GMapOverlay staticOverlay = MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault();
			if (staticOverlay is null)
				staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

			staticOverlay.Clear();
			List<PointLatLng> points = null;
			
			if (BriefingStatus == ElementGroupStatusId.Point)
			{
				points = InitializeMapDataPoint(staticOverlay);
			}
			else if (BriefingStatus == ElementGroupStatusId.Orbit)
			{
				points = InitializeMapDataOrbit(staticOverlay);
			}
			else if(BriefingStatus == ElementGroupStatusId.FullRoute)
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

			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
			MapData.AdditionalMapOverlays.Add(m_briefingCoalition.MapData.MapOverlayCustom);
		}

		private List<PointLatLng> InitializeMapDataPoint(GMapOverlay staticOverlay)
		{
			PointLatLng p = new PointLatLng(RoutePoints[0].Coordinate.Latitude.DecimalDegree, RoutePoints[0].Coordinate.Longitude.DecimalDegree);
			GMarkerBriefop marker = new GMarkerBriefop(p, DefaultMarker, m_briefingCoalition.Color, Name);
			staticOverlay.Markers.Add(marker);

			return new List<PointLatLng>() { p };
		}

		private List<PointLatLng> InitializeMapDataOrbit(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (BriefingRoutePoint routePoint in RoutePoints)
			{
				if (points.Count > 1)
					break;
				else if (points.Count <= 0 && routePoint.RouteTasks.Where(_rt => _rt.Id == ElementRouteTask.Orbit).Any())
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

			foreach (BriefingRoutePoint routePoint in RoutePoints)
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

		public string GetUnitTypes()
		{
			IEnumerable<string> grouped = m_group.Units.GroupBy(u => u.Type).Select(g => g.Key);
			return string.Join(",", grouped);
		}

		public string GetTacanString()
		{
			foreach (BriefingRoutePoint brp in RoutePoints)
			{
				RouteTask rtBeacon = brp.RouteTasks.Where(_rt => _rt.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
				if (rtBeacon?.Action is RouteTaskAction rta)
					return ToolsMasterData.GetTacanString(rta.ParamChannel.GetValueOrDefault(), rta.ParamModeChannel, rta.ParamCallsign);
			}

			return null;
		}
		#endregion
	}
}
