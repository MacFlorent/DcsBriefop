using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using Org.BouncyCastle.Asn1.Cms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroup : BaseBop
	{
		#region Fields
		protected MizGroup m_mizGroup;
		#endregion

		#region Properties
		public ElementDcsObjectClass Class;
		public ElementDcsObjectAttribute Attributes;
		public string CoalitionName { get; protected set; }
		public string CountryName { get; protected set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public bool LateActivation { get; set; }
		public List<BopUnit> Units { get; set; }
		public BopUnit MainUnit { get; set; }
		public List<BopMapPoint> MapPoints { get; set; }
		#endregion

		#region CTOR
		public BopGroup(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre)
		{
			Class = ElementDcsObjectClass.None;
			Attributes = ElementDcsObjectAttribute.None;

			CoalitionName = sCoalitionName;
			CountryName = sCountryName;
			m_mizGroup = mizGroup;

			Id = m_mizGroup.Id;
			Name = m_mizGroup.Name;
			LateActivation = m_mizGroup.LateActivation;

			FromMizUnits();

			MapPoints = new List<BopMapPoint>();
			if (m_mizGroup.RoutePoints is object && m_mizGroup.RoutePoints.Count > 0)
			{
				int iNumber = 0;
				foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
				{
					MapPoints.Add(new BopRoutePoint(Miz, Theatre, iNumber, mizRoutePoint));
					iNumber++;
				}
			}
			else
			{
				MapPoints.Add(new BopMapPoint(Miz, Theatre, m_mizGroup.Y, m_mizGroup.X));
			}

			Playable = m_mizGroup.Units.Where(_u => _u.Skill == ElementSkill.Player || _u.Skill == ElementSkill.Client).Any();
			Class = MainUnit.Class;
			Attributes = Units.Aggregate<BopUnit, ElementDcsObjectAttribute>(0, (currentAttributes, _bopUnit) => currentAttributes | _bopUnit.Attributes);
			Type = string.Join(",", Units.GroupBy(_u => _u.Type).Select(_g => _g.Key));

			//MapMarker = MainUnit.MapMarker ?? MapMarker;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizGroup.Id = Id;
			m_mizGroup.Name = Name;
			m_mizGroup.LateActivation = LateActivation;

			foreach (BopUnit bopUnit in Units)
			{
				bopUnit.ToMiz();
			}
		}

		protected virtual void FromMizUnits()
		{
			Units = new List<BopUnit>();
			foreach (MizUnit mizUnit in m_mizGroup.Units)
			{
				BopUnit bopUnit = new BopUnit(Miz, Theatre, mizUnit);
				Units.Add(bopUnit);
				if (MainUnit is null)
					MainUnit = bopUnit;
				else if (!MainUnit.MainInGroup && bopUnit.MainInGroup)
					MainUnit = bopUnit;
			}
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			foreach (BopUnit bopUnit in Units)
			{
				bopUnit.FinalizeFromMiz();
			}
			foreach (BopMapPoint bopMapPoint in MapPoints)
			{
				bopMapPoint.FinalizeFromMiz();
			}
		}
		#endregion

		#region Methods
		public Tacan GetTacanFromTaskAction(int iUnitId)
		{
			foreach (MizRoutePoint routePoint in m_mizGroup.RoutePoints)
			{
				MizRouteTaskAction taskAction = routePoint.GetRouteTaskAction(ElementRouteTaskAction.ActivateBeacon);
				if (taskAction is object && taskAction.ParamUnitId.GetValueOrDefault(0) == iUnitId && taskAction.ParamChannel is object)
					return new Tacan() { Channel = taskAction.ParamChannel.Value, Mode = taskAction.ParamModeChannel, Identifier = taskAction.ParamCallsign };
			}

			return null;
		}

		//public override void Persist()
		//{
		//	base.Persist();

		//	foreach (BopAssetUnit unit in Units)
		//	{
		//		unit.Persist();
		//	}

		//	m_mizGroup.RoutePoints.Clear();
		//	foreach (BopRoutePoint routePoint in MapPoints.OfType<BopRoutePoint>())
		//	{
		//		routePoint.Persist();
		//		m_mizGroup.RoutePoints.Add(routePoint.MizRoutePoint);
		//	}
		//}

		//public string GetTacanString()
		//{
		//	foreach (BopRoutePoint routePoint in MapPoints.OfType<BopRoutePoint>())
		//	{
		//		MizRouteTask taskBeacon = routePoint.MizRoutePoint.RouteTaskHolder.Tasks.Where(_rt => _rt.Params.Action?.Id == ElementRouteTask.ActivateBeacon).FirstOrDefault();
		//		if (taskBeacon?.Params.Action is MizRouteTaskAction rta)
		//			return new Tacan() { Channel = rta.ParamChannel.GetValueOrDefault(), Mode = rta.ParamModeChannel, Identifier = rta.ParamCallsign }.ToString();
		//	}

		//	return null;
		//}

		//protected void NumberMapPoints()
		//{
		//	int iNumber = 0;
		//	foreach (BopRoutePoint rp in MapPoints.OfType<BopRoutePoint>())
		//	{
		//		rp.Number = iNumber;
		//		iNumber++;
		//	}
		//}
		#endregion
	}
}
