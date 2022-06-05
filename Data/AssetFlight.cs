using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DcsBriefop.Data
{
	internal class AssetFlight : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		public override ElementDcsObjectClass Class { get { return base.Class == ElementDcsObjectClass.None ? ElementDcsObjectClass.Air : base.Class; } }
		private MizGroupFlight MizGroupFlight { get { return m_mizGroup as MizGroupFlight; } }
		public string Callsign { get; set; }
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		public AssetFlight(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupFlight group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();

			MizUnitFlight mizUnitFlight = MizGroupFlight.Units.OfType<MizUnitFlight>().FirstOrDefault();
			if (mizUnitFlight is object && mizUnitFlight.Callsign is MizCallsign callsign)
				Callsign = callsign.Name.Substring(0, callsign.Name.Length - 1);
			else if (mizUnitFlight is object && mizUnitFlight.CallsignNumber is object)
				Callsign = mizUnitFlight.CallsignNumber.ToString();

			Task = MizGroupFlight.Task;
			Type = MizGroupFlight.Units.OfType<MizUnitFlight>().FirstOrDefault()?.Type;

			SetDisplayName();

			Radio = new Radio() { Frequency = MizGroupFlight.RadioFrequency, Modulation = MizGroupFlight.RadioModulation };

			if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
				Function = ElementAssetFunction.Support;
		}

		protected override void InitializeDataCustom()
		{
			m_briefopCustomGroup = Core.Miz.BriefopCustomData.GetGroup(Id, Coalition.CoalitionName);

			if (m_briefopCustomGroup is null)
			{
				m_briefopCustomGroup = new BriefopCustomGroup(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.AssetGroups.Add(m_briefopCustomGroup);

				if (Side != ElementAssetSide.Own)
				{
					m_briefopCustomGroup.Included = false;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}
				else if (Playable)
				{
					m_briefopCustomGroup.Included = false;
					//m_briefopCustomGroup.WithMissionData = true;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}
				else if (Function == ElementAssetFunction.Support)
				{
					m_briefopCustomGroup.Included = true;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.Orbit;
				}
				else
				{
					m_briefopCustomGroup.Included = false;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomGroup.SetDefaultData();
			}

			Included = m_briefopCustomGroup.Included;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomGroup.MapDisplay;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			MizGroupFlight.RadioFrequency = Radio.Frequency;
			MizGroupFlight.RadioModulation = Radio.Modulation;
		}

		public void SetDisplayName()
		{
			DisplayName = $"{Callsign} | {m_mizGroup.Name}";
			if (Playable && Core.Miz.BriefopCustomData.NoCallsignForPlayableFlights)
				DisplayName = m_mizGroup.Name;

		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";

			if (Side == ElementAssetSide.Own)
			{
				StringBuilder sbInformation = new StringBuilder();

				if (Task == ElementTask.Refueling)
				{
					sbInformation.AppendWithSeparator($"{GetTacanString()}", " ");
				}

				//sbInformation.AppendWithSeparator(GetBaseInformation(), Environment.NewLine);
				sInformation = sbInformation.ToString();
			}

			return sInformation;
		}

		public override string GetRadioString()
		{
			if (Playable)
				return "initial preset only";
			else
				return Radio.ToString();
		}

		public override string GetLocalisation()
		{
			return GetBaseInformation();
		}

		private string GetBaseInformation()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Airdrome airdrome in GetAirdromeIds().Select(_i => Core.Theatre.GetAirdrome(_i)))
			{
				if (airdrome is object)
				{
					if (sb.Length <= 0)
					{
						sb.Append($"Base={airdrome.Name}");
					}
					else
					{
						sb.AppendWithSeparator($"{airdrome.Name}", ",");
					}
				}
			}
			foreach (AssetShip carrier in GetCarrierAssets())
			{
				if (sb.Length <= 0)
				{
					sb.Append($"Base={carrier.Name}");
				}
				else
				{
					sb.AppendWithSeparator($"{carrier.Name}", ",");
				}
			}

			return sb.ToString();
		}

		public List<int> GetAirdromeIds()
		{
			IEnumerable<int> grouped = MapPoints.OfType<AssetRoutePoint>()
				.Where(_rp => _rp.AirdromeId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

			return grouped.ToList();
		}

		public List<AssetAirdrome> GetAirdromeAssets()
		{
			List<int> flightAirdromes = GetAirdromeIds();
			return Coalition.Airdromes.Where(_a => flightAirdromes.Contains(_a.Id)).ToList();
		}

		public List<AssetShip> GetCarrierAssets()
		{
			IEnumerable<int> grouped = MapPoints.OfType<AssetRoutePoint>()
				.Where(_rp => _rp.HelipadId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);

			return Coalition.OwnAssets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
		}

		//public void AddMissionData()
		//{
		//	if (MissionData is null)
		//	{
		//		MissionData = new AssetFlightMission(Core, Coalition, this);
		//	}
		//}

		//public void RemoveMissionData()
		//{
		//	if (MissionData is object)
		//	{
		//		MissionData.Remove();
		//		MissionData = null;
		//	}
		//}

		private void AddBullseyeWaypoint()
		{
			AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();

			if (bullsPoint is null)
			{
				MizRoutePoint mizRoutePoint = MizRoutePoint.NewFromLuaTemplate();
				mizRoutePoint.Y = Coalition.MizCoalition.BullseyeY;
				mizRoutePoint.X = Coalition.MizCoalition.BullseyeX;

				AssetRoutePoint routePoint = new AssetRoutePoint(Core, 0, this, mizRoutePoint);
				routePoint.Name = m_sBullsPointName;

				MapPoints.Insert(1, routePoint);
				NumberMapPoints();
			}
			else
			{
				bullsPoint.SetYX(Coalition.MizCoalition.BullseyeY, Coalition.MizCoalition.BullseyeX);
			}
		}

		private void RemoveBullseyeWaypoint()
		{
			AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();
			if (bullsPoint is object)
				MapPoints.Remove(bullsPoint);

			NumberMapPoints();
		}

		public void InitializeBullseyeWaypoint(bool bWithWaypoint)
		{
			//dataCartridge F18
			// bullseye m2000
			Log.Info("Updating BULLS waypoints status");
			if (bWithWaypoint && Playable)
				AddBullseyeWaypoint();
			else
				RemoveBullseyeWaypoint();

			InitializeMapOverlay();
		}
		#endregion
	}
}
