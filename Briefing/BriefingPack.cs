using CoordinateSharp;
using DcsBriefop.MasterData;
using GMap.NET.WindowsForms;
using System;

namespace DcsBriefop.Briefing
{
	internal class BriefingPack : BaseBriefing
	{
		#region Properties
		public string Sortie
		{
			get { return RootDictionary.Sortie; }
		}

		public string Description
		{
			get { return RootDictionary.Description; }
			set { RootDictionary.Description = value; }
		}

		public DateTime Date
		{
			get { return new DateTime(RootMission.Date.Year, RootMission.Date.Month, RootMission.Date.Day).AddSeconds(RootMission.StartTime); }
			set
			{
				RootMission.Date = new DateTime(value.Year, value.Month, value.Day);
				RootMission.StartTime = Convert.ToInt32((value - RootMission.Date).TotalSeconds);
			}
		}

		public bool DisplayRed
		{
			get { return RootCustom.DisplayRed.GetValueOrDefault(); }
			set { RootCustom.DisplayRed = value; }
		}
		public bool DisplayBlue
		{
			get { return RootCustom.DisplayBlue.GetValueOrDefault(); }
			set { RootCustom.DisplayBlue = value; }
		}
		public bool DisplayNeutral
		{
			get { return RootCustom.DisplayNeutral.GetValueOrDefault(); }
			set { RootCustom.DisplayNeutral = value; }
		}

		public CustomDataMap MapData
		{
			get { return RootCustom.MapData; }
			set { RootCustom.MapData = value; }
		}


		public BriefingWeather Weather { get; private set; }
		public BriefingCoalition BriefingRed { get; private set; }
		public BriefingCoalition BriefingBlue { get; private set; }
		public BriefingCoalition BriefingNeutral { get; private set; }
		#endregion

		#region CTOR
		public BriefingPack(MissionManager manager) : base(manager)
		{
			InitializeMapData();

			Weather = new BriefingWeather(this);
			BriefingRed = new BriefingCoalition(this, ElementCoalition.Red);
			BriefingBlue = new BriefingCoalition(this, ElementCoalition.Blue);
			BriefingNeutral = new BriefingCoalition(this, ElementCoalition.Neutral);
		}
		#endregion

		#region Methods
		private void InitializeMapData()
		{
			if (MapData is null)
			{
				MapData = new CustomDataMap();
				Coordinate coordinateCenter = Theatre.GetCoordinate(RootMission.Map.CenterY, RootMission.Map.CenterX);
				MapData.CenterLatitude = coordinateCenter.Latitude.DecimalDegree;
				MapData.CenterLongitude = coordinateCenter.Longitude.DecimalDegree;
				MapData.Zoom = 7;
				MapData.MapOverlayCustom = new GMapOverlay();
			}
		}

		#endregion
	}
}
