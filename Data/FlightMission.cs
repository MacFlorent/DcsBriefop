//using DcsBriefop.DataMiz;
//using DcsBriefop.Tools;
//using GMap.NET;
//using GMap.NET.WindowsForms;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DcsBriefop.Data
//{
//	internal class FlightMission : BaseBriefing
//	{
//		#region Fields
//		private BriefopCustomMission m_briefopCustomMission;
//		#endregion

//		#region Properties
//		public BriefingCoalition Coalition { get; protected set; }
//		public string TypeName { get; set; }
//		public string MissionInformation { get; set; }

//		public BriefopCustomMap MapData { get { return m_briefopCustomMission?.MapData; } }
//		public List<int> OpposingAssetIds { get { return m_briefopCustomMission?.OpposingAssetIds; } }
//		public List<int> OpposingUnitIds { get { return m_briefopCustomMission?.OpposingUnitIds; } }

//		#endregion

//		#region CTOR
//		public FlightMission(BaseBriefingCore core, BriefingCoalition coalition, string sTypeName) : base(core)
//		{
//			TypeName = sTypeName;
//			Coalition = coalition;

//			InitializeDataCustom();
//			InitializeData();
//			InitializeMapData();
//		}
//		#endregion

//		#region Initialize
//		private void InitializeDataCustom()
//		{
//			m_briefopCustomMission = Core.Miz.BriefopCustomData.GetMission(TypeName, Coalition.CoalitionName);
//			if (m_briefopCustomMission is null)
//			{
//				m_briefopCustomMission = new BriefopCustomMission(TypeName, Coalition.CoalitionName);
//				Core.Miz.BriefopCustomData.FlightMissions.Add(m_briefopCustomMission);
//			}
//		}

//		private void InitializeData()
//		{
//			MissionInformation = m_briefopCustomMission.MissionInformation;
//		}

//		public void InitializeMapData()
//		{
//			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

//			if (MapData is null)
//			{
//				m_briefopCustomMission.MapData = new BriefopCustomMap();
//				m_briefopCustomMission.MapData.Provider = Core.Miz.BriefopCustomData.DefaultMapProvider;
//				MapData.CenterLatitude = Coalition.Bullseye.Latitude.DecimalDegree;
//				MapData.CenterLongitude = Coalition.Bullseye.Longitude.DecimalDegree;
//				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
//				MapData.MapOverlayCustom = new GMapOverlay();
//			}

//			MapData.AdditionalMapOverlays.Clear();
//			MapData.AdditionalMapOverlays.Add(staticOverlay);
//			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustomData.MapData.MapOverlayCustom);
//			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustomData.MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault());
//			MapData.AdditionalMapOverlays.Add(Coalition.MapData.MapOverlayCustom);
//			MapData.AdditionalMapOverlays.Add(Coalition.MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault());

//			foreach (AssetAirdrome airdrome in m_flight.GetAirdromeAssets())
//			{
//				MapData.AdditionalMapOverlays.Add(airdrome.MapOverlayStatic);
//			}
//			foreach (AssetShip carrier in m_flight.GetCarrierAssets())
//			{
//				MapData.AdditionalMapOverlays.Add(carrier.MapOverlayStatic);
//			}
//			foreach (AssetGroup group in Coalition.OpposingAssets.OfType<AssetGroup>())
//			{
//				if (OpposingAssetIds.Contains(group.Id) || group.Units.Select(_u => _u.Id).Intersect(OpposingUnitIds).Any())
//				{
//					group.InitializeMapDataPoint(staticOverlay);
//				}
//			}
//		}
//		#endregion

//		#region Methods
//		public override void Persist()
//		{
//			m_briefopCustomMission.MissionInformation = MissionInformation;
//		}

//		public void Remove()
//		{
//			if (Core.Miz.BriefopCustomData.FlightMissions.Contains(m_briefopCustomMission))
//				Core.Miz.BriefopCustomData.FlightMissions.Contains(m_briefopCustomMission);
//		}

//		public void IncludeOpposingAsset(int iAssetId, bool bIncluded)
//		{
//			if (bIncluded && !OpposingAssetIds.Contains(iAssetId))
//			{
//				OpposingAssetIds.Add(iAssetId);
//				InitializeMapData();
//			}
//			else if (!bIncluded && OpposingAssetIds.Contains(iAssetId))
//			{
//				OpposingAssetIds.Remove(iAssetId);
//				InitializeMapData();
//			}
//		}

//		public void IncludeOpposingUnit(int iUnitId, bool bIncluded)
//		{
//			if (bIncluded && !OpposingUnitIds.Contains(iUnitId))
//			{
//				OpposingUnitIds.Add(iUnitId);
//				InitializeMapData();
//			}
//			else if (!bIncluded && OpposingUnitIds.Contains(iUnitId))
//			{
//				OpposingUnitIds.Remove(iUnitId);
//				InitializeMapData();
//			}
//		}
//		#endregion
//	}
//}
