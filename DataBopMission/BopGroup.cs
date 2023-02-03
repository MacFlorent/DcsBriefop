using CommandLine;
using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UnitsNet.Units;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroup : BaseBop
	{
		#region Fields
		protected MizGroup m_mizGroup;
		protected MizBopGroup m_mizBopGroup;
		#endregion

		#region Properties
		public string DcsGroupType { get; protected set; }
		public ElementDcsObjectClass ObjectClass { get; protected set; }
		public ElementDcsObjectAttribute Attributes { get; protected set; }
		public string CoalitionName { get; protected set; }
		public string CountryName { get; protected set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public bool LateActivation { get; set; }
		public Radio Radio { get; set; }
		public List<BopUnit> Units { get; set; }
		public BopUnit MainUnit { get; set; }
		public List<BopMapPoint> MapPoints { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region CTOR
		public BopGroup(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, string sDcsGroupType, ElementDcsObjectClass objectClass, MizGroup mizGroup) : base(miz, theatre)
		{
			CoalitionName = sCoalitionName;
			CountryName = sCountryName;
			DcsGroupType = sDcsGroupType;
			ObjectClass = objectClass;
			m_mizGroup = mizGroup;
			InitializeMizBopCustom();

			Attributes = ElementDcsObjectAttribute.None;

			Id = m_mizGroup.Id;
			Name = m_mizGroup.Name;
			LateActivation = m_mizGroup.LateActivation;

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

			FromMizUnits();

			Playable = m_mizGroup.Units.Where(_u => _u.Skill == ElementSkill.Player || _u.Skill == ElementSkill.Client).Any();
			ObjectClass = MainUnit?.ObjectClass ?? ObjectClass;
			Attributes = Units.Aggregate<BopUnit, ElementDcsObjectAttribute>(0, (currentAttributes, _bopUnit) => currentAttributes | _bopUnit.Attributes);
			Type = string.Join(",", Units.GroupBy(_u => _u.Type).Select(_g => _g.Key));
			MapMarker = m_mizBopGroup?.MapMarker ?? MainUnit.MapMarker;
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

			if (MapMarker != MainUnit.MapMarker)
				m_mizBopGroup.MapMarker = MapMarker;
		}

		protected virtual void FromMizUnits()
		{
			Units = new List<BopUnit>();
			foreach (MizUnit mizUnit in m_mizGroup.Units)
			{
				BopUnit bopUnit = new BopUnit(Miz, Theatre, this, mizUnit);
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

			Coordinate = Theatre.GetCoordinate(m_mizGroup.Y, m_mizGroup.X);

			foreach (BopUnit bopUnit in Units)
			{
				bopUnit.FinalizeFromMiz();
			}
			foreach (BopMapPoint bopMapPoint in MapPoints)
			{
				bopMapPoint.FinalizeFromMiz();
			}

			AltitudeFeet = MapPoints.OfType<BopRoutePoint>().FirstOrDefault()?.AltitudeFeet ?? MainUnit.AltitudeFeet;
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopGroup = Miz.MizBopCustom.MizBopGroups.Where(_u => _u.Id == m_mizGroup.Id).FirstOrDefault();
			if (m_mizBopGroup is null)
			{
				m_mizBopGroup = new MizBopGroup() { Id = m_mizGroup.Id };
				m_mizBopGroup.SetDefaultData();
				Miz.MizBopCustom.MizBopGroups.Add(m_mizBopGroup);
			}
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return ToStringDisplayName();
		}

		public virtual string ToStringDisplayName()
		{
			return Name;
		}

		public virtual string ToStringAdditionnal()
		{
			return "";
		}

		public virtual string ToStringCoordinate()
		{
			return Coordinate.ToStringLocalisation(Miz.MizBopCustom.PreferencesMission.CoordinateDisplay);

		}

		public BopRouteTask GetRouteTask(IEnumerable<string> sTaskIds, int? iUnitId)
		{
			BopRouteTask routeTask = null;
			foreach (BopRoutePoint routePoint in MapPoints.OfType<BopRoutePoint>())
			{
				routeTask = routePoint.GetRouteTask(sTaskIds, iUnitId);
				if (routeTask is object)
					break;
			}

			return routeTask;
		}

		public Tacan GetTacanFromRouteTask(int? iUnitId)
		{
			BopRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTaskAction.ActivateBeacon }, iUnitId);
			return (routeTask as BopRouteTaskBeacon)?.Tacan;
		}

		public GMarkerBriefop GetMarkerBriefop(Color? color)
		{
			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), MapMarker, color ?? ToolsBriefop.GetCoalitionColor(CoalitionName), ToStringDisplayName(), 1, 0);
		}

		public GMapOverlay GetMapOverlayPosition()
		{
			GMapOverlay mapOverlay = new GMapOverlay();
			mapOverlay.Markers.Add(GetMarkerBriefop(null));
			return mapOverlay;
		}

		public GMapOverlay GetMapOverlayUnits(int? iIdSelectedUnit)
		{
			Color color = ToolsBriefop.GetCoalitionColor(CoalitionName);
			GMapOverlay mapOverlay = new GMapOverlay();

			BopUnit selectedUnit = Units.Where(_u => _u.Id == iIdSelectedUnit.GetValueOrDefault(0)).FirstOrDefault();
			if (selectedUnit is object)
				color = ToolsImage.Lerp(color, Color.White, 0.5f);

			foreach (BopUnit bopUnit in Units.Where(_u => _u != selectedUnit))
			{
				GMarkerBriefop marker = bopUnit.GetMarkerBriefop(color);
				mapOverlay.Markers.Add(marker);
			}
			
			if (selectedUnit is object)
			{
				GMarkerBriefop marker = selectedUnit.GetMarkerBriefop(null);
				mapOverlay.Markers.Add(marker);
			}

			return mapOverlay;
		}

		//public GRouteBriefop GetGMapRouteOrbit(GMapOverlay staticOverlay)
		//{
		//	List<PointLatLng> points = new List<PointLatLng>();
		//	bool bDone = false;
		//	int iCount = 0;
		//	foreach (AssetMapPoint mapPoint in MapPoints)
		//	{
		//		iCount++;

		//		if (bDone)
		//			break;
		//		else if (points.Count <= 0 && mapPoint.GetOrbitPattern() is string sPattern)
		//		{
		//			PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
		//			GMarkerBriefop marker;
		//			if (sPattern == "Circle" || iCount == MapPoints.Count)
		//			{
		//				marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Circle, Color, Description, 2, 0);
		//				bDone = true;
		//			}
		//			else
		//			{
		//				marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
		//			}

		//			staticOverlay.Markers.Add(marker);
		//			points.Add(p);
		//		}
		//		else if (points.Count == 1)
		//		{
		//			PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
		//			GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, Color, null, 1, 0);
		//			staticOverlay.Markers.Add(marker);
		//			points.Add(p);
		//			bDone = true;
		//		}
		//	}

		//	if (points.Count > 1)
		//	{
		//		GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, Description, Description, ElementMapTemplateRoute.DashDot, Color, 2);
		//		staticOverlay.Routes.Add(route);
		//	}

		//	return points;
		//}

		public GMapOverlay GetMapOverlayRoute(int? iSelectedPointNumber, ElementMapOverlayRouteDisplay options)
		{
			GMapOverlay mapOverlay = new GMapOverlay();
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (BopRoutePoint bopRoutePoint in MapPoints.OfType<BopRoutePoint>())//.Where(_bopMapPoint => _bopMapPoint.Name != m_sBullsPointName))
			{
				PointLatLng p = new PointLatLng(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
				points.Add(p);

				if (bopRoutePoint.Number > 0
					|| iSelectedPointNumber.GetValueOrDefault(0) == bopRoutePoint.Number
					|| (options & ElementMapOverlayRouteDisplay.NoMarkerFirstPoint) == 0)
				{
					Color color = ToolsBriefop.GetCoalitionColor(CoalitionName);
					if (iSelectedPointNumber is object && iSelectedPointNumber.Value != bopRoutePoint.Number)
						color = ToolsImage.Lerp(color, Color.White, 0.5f);

					GMarkerBriefop markerBriefop = bopRoutePoint.GetMarkerBriefop(color, options);
					mapOverlay.Markers.Add(markerBriefop);
				}
			}

			string sRouteLabel = "";
			if ((options & ElementMapOverlayRouteDisplay.RouteLabel) != 0)
				sRouteLabel = ToStringDisplayName();

			GRouteBriefop route = GRouteBriefop.NewFromTemplateName(points, sRouteLabel, ElementMapTemplateRoute.DashDot, ToolsBriefop.GetCoalitionColor(CoalitionName), 2);
			mapOverlay.Routes.Add(route);

			return mapOverlay;
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
