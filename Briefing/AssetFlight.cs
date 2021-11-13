using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using GMap.NET;

namespace DcsBriefop.Briefing
{
	internal class AssetFlight : AssetGroup
	{
		#region Fields
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }
		#endregion

		#region Properties
		public AssetFlightMission MissionData { get; set; }

		protected override string MapMarker { get; set; } = MarkerBriefopType.aircraft.ToString();
		public override string Task { get { return GroupFlight.Task; } }
		public override string Type { get { return GroupFlight.Units.FirstOrDefault()?.Type; } }
		public override string RadioString { get { return Radio.ToString(); } }

		private Radio m_radio;
		public Radio Radio
		{
			get
			{
				if (m_radio is null)
					m_radio = new Radio() { Frequency = GroupFlight.RadioFrequency, Modulation = GroupFlight.RadioModulation };
				return m_radio;
			}
			set
			{
				GroupFlight.RadioFrequency = value.Frequency;
				GroupFlight.RadioModulation = value.Modulation;
			}
		}

		#endregion

		#region CTOR
		public AssetFlight(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, GroupFlight group) : base(briefingPack, briefingCoalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.GetAssetGroup(Id, BriefingCoalition.Name);
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id, BriefingCoalition.Name);
			RootCustom.AssetGroups.Add(CustomData);

			if (Side != ElementAssetSide.Own)
			{
				Usage = ElementAssetUsage.Excluded;
				MapDisplay = ElementAssetMapDisplay.None;
			}
			else if (Playable)
			{
				Usage = ElementAssetUsage.MissionWithDetail;
				MapDisplay = ElementAssetMapDisplay.None;
			}
			else if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
			{
				Usage = ElementAssetUsage.Support;
				MapDisplay = ElementAssetMapDisplay.Orbit;
			}
			else
			{
				Usage = ElementAssetUsage.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}

			CustomData.SetDefaultData();
		}
		#endregion

		#region Methods
		public void AddMissionData()
		{
			if (MissionData is null)
			{
				MissionData = new AssetFlightMission(RootCustom, BriefingCoalition, this);
			}
		}

		public void RemoveMissionData()
		{
			if (MissionData is object)
			{
				if (RootCustom.Missions.Contains(MissionData.CustomData))
					RootCustom.Missions.Contains(MissionData.CustomData);

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

		protected override string GetDefaultInformation()
		{
			StringBuilder sbInformation = new StringBuilder();

			if (Task == ElementTask.Refueling)
			{
				sbInformation.AppendWithSeparator($"TCN={GetTacanString()}", " ");
			}

			sbInformation.AppendWithSeparator(GetBaseInformation(), " ");
			return sbInformation.ToString();
		}

		public string GetCallsign()
		{
			string sCallsign = m_group.Units.OfType<UnitFlight>().FirstOrDefault()?.Callsign;
			if (!string.IsNullOrEmpty(sCallsign))
				return sCallsign.Substring(0, sCallsign.Length - 1);
			else
				return null;
		}

		private string GetBaseInformation()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Airdrome airdrome in GetAirdromeIds().Select(_i => Theatre.GetAirdrome(_i)))
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
			IEnumerable<int> grouped = m_group.RoutePoints
				.Where(_rp => _rp.AirdromeId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

			return grouped.ToList();
		}

		public List<AssetAirdrome> GetAirdromeAssets()
		{
			return GetAirdromeIds().Select(_i => BriefingCoalition.Airdromes.Where(_a => _a.Id == _i).FirstOrDefault()).ToList();
		}

		public List<AssetShip> GetCarrierAssets()
		{
			IEnumerable<int> grouped = m_group.RoutePoints
				.Where(_rp => _rp.HelipadId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);

			return BriefingCoalition.OwnAssets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
		}

		public override string GetLocalisation()
		{
			return GetBaseInformation();
		}
		#endregion
	}


	internal class AssetFlightMission
	{
		#region Fields
		private AssetFlight m_flight;
		private CustomData m_rootCustom;
		#endregion

		#region Properties
		public BriefingCoalition BriefingCoalition { get; protected set; }
		public CustomDataMission CustomData { get; set; }

		public string MissionInformation
		{
			get { return CustomData.MissionInformation; }
			set { CustomData.MissionInformation = value; }
		}

		public CustomDataMap MapDataMission
		{
			get { return CustomData.MapData; }
			set { CustomData.MapData = value; }
		}

		#endregion

		#region CTOR
		public AssetFlightMission(CustomData rootCustom, BriefingCoalition briefingCoalition, AssetFlight flight)
		{
			m_rootCustom = rootCustom;
			m_flight = flight;
			BriefingCoalition = briefingCoalition;
			InitializeCustomData();
			InitializeMapData();
		}
		#endregion

		#region Initialize
		private void InitializeCustomData()
		{
			CustomData = m_rootCustom.GetMission(m_flight.Id, BriefingCoalition.Name);
			if (CustomData is null)
			{
				CustomData = new CustomDataMission(m_flight.Id, BriefingCoalition.Name);
				m_rootCustom.Missions.Add(CustomData);
			}
		}

		public void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			List<PointLatLng> points = m_flight.InitializeMapDataFullRoute(staticOverlay);

			if (MapDataMission is null)
			{
				MapDataMission = new CustomDataMap();

				PointLatLng? centerPoint = ToolsMap.GetPointsCenter(points);
				if (centerPoint is object)
				{
					MapDataMission.CenterLatitude = centerPoint.Value.Lat;
					MapDataMission.CenterLongitude = centerPoint.Value.Lng;
				}
				else
				{
					MapDataMission.CenterLatitude = BriefingCoalition.Bullseye.Latitude.DecimalDegree;
					MapDataMission.CenterLongitude = BriefingCoalition.Bullseye.Longitude.DecimalDegree;
				}
				MapDataMission.Zoom = ElementMapValue.DefaultZoom;
				MapDataMission.MapOverlayCustom = new GMapOverlay();
			}

			MapDataMission.AdditionalMapOverlays.Clear();
			MapDataMission.AdditionalMapOverlays.Add(staticOverlay);
			MapDataMission.AdditionalMapOverlays.Add(m_rootCustom.MapData.MapOverlayCustom);
			MapDataMission.AdditionalMapOverlays.Add(BriefingCoalition.MapData.MapOverlayCustom);

			foreach (AssetAirdrome airdrome in m_flight.GetAirdromeAssets())
			{
				MapDataMission.AdditionalMapOverlays.Add(airdrome.MapOverlayStatic);
			}
			foreach (AssetShip carrier in m_flight.GetCarrierAssets())
			{
				MapDataMission.AdditionalMapOverlays.Add(carrier.MapOverlayStatic);
			}
		}
		#endregion
	}
}
