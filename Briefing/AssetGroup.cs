using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal abstract class AssetGroup : Asset
	{
		#region Fields
		protected Group m_group;
		#endregion

		#region Properties
		public override int Id { get { return m_group.Id; } }
		public override string Name { get { return m_group.Name; } }

		public bool Playable
		{
			get { return m_group.Units.Where(u => u.Skill == ElementSkill.Player || u.Skill == ElementSkill.Client).Any(); }
		}

		public bool LateActivation
		{
			get { return m_group.LateActivation; }
		}
		#endregion

		#region CTOR
		public AssetGroup(BriefingPack briefingPack, BriefingCoalition briefingCoalition, Group group) : base(briefingPack, briefingCoalition)
		{
			m_group = group;
			InitializeData(briefingPack);
		}
		#endregion

		#region Methods
		protected override void InitializeMapPoints(BriefingPack briefingPack)
		{
			int iNumber = 0;
			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				MapPoints.Add(new AssetRoutePoint(briefingPack, rp, iNumber));
				iNumber++;
			}
		}

		public string GetUnitTypes()
		{
			IEnumerable<string> grouped = m_group.Units.GroupBy(u => u.Type).Select(g => g.Key);
			return string.Join(",", grouped);
		}

		public string GetTacanString()
		{
			foreach (AssetRoutePoint brp in MapPoints.OfType<AssetRoutePoint>())
			{
				RouteTask rtBeacon = brp.RouteTasks.Where(_rt => _rt.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
				if (rtBeacon?.Action is RouteTaskAction rta)
					return ToolsMisc.GetTacanString(rta.ParamChannel.GetValueOrDefault(), rta.ParamModeChannel, rta.ParamCallsign);
			}

			return null;
		}
		#endregion
	}
}
