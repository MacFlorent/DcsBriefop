using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using GMap.NET;

namespace DcsBriefop.Data
{
	internal class AssetFlight : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		private MizGroupFlight GroupFlight { get { return m_mizGroup as MizGroupFlight; } }
		public AssetFlightMission MissionData { get; set; }
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		public AssetFlight(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupFlight group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();
		
			MapMarker = MarkerBriefopType.aircraft.ToString();

			Task = GroupFlight.Task;
			Radio = new Radio() { Frequency = GroupFlight.RadioFrequency, Modulation = GroupFlight.RadioModulation };
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
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Excluded;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}
				else if (Playable)
				{
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.MissionWithDetail;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}
				else if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
				{
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Support;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.Orbit;
				}
				else
				{
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Mission;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomGroup.SetDefaultData();
			}

			Usage = (ElementAssetUsage)m_briefopCustomGroup.Usage;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomGroup.MapDisplay;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			GroupFlight.RadioFrequency = Radio.Frequency;
			GroupFlight.RadioModulation = Radio.Modulation;

			MissionData?.Persist();
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";

			if (Side == ElementAssetSide.Own)
			{
				StringBuilder sbInformation = new StringBuilder();

				if (Task == ElementTask.Refueling)
				{
					sbInformation.AppendWithSeparator($"TCN={GetTacanString()}", " ");
				}

				sbInformation.AppendWithSeparator(GetBaseInformation(), " ");
				sInformation = sbInformation.ToString();
			}

			return sInformation;
		}

		public override string GetRadioString()
		{
			return Radio.ToString();
		}

		public override string GetLocalisation()
		{
			return GetBaseInformation();
		}

		public string GetCallsign()
		{
			string sCallsign = m_mizGroup.Units.OfType<MizUnitFlight>().FirstOrDefault()?.Callsign;
			if (!string.IsNullOrEmpty(sCallsign))
				return sCallsign.Substring(0, sCallsign.Length - 1);
			else
				return null;
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
			IEnumerable<int> grouped = m_mizGroup.RoutePoints
				.Where(_rp => _rp.AirdromeId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

			return grouped.ToList();
		}

		public List<AssetAirdrome> GetAirdromeAssets()
		{
			return GetAirdromeIds().Select(_i => Coalition.Airdromes.Where(_a => _a.Id == _i).FirstOrDefault()).ToList();
		}

		public List<AssetShip> GetCarrierAssets()
		{
			IEnumerable<int> grouped = m_mizGroup.RoutePoints
				.Where(_rp => _rp.HelipadId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);

			return Coalition.OwnAssets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
		}

		public void AddMissionData()
		{
			if (MissionData is null)
			{
				MissionData = new AssetFlightMission(Core, Coalition, this);
			}
		}

		public void RemoveMissionData()
		{
			if (MissionData is object)
			{
				MissionData.Remove();
				MissionData = null;
			}
		}

		public void SetMissionData()
		{
			if (Usage == ElementAssetUsage.MissionWithDetail)
				AddMissionData();
			else
				RemoveMissionData();
		}
		#endregion
	}


	internal class AssetFlightMission : BaseBriefing
	{
		#region Fields
		private AssetFlight m_flight;
		private BriefopCustomMission m_briefopCustomMission;
		#endregion

		#region Properties
		public BriefingCoalition Coalition { get; protected set; }
		public string MissionInformation { get; set; }
		
		
		public BriefopCustomMap MapData { get { return m_briefopCustomMission?.MapData; } }
		public List<int> TargetIds { get { return m_briefopCustomMission?.TargetIds; } }
		public Dictionary<int, string> WaypointNotes { get { return m_briefopCustomMission?.WaypointNotes; } }

		#endregion

		#region CTOR
		public AssetFlightMission(BaseBriefingCore core, BriefingCoalition coalition, AssetFlight flight) : base(core)
		{
			m_flight = flight;
			Coalition = coalition;

			InitializeDataCustom();
			InitializeData();
			InitializeMapData();
		}
		#endregion

		#region Initialize
		private void InitializeDataCustom()
		{
			m_briefopCustomMission = Core.Miz.BriefopCustomData.GetMission(m_flight.Id, Coalition.CoalitionName);
			if (m_briefopCustomMission is null)
			{
				m_briefopCustomMission = new BriefopCustomMission(m_flight.Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.Missions.Add(m_briefopCustomMission);
			}
		}

		private void InitializeData()
		{
			MissionInformation = m_briefopCustomMission.MissionInformation;
		}

		public void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			List<PointLatLng> points = m_flight.InitializeMapDataFullRoute(staticOverlay);

			if (MapData is null)
			{
				m_briefopCustomMission.MapData = new BriefopCustomMap();

				PointLatLng? centerPoint = ToolsMap.GetPointsCenter(points);
				if (centerPoint is object)
				{
					MapData.CenterLatitude = centerPoint.Value.Lat;
					MapData.CenterLongitude = centerPoint.Value.Lng;
				}
				else
				{
					MapData.CenterLatitude = Coalition.Bullseye.Latitude.DecimalDegree;
					MapData.CenterLongitude = Coalition.Bullseye.Longitude.DecimalDegree;
				}
				MapData.Zoom = ElementMapValue.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustomData.MapData.MapOverlayCustom);
			MapData.AdditionalMapOverlays.Add(Coalition.MapData.MapOverlayCustom);

			foreach (AssetAirdrome airdrome in m_flight.GetAirdromeAssets())
			{
				MapData.AdditionalMapOverlays.Add(airdrome.MapOverlayStatic);
			}
			foreach (AssetShip carrier in m_flight.GetCarrierAssets())
			{
				MapData.AdditionalMapOverlays.Add(carrier.MapOverlayStatic);
			}
			foreach (AssetGroup target in Coalition.OpposingAssets.OfType<AssetGroup>())
			{
				if (target.Units.Select(_u => _u.Id).Intersect(TargetIds).Any())
					MapData.AdditionalMapOverlays.Add(target.MapOverlayStatic);
			}
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			m_briefopCustomMission.MissionInformation = MissionInformation;
		}

		public void Remove()
		{
			if (Core.Miz.BriefopCustomData.Missions.Contains(m_briefopCustomMission))
				Core.Miz.BriefopCustomData.Missions.Contains(m_briefopCustomMission);
		}

		public bool IsTarget(int iTargetId)
		{
			return TargetIds.Contains(iTargetId);
		}

		public void SetTarget(int iTargetId, bool bSelected)
		{
			if (bSelected && !IsTarget(iTargetId))
				TargetIds.Add(iTargetId);
			else if (!bSelected && IsTarget(iTargetId))
				TargetIds.Remove(iTargetId);
		}

		public List<AssetUnit> GetListTargetUnits()
		{
			List<AssetUnit> list = new List<AssetUnit>();
			foreach (AssetGroup target in Coalition.OpposingAssets.OfType<AssetGroup>())
			{
				foreach (AssetUnit unit in target.Units.Where(_u => IsTarget(_u.Id)))
				{
					list.Add(unit);
				}
			}

			return list;
		}
		#endregion
	}
}
