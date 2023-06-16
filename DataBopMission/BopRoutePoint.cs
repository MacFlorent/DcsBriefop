using CoordinateSharp;
using CoordinateSharp.Magnetic;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopRoutePoint : BaseBop
	{
		#region Fields
		private MizRoutePoint m_mizRoutePoint;
		private BopGroup m_bopGroup;
		#endregion

		#region Properties
		public int GroupId { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Notes { get; set; }
		public string Action { get; set; }
		public decimal AltitudeMeters { get; set; }
		public decimal? AltitudeCustomMeters { get; set; }
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

			Coordinate = Theatre.GetCoordinate(m_mizRoutePoint.Y, m_mizRoutePoint.X);

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

		public decimal? GetAltitude(ElementMeasurementSystem measurementSystem)
		{
			decimal altitudeMeters = AltitudeCustomMeters ?? AltitudeMeters;
			return ToolsMeasurement.AltitudeDisplay(altitudeMeters, measurementSystem);
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

		public void SetYX(decimal dY, decimal dX)
		{
			m_mizRoutePoint.Y = dY;
			m_mizRoutePoint.X = dX;
			Coordinate = Theatre.GetCoordinate(m_mizRoutePoint.Y, m_mizRoutePoint.X);
		}

		public Distance GetRouteSegmentFromPrevious()
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
				return GetRouteSegment(rpPrevious, this);
			}
			else
			{
				return null;
			}
		}

		public static Distance GetRouteSegment(BopRoutePoint rpFrom, BopRoutePoint rpTo)
		{
			//GlobalSettings.Default_EagerLoad = new EagerLoad(EagerLoadType.UTM_MGRS);

			Coordinate cFrom = rpFrom.Coordinate;
			Coordinate cTo = rpTo.Coordinate;

			Magnetic m = new Magnetic(cFrom, DataModel.WMM2020);

			return cTo.Get_Distance_From_Coordinate(cFrom);
		}

		#endregion
	}
}