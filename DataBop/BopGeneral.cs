using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataBopCustom;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using System;
using System.Linq;

namespace DcsBriefop.DataBop
{
	internal class BopGeneral : BaseBop
	{
		#region Fields
		private BopCustomGeneral m_customGeneral;
		#endregion

		#region Properties
		public string Sortie { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }

		public BopWeather Weather { get; private set; }

		public BopCustomMap MapData { get { return m_customGeneral.MapData; } }
		#endregion

		#region CTOR
		public BopGeneral(BopManager parentManager) : base(parentManager)
		{
			InitializeCustom();

			Weather = new BopWeather(ParentManager.Miz.RootMission.Weather);
			Sortie = ParentManager.Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay(ParentManager.Miz.RootDictionary.Description);
			Date = new DateTime(ParentManager.Miz.RootMission.Date.Year, ParentManager.Miz.RootMission.Date.Month, ParentManager.Miz.RootMission.Date.Day).AddSeconds(ParentManager.Miz.RootMission.StartTime);
		}
		#endregion

		#region Initialize & Persist
		private void InitializeCustom()
		{
			if (ParentManager.BopCustomMain.BopCustomGeneral is null)
			{
				ParentManager.BopCustomMain.BopCustomGeneral = new BopCustomGeneral();
			}
			m_customGeneral = ParentManager.BopCustomMain.BopCustomGeneral;
		}

		public override void PostInitialize()
		{
			base.PostInitialize();

			InitializeMapData();
		}

		public override void Persist()
		{
			ParentManager.Miz.RootDictionary.Sortie = Sortie;
			ParentManager.Miz.RootDictionary.Description = ToolsLua.DisplayToDcsText(Description);
			ParentManager.Miz.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
			ParentManager.Miz.RootMission.StartTime = Convert.ToInt32((Date - ParentManager.Miz.RootMission.Date).TotalSeconds);
		}
		#endregion

		#region Methods
		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

			ToolsMap.AddMizDrawingLayers(ParentManager.Theatre, staticOverlay, ParentManager.Miz.RootMission.DrawingLayers.Where(_dl => _dl.Name == ElementDrawingLayer.Common).ToList());

			if (MapData is null)
			{
				m_customGeneral.MapData = new BopCustomMap();
				MapData.Provider = ParentManager.BopCustomMain.DefaultMapProvider;
				Coordinate coordinateCenter = ParentManager.Theatre.GetCoordinate(ParentManager.Miz.RootMission.Map.CenterY, ParentManager.Miz.RootMission.Map.CenterX);
				MapData.CenterLatitude = coordinateCenter.Latitude.DecimalDegree;
				MapData.CenterLongitude = coordinateCenter.Longitude.DecimalDegree;
				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);
		}

		public void SetMapProvider(string sProviderName)
		{
			MapData.Provider = sProviderName;
		}
		#endregion
	}
}