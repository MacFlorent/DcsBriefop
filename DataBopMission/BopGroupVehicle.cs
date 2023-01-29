using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupVehicle : BopGroup
	{
		#region Fields
		private static Dictionary<int, string> m_callsigns;// TODO replace by a json resource
		#endregion

		#region Properties
		public string Callsign { get; set; }
		public Tacan Tacan { get; set; }
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

		public BopGroupVehicle(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, ElementDcsGroupType.Vehicle, ElementDcsObjectClass.Ground, mizGroup)
		{
			Callsign = GetCallsignFromRouteTaskFac() ?? GetCallsignFromRouteTaskAction();
			Radio = GetRadioFromRouteTaskFac() ?? GetRadioFromRouteTaskAction();
			Tacan = GetTacanFromRouteTaskAction(null);
		}
		#endregion

		#region Miz
		#endregion

		#region Methods
		public override string ToStringDisplayName()
		{
			string sDisplayName = base.ToStringDisplayName();

			if (!string.IsNullOrEmpty(Callsign))
				return $"{Callsign} | {sDisplayName}";
			else
				return sDisplayName;
		}

		public override string ToStringAdditionnal()
		{
			string sDescription = base.ToStringDisplayName();
			if (Tacan is object)
				sDescription = $"{sDescription} TACAN:{Tacan}";

			return sDescription;
		}

		private string GetCallsignFromRouteTaskAction()
		{
			int? iCallsignId = null, iCallsignNumber = null;
			MizRouteTaskAction routeTaskAction = GetRouteTaskAction(new List<string> { ElementRouteTaskAction.SetCallsign }, null);

			if (routeTaskAction is object)
			{
				iCallsignId = routeTaskAction.ParamCallname;
				iCallsignNumber = routeTaskAction.ParamNumber;
			}

			return GetCallsignFromId(iCallsignId, iCallsignNumber);
		}

		private Radio GetRadioFromRouteTaskAction()
		{
			MizRouteTaskAction routeTaskAction = GetRouteTaskAction(new List<string> { ElementRouteTaskAction.SetFrequency }, null);
			if (routeTaskAction is object)
				return new Radio(routeTaskAction.ParamFrequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, routeTaskAction.ParamModulation.GetValueOrDefault(ElementRadioModulation.AM));
			else
				return null;
		}

		private string GetCallsignFromRouteTaskFac()
		{
			int? iCallsignId = null, iCallsignNumber = null;
			MizRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });

			if (routeTask is object)
			{
				iCallsignId = routeTask.Params.Callname;
				iCallsignNumber = routeTask.Params.Number;
			}

			return GetCallsignFromId(iCallsignId, iCallsignNumber);
		}

		private Radio GetRadioFromRouteTaskFac()
		{
			MizRouteTask routeTask = GetRouteTask(new List<string> { ElementRouteTask.Fac, ElementRouteTask.FacEngageGroup, ElementRouteTask.FacAttackGroup });
			if (routeTask is object)
				return new Radio(routeTask.Params.Frequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, routeTask.Params.Modulation.GetValueOrDefault(ElementRadioModulation.AM));
			else
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
