//using CoordinateSharp;
//using DcsBriefop.DataMiz;
//using DcsBriefop.Map;
//using DcsBriefop.Tools;
//using GMap.NET;
//using GMap.NET.WindowsForms;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;

//namespace DcsBriefop.DataBriefop
//{
//	internal class BriefopCoalition : BaseBriefop
//	{
//		#region Fields
//		private BriefopCustomCoalition m_briefopCustomCoalition;
//		private GMarkerBriefop m_markerkBullseye;
//		#endregion

//		#region Properties
//		public MizCoalition MizCoalition { get; set; }

//		public string CoalitionName { get; set; }
//		public bool Included { get; set; }
//		public Coordinate Bullseye { get; set; }
//		public string BullseyeDescription { get; set; }
//		public bool BullseyeWaypoint { get; set; }
//		public string Task { get; set; }

//		public Color OwnColor { get; set; }
//		public Color OpposingColor { get; set; }

//		public List<Asset> OwnAssets { get; private set; } = new List<Asset>();
//		public List<Asset> OpposingAssets { get; private set; } = new List<Asset>();
//		public List<AssetAirdrome> Airdromes { get; private set; } = new List<AssetAirdrome>();
//		public ListComPreset ComPresets { get; set; }

//		public BriefopCustomMap MapData { get { return m_briefopCustomCoalition.MapData; } }
//		#endregion

//		#region CTOR
//		public BriefopCoalition(BaseBriefingCore core, string sCoalitionName) : base(core)
//		{
//			CoalitionName = sCoalitionName;
//			string sOpposingCoalitionName = ToolsBriefop.GetOpposingCoalitionName(CoalitionName);
//			MizCoalition = Core.Miz.RootMission.Coalitions.Where(c => c.Name == CoalitionName).FirstOrDefault();
//			m_mizOpposingCoalition = Core.Miz.RootMission.Coalitions.Where(c => c.Name == sOpposingCoalitionName).FirstOrDefault();

//			Initialize();
//		}
//		#endregion

//		#region Initialize
//		private void Initialize()
//		{
//			InitializeDataCustom();
//			InitializeData();

//			InitializeBullseyeWaypoints();
//			ComPresets?.Compute(this);
//		}

//		private void InitializeDataCustom()
//		{
//			m_briefopCustomCoalition = Core.Miz.BriefopCustomData.GetCoalition(CoalitionName);
//			if (m_briefopCustomCoalition is null)
//			{
//				m_briefopCustomCoalition = new BriefopCustomCoalition() { CoalitionName = CoalitionName, Included = true };
//				Core.Miz.BriefopCustomData.Coalitions.Add(m_briefopCustomCoalition);
//			}

//			Included = m_briefopCustomCoalition.Included;

//			if (m_briefopCustomCoalition.ComPresets is object)
//				ComPresets = m_briefopCustomCoalition.ComPresets.GetCopy();
//		}

//		private void InitializeData()
//		{
//			string sOpposingCoalitionName = m_mizOpposingCoalition.Name;

//			Bullseye = Core.Theatre.GetCoordinate(MizCoalition.BullseyeY, MizCoalition.BullseyeX);
//			if (CoalitionName == ElementCoalition.Red)
//				Task = Core.Miz.RootDictionary.RedTask;
//			else if (CoalitionName == ElementCoalition.Blue)
//				Task = Core.Miz.RootDictionary.BlueTask;
//			else if (CoalitionName == ElementCoalition.Neutral)
//				Task = Core.Miz.RootDictionary.NeutralTask;

//			Task = ToolsLua.DcsTextToDisplay(Task);

//			OwnColor = ToolsBriefop.GetCoalitionColor(CoalitionName);
//			OpposingColor = ToolsBriefop.GetCoalitionColor(sOpposingCoalitionName);

//			BullseyeDescription = m_briefopCustomCoalition.BullseyeDescription;
//			BullseyeWaypoint = m_briefopCustomCoalition.BullseyeWaypoint;

//			InitializeMapData();

//			OwnAssets = BuildCoalitionAssets(MizCoalition, ElementAssetSide.Own);
//			OpposingAssets = BuildCoalitionAssets(m_mizOpposingCoalition, ElementAssetSide.Opposing);

//			// airdromes must be intialized after assets so they can refer assets lists to determine if they are a base
//			foreach (Airdrome airdrome in Core.Theatre.Airdromes)
//			{
//				Airdromes.Add(new AssetAirdrome(Core, this, airdrome));
//			}

//			InitializeMapDataChildrenOverlays();
//		}

//		private void InitializeMapData()
//		{
//			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
//			PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
//			m_markerkBullseye = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Bullseye, OwnColor, BullseyeDescription, 1, 0);
//			staticOverlay.Markers.Add(m_markerkBullseye);

//			ToolsMap.AddMizDrawingLayers(Core.Theatre, staticOverlay, Core.Miz.RootMission.DrawingLayers.Where(_dl => _dl.Name.ToUpper() == CoalitionName.ToUpper()).ToList());

//			if (MapData is null)
//			{
//				m_briefopCustomCoalition.MapData = new BriefopCustomMap();
//				m_briefopCustomCoalition.MapData.Provider = Core.Miz.BriefopCustomData.DefaultMapProvider;
//				MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
//				MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
//				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
//				MapData.MapOverlayCustom = new GMapOverlay();
//			}

//			MapData.AdditionalMapOverlays.Clear();
//			MapData.AdditionalMapOverlays.Add(staticOverlay);
//			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustomData.MapData.MapOverlayCustom);
//			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustomData.MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault());
//		}

//		private void InitializeMapDataChildrenOverlays()
//		{
//			foreach (Asset asset in OwnAssets)
//			{
//				if (asset.MapOverlayStatic is object)
//					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
//			}
//			foreach (Asset asset in OpposingAssets)
//			{
//				if (asset.MapOverlayStatic is object)
//					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
//			}
//			foreach (Asset asset in Airdromes)
//			{
//				if (asset.MapOverlayStatic is object)
//					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
//			}
//		}

//		public void InitializeBullseyeWaypoints()
//		{
//			foreach (AssetFlight flight in OwnAssets.OfType<AssetFlight>())
//			{
//				flight.InitializeBullseyeWaypoint(BullseyeWaypoint);
//			}
//		}
//		#endregion

//		#region Methods
//		public override void Persist()
//		{
//			if (CoalitionName == ElementCoalition.Red)
//				Core.Miz.RootDictionary.RedTask = ToolsLua.DisplayToDcsText(Task);
//			else if (CoalitionName == ElementCoalition.Blue)
//				Core.Miz.RootDictionary.BlueTask = ToolsLua.DisplayToDcsText(Task);
//			else if (CoalitionName == ElementCoalition.Neutral)
//				Core.Miz.RootDictionary.NeutralTask = ToolsLua.DisplayToDcsText(Task);

//			m_briefopCustomCoalition.Included = Included;
//			m_briefopCustomCoalition.BullseyeDescription = BullseyeDescription;
//			m_briefopCustomCoalition.BullseyeWaypoint = BullseyeWaypoint;

//			if (ComPresets is object && ComPresets.Count > 0)
//				m_briefopCustomCoalition.ComPresets = ComPresets.GetCopy();

//			foreach (Asset asset in OwnAssets)
//				asset.Persist();
//			foreach (Asset asset in OpposingAssets)
//				asset.Persist();
//			foreach (Asset asset in Airdromes)
//				asset.Persist();
//		}

//		private List<Asset> BuildCoalitionAssets(MizCoalition mizCoalition, ElementAssetSide side)
//		{
//			List<Asset> assets = new List<Asset>();

//			if (mizCoalition is object)
//			{
//				foreach (MizCountry c in mizCoalition.Countries)
//				{
//					foreach (MizGroupFlight g in c.GroupFlights)
//					{
//						assets.Add(new AssetFlight(Core, this, side, g));
//					}
//					foreach (MizGroupShip g in c.GroupShips)
//					{
//						assets.Add(new AssetShip(Core, this, side, g));
//					}
//					foreach (MizGroupVehicle g in c.GroupVehicles)
//					{
//						assets.Add(new AssetVehicle(Core, this, side, g));
//					}
//				}
//			}

//			return assets;
//		}

//		public string GetBullseyeCoordinatesString()
//		{
//			return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}";
//		}

//		public void ResetBullseyeMarkerDescription()
//		{
//			if (m_markerkBullseye is object)
//				m_markerkBullseye.Label = BullseyeDescription;
//		}

//		public void SetMapProvider(string sProviderName)
//		{
//			MapData.Provider = sProviderName;
//			foreach (AssetFlight flight in OwnAssets.OfType<AssetFlight>().Where(_f => _f.MissionData is object))
//			{
//				flight.MissionData.MapData.Provider = sProviderName;
//			}
//		}
//		#endregion
//	}
//}
