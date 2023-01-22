using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupVehicle : BopGroup
	{
		#region Fields
		private static Dictionary<int, string> m_callsigns;// TODO replace by a json resource
		#endregion

		#region Properties
		public string Callsign { get; set; }
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		static BopGroupVehicle()
		{
			m_callsigns = new Dictionary<int, string>();
			int i = 0;
			m_callsigns.Add(++i, "Axeman");
			m_callsigns.Add(++i, "Darknight");
			m_callsigns.Add(++i, "Warrior");
			m_callsigns.Add(++i, "Pointer");
			m_callsigns.Add(++i, "Eyeball");
			m_callsigns.Add(++i, "Moonbeam");
			m_callsigns.Add(++i, "Whiplash");
			m_callsigns.Add(++i, "Finger");
			m_callsigns.Add(++i, "Pinpoint");
			m_callsigns.Add(++i, "Ferret");
			m_callsigns.Add(++i, "Shaba");
			m_callsigns.Add(++i, "Playboy");
			m_callsigns.Add(++i, "Hammer");
			m_callsigns.Add(++i, "Jaguar");
			m_callsigns.Add(++i, "Deathstar");
			m_callsigns.Add(++i, "Anvil");
			m_callsigns.Add(++i, "Firefly");
			m_callsigns.Add(++i, "Mantis");
			m_callsigns.Add(++i, "Badger");
		}

		public BopGroupVehicle(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, mizGroup)
		{
			Class = ElementDcsObjectClass.Ground;

			Callsign = GetCallsignFromFacTask() ?? GetCallsignFromTaskAction();
			Radio = GetRadioFromFacTask() ?? GetRadioFromTaskAction ();
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		private string GetCallsignFromTaskAction()
		{
			int? iCallsignId = null, iCallsignNumber = null;

			foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
			{
				MizRouteTaskAction routeTaskAction = mizRoutePoint.GetRouteTaskAction(ElementRouteTaskAction.SetCallsign);
				if (routeTaskAction is object)
				{
					iCallsignId = routeTaskAction.ParamCallname;
					iCallsignNumber = routeTaskAction.ParamNumber;
					break;
				}
			}

			return GetCallsignFromId(iCallsignId, iCallsignNumber);
		}

		private Radio GetRadioFromTaskAction()
		{
			foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
			{
				MizRouteTaskAction routeTaskAction = mizRoutePoint.GetRouteTaskAction(ElementRouteTaskAction.SetFrequency);
				if (routeTaskAction is object && routeTaskAction.ParamFrequency is object && routeTaskAction.ParamModulation is object)
					return new Radio(routeTaskAction.ParamFrequency.Value, routeTaskAction.ParamModulation.Value);
			}

			return null;
		}

		private string GetCallsignFromFacTask()
		{
			int? iCallsignId = null, iCallsignNumber = null;

			foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
			{
				MizRouteTask routeTask = mizRoutePoint.GetAnyRouteTask(new List<string>() { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });
				if (routeTask is object)
				{
					iCallsignId = routeTask.Params.Callname;
					iCallsignNumber = routeTask.Params.Number;
					break;
				}
			}

			return GetCallsignFromId(iCallsignId, iCallsignNumber);
		}

		private Radio GetRadioFromFacTask()
		{
			foreach (MizRoutePoint mizRoutePoint in m_mizGroup.RoutePoints)
			{
				MizRouteTask routeTask = mizRoutePoint.GetAnyRouteTask(new List<string>() { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });
				if (routeTask is object && routeTask.Params.Frequency is object && routeTask.Params.Modulation is object)
					return new Radio(routeTask.Params.Frequency.Value, routeTask.Params.Modulation.Value);
			}

			return null;
		}

		private string GetCallsignFromId(int? iCallsignId, int? iNumber)
		{
			string sCallsign = null;
			if (iCallsignId is object && iNumber is object)
			{
				string sCallsignName = m_callsigns[iCallsignId.Value];
				if (!string.IsNullOrEmpty(sCallsignName))
					sCallsign = $"{sCallsignName}-{iNumber}";
			}

			return sCallsign;
		}
		#endregion
	}

}
