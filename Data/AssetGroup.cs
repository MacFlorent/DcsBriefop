﻿using CoordinateSharp;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	internal abstract class AssetGroup : Asset
	{
		#region Fields
		protected MizGroup m_mizGroup;
		protected BriefopCustomGroup m_briefopCustomGroup;
		#endregion

		#region Properties
		public bool Playable { get; set; }
		public bool LateActivation { get; set; }
		public Coordinate Coordinate { get; set; }
		public List<AssetUnit> Units { get; set; } = new List<AssetUnit>();
		#endregion

		#region CTOR
		public AssetGroup(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroup group) : base(core, coalition, side)
		{
			m_mizGroup = group;
			Initialize();
		}
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();

			Id = m_mizGroup.Id;
			Name = m_mizGroup.Name;
			Type = "Group";
			Playable = m_mizGroup.Units.Where(u => u.Skill == ElementSkill.Player || u.Skill == ElementSkill.Client).Any();
			LateActivation = m_mizGroup.LateActivation;
			Coordinate = Core.Theatre.GetCoordinate(m_mizGroup.Y, m_mizGroup.X);

			foreach (MizUnit unit in m_mizGroup.Units)
			{
				Units.Add(new AssetUnit(Core, unit, this));
			}
		}

		protected override void InitializeDataCustom()
		{
			m_briefopCustomGroup = Core.Miz.BriefopCustomData.GetGroup(Id, Coalition.CoalitionName);

			if (m_briefopCustomGroup is null)
			{
				m_briefopCustomGroup = new BriefopCustomGroup(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.AssetGroups.Add(m_briefopCustomGroup);

				m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Excluded;
				m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;

				m_briefopCustomGroup.SetDefaultData();
			}

			Usage = (ElementAssetUsage)m_briefopCustomGroup.Usage;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomGroup.MapDisplay;
		}

		protected override void InitializeMapPoints()
		{
			int iNumber = 0;
			foreach (MizRoutePoint rp in m_mizGroup.RoutePoints)
			{
				MapPoints.Add(new AssetRoutePoint(Core, iNumber, this, rp));
				iNumber++;
			}
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			m_briefopCustomGroup.Usage = (int)Usage;
			m_briefopCustomGroup.MapDisplay = (int)MapDisplay;
			m_briefopCustomGroup.Information = m_customInformation;
		}

		public string GetUnitTypes()
		{
			IEnumerable<string> grouped = m_mizGroup.Units.GroupBy(u => u.Type).Select(g => g.Key);
			return string.Join(",", grouped);
		}

		public string GetTacanString()
		{
			foreach (AssetRoutePoint brp in MapPoints.OfType<AssetRoutePoint>())
			{
				MizRouteTask rtBeacon = brp.RouteTasks.Where(_rt => _rt.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
				if (rtBeacon?.Action is MizRouteTaskAction rta)
					return new Tacan() { Channel = rta.ParamChannel.GetValueOrDefault(), Mode = rta.ParamModeChannel, Identifier = rta.ParamCallsign }.ToString();
			}

			return null;
		}
		#endregion
	}
}