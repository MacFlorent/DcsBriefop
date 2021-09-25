using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingGroup : BaseBriefing
	{
		protected Group m_group;

		public bool Included
		{
			get
			{
				CustomDataGroup cdf = m_manager.RootCustom.Groups?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null)
					return Playable;
				else
					return cdf.Included;
			}
			set
			{
				CustomDataGroup cdf = m_manager.RootCustom.Groups?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null && value != Playable)
					m_manager.RootCustom.Groups.Add(new CustomDataGroup { Id = Id, Included = value });
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

		public BriefingGroup(MissionManager manager, Group g) : base(manager)
		{
			m_group = g;
			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				RoutePoints.Add(new BriefingRoutePoint(m_manager, rp));
			}
		}

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
	}
}
