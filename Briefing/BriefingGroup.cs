using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
		public int BriefingCategory
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
			}

			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				RoutePoints.Add(new BriefingRoutePoint(bp, rp));
			}
		}
		#endregion

		#region Methods
		protected void InitializeMapOverlay()
		{
			GMapOverlay staticOverlay = new GMapOverlay();
			List<PointLatLng> points = GetListRouteMapPoints();

			foreach (PointLatLng p in points)
			{
				staticOverlay.Markers.Add(new GMarkerCross(p));
			}

			if (points.Count >= 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(m_briefingCoalition.Color, 2);
				staticOverlay.Routes.Add(route);
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

		protected List<PointLatLng> GetListRouteMapPoints()
		{
			List<PointLatLng> points = new List<PointLatLng>();

			if (BriefingCategory != ElementGroupBriefingCategory.NotSet && BriefingCategory != ElementGroupBriefingCategory.Excluded && RoutePoints.Count > 0)
			{
				if (BriefingCategory == ElementGroupBriefingCategory.Point)
				{
					points.Add(new PointLatLng(RoutePoints[0].Coordinate.Latitude.DecimalDegree, RoutePoints[0].Coordinate.Longitude.DecimalDegree));
				}
				else if (BriefingCategory == ElementGroupBriefingCategory.Orbit)
				{
					foreach (BriefingRoutePoint brp in RoutePoints)
					{
						if (points.Count <= 0 && brp.RouteTasks.Where(_rt => _rt.Action?.Id == ElementRouteTask.Orbit).Any())
						{
							points.Add(new PointLatLng(brp.Coordinate.Latitude.DecimalDegree, brp.Coordinate.Longitude.DecimalDegree));
						}
						else if (points.Count == 1)
							points.Add(new PointLatLng(brp.Coordinate.Latitude.DecimalDegree, brp.Coordinate.Longitude.DecimalDegree));
						else
							break;
					}
				}
				else if (BriefingCategory == ElementGroupBriefingCategory.FullRoute)
				{
					foreach (BriefingRoutePoint brp in RoutePoints)
					{
						PointLatLng p = new PointLatLng(brp.Coordinate.Latitude.DecimalDegree, brp.Coordinate.Longitude.DecimalDegree);
						points.Add(p);
					}
				}
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
