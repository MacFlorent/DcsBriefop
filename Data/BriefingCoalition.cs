using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class BriefingCoalition : BaseBriefing
	{
		#region Fields
		private MizCoalition m_mizCoalition;
		private MizCoalition m_mizOpposingCoalition;
		private BriefopCustomCoalition m_briefopCustomCoalition;
		private GMarkerBriefop m_markerkBullseye;

		#endregion

		#region Properties
		public string CoalitionName { get; set; }
		public Coordinate Bullseye { get; set; }
		public string BullseyeDescription { get; set; }
		public string Task { get; set; }

		public Color OwnColor { get; set; }
		public Color OpposingColor { get; set; }

		public List<Asset> OwnAssets { get; private set; } = new List<Asset>();
		public List<Asset> OpposingAssets { get; private set; } = new List<Asset>();
		public List<AssetAirdrome> Airdromes { get; private set; } = new List<AssetAirdrome>();

		public BriefopCustomMap MapData { get { return m_briefopCustomCoalition.MapData; } }
		#endregion

		#region CTOR
		public BriefingCoalition(BaseBriefingCore core, string sCoalitionName) : base(core)
		{
			CoalitionName = sCoalitionName;
			string sOpposingCoalitionName = ToolsBriefop.GetOpposingCoalitionName(CoalitionName);
			m_mizCoalition = Core.Miz.RootMission.Coalitions.Where(c => c.Name == CoalitionName).FirstOrDefault();
			m_mizOpposingCoalition = Core.Miz.RootMission.Coalitions.Where(c => c.Name == sOpposingCoalitionName).FirstOrDefault();

			Initialize();
		}
		#endregion

		#region Initialize
		private void Initialize()
		{
			InitializeDataCustom();
			InitializeData();
		}

		private void InitializeDataCustom()
		{
			m_briefopCustomCoalition = Core.Miz.BriefopCustom.GetCoalition(CoalitionName);
			if (m_briefopCustomCoalition is null)
			{
				m_briefopCustomCoalition = new BriefopCustomCoalition() { CoalitionName = CoalitionName };
				Core.Miz.BriefopCustom.Coalitions.Add(m_briefopCustomCoalition);
			}
		}

		private void InitializeData()
		{
			string sOpposingCoalitionName = m_mizOpposingCoalition.Name;

			Bullseye = Core.Theatre.GetCoordinate(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
			if (CoalitionName == ElementCoalition.Red)
				Task = Core.Miz.RootDictionary.RedTask;
			else if (CoalitionName == ElementCoalition.Blue)
				Task = Core.Miz.RootDictionary.BlueTask;
			else if (CoalitionName == ElementCoalition.Neutral)
				Task = Core.Miz.RootDictionary.NeutralTask;

			OwnColor = ToolsBriefop.GetCoalitionColor(CoalitionName);
			OpposingColor = ToolsBriefop.GetCoalitionColor(sOpposingCoalitionName);

			BullseyeDescription = m_briefopCustomCoalition.BullseyeDescription;

			InitializeMapData();

			OwnAssets = BuildCoalitionAssets(m_mizCoalition, ElementAssetSide.Own);
			OpposingAssets = BuildCoalitionAssets(m_mizOpposingCoalition, ElementAssetSide.Opposing);

			// airdromes must be intialized after assets so they can refer assets lists to determine if they are a base
			foreach (Airdrome airdrome in Core.Theatre.Airdromes)
			{
				Airdromes.Add(new AssetAirdrome(Core, this, airdrome));
			}

			InitializeMapDataChildrenOverlays();
			InitializeFlightDataMissions();
		}

		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
			m_markerkBullseye = new GMarkerBriefop(p, MarkerBriefopType.bullseye.ToString(), OwnColor, BullseyeDescription);
			staticOverlay.Markers.Add(m_markerkBullseye);

			if (MapData is null)
			{
				m_briefopCustomCoalition.MapData = new BriefopCustomMap();
				MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
				MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
				MapData.Zoom = ElementMapValue.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(Core.Miz.BriefopCustom.MapData.MapOverlayCustom);
		}

		private void InitializeMapDataChildrenOverlays()
		{
			foreach (Asset asset in OwnAssets)
			{
				if (asset.MapOverlayStatic is object)
					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
			}
			foreach (Asset asset in OpposingAssets)
			{
				if (asset.MapOverlayStatic is object)
					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
			}
			foreach (Asset asset in Airdromes)
			{
				if (asset.MapOverlayStatic is object)
					MapData.AdditionalMapOverlays.Add(asset.MapOverlayStatic);
			}
		}

		private void InitializeFlightDataMissions()
		{
			foreach (AssetFlight asset in OwnAssets.OfType<AssetFlight>())
			{
				asset.SetMissionData();
			}
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			if (CoalitionName == ElementCoalition.Red)
				Core.Miz.RootDictionary.RedTask = Task;
			else if (CoalitionName == ElementCoalition.Blue)
				Core.Miz.RootDictionary.BlueTask = Task;
			else if (CoalitionName == ElementCoalition.Neutral)
				Core.Miz.RootDictionary.NeutralTask = Task;

			m_briefopCustomCoalition.BullseyeDescription = BullseyeDescription;

			foreach (Asset asset in OwnAssets)
				asset.Persist();
			foreach (Asset asset in OpposingAssets)
				asset.Persist();
			foreach (Asset asset in Airdromes)
				asset.Persist();

		}

		private List<Asset> BuildCoalitionAssets(MizCoalition mizCoalition, ElementAssetSide side)
		{
			List<Asset> assets = new List<Asset>();

			if (mizCoalition is object)
			{
				foreach (MizCountry c in mizCoalition.Countries)
				{
					foreach (MizGroupFlight g in c.GroupFlights)
					{
						assets.Add(new AssetFlight(Core, this, side, g));
					}
					foreach (MizGroupShip g in c.GroupShips)
					{
						assets.Add(new AssetShip(Core, this, side, g));
					}
					foreach (MizGroupVehicle g in c.GroupVehicles)
					{
						assets.Add(new AssetVehicle(Core, this, side, g));
					}
				}
			}

			return assets;
		}

		public string GetBullseyeCoordinatesString()
		{
			return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}";
		}
		#endregion
	}
}
