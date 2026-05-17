using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;
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
			StringBuilder sb = new(Coordinate.ToString(coordinateDisplay));
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
				if (routeTask is not null)
					break;
			}

			return routeTask;
		}

		public Tacan GetTacanFromRouteTask(int? iUnitId)
		{
			BopRouteTask routeTask = GetRouteTask([ElementRouteTaskAction.ActivateBeacon], iUnitId);
			return (routeTask as BopRouteTaskBeacon)?.Tacan;
		}

		public BriefopMarker GetBriefopMarker(Color? color)
		{
			GeoPoint pos = new(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree);
			return BriefopMarker.NewFromTemplateName(pos, MapMarker, color ?? ToolsBriefop.GetCoalitionColor(CoalitionName), ToStringDisplayName(), null, 0f, 1, 0);
		}

		public MemoryLayer GetMapLayer()
		{
			BopRoutePoint orbitPoint = RoutePoints.FirstOrDefault(_rp => _rp.Tasks.OfType<BopRouteTaskOrbit>().Any());
			if (orbitPoint is not null)
				return GetOrbitMapLayer();
			else
				return GetPositionMapLayer();
		}

		public MemoryLayer GetPositionMapLayer()
		{
			BriefopMarker marker = GetBriefopMarker(null);
			PointFeature mapFeature = new(MapProjection.ToMPoint(marker.Position));
			mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
			return new MemoryLayer { Style = null, Features = [mapFeature] };
		}

		public MemoryLayer GetUnitsMapLayer(int? iIdSelectedUnit)
		{
			Color color = ToolsBriefop.GetCoalitionColor(CoalitionName);
			List<IFeature> mapFeatures = [];

			BopUnit selectedUnit = Units.Where(_u => _u.Id == iIdSelectedUnit.GetValueOrDefault(0)).FirstOrDefault();
			if (selectedUnit is not null)
				color = ToolsImage.Lerp(color, Color.White, 0.5f);

			foreach (BopUnit bopUnit in Units.Where(_u => _u != selectedUnit))
			{
				BriefopMarker marker = bopUnit.GetBriefopMarker(color);
				PointFeature mapFeature = new(MapProjection.ToMPoint(marker.Position));
				mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
				mapFeatures.Add(mapFeature);
			}

			if (selectedUnit is not null)
			{
				BriefopMarker marker = selectedUnit.GetBriefopMarker(null);
				PointFeature mapFeature = new(MapProjection.ToMPoint(marker.Position));
				mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
				mapFeatures.Add(mapFeature);
			}

			return new MemoryLayer { Style = null, Features = mapFeatures };
		}

		public MemoryLayer GetOrbitMapLayer()
		{
			List<IFeature> mapFeatures = [];
			List<GeoPoint> points = [];

			bool bDone = false;
			int iCount = 0;
			foreach (BopRoutePoint bopRoutePoint in RoutePoints.Where(_bopRoutePoint => _bopRoutePoint.Name != ElementGlobalData.BullseyeRoutePointName))
			{
				iCount++;

				if (bDone)
					break;
				else if (points.Count <= 0 && bopRoutePoint.Tasks.OfType<BopRouteTaskOrbit>().FirstOrDefault() is BopRouteTaskOrbit bopRouteTaskOrbit)
				{
					GeoPoint pos = new(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					BriefopMarker marker;
					if (bopRouteTaskOrbit.Pattern == "Circle" || iCount == RoutePoints.Count)
					{
						marker = BriefopMarker.NewFromTemplateName(pos, ElementMapTemplateMarker.Circle, ToolsBriefop.GetCoalitionColor(CoalitionName), ToStringDisplayName(), null, 0f, 2, 0);
						bDone = true;
					}
					else
					{
						marker = BriefopMarker.NewFromTemplateName(pos, ElementMapTemplateMarker.Waypoint, ToolsBriefop.GetCoalitionColor(CoalitionName), null, null, 0f, 1, 0);
					}
					PointFeature mapFeature = new(MapProjection.ToMPoint(pos));
					mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
					mapFeatures.Add(mapFeature);
					points.Add(pos);
				}
				else if (points.Count == 1)
				{
					GeoPoint pos = new(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					BriefopMarker marker = BriefopMarker.NewFromTemplateName(pos, ElementMapTemplateMarker.Waypoint, ToolsBriefop.GetCoalitionColor(CoalitionName), null, null, 0f, 1, 0);
					PointFeature feature = new(MapProjection.ToMPoint(pos));
					feature.Styles.Add(new BriefopMarkerStyle(marker));
					mapFeatures.Add(feature);
					points.Add(pos);
					bDone = true;
				}
			}

			if (points.Count > 1)
			{
				Color colorText = ToolsBriefop.GetCoalitionColor(CoalitionName);
				Color colorLine = Color.FromArgb(70, colorText);
				BriefopLine line = BriefopLine.NewLineFromTemplateName(points, ElementMapTemplateLine.DashLine, colorLine, 3, colorText, ToStringDisplayName());
				Mapsui.Nts.GeometryFeature lineFeature = line.ToGeometryFeature();
				if (lineFeature is not null)
					mapFeatures.Add(lineFeature);
			}

			return new MemoryLayer { Style = null, Features = mapFeatures };
		}

		public MemoryLayer GetRouteMapLayer(int? iSelectedPointNumber, ElementMapOverlayRouteDisplay options, ElementMeasurementSystem measurementSystem)
		{
			List<IFeature> mapFeatures = [];
			List<GeoPoint> points = [];
			List<string> segmentLabels = [];

			foreach (BopRoutePoint bopRoutePoint in RoutePoints)
			{
				if (bopRoutePoint.Name != ElementGlobalData.BullseyeRoutePointName)
				{
					GeoPoint pos = new(bopRoutePoint.Coordinate.Latitude.DecimalDegree, bopRoutePoint.Coordinate.Longitude.DecimalDegree);
					points.Add(pos);
					segmentLabels.Add($"{bopRoutePoint.GetTrack(true):000}°/{bopRoutePoint.GetDistance(measurementSystem):0}{ToolsMeasurement.DistanceUnit(measurementSystem)}");
				}

				if (bopRoutePoint.Number > 0
					|| iSelectedPointNumber.GetValueOrDefault(0) == bopRoutePoint.Number
					|| (options & ElementMapOverlayRouteDisplay.NoMarkerFirstPoint) == 0)
				{
					bool? bIsSelected = null;
					if (iSelectedPointNumber is not null)
						bIsSelected = iSelectedPointNumber.Value == bopRoutePoint.Number;

					BriefopMarker briefopMarker = bopRoutePoint.GetBriefopMarker(ToolsBriefop.GetCoalitionColor(CoalitionName), bIsSelected, options);
					PointFeature mapFeature = new(MapProjection.ToMPoint(briefopMarker.Position));
					mapFeature.Styles.Add(new BriefopMarkerStyle(briefopMarker));
					mapFeatures.Add(mapFeature);
				}
			}

			Color colorText = ToolsBriefop.GetCoalitionColor(CoalitionName);
			Color colorLine = Color.FromArgb(70, colorText);
			BriefopLine route = BriefopLine.NewRouteFromTemplateName(points, ElementMapTemplateLine.DashLine, colorLine, 5, colorText, segmentLabels);
			Mapsui.Nts.GeometryFeature routeFeature = route.ToGeometryFeature();
			if (routeFeature is not null)
				mapFeatures.Add(routeFeature);

			return new MemoryLayer { Style = null, Features = mapFeatures };
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
