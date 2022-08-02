using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class BriefingContainer : BaseBriefing
	{
		#region Properties
		public MissionContent Mission { get; set; }
		public List<BriefingCoalition> BriefingCoalitions { get; private set; } = new List<BriefingCoalition>();

		public BriefopCustomMap MapData { get { return Core.Miz.BriefopCustomData.MapData; } }
		#endregion

		#region CTOR
		public BriefingContainer(Miz miz) : base(new BaseBriefingCore(miz))
		{
			Initialize();
		}
		#endregion

		#region Initialize
		private void Initialize()
		{
			var stopWatch = System.Diagnostics.Stopwatch.StartNew();
			Log.Debug("Initialize all briefing data start");
			Mission = new MissionContent(Core);
			InitializeMapData(); // initialize map data before coalitions as they will need main map overlays

			InitializeCoalition(ElementCoalition.Red);
			InitializeCoalition(ElementCoalition.Blue);
			//InitializeCoalition(ElementCoalition.Neutral);
			stopWatch.Stop();
			Log.Debug($@"Initialize all briefing data end [{stopWatch.Elapsed:hh\:mm\:ss\.ff}]");
		}

		private void InitializeMapData()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);

			ToolsMap.AddMizDrawingLayers(Core.Theatre, staticOverlay, Core.Miz.RootMission.DrawingLayers.Where(_dl => _dl.Name == ElementDrawingLayer.Common).ToList());

			if (MapData is null)
			{
				Core.Miz.BriefopCustomData.MapData = new BriefopCustomMap();
				Core.Miz.BriefopCustomData.MapData.Provider = Core.Miz.BriefopCustomData.DefaultMapProvider;
				Coordinate coordinateCenter = Core.Theatre.GetCoordinate(Core.Miz.RootMission.Map.CenterY, Core.Miz.RootMission.Map.CenterX);
				MapData.CenterLatitude = coordinateCenter.Latitude.DecimalDegree;
				MapData.CenterLongitude = coordinateCenter.Longitude.DecimalDegree;
				MapData.Zoom = Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
				MapData.MapOverlayCustom = new GMapOverlay();
			}

			MapData.AdditionalMapOverlays.Clear();
			MapData.AdditionalMapOverlays.Add(staticOverlay);

		}

		private void InitializeCoalition(string sCoalitionName)
		{
			BriefopCustomCoalition customCoalitionData = Core.Miz.BriefopCustomData.GetCoalition(sCoalitionName);
			if (customCoalitionData is object)
			{
				if (customCoalitionData.Included)
					AddCoalition(sCoalitionName);
			}
			else
			{
				string sTask = null;
				if (sCoalitionName == ElementCoalition.Red)
					sTask = Core.Miz.RootDictionary.RedTask;
				else if (sCoalitionName == ElementCoalition.Blue)
					sTask = Core.Miz.RootDictionary.BlueTask;
				else if (sCoalitionName == ElementCoalition.Neutral)
					sTask = Core.Miz.RootDictionary.NeutralTask;

				if (!string.IsNullOrEmpty(sTask))
					AddCoalition(sCoalitionName);
			}
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			Mission.Persist();

			foreach (BriefingCoalition coalition in BriefingCoalitions)
				coalition.Persist();
		}

		public void AddCoalition(string sCoalitionName)
		{
			if (GetCoalition(sCoalitionName) is null)
				BriefingCoalitions.Add(new BriefingCoalition(Core, sCoalitionName));
		}

		public BriefingCoalition GetCoalition(string sCoalitionName)
		{
			return BriefingCoalitions.Where(c => c.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public void SetMapProvider(string sProviderName)
		{
			MapData.Provider = sProviderName;
			foreach (BriefingCoalition coalition in BriefingCoalitions)
			{
				coalition.SetMapProvider(sProviderName);
			}
		}
		#endregion

	}
}