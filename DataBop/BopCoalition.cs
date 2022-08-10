using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataBopCustom;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.DataBop
{
	internal class BopCoalition : BaseBop
	{
		#region Fields
		private MizCoalition m_mizCoalition;
		private BopCustomCoalition m_customCoalition;
		private GMarkerBriefop m_markerkBullseye;
		#endregion

		#region Properties
		public string CoalitionName { get; set; }
		public Coordinate Bullseye { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public string Task { get; set; }

		//public ListComPreset ComPresets { get; set; }

		public BopCustomMap MapData { get { return m_customCoalition.MapData; } }
		#endregion

		#region CTOR
		public BopCoalition(BopManager parentManager, MizCoalition mizCoalition) : base(parentManager)
		{
			m_mizCoalition = mizCoalition;
			InitializeCustom();

			CoalitionName = m_mizCoalition.Name;

			Bullseye = ParentManager.Theatre.GetCoordinate(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
			if (CoalitionName == ElementCoalition.Red)
				Task = ParentManager.Miz.RootDictionary.RedTask;
			else if (CoalitionName == ElementCoalition.Blue)
				Task = ParentManager.Miz.RootDictionary.BlueTask;
			else if (CoalitionName == ElementCoalition.Neutral)
				Task = ParentManager.Miz.RootDictionary.NeutralTask;

			Task = ToolsLua.DcsTextToDisplay(Task);

			BullseyeDescription = m_customCoalition.BullseyeDescription;
			BullseyeWaypoint = m_customCoalition.BullseyeWaypoint;
		}
		#endregion

		#region Initialize & Persist
		private void InitializeCustom()
		{
			m_customCoalition = ParentManager.BopCustomMain.GetCoalition(m_mizCoalition.Name);
			if (m_customCoalition is null)
			{
				m_customCoalition = new BopCustomCoalition() { CoalitionName = CoalitionName };
				ParentManager.BopCustomMain.Coalitions.Add(m_customCoalition);
			}

			//if (m_briefopCustomCoalition.ComPresets is object)
			//	ComPresets = m_briefopCustomCoalition.ComPresets.GetCopy();
		}

		public override void PostInitialize()
		{
			InitializeMapData();
			InitializeBullseyeWaypoints();
			//ComPresets?.Compute(this);
		}

		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
			m_markerkBullseye = GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.Bullseye, Color.Pink, BullseyeDescription, 1, 0);
			staticOverlay.Markers.Add(m_markerkBullseye);

			ToolsMap.AddMizDrawingLayers(ParentManager.Theatre, staticOverlay, ParentManager.Miz.RootMission.DrawingLayers.Where(_dl => _dl.Name.ToUpper() == CoalitionName.ToUpper()).ToList());

			if (MapData is null)
			{
				m_customCoalition.MapData = new BopCustomMap();
				MapData.Provider = ParentManager.BopCustomMain.DefaultMapProvider;
				MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
				MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
			MapData.AdditionalMapOverlays.Add(ParentManager.BopCustomMain.BopCustomGeneral.MapData.MapOverlayCustom);
			MapData.AdditionalMapOverlays.Add(ParentManager.BopCustomMain.BopCustomGeneral.MapData.GetStaticOverlay());
		}

		public void InitializeBullseyeWaypoints()
		{
			//foreach (AssetFlight flight in OwnAssets.OfType<AssetFlight>())
			//{
			//	flight.InitializeBullseyeWaypoint(BullseyeWaypoint);
			//}
		}

		public override void Persist()
		{
			//if (CoalitionName == ElementCoalition.Red)
			//	Core.Miz.RootDictionary.RedTask = ToolsLua.DisplayToDcsText(Task);
			//else if (CoalitionName == ElementCoalition.Blue)
			//	Core.Miz.RootDictionary.BlueTask = ToolsLua.DisplayToDcsText(Task);
			//else if (CoalitionName == ElementCoalition.Neutral)
			//	Core.Miz.RootDictionary.NeutralTask = ToolsLua.DisplayToDcsText(Task);

			//m_briefopCustomCoalition.Included = Included;
			//m_briefopCustomCoalition.BullseyeDescription = BullseyeDescription;
			//m_briefopCustomCoalition.BullseyeWaypoint = BullseyeWaypoint;

			//if (ComPresets is object && ComPresets.Count > 0)
			//	m_briefopCustomCoalition.ComPresets = ComPresets.GetCopy();

			//foreach (Asset asset in OwnAssets)
			//	asset.Persist();
			//foreach (Asset asset in OpposingAssets)
			//	asset.Persist();
			//foreach (Asset asset in Airdromes)
			//	asset.Persist();
		}
		#endregion

		#region Methods


		public string GetBullseyeCoordinatesString()
		{
			return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}";
		}

		public void ResetBullseyeMarkerDescription()
		{
			if (m_markerkBullseye is object)
				m_markerkBullseye.Label = BullseyeDescription;
		}

		public void SetMapProvider(string sProviderName)
		{
			MapData.Provider = sProviderName;
		}
		#endregion
	}
}
