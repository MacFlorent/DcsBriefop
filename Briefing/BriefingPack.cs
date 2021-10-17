using CoordinateSharp;
using DcsBriefop.MasterData;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

		#region Generate briefing files
		public void GenerateBriefingFiles()
		{
			string sPath = @"D:\Projects"; //Path.GetTempPath();
			GenerateBriefingFiles(sPath);
		}

		public void GenerateBriefingFiles(string sPath)
		{
			Dictionary<string, string> listFiles = new Dictionary<string, string>();
			if (DisplayRed)
			{
				listFiles.Add("01_SITUATION_RED", GetBriefingSituation(BriefingRed));
			}
			if (DisplayBlue)
			{
				listFiles.Add("01_SITUATION_BLUE", GetBriefingSituation(BriefingBlue));
				listFiles.Add("02_OPERATIONS_BLUE", GetBriefingOperations(BriefingBlue));
			}
			if (DisplayNeutral)
				listFiles.Add("SITUATION_NEUTRAL", GetBriefingSituation(BriefingNeutral));

			foreach (KeyValuePair<string, string> kvp in listFiles)
			{
				string sFilePath = Path.Combine(sPath, $"{kvp.Key}.bmp");
				if (File.Exists(sFilePath))
					File.Delete(sFilePath);

				//Bitmap b = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImageGdiPlus(hb.ToString(), new Size(800, 1200)) as Bitmap;
				using (Bitmap b = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage(kvp.Value, new Size(800, 1200), Color.LightGray) as Bitmap)
				{
					b.Save(sFilePath);
				}
			}

			//https://github.com/itext/itext7-dotnet
			//string sPdfFileName = Path.Combine(sPath, $"brieftest_situation.pdf");
			//if (File.Exists(sPdfFileName))
			//	File.Delete(sPdfFileName);
			//HtmlConverter.ConvertToPdf(new FileInfo(sFilePath), new FileInfo(sPdfFileName));
		}

		public string GetBriefingSituation(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.OpenTag("html", "");
			hb.OpenTag("body", "");

			hb.AppendHeader(Sortie, 3);
			hb.AppendHeader("SITUATION", 2);
			hb.AppendParagraphJustified(Description);
			hb.AppendHeader("BLUE OPS", 3);
			hb.AppendParagraphJustified(coalition.Task);
			hb.AppendHeader("WEATHER", 3);
			hb.AppendParagraphCentered(Weather.ToString());

			hb.CloseTag();
			hb.CloseTag();

			return hb.ToString();
		}

		public string GetBriefingOperations(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.OpenTag("html", "");
			hb.OpenTag("body", "");

			hb.AppendHeader(Sortie, 3);
			hb.AppendHeader("OPERATIONS", 2);
			hb.AppendHeader("BULSEYE", 3);
			hb.AppendParagraphCentered(coalition.BullseyeCoordinates);
			hb.AppendParagraphCentered(coalition.BullseyeDescription);
			hb.AppendHeader("OPERATIONS", 3);
			// tab with groups

			hb.CloseTag();
			hb.CloseTag();

			return hb.ToString();
		}
		#endregion
	}
}
