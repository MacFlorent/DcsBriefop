using DcsBriefop.Map;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace DcsBriefop.Briefing
{
	//https://github.com/itext/itext7-dotnet
	//HtmlConverter.ConvertToPdf(new FileInfo(sFilePath), new FileInfo(sPdfFileName));

	internal class BriefingFile : IDisposable
	{
		public string FileName { get; set; }
		public string KneeboardFolder { get; set; }
		public Bitmap BitmapContent { get; set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				BitmapContent.Dispose();
			}
		}
	}

	internal class BriefingFileHtml : BriefingFile
	{
		public string HtmlContent { get; set; }
	}

	internal class BriefingFilesBuilder : IDisposable
	{
		private static class KneeboardFolders
		{
			public static readonly string Images = "IMAGES";
			public static Dictionary<string, string> DictionaryAircrafts = new Dictionary<string, string>()
			{

			};
		}

		#region Fields
		private BriefingPack m_briefingPack;
		private MissionManager m_missionManager;
		private List<BriefingFile> m_listFiles = new List<BriefingFile>();
		#endregion

		#region CTOR
		public BriefingFilesBuilder(BriefingPack briefingPack, MissionManager missionManager)
		{
			m_briefingPack = briefingPack;
			m_missionManager = missionManager;
		}
		#endregion

		#region Methods
		public void Generate(bool bUpdateMiz, bool bLocalExportDirectoryActive, bool bLocalExportDirectoryWithHtmlBitmaps, string sLocalExportDirectory)
		{
			if (bLocalExportDirectoryActive && !Directory.Exists(sLocalExportDirectory))
			{
					throw new ExceptionDcsBriefop ($"Local export directory not found {sLocalExportDirectory}.");
			}

			GenerateAllFiles();

			if (bLocalExportDirectoryActive)
				ExportFilesToPath(sLocalExportDirectory, bLocalExportDirectoryWithHtmlBitmaps);
			if (bUpdateMiz)
				ExportFilesToMiz();
		}

		private void GenerateAllFiles()
		{
			AddMapData("00_GENERAL_MAP", KneeboardFolders.Images, m_briefingPack.MapData);

			if (m_briefingPack.DisplayRed)
			{
				GenerateAllFilesCoalition(m_briefingPack.BriefingRed);
			}
			if (m_briefingPack.DisplayBlue)
			{
				GenerateAllFilesCoalition(m_briefingPack.BriefingBlue);
			}
			if (m_briefingPack.DisplayNeutral)
			{
				GenerateAllFilesCoalition(m_briefingPack.BriefingNeutral);
			}
		}

		private void GenerateAllFilesCoalition(BriefingCoalition coalition)
		{
			string sKneeboardFolder = KneeboardFolders.Images;
			string sFileName = $"{coalition.Name}_01_SITUATION";
			AddFileHtml(sFileName, sKneeboardFolder, GenerateHtmlSituation(coalition));
			AddMapData($"{sFileName}_MAP", sKneeboardFolder, coalition.MapData);

			sKneeboardFolder = KneeboardFolders.Images;
			sFileName = $"{coalition.Name}_02_OPERATIONS";
			AddFileHtml(sFileName, sKneeboardFolder, GenerateHtmlOperations(coalition));

			foreach (AssetGroup asset in coalition.Assets.OfType<AssetGroup>().Where(_a => _a.Category == ElementAssetCategory.Mission))
			{
				sKneeboardFolder = KneeboardFolders.Images; // TODO aircrat specific kneeboard folder
				sFileName = $"{coalition.Name}_03_MISSION_{asset.Name}";
				AddFileHtml(sFileName, sKneeboardFolder, GenerateHtmlMission(coalition, asset));
				AddMapData($"{sFileName}_MAP", sKneeboardFolder, asset.MapDataMission);
			}
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlSituation(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			//hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("SITUATION", 2);
			hb.AppendParagraphJustified(m_briefingPack.Description);
			hb.AppendHeader("TASKS", 3);
			hb.AppendParagraphJustified(coalition.Task);
			hb.AppendHeader("WEATHER", 3);
			hb.AppendParagraphCentered(m_briefingPack.Weather.ToString());

			hb.FinalizeDocument(); 
			return hb;
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlOperations(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			//hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("OPERATIONS", 2);
			hb.AppendHeader("BULLSEYE", 3);
			hb.AppendParagraphCentered(coalition.BullseyeCoordinates);
			hb.AppendParagraphCentered(coalition.BullseyeDescription);
			hb.AppendHeader("ASSETS", 3);

			hb.OpenTable("Name", "Task", "Type", "Notes");
			hb.AppendTableRow("Missions");
			foreach (Asset asset in coalition.Assets.Where(_a =>_a.Category == ElementAssetCategory.Mission))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.Information);
			}

			hb.AppendTableRow("Support");
			foreach (Asset asset in coalition.Assets.Where(_a => _a.Category == ElementAssetCategory.Support))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.Information);
			}

			hb.AppendTableRow("Base");
			foreach (Asset asset in coalition.Assets.Where(_a => _a.Category == ElementAssetCategory.Base))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.Information);
			}

			hb.CloseTable();

			hb.FinalizeDocument();
			return hb;
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlMission(BriefingCoalition coalition, AssetGroup asset)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.AppendHeader($"MISSION {asset.Name} | {asset.Task}", 2);
			hb.AppendParagraphCentered(asset.MissionInformation);
			hb.AppendHeader("WAYPOINTS", 3);

			hb.OpenTable("#", "Waypoint", "Action", "Alt.", "Notes");

			foreach (AssetRoutePoint routePoint in asset.MapPoints.OfType<AssetRoutePoint>())
			{
				hb.AppendTableRow(routePoint.Number.ToString(), routePoint.Name, routePoint.Action, routePoint.AltitudeFeet, routePoint.Notes);
			}


			hb.CloseTable();

			hb.FinalizeDocument();
			return hb;
		}

		private void AddFileHtml(string sFileName, string sKneeboardFolder, HtmlBuilder.HtmlDocument hb)
		{
			string sHtmlContent = hb.ToString();
			BriefingFileHtml bf = new BriefingFileHtml() { FileName = sFileName, KneeboardFolder = sKneeboardFolder, HtmlContent = sHtmlContent };
			bf.BitmapContent = HtmlRender.RenderToImage(sHtmlContent, new Size(ElementImageSize.Width, ElementImageSize.Height), Color.LightGray) as Bitmap;
			//Bitmap b = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImageGdiPlus(hb.ToString(), new Size(800, 1200)) as Bitmap;
			m_listFiles.Add(bf);
		}

		private void AddMapData(string sFileName, string sKneeboardFolder, CustomDataMap mapData)
		{
			BriefingFile bf = new BriefingFile() { FileName = sFileName, KneeboardFolder = sKneeboardFolder, BitmapContent = ToolsMap.GenerateMapImage(mapData) };
			m_listFiles.Add(bf);
		}

		private void ExportFilesToPath(string sPath, bool bWithHtmlBitmaps)
		{
			if (!Directory.Exists(sPath))
				throw new ExceptionDcsBriefop($"Path does not exists : {sPath}");

			foreach (BriefingFile briefingFile in m_listFiles)
			{
				string sFilePath;
				if (briefingFile is BriefingFileHtml briefingFileHtml)
				{
					sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.html");
					if (File.Exists(sFilePath))
						File.Delete(sFilePath);

					File.WriteAllText(sFilePath, briefingFileHtml.HtmlContent);

					if (bWithHtmlBitmaps)
					{
						sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.jpg");
						if (File.Exists(sFilePath))
							File.Delete(sFilePath);

						briefingFile.BitmapContent.Save(sFilePath, ImageFormat.Jpeg);
					}
				}
				else
				{
					sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.jpg");
					if (File.Exists(sFilePath))
						File.Delete(sFilePath);

					briefingFile.BitmapContent.Save(sFilePath, ImageFormat.Jpeg);
				}
			}

		}

		private void ExportFilesToMiz()
		{
			if (!File.Exists(m_missionManager.MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {m_missionManager.MizFilePath}");

			using (ZipArchive za = ZipFile.Open(m_missionManager.MizFilePath, ZipArchiveMode.Update))
			{
				foreach (BriefingFile briefingFile in m_listFiles)
				{
					string sZipEntry = $@"KNEEBOARD/{briefingFile.KneeboardFolder}/{briefingFile.FileName}";

					if (briefingFile is BriefingFileHtml bfHtml)
					{
						using (Bitmap b = HtmlRender.RenderToImage(bfHtml.HtmlContent, new Size(ElementImageSize.Width, ElementImageSize.Height)) as Bitmap)
						{
							string sTempPath = Path.GetTempFileName();
							b.Save(sTempPath);

							ToolsZip.RemoveZipEntries(za, sZipEntry);
							za.CreateEntryFromFile(sTempPath, sZipEntry);
						}
					}
					else if (briefingFile is BriefingFile bf) { }
				}

			}
		}
		#endregion

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (BriefingFile briefingFile in m_listFiles)
				{
					briefingFile.Dispose();
				}
			}
		}
	}
}
