using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
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
		public CustomDataAssetGroup CustomData { get; set; }

		public override ElementAssetUsage Usage
		{
			get { return (ElementAssetUsage)CustomData.Usage; }
			set { CustomData.Usage = (int)value; }
		}

		public override ElementAssetMapDisplay MapDisplay
		{
			get { return (ElementAssetMapDisplay)CustomData.MapDisplay; }
			set { CustomData.MapDisplay = (int)value; }
		}

		public override int Id { get { return m_group.Id; } }
		public override string Name { get { return m_group.Name; } }

		public override string CustomInformation
		{
			get { return CustomData.Information; }
			set { CustomData.Information = value; }
		}

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
		public AssetGroup(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, Group group) : base(briefingPack, briefingCoalition, side)
		{
			m_group = group;
			InitializeData(briefingPack);
		}
		#endregion

		#region Initialize
		protected override void InitializeMapPoints(BriefingPack briefingPack)
		{
			int iNumber = 0;
			foreach (RoutePoint rp in m_group.RoutePoints)
			{
				MapPoints.Add(new AssetRoutePoint(briefingPack, iNumber, this, rp));
				iNumber++;
			}
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
			foreach (AssetRoutePoint brp in MapPoints.OfType<AssetRoutePoint>())
			{
				RouteTask rtBeacon = brp.RouteTasks.Where(_rt => _rt.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
				if (rtBeacon?.Action is RouteTaskAction rta)
					return new Tacan() { Channel = rta.ParamChannel.GetValueOrDefault(), Mode = rta.ParamModeChannel, Identifier = rta.ParamCallsign }.ToString();
			}

			return null;
		}
		#endregion
	}
}
