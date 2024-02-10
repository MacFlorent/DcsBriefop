using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using System.Text;
using static DcsBriefop.Tools.ToolsSpeeds;
using UnitsNet;

namespace DcsBriefop.DataBopMission
{
	internal class BopRoutePoint : BaseBop
	{
		#region Fields
		private MizRoutePoint m_mizRoutePoint;
		private BopGroup m_bopGroup;
		private Distance m_distance;
		private CoordinateSharp.Magnetic.Magnetic m_magnetic;
		#endregion

		#region Properties
		public int GroupId { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Notes { get; set; }
		public string Action { get; set; }
		public double AltitudeMeters { get; set; }
		public double? AltitudeCustomMeters { get; set; }
		public double SpeedMs { get; set; }
		public int DistanceMeters { get { return (int)m_distance.Meters; } }
		public Coordinate Coordinate { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		public List<BopRouteTask> Tasks { get; set; }
		public MizRoutePoint MizRoutePoint { get { return m_mizRoutePoint; } }
		#endregion

		#region CTOR
		public BopRoutePoint(Miz miz, Theatre theatre, int iGroupId, int iNumber, MizRoutePoint mizRoutePoint, BopGroup bopGroup) : base(miz, theatre)
		{
			GroupId = iGroupId;
			Number = iNumber;
			m_mizRoutePoint = mizRoutePoint;
			m_bopGroup = bopGroup;

			Name = m_mizRoutePoint.Name;
			Action = m_mizRoutePoint.Action;
			AltitudeMeters = m_mizRoutePoint.Altitude;
			SpeedMs = m_mizRoutePoint.Speed;
			Type = m_mizRoutePoint.Type;
			AirdromeId = m_mizRoutePoint.AirdromeId;
			HelipadId = m_mizRoutePoint.HelipadId;

			Tasks = new List<BopRouteTask>();
			if (m_mizRoutePoint.RouteTaskHolder is not null)
			{
				foreach (MizRouteTask mizRouteTask in m_mizRoutePoint.RouteTaskHolder.Tasks)
				{
					if (mizRouteTask.Id == ElementRouteTask.Fac || mizRouteTask.Id == ElementRouteTask.FacEngageGroup || mizRouteTask.Id == ElementRouteTask.FacAttackGroup)
						Tasks.Add(new BopRouteTaskFac(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Id == ElementRouteTask.Orbit)
						Tasks.Add(new BopRouteTaskOrbit(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Params?.Task?.Id == ElementRouteTask.Orbit)
						Tasks.Add(new BopRouteTaskOrbit(Miz, Theatre, mizRouteTask.Params.Task));
					else if (mizRouteTask.Params?.Action?.Id == ElementRouteTaskAction.SetCallsign)
						Tasks.Add(new BopRouteTaskCallsign(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Params?.Action?.Id == ElementRouteTaskAction.SetFrequency)
						Tasks.Add(new BopRouteTaskRadio(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Params?.Action?.Id == ElementRouteTaskAction.ActivateBeacon)
						Tasks.Add(new BopRouteTaskBeacon(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Params?.Action?.Id == ElementRouteTaskAction.ActivateIcls)
						Tasks.Add(new BopRouteTaskIcls(Miz, Theatre, mizRouteTask));
					else if (mizRouteTask.Params?.Action?.Id == ElementRouteTaskAction.ActivateLink4)
						Tasks.Add(new BopRouteTaskLink4(Miz, Theatre, mizRouteTask));
				}
			}

			MizBopRoutePoint mizBopRoutePoint = Miz.MizBopCustom.MizBopRoutePoints.Where(_rp => _rp.GroupId == GroupId && _rp.Number == Number).FirstOrDefault();
			AltitudeCustomMeters = mizBopRoutePoint?.AltitudeMeters;
			Notes = mizBopRoutePoint?.Notes;
			m_bopGroup = bopGroup;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizRoutePoint.Name = Name;
			m_mizRoutePoint.Action = Action;
			m_mizRoutePoint.Type = Type;

			ToMizBopCustom();
		}

		private void ToMizBopCustom()
		{
			MizBopRoutePoint mizBopRoutePoint = Miz.MizBopCustom.MizBopRoutePoints.Where(_rp => _rp.GroupId == GroupId && _rp.Number == Number).FirstOrDefault();
			if (mizBopRoutePoint is null)
			{
				mizBopRoutePoint = new MizBopRoutePoint() { GroupId = GroupId, Number = Number };
				Miz.MizBopCustom.MizBopRoutePoints.Add(mizBopRoutePoint);
			}

			mizBopRoutePoint.Notes = null;
			mizBopRoutePoint.AltitudeMeters = null;
			bool bMizBopCustomModified = false;

			if (!string.IsNullOrEmpty(Notes))
			{
				mizBopRoutePoint.Notes = Notes;
				bMizBopCustomModified = true;
			}
			if (AltitudeCustomMeters is not null && AltitudeCustomMeters.Value != AltitudeMeters)
			{
				mizBopRoutePoint.AltitudeMeters = AltitudeCustomMeters;
				bMizBopCustomModified = true;
			}

			if (!bMizBopCustomModified)
				Miz.MizBopCustom.MizBopRoutePoints.Remove(mizBopRoutePoint);
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Coordinate = Theatre.GetCoordinate(m_mizRoutePoint.X, m_mizRoutePoint.Y);
			m_magnetic = new CoordinateSharp.Magnetic.Magnetic(Coordinate, CoordinateSharp.Magnetic.DataModel.WMM2020);

			if (Number > 0 && Name != ElementGlobalData.BullseyeRoutePointName)
			{
				BopRoutePoint rpPrevious = null;
				bool bFound = false;
				foreach (BopRoutePoint rp in m_bopGroup.RoutePoints.Where(_rp => _rp.Name != ElementGlobalData.BullseyeRoutePointName))
				{
					if (rp == this)
					{
						bFound = true;
						break;
					}
					else
					{
						rpPrevious = rp;
					}
				}

				if (bFound && rpPrevious is not null)
				{
					rpPrevious.FinalizeFromMiz();
					m_distance = rpPrevious.Coordinate.Get_Distance_From_Coordinate(Coordinate);
				}
			}

			foreach (BopRouteTask task in Tasks)
				task.FinalizeFromMiz();
		}
		#endregion

		#region Methods
		public string ToStringDisplayName()
		{
			return $"{Number}:{Name}";
		}

		public string ToStringAdditional(ElementMeasurementSystem measurementSystem)
		{
			StringBuilder sb = new StringBuilder();

			//if (AltitudeCustomMeters is not null)
			//{
			//	sb.Append($"Ground alt:{ToolsMeasurement.AltitudeDisplay(AltitudeMeters, measurementSystem):0} {ToolsMeasurement.AltitudeUnit(measurementSystem)}");
			//}

			Airdrome airdrome = Theatre.GetAirdrome(AirdromeId.GetValueOrDefault(0));
			if (airdrome is not null)
				sb.AppendWithSeparator($"Airdrome:{airdrome.Name}", " ");
			if (HelipadId is not null)
				sb.AppendWithSeparator($"Helipad:{HelipadId}", " ");

			foreach (BopRouteTask task in Tasks)
			{
				sb.AppendWithSeparator($"{task.ToStringDisplayName()}:{task.ToStringAdditional(measurementSystem)}", Environment.NewLine);
			}

			return sb.ToString();
		}

		public double? GetAltitude(ElementMeasurementSystem measurementSystem)
		{
			double altitudeMeters = AltitudeCustomMeters ?? AltitudeMeters;
			return ToolsMeasurement.AltitudeDisplay(altitudeMeters, measurementSystem);
		}

		private ComputedSpeeds GetSpeeds()
		{
			double altitudeMeters = AltitudeCustomMeters ?? AltitudeMeters;
			double dSpeedKnots = UnitConverter.Convert(SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot);
			return ToolsSpeeds.ConvertTrueAirSpeed(dSpeedKnots, altitudeMeters);
		}

		public double? GetSpeedTrue(ElementMeasurementSystem measurementSystem)
		{
			return ToolsMeasurement.SpeedDisplay(SpeedMs, measurementSystem);
		}

		public double? GetSpeedCalibrated(ElementMeasurementSystem measurementSystem)
		{
			ComputedSpeeds computedSpeeds = GetSpeeds();
			double dSpeedMs = computedSpeeds.IAS_ms;
			return ToolsMeasurement.SpeedDisplay(dSpeedMs, measurementSystem);
		}

		public double? GetSpeedMach()
		{
			ComputedSpeeds computedSpeeds = GetSpeeds();
			return computedSpeeds.Mach;
		}

		public int? GetDistance(ElementMeasurementSystem measurementSystem)
		{
			FinalizeFromMiz();
			if (m_distance is null)
				return null;

			return ToolsMeasurement.DistanceDisplay((int)m_distance.Meters, measurementSystem);
		}

		public double? GetTrack(bool bMagnetic)
		{
			FinalizeFromMiz();
			if (m_distance is null)
				return null;

			double dTrack = m_distance.Bearing;
			if (bMagnetic)
				dTrack += m_magnetic.MagneticFieldElements.Declination;
			
			return ToolsCoordinate.NormalizeBearing(dTrack);
		}

		public IEnumerable<BopRouteTask> GetTasks(IEnumerable<string> sTaskIds, int? iUnitId)
		{
			return Tasks.Where(_t => _t.Enabled && sTaskIds.Contains(_t.Id) && _t.UnitId.GetValueOrDefault(0) == iUnitId.GetValueOrDefault(0));
		}

		public BopRouteTask GetRouteTask(IEnumerable<string> sTaskIds, int? iUnitId)
		{
			return GetTasks(sTaskIds, iUnitId)?.OrderBy(_t => _t.Number).FirstOrDefault();
		}

		public GMarkerBriefop GetMarkerBriefop(Color color, ElementMapOverlayRouteDisplay options)
		{
			string sLabel = "";
			if ((options & ElementMapOverlayRouteDisplay.PointLabelFull) != 0)
				sLabel = ToStringDisplayName();
			else if ((options & ElementMapOverlayRouteDisplay.PointLabelLight) != 0)
				sLabel = $"{Number}";

			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), ElementMapTemplateMarker.Waypoint, color, sLabel, 1, 0);
		}

		public void SetYX(double dY, double dX)
		{ // only used for Bullseye for now, so no need to recompute distances
			m_mizRoutePoint.Y = dY;
			m_mizRoutePoint.X = dX;
			Coordinate = Theatre.GetCoordinate(m_mizRoutePoint.X, m_mizRoutePoint.Y);
		}
		#endregion
	}

	//internal class BopRouteSegment
	//{
	//	#region Fields
	//	private Coordinate m_coordinateFrom;
	//	private Coordinate m_coordinateTo;
	//	private Distance m_distance;
	//	private Magnetic m_magnetic;
	//	#endregion

	//	#region Properties
	//	public decimal TrackTrue { get { return ToolsCoordinate.NormalizeBearing((decimal)m_distance.Bearing); }	}
	//	public decimal TrackMagnetic { get { return ToolsCoordinate.NormalizeBearing((decimal)(m_distance.Bearing + m_magnetic.MagneticFieldElements.Declination)); }	}
	//	public decimal SpeedMs { get; set; }
	//	public decimal AltitudeMeters { get; set; }
	//	public int DistanceMeters { get { return (int)m_distance.Meters; } }
	//	#endregion

	//	#region CTOR
	//	public BopRouteSegment(BopRoutePoint rpFrom, BopRoutePoint rpTo)
	//	{
	//		m_coordinateFrom = rpFrom.Coordinate;
	//		m_coordinateTo = rpTo.Coordinate;
	//		m_distance = m_coordinateFrom.Get_Distance_From_Coordinate(m_coordinateTo);
	//		m_magnetic = new Magnetic(m_coordinateFrom, DataModel.WMM2020);
	//		AltitudeMeters = rpTo.AltitudeCustomMeters ?? rpTo.AltitudeMeters;
	//		SpeedMs = rpTo.SpeedMs;
	//	}
	//	#endregion

	//	#region Methods
	//	public override string ToString()
	//	{
	//		StringBuilder sb = new();
	//		sb.AppendWithSeparator($"{TrackTrue:000}°T {TrackMagnetic:000}°M", " - ");
	//		sb.AppendWithSeparator($"{ToolsMeasurement.DistanceDisplay(DistanceMeters, PreferencesManager.Preferences.Briefing.MeasurementSystem):0}{ ToolsMeasurement.DistanceUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)}", " - ");
	//		sb.AppendWithSeparator($"{GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}{ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)}", " - ");
	//		sb.AppendWithSeparator($"{GetSpeed(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}{ToolsMeasurement.SpeedUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)}", " - ");

	//		return sb.ToString();
	//	}

	//	public decimal? GetAltitude(ElementMeasurementSystem measurementSystem)
	//	{
	//		return ToolsMeasurement.AltitudeDisplay(AltitudeMeters, measurementSystem);
	//	}

	//	public decimal? GetSpeed(ElementMeasurementSystem measurementSystem)
	//	{
	//		return ToolsMeasurement.SpeedDisplay(SpeedMs, measurementSystem);
	//	}
	//	#endregion

	//}
}