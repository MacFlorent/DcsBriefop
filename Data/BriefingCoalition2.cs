using CoordinateSharp;
using System.Drawing;

namespace DcsBriefop.Data
{
	internal class BriefingCoalition2 : BaseBriefing
	{
		#region Properties
		public Coordinate Bullseye { get; set; }
		public string BullseyeDescription { get; set; }
		public string Coalition { get; set; }
		public string Task { get; set; }

		public Color OwnColor { get; set; }
		public Color OpposingColor { get; set; }

		//public List<Asset> OwnAssets { get; private set; } = new List<Asset>();
		//public List<Asset> OpposingAssets { get; private set; } = new List<Asset>();
		//public List<AssetAirdrome> Airdromes { get; private set; } = new List<AssetAirdrome>();
		#endregion

		#region CTOR
		public BriefingCoalition2(BriefingContext missionContext, string sCoalitionName) : base(missionContext)
		{
			//m_coalition = RootMission.Coalitions.Where(c => c.Name == sCoalitionName).FirstOrDefault();

			//string sOpposingCoalitionName = "";
			//if (sCoalitionName == ElementCoalition.Blue)
			//	sOpposingCoalitionName = ElementCoalition.Red;
			//else if (sCoalitionName == ElementCoalition.Red)
			//	sOpposingCoalitionName = ElementCoalition.Blue;
			//m_opposingCoalition = RootMission.Coalitions.Where(c => c.Name == sOpposingCoalitionName).FirstOrDefault();

			//m_customDataCoalition = RootCustom.GetCoalition(sCoalitionName);

			//InitializeMapData();

			//OwnAssets = BuildCoalitionAssets(briefingPack, m_coalition, ElementAssetSide.Own);
			//OpposingAssets = BuildCoalitionAssets(briefingPack, m_opposingCoalition, ElementAssetSide.Opposing);

			//foreach (Airdrome airdrome in Theatre.Airdromes)
			//{
			//	Airdromes.Add(new AssetAirdrome(briefingPack, this, ElementAssetSide.None, airdrome));
			//}

			//InitializeMapDataChildrenOverlays();
			//InitializeFlightDataMissions();
		}
		#endregion

		//#region Methods
		//private List<Asset> BuildCoalitionAssets(BriefingPack briefingPack, Coalition coalition, ElementAssetSide side)
		//{
		//	List<Asset> assets = new List<Asset>();

		//	if (coalition is object)
		//	{
		//		foreach (Country c in coalition.Countries)
		//		{
		//			foreach (GroupFlight g in c.GroupFlights)
		//			{
		//				assets.Add(new AssetFlight(briefingPack, this, side, g));
		//			}
		//			foreach (GroupShip g in c.GroupShips)
		//			{
		//				assets.Add(new AssetShip(briefingPack, this, side, g));
		//			}
		//			foreach (GroupVehicle g in c.GroupVehicles)
		//			{
		//				assets.Add(new AssetVehicle(briefingPack, this, side, g));
		//			}
		//		}
		//	}

		//	return assets;
		//}

		//private void InitializeMapData()
		//{
		//	GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
		//	PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
		//	m_markerkBullseye = new GMarkerBriefop(p, MarkerBriefopType.bullseye.ToString(), OwnColor, BullseyeDescription);
		//	staticOverlay.Markers.Add(m_markerkBullseye);

		//	if (MapData is null)
		//	{
		//		MapData = new CustomDataMap();
		//		MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
		//		MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
		//		MapData.Zoom = ElementMapValue.DefaultZoom;
		//		MapData.MapOverlayCustom = new GMapOverlay();
		//	}

		//	MapData.AdditionalMapOverlays.Clear();
		//	MapData.AdditionalMapOverlays.Add(staticOverlay);
		//	MapData.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
		//}

		//private void InitializeMapDataChildrenOverlays()
		//{
		//	foreach (Asset asset in OwnAssets)
		//	{
		//		if (asset.MapOverlayStatic is object)
		//			MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
		//	}
		//	foreach (Asset asset in OpposingAssets)
		//	{
		//		if (asset.MapOverlayStatic is object)
		//			MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
		//	}
		//	foreach (Asset asset in Airdromes)
		//	{
		//		if (asset.MapOverlayStatic is object)
		//			MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
		//	}
		//}

		//private void InitializeFlightDataMissions()
		//{
		//	foreach (AssetFlight asset in OwnAssets.OfType<AssetFlight>())
		//	{
		//		asset.SetMissionData();
		//	}
		//}
		//#endregion
	}
}
