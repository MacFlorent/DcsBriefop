﻿using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroup : BaseBop, IEquatable<BopGroup>
	{
		#region Fields
		protected MizGroup m_mizGroup;
		#endregion

		#region Properties
		public string DcsGroupType { get; protected set; }
		public ElementGroupClass GroupClass { get; protected set; }
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
		public List<BopRoutePoint> RoutePoints { get; set; }
		public double? AltitudeMeters { get; set; }
		public Coordinate Coordinate { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region CTOR
		public BopGroup(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, string sDcsGroupType, ElementGroupClass groupClass, MizGroup mizGroup) : base(miz, theatre)
		{
			CoalitionName = sCoalitionName;
			CountryName = sCountryName;
			DcsGroupType = sDcsGroupType;
			GroupClass = groupClass;
			m_mizGroup = mizGroup;

			Attributes = ElementDcsObjectAttribute.None;

			Id = m_mizGroup.Id;
			Name = m_mizGroup.Name;
			LateActivation = m_mizGroup.LateActivation;

			RoutePoints = new List<BopRoutePoint>();
			if (m_mizGroup.RoutePoints is not null && m_mizGroup.RoutePoints.Count > 0)
			{
				int iNumber = 0;
				foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
				{
					RoutePoints.Add(new BopRoutePoint(Miz, Theatre, Id, iNumber, mizRoutePoint, this));
					iNumber++;
				}
			}

			FromMizUnits();

			Playable = m_mizGroup.Units.Where(_u => _u.Skill == ElementSkill.Player || _u.Skill == ElementSkill.Client).Any();
			GroupClass = MainUnit?.GroupClass ?? GroupClass;
			Attributes = Units.Aggregate<BopUnit, ElementDcsObjectAttribute>(0, (currentAttributes, _bopUnit) => currentAttributes | _bopUnit.Attributes);
			Type = string.Join(",", Units.GroupBy(_u => _u.Type).Select(_g => _g.Key));

			MizBopGroup mizBopGroup = Miz.MizBopCustom.MizBopGroups.Where(_u => _u.Id == m_mizGroup.Id).FirstOrDefault();
			MapMarker = mizBopGroup?.MapMarker ?? MainUnit.MapMarker;
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

			m_mizGroup.RoutePoints.Clear();
			foreach (BopRoutePoint bopRoutePoint in RoutePoints)
			{
				bopRoutePoint.ToMiz();
				m_mizGroup.RoutePoints.Add(bopRoutePoint.MizRoutePoint);
			}

			ToMizBopCustom();
		}

		protected void ToMizBopCustom()
		{
			MizBopGroup mizBopGroup = Miz.MizBopCustom.MizBopGroups.Where(_u => _u.Id == m_mizGroup.Id).FirstOrDefault();
			if (mizBopGroup is null)
			{
				mizBopGroup = new MizBopGroup() { Id = m_mizGroup.Id };
				Miz.MizBopCustom.MizBopGroups.Add(mizBopGroup);
			}

			mizBopGroup.MapMarker = null;
			bool bMizBopCustomModified = false;

			if (MapMarker != MainUnit.MapMarker)
			{
				mizBopGroup.MapMarker = MapMarker;
				bMizBopCustomModified = true;
			}

			if (!bMizBopCustomModified)
				Miz.MizBopCustom.MizBopGroups.Remove(mizBopGroup);
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

			AltitudeMeters = RoutePoints.FirstOrDefault()?.AltitudeMeters ?? MainUnit.AltitudeMeters;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Coordinate = Theatre.GetCoordinate(m_mizGroup.X, m_mizGroup.Y);

			foreach (BopUnit bopUnit in Units)
			{
				bopUnit.FinalizeFromMiz();
			}
			foreach (BopRoutePoint bopRoutePoint in RoutePoints)
			{
				bopRoutePoint.FinalizeFromMiz();
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

		public virtual string ToStringAdditional()
		{
			return "";
		}

		public virtual string ToStringLocalisation(ElementCoordinateDisplay coordinateDisplay, ElementMeasurementSystem? measurementSystem)
		{
			StringBuilder sb = new StringBuilder(Coordinate.ToString(coordinateDisplay));
			if (measurementSystem is not null && GroupClass == ElementGroupClass.Ground)
			{
				sb.AppendWithSeparator($"{GetAltitude(measurementSystem.Value):0}{ToolsMeasurement.AltitudeUnit(measurementSystem.Value)}", Environment.NewLine);
			}

			return sb.ToString();
		}

		public double? GetAltitude(ElementMeasurementSystem measurementSystem)
		{
			if (AltitudeMeters is null)
				return null;
			else
				return ToolsMeasurement.AltitudeDisplay(AltitudeMeters.Value, measurementSystem);
		}

		public BopRouteTask GetRouteTask(IEnumerable<string> sTaskIds, int? iUnitId)
		{
			BopRouteTask routeTask = null;
			foreach (BopRoutePoint routePoint in RoutePoints)
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

		public GMapOverlay GetMapOverlay()
		{
			BopRoutePoint orbitPoint = RoutePoints.Where(_rp => _rp.Tasks.OfType<BopRouteTaskOrbit>().Any()).FirstOrDefault();
			if (orbitPoint is not null)
				return GetMapOverlayOrbit();
			else
				return GetMapOverlayPosition();
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

		public GMapOverlay GetMapOverlayOrbit()
		{
			GMapOverlay mapOverlay = new GMapOverlay();
			List<PointLatLng> points = new List<PointLatLng>();

			bool bDone = false;
			int iCount = 0;
			foreach (BopRoutePoint bopRoutePoint in RoutePoints.Where(_bopRoutePoint => _bopRoutePoint.Name != ElementGlobalData.BullseyeRoutePointName))
			{
				iCount++;

				if (bDone)
					break;
				else if (points.Count <= 0 && bopRoutePoint.Tasks.OfType<BopRouteTaskOrbit>().FirstOrDefault() is BopRouteTaskOrbit bopRouteTaskOrbit)
				{
					PointLatLng p = new PointLatLng(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker;
					if (bopRouteTaskOrbit.Pattern == "Circle" || iCount == RoutePoints.Count)
					{
						marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Circle, ToolsBriefop.GetCoalitionColor(CoalitionName), ToStringDisplayName(), 2, 0);
						bDone = true;
					}
					else
					{
						marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, ToolsBriefop.GetCoalitionColor(CoalitionName), null, 1, 0);
					}

					mapOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Waypoint, ToolsBriefop.GetCoalitionColor(CoalitionName), null, 1, 0);
					mapOverlay.Markers.Add(marker);
					points.Add(p);
					bDone = true;
				}
			}

			if (points.Count > 1)
			{
				Color colorText = ToolsBriefop.GetCoalitionColor(CoalitionName);
				Color colorLine = Color.FromArgb(70, colorText);

				GLineBriefop route = GLineBriefop.NewLineFromTemplateName(points, ToStringDisplayName(), ElementMapTemplateLine.DashLine, colorLine, 3, colorText, ToStringDisplayName());
				mapOverlay.Routes.Add(route);
			}

			return mapOverlay;
		}

		public GMapOverlay GetMapOverlayRoute(int? iSelectedPointNumber, ElementMapOverlayRouteDisplay options, ElementMeasurementSystem measurementSystem)
		{
			GMapOverlay mapOverlay = new();
			List<PointLatLng> points = new();
			List<string> segmentLabels = new();

			foreach (BopRoutePoint bopRoutePoint in RoutePoints)
			{
				if (bopRoutePoint.Name != ElementGlobalData.BullseyeRoutePointName)
				{
					PointLatLng p = new PointLatLng(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					points.Add(p);
					segmentLabels.Add($"{bopRoutePoint.GetTrack(true):000}°/{bopRoutePoint.GetDistance(measurementSystem):0}{ToolsMeasurement.DistanceUnit(measurementSystem)}");
				}

				if (bopRoutePoint.Number > 0
					|| iSelectedPointNumber.GetValueOrDefault(0) == bopRoutePoint.Number
					|| (options & ElementMapOverlayRouteDisplay.NoMarkerFirstPoint) == 0)
				{
					Color colorPoint = ToolsBriefop.GetCoalitionColor(CoalitionName);
					if (iSelectedPointNumber is object && iSelectedPointNumber.Value != bopRoutePoint.Number)
						colorPoint = ToolsImage.Lerp(colorPoint, Color.White, 0.5f);

					GMarkerBriefop markerBriefop = bopRoutePoint.GetMarkerBriefop(colorPoint, options);
					mapOverlay.Markers.Add(markerBriefop);
				}
			}

			Color colorText = ToolsBriefop.GetCoalitionColor(CoalitionName);
			Color colorLine = Color.FromArgb(70, colorText);

			GLineBriefop route = GLineBriefop.NewRouteFromTemplateName(points, null, ElementMapTemplateLine.DashLine, colorLine, 5, colorText, segmentLabels);
			mapOverlay.Routes.Add(route);

			return mapOverlay;
		}
		#endregion

		#region IEquatable
		public bool Equals(BopGroup other)
		{
			if (other is null)
				return false;

			return (Id == other.Id);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BopGroup);
		}

		public override int GetHashCode() => Id.GetHashCode();
		#endregion
	}
}
