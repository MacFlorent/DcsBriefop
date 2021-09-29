using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
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
		#endregion

		#region Properties
		public bool Included
		{
			get
			{
				CustomDataGroup cdf = RootCustom.Groups?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null)
					return Playable;
				else
					return cdf.Included;
			}
			set
			{
				CustomDataGroup cdf = RootCustom.Groups?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null && value != Playable)
					RootCustom.Groups.Add(new CustomDataGroup { Id = Id, Included = value });
				else if (cdf is object)
					cdf.Included = value;
			}
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

		public List<BriefingRoutePoint> RoutePoints { get; private set; } = new List<BriefingRoutePoint>();

		public GMapOverlay MapOverlay { get; private set; }
		#endregion

		#region CTOR
		public BriefingGroup(BriefingPack bp, Group g) : base(bp)
		{
			m_group = g;
			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				RoutePoints.Add(new BriefingRoutePoint(bp, rp));
			}

			InitializeMapOverlay();
		}
		#endregion

		#region Methods
		public string GetUnitTypes()
		{
			IEnumerable<string> grouped = m_group.Units.GroupBy(u => u.Type).Select(g => g.Key);
			return string.Join(",", grouped);
		}

		public string GetTacanString()
		{
			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				RouteTask rtBeacon = rp.RouteTasks.Where(_rt => _rt.Action?.Id == "ActivateBeacon").FirstOrDefault();
				if (rtBeacon?.Action is RouteTaskAction rta)
					return ToolsMasterData.GetTacanString(rta.ParamChannel.GetValueOrDefault(), rta.ParamModeChannel, rta.ParamCallsign);
			}

			return null;
		}


		private void InitializeMapOverlay()
		{
			MapOverlay = new GMapOverlay("group");

			List<PointLatLng> points = new List<PointLatLng>();
			foreach (BriefingRoutePoint brp in RoutePoints)
			{
				PointLatLng p = new PointLatLng(brp.Coordinate.Latitude.DecimalDegree, brp.Coordinate.Longitude.DecimalDegree);
				points.Add(p);
			}


			GMapRoute route = new GMapRoute(points, "route");
			route.Stroke = new Pen(Color.Red, 3);
			MapOverlay.Routes.Add(route);
		}
		#endregion
	}
}
