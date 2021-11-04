﻿using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetFlight : AssetGroup
	{
		#region Fields
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.aircraft.ToString();
		public override string Task { get { return GroupFlight.Task; } }
		public override string Type { get { return GroupFlight.Units.FirstOrDefault()?.Type; } }
		public override string Radio { get { return ToolsMisc.GetRadioString(RadioFrequency, RadioModulation); } }

		public decimal RadioFrequency
		{
			get { return GroupFlight.RadioFrequency; }
			set { GroupFlight.RadioFrequency = value; }
		}
		public int RadioModulation
		{
			get { return GroupFlight.RadioModulation; }
			set { GroupFlight.RadioModulation = value; }
		}
		#endregion

			#region CTOR
		public AssetFlight(BriefingPack briefingPack, BriefingCoalition briefingCoalition, GroupFlight group) : base(briefingPack, briefingCoalition, group) { }
		#endregion

		#region Methods
		protected override string GetDefaultInformation()
		{
			string sInformation = "";

			if (Task == ElementTask.Refueling)
			{
				sInformation = $"TCN={GetTacanString()} ";
			}

			//sInformation = $"{sInformation} Base={GetAirdromeNames()}";
			return sInformation;
		}

		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.AssetGroups?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id);
			RootCustom.AssetGroups.Add(CustomData);

			if (Playable)
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
			else if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
			{
				Category = ElementAssetCategory.Support;
				MapDisplay = ElementAssetMapDisplay.Orbit;
			}
			else
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
		}

		public string GetRadioString()
		{
			return ToolsMisc.GetRadioString(RadioFrequency, RadioModulation);
		}

		public string GetCallsign()
		{
				string sCallsign = m_group.Units.OfType<UnitPlane>().FirstOrDefault()?.Callsign;
				if (!string.IsNullOrEmpty(sCallsign))
					return sCallsign.Substring(0, sCallsign.Length - 1);
				else
					return null;
		}

		public List<int> GetAirdromeIds()
		{
			IEnumerable<int> grouped = m_group.RoutePoints
				.Where(_rp => _rp.AirdromeId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

			return grouped.ToList(); ;
			//List<Airdrome> airdromes = new List<Airdrome>();
			//foreach(int iId in grouped)
			//{
			//	Airdrome ad = Theatre.GetAirdrome(iId);
			//	if (ad is object)
			//		airdromes.Add(Theatre.GetAirdrome(iId));
			//}

			//return airdromes;
		}

		//public string GetAirdromeNames()
		//{
		//	IEnumerable<string> grouped = GetAirdromes().Select(_ad => _ad.Name);
		//	return string.Join(",", grouped);
		//}
		#endregion
	}
}
