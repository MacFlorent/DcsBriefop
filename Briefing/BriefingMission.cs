//using DcsBriefop.LsonStructure;
//using DcsBriefop.Data;
//using DcsBriefop.Tools;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DcsBriefop.Map;
//using GMap.NET.WindowsForms;
//using GMap.NET;

//namespace DcsBriefop.Briefing
//{
//	internal class BriefingMission : BaseBriefing
//	{
//		#region Fields
//		private GroupFlight m_group;
//		#endregion

//		#region Properties
//		public CustomDataMission CustomData { get; set; }
//		public BriefingCoalition BriefingCoalition { get; protected set; }
//		public int Id { get { return m_group.Id; } }
//		public string MissionInformation
//		{
//			get { return CustomData.MissionInformation; }
//			set { CustomData.MissionInformation = value; }
//		}
//		public CustomDataMap MapDataMission
//		{
//			get { return CustomData.MapData; }
//			set { CustomData.MapData = value; }
//		}
//		#endregion

//		#region CTOR
//		public BriefingMission(BriefingPack briefingPack, BriefingCoalition briefingCoalition, GroupFlight group) : base(briefingPack)
//		{
//			BriefingCoalition = briefingCoalition;
//			m_group = group;

//			int iNumber = 0;
//			foreach (RoutePoint rp in m_group.RoutePoints)
//			{
//				MissionPoints.Add(new AssetRoutePoint(briefingPack, iNumber, this, rp));
//				iNumber++;
//			}
//		}
//		#endregion

//		#region Initialize
//		public void InitializeCustomDataMission()
//		{
//			CustomData = RootCustom.GetMission(Id, BriefingCoalition.Name);
//			if (CustomData is null)
//			{
//				CustomData = new CustomDataMission(Id, BriefingCoalition.Name);
//				RootCustom.Missions.Add(CustomData);
//			}

//			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
//			List<PointLatLng> points = InitializeMapDataFullRoute(staticOverlay);

//			if (MapDataMission is null)
//			{
//				MapDataMission = new CustomDataMap();

//				PointLatLng? centerPoint = ToolsMap.GetPointsCenter(points);
//				if (centerPoint is object)
//				{
//					MapDataMission.CenterLatitude = centerPoint.Value.Lat;
//					MapDataMission.CenterLongitude = centerPoint.Value.Lng;
//				}
//				else
//				{
//					MapDataMission.CenterLatitude = BriefingCoalition.Bullseye.Latitude.DecimalDegree;
//					MapDataMission.CenterLongitude = BriefingCoalition.Bullseye.Longitude.DecimalDegree;
//				}
//				MapDataMission.Zoom = ElementMapValue.DefaultZoom;
//				MapDataMission.MapOverlayCustom = new GMapOverlay();
//			}

//			MapDataMission.AdditionalMapOverlays.Clear();
//			MapDataMission.AdditionalMapOverlays.Add(staticOverlay);
//			MapDataMission.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
//			MapDataMission.AdditionalMapOverlays.Add(BriefingCoalition.MapData.MapOverlayCustom);

//			foreach (AssetAirdrome airdrome in GetAirdromeAssets())
//			{
//				MapDataMission.AdditionalMapOverlays.Add(airdrome.MapOverlayStatic);
//			}
//			foreach (AssetShip carrier in GetCarrierAssets())
//			{
//				MapDataMission.AdditionalMapOverlays.Add(carrier.MapOverlayStatic);
//			}
//		}
//	}
//	#endregion

//	#region Methods
//	#endregion
//}

