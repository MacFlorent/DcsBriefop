using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupVehicle : BopGroup
	{
		#region Fields
		#endregion

		#region Properties
		public BopCallsign Callsign { get; set; }
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopGroupVehicle(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, ElementDcsGroupType.Vehicle, ElementDcsObjectClass.Ground, mizGroup)
		{
			BopRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup }, null);
			if (routeTask is BopRouteTaskFac routeTaskFac)
			{
				Callsign = routeTaskFac.Callsign;
				Radio = routeTaskFac.Radio;
			}

			if (Callsign is null)
			{
				routeTask = GetRouteTask(new List<string> { ElementRouteTaskAction.SetCallsign }, null);
				Callsign = (routeTask as BopRouteTaskCallsign)?.Callsign;
			}
			if (Radio is null)
			{
				routeTask = GetRouteTask(new List<string> { ElementRouteTaskAction.SetFrequency }, null);
				Radio = (routeTask as BopRouteTaskRadio)?.Radio;
			}

			Tacan = GetTacanFromRouteTask(null);
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		public override string ToStringDisplayName()
		{
			string sDisplayName = base.ToStringDisplayName();

			if (Callsign is object)
				return $"{Callsign} | {sDisplayName}";
			else
				return sDisplayName;
		}

		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");

			return sb.ToString();
		}

		//private Radio GetRadioFromRouteTaskAction()
		//{
		//	MizRouteTaskAction routeTaskAction = GetRouteTaskAction(new List<string> { ElementRouteTaskAction.SetFrequency }, null);
		//	if (routeTaskAction is object)
		//		return new Radio(routeTaskAction.ParamFrequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, routeTaskAction.ParamModulation.GetValueOrDefault(ElementRadioModulation.AM));
		//	else
		//		return null;
		//}

		//private BopCallsign GetCallsignFromRouteTaskFac()
		//{
		//	int? iCallsignId = null, iCallsignNumber = null;
		//	MizRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });

		//	if (routeTask is object)
		//	{
		//		iCallsignId = routeTask.Params.Callname;
		//		iCallsignNumber = routeTask.Params.Number;
		//	}

		//	return BopCallsign.NewFromJtacId(iCallsignId, iCallsignNumber);
		//}

		//private Radio GetRadioFromRouteTaskFac()
		//{
		//	MizRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });
		//	if (routeTask is object)
		//		return new Radio(routeTask.Params.Frequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, routeTask.Params.Modulation.GetValueOrDefault(ElementRadioModulation.AM));
		//	else
		//		return null;
		//}
		#endregion
	}

}
