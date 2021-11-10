using CoordinateSharp;
using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingCoalition : BaseBriefing
	{
		#region Fields
		private Coalition m_coalition;
		private CustomDataCoalition m_customDataCoalition;
		private GMarkerBriefop m_markerkBullseye;
		#endregion

		#region Properties
		public Coordinate Bullseye
		{
			get { return Theatre.GetCoordinate(m_coalition.BullseyeY, m_coalition.BullseyeX); }
		}

		public string BullseyeCoordinates
		{
			get { return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}"; }
		}

		public string BullseyeDescription
		{
			get { return RootCustom.GetCoalition(m_coalition.Code)?.BullseyeDescription; }
			set
			{
				RootCustom.GetCoalition(m_coalition.Code).BullseyeDescription = value;
				m_markerkBullseye.Label = value;
			}
		}

		public string Name
		{
			get { return m_coalition.Code; }
		}

		public string Task
		{
			get
			{
				if (Name == ElementCoalition.Red)
					return RootDictionary.RedTask;
				else if (Name == ElementCoalition.Blue)
					return RootDictionary.BlueTask;
				else if (Name == ElementCoalition.Neutral)
					return RootDictionary.NeutralTask;
				else
					return null;
			}
			set
			{
				if (Name == ElementCoalition.Red)
					RootDictionary.RedTask = value;
				else if (Name == ElementCoalition.Blue)
					RootDictionary.BlueTask = value;
				else if (Name == ElementCoalition.Neutral)
					RootDictionary.NeutralTask = value;
			}
		}

		public Color Color
		{
			get
			{
				if (Name == ElementCoalition.Red)
					return ElementCoalitionColor.Red;
				else if (Name == ElementCoalition.Blue)
					return ElementCoalitionColor.Blue;
				else
					return ElementCoalitionColor.Neutral;
			}
		}

		public List<Asset> OwnAssets { get; private set; } = new List<Asset>();
		public List<Asset> OpposingAssets { get; private set; } = new List<Asset>();
		public List<AssetAirdrome> Airdromes { get; private set; } = new List<AssetAirdrome>();

		public CustomDataMap MapData
		{
			get { return m_customDataCoalition.MapData; }
			set { m_customDataCoalition.MapData = value; }
		}
		#endregion

		#region CTOR
		public BriefingCoalition(BriefingPack briefingPack, string sCoalitionName) : base(briefingPack)
		{
			m_coalition = RootMission.Coalitions.Where(c => c.Code == sCoalitionName).FirstOrDefault();
			
			string sOtherCoalitionName = "";
			if (sCoalitionName == ElementCoalition.Blue)
				sOtherCoalitionName = ElementCoalition.Red;
			else if (sCoalitionName == ElementCoalition.Red)
				sOtherCoalitionName = ElementCoalition.Blue;
			Coalition otherCoalition = RootMission.Coalitions.Where(c => c.Code == sOtherCoalitionName).FirstOrDefault();

			m_customDataCoalition = RootCustom.GetCoalition(sCoalitionName);

			InitializeMapData();

			OwnAssets = BuildCoalitionAssets(briefingPack, m_coalition, ElementAssetSide.Own);
			OpposingAssets = BuildCoalitionAssets(briefingPack, otherCoalition, ElementAssetSide.Opposing);

			foreach (Airdrome airdrome in Theatre.Airdromes)
			{
				Airdromes.Add(new AssetAirdrome(briefingPack, this, ElementAssetSide.None, airdrome));
			}


			InitializeMapDataChildrenOverlays();
		}
		#endregion

		#region Methods
		private List<Asset> BuildCoalitionAssets(BriefingPack briefingPack, Coalition coalition, ElementAssetSide side)
		{
			List<Asset> assets = new List<Asset>();

			if (coalition is object)
			{
				foreach (Country c in coalition.Countries)
				{
					foreach (GroupFlight g in c.GroupFlights)
					{
						assets.Add(new AssetFlight(briefingPack, this, side, g));
					}
					foreach (GroupShip g in c.GroupShips)
					{
						assets.Add(new AssetShip(briefingPack, this, side, g));
					}
					foreach (GroupVehicle g in c.GroupVehicles)
					{
						assets.Add(new AssetVehicle(briefingPack, this, side, g));
					}
				}
			}

			return assets;
		}

		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
			m_markerkBullseye = new GMarkerBriefop(p, MarkerBriefopType.bullseye.ToString(), Color, BullseyeDescription);
			staticOverlay.Markers.Add(m_markerkBullseye);

			if (MapData is null)
			{
				MapData = new CustomDataMap();
				MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
				MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
				MapData.Zoom = ElementMapValue.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
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
		#endregion
	}
}
