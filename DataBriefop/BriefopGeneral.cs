using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using System;
using System.Linq;

namespace DcsBriefop.DataBriefop
{
	internal class BriefopGeneral : BaseBriefop
	{
		#region Properties
		public string Sortie { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }

		public BriefopWeather Weather { get; private set; }

		public BriefopCustomMap MapData { get { return ParentManager.Miz.BriefopCustomData.MapData; } }
		#endregion

		#region CTOR
		public BriefopGeneral(BriefopManager parentManager) : base(parentManager)
		{
			Weather = new BriefopWeather(ParentManager.Miz.RootMission.Weather);
			Sortie = ParentManager.Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay(ParentManager.Miz.RootDictionary.Description);
			Date = new DateTime(ParentManager.Miz.RootMission.Date.Year, ParentManager.Miz.RootMission.Date.Month, ParentManager.Miz.RootMission.Date.Day).AddSeconds(ParentManager.Miz.RootMission.StartTime);
		}
		#endregion

		#region Initialize
		public override void PostInitialize()
		{
			InitializeMapData();
		}

		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

			ToolsMap.AddMizDrawingLayers(ParentManager.Theatre, staticOverlay, ParentManager.Miz.RootMission.DrawingLayers.Where(_dl => _dl.Name == ElementDrawingLayer.Common).ToList());

			if (MapData is null)
			{
				ParentManager.Miz.BriefopCustomData.MapData = new BriefopCustomMap();
				ParentManager.Miz.BriefopCustomData.MapData.Provider = ParentManager.Miz.BriefopCustomData.DefaultMapProvider;
				Coordinate coordinateCenter = ParentManager.Theatre.GetCoordinate(ParentManager.Miz.RootMission.Map.CenterY, ParentManager.Miz.RootMission.Map.CenterX);
				MapData.CenterLatitude = coordinateCenter.Latitude.DecimalDegree;
				MapData.CenterLongitude = coordinateCenter.Longitude.DecimalDegree;
				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);

		}
		#endregion

		#region Methods
		public override void Persist()
		{
			ParentManager.Miz.RootDictionary.Sortie = Sortie;
			ParentManager.Miz.RootDictionary.Description = ToolsLua.DisplayToDcsText(Description);
			ParentManager.Miz.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
			ParentManager.Miz.RootMission.StartTime = Convert.ToInt32((Date - ParentManager.Miz.RootMission.Date).TotalSeconds);
		}

		public void SetMapProvider(string sProviderName)
		{
			MapData.Provider = sProviderName;
		}
		#endregion

	}
}