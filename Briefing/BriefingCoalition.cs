using CoordinateSharp;
using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
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

		public List<AssetFlight> GroupFlights { get; private set; } = new List<AssetFlight>();
		public List<AssetShip> GroupShips { get; private set; } = new List<AssetShip>();
		// ships
		// planes

		public CustomDataMap MapData
		{
			get { return m_customDataCoalition.MapData; }
			set { m_customDataCoalition.MapData = value; }
		}
		#endregion

		#region CTOR
		public BriefingCoalition(BriefingPack bp, string sCoalitionName) : base(bp)
		{
			m_coalition = RootMission.Coalitions.Where(c => c.Code == sCoalitionName).FirstOrDefault();
			m_customDataCoalition = RootCustom.GetCoalition(sCoalitionName);

			InitializeMapData();

			foreach (Country c in m_coalition.Countries)
			{
				foreach (GroupFlight ga in c.GroupAirs)
				{
					GroupFlights.Add(new AssetFlight(bp, ga, this));
				}
				foreach (GroupShip gs in c.GroupShips)
				{
					GroupShips.Add(new AssetShip(bp, gs, this));
				}
			}

			InitializeMapDataChildrenOverlays();
		}
		#endregion

		#region Methods
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
			foreach (AssetFlight flight in GroupFlights.Where(_f => _f.MapDisplay != ElementAssetMapDisplay.None))
			{
				GMapOverlay staticGroupOverlay = flight.MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault();
				if (staticGroupOverlay is object)
					MapData.AdditionalMapOverlays.Add(staticGroupOverlay);
			}
			foreach (AssetShip ship in GroupShips.Where(_s => _s.MapDisplay != ElementAssetMapDisplay.None))
			{
				GMapOverlay staticGroupOverlay = ship.MapData.AdditionalMapOverlays.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault();
				if (staticGroupOverlay is object)
					MapData.AdditionalMapOverlays.Add(staticGroupOverlay);

			}
		}
		#endregion
	}
}
