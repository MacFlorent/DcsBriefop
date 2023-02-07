using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UnitsNet.Units;

namespace DcsBriefop.DataBopMission
{
	internal class BopRoutePoint : BaseBop
	{
		#region Fields
		private MizRoutePoint m_mizRoutePoint;
		#endregion

		#region Properties
		public int Number { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public decimal AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		public List<BopRouteTask> Tasks { get; set; }
		#endregion

		#region CTOR
		public BopRoutePoint(Miz miz, Theatre theatre, int iNumber, MizRoutePoint mizRoutePoint) : base(miz, theatre)
		{
			Number = iNumber;
			m_mizRoutePoint = mizRoutePoint;

			Name = m_mizRoutePoint.Name;
			Action = m_mizRoutePoint.Action;
			Type = m_mizRoutePoint.Type;
			AirdromeId = m_mizRoutePoint.AirdromeId;
			HelipadId = m_mizRoutePoint.HelipadId;

			Tasks = new List<BopRouteTask>();
			if (m_mizRoutePoint.RouteTaskHolder is object)
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
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizRoutePoint.Name = Name;
			m_mizRoutePoint.Action = Action;
			m_mizRoutePoint.Type = Type;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			AltitudeFeet = (decimal)UnitsNet.UnitConverter.Convert(m_mizRoutePoint.Altitude, LengthUnit.Meter, LengthUnit.Foot);
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

		public virtual string ToStringCoordinate()
		{
			return Coordinate.ToString(Miz.MizBopCustom.PreferencesMission.CoordinateDisplay);
		}

		public string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder();

			Airdrome airdrome = Theatre.GetAirdrome(AirdromeId.GetValueOrDefault(0));
			if (airdrome is object)
				sb.AppendWithSeparator($"Airdrome:{airdrome.Name}", " ");
			if (HelipadId is object)
				sb.AppendWithSeparator($"Helipad:{HelipadId}", " ");

			foreach (BopRouteTask task in Tasks)
			{
				sb.AppendWithSeparator($"{task.ToStringDisplayName()}:{task.ToStringAdditionnal()}", Environment.NewLine);
			}

			return sb.ToString();
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
		//public string GetOrbitPattern()
		//{
		//	string sOrbitPattern = null;
		//	if (m_mizRoutePoint.RouteTaskHolder is object)
		//	{
		//		foreach (MizRouteTask mizTask in m_mizRoutePoint.RouteTaskHolder.Tasks)
		//		{
		//			if (mizTask.Id == ElementRouteTask.Orbit)
		//			{
		//				sOrbitPattern = mizTask.Params.Pattern;
		//				break;
		//			}
		//			else if (mizTask.Params.Task is object && mizTask.Params.Task.Id == ElementRouteTask.Orbit)
		//			{
		//				sOrbitPattern = mizTask.Params.Task.Params.Pattern;
		//				break;
		//			}
		//		}
		//	}

		//	return sOrbitPattern;
		//}
		#endregion
	}
}