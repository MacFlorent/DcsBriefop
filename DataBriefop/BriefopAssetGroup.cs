using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBriefop
{
	internal abstract class BriefopAssetGroup : BriefopAsset
	{
		#region Fields
		protected MizGroup m_mizGroup;
		#endregion

		#region Properties
		public override ElementDcsObjectClass Class { get { return MainUnit?.Class ?? ElementDcsObjectClass.None; } }

		public bool Playable { get; set; }
		public bool LateActivation { get; set; }
		public List<BriefopAssetUnit> Units { get; set; }
		public BriefopAssetUnit MainUnit { get; set; }
		#endregion

		#region CTOR
		public BriefopAssetGroup(BriefopManager parentManager, string sCoalitionName, string sCountryName, MizGroup group) : base(parentManager, sCoalitionName, sCountryName)
		{
			m_mizGroup = group;

			Id = m_mizGroup.Id;
			Name = DisplayName = m_mizGroup.Name;
			LateActivation = m_mizGroup.LateActivation;

			int iNumber = 0;
			foreach (MizRoutePoint routePoint in m_mizGroup.RoutePoints)
			{
				MapPoints.Add(new BriefopRoutePoint(ParentManager, iNumber, routePoint));
				iNumber++;
			}

			Units = new List<BriefopAssetUnit>();
			foreach (MizUnit unit in m_mizGroup.Units)
			{
				BriefopAssetUnit assetUnit = new BriefopAssetUnit(ParentManager, unit, this);
				Units.Add(assetUnit);
				if (MainUnit is null)
					MainUnit = assetUnit;
				else if (!MainUnit.MainInGroup && assetUnit.MainInGroup)
					MainUnit = assetUnit;
			}

			Playable = m_mizGroup.Units.Where(u => u.Skill == ElementSkill.Player || u.Skill == ElementSkill.Client).Any();
			Type = string.Join(",", Units.GroupBy(u => u.Type).Select(g => g.Key));
			
			MapMarker = MainUnit.MapMarker ?? MapMarker;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			foreach (BriefopAssetUnit unit in Units)
			{
				unit.Persist();
			}

			m_mizGroup.RoutePoints.Clear();
			foreach (BriefopRoutePoint routePoint in MapPoints.OfType<BriefopRoutePoint>())
			{
				routePoint.Persist();
				m_mizGroup.RoutePoints.Add(routePoint.MizRoutePoint);
			}
		}

		public string GetTacanString()
		{
			foreach (AssetRoutePoint routePoint in MapPoints.OfType<AssetRoutePoint>())
			{
				MizRouteTask taskBeacon = routePoint.MizRoutePoint.RouteTaskHolder.Tasks.Where(_rt => _rt.Params.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
				if (taskBeacon?.Params.Action is MizRouteTaskAction rta)
					return new Tacan() { Channel = rta.ParamChannel.GetValueOrDefault(), Mode = rta.ParamModeChannel, Identifier = rta.ParamCallsign }.ToString();
			}

			return null;
		}

		protected void NumberMapPoints()
		{
			int iNumber = 0;
			foreach (AssetRoutePoint rp in MapPoints.OfType<AssetRoutePoint>())
			{
				rp.Number = iNumber;
				iNumber++;
			}
		}
		#endregion
	}
}
