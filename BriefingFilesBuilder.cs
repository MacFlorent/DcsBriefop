using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace DcsBriefop
{
	//https://github.com/itext/itext7-dotnet
	//HtmlConverter.ConvertToPdf(new FileInfo(sFilePath), new FileInfo(sPdfFileName));

	internal class BriefingFile : IDisposable
	{
		public string FileName { get; set; }
		public string KneeboardFolder { get; set; }
		public Bitmap BitmapContent { get; set; }

		public string GetKneeboardZipEntryName()
		{
			return $@"KNEEBOARD/{KneeboardFolder}/{FileName}.jpg";
		}

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
		private static readonly string m_sFilePrefix = "BOP_";

		private static class KneeboardFolders
		{
			public static readonly string Images = "IMAGES";
			public static Dictionary<string, string> DictionaryAircrafts = new Dictionary<string, string>()
			{

			};
		}

		#region Fields
		private BriefingContainer m_briefingContainer;
		private MissionManager m_missionManager;
		private List<BriefingFile> m_listFiles = new List<BriefingFile>();
		#endregion

		#region CTOR
		public BriefingFilesBuilder(BriefingContainer briefingContainer, MissionManager missionManager)
		{
			m_briefingContainer = briefingContainer;
			m_missionManager = missionManager;
		}
		#endregion

		#region Methods
		public void Generate(List<ElementExportFileType> exportFileTypes, bool bUpdateMiz, bool bLocalExportDirectoryActive, bool bLocalExportDirectoryWithHtml, string sLocalExportDirectory)
		{
			if (bLocalExportDirectoryActive && !Directory.Exists(sLocalExportDirectory))
			{
				throw new ExceptionDcsBriefop($"Local export directory not found {sLocalExportDirectory}.");
			}

			GenerateAllFiles(exportFileTypes);

			if (bLocalExportDirectoryActive)
				ExportFilesToPath(sLocalExportDirectory, bLocalExportDirectoryWithHtml);
			if (bUpdateMiz)
				ExportFilesToMiz();
		}

		private string GenerateFileName(ElementExportFileType fileType, string sCoalitionName)
		{
			return $"{m_sFilePrefix}{sCoalitionName}{(int)fileType:00}_{fileType}";
		}

		private void GenerateAllFiles(List<ElementExportFileType> exportFileTypes)
		{
			foreach(BriefingCoalition coalition in m_briefingContainer.BriefingCoalitions)
			{
				GenerateAllFilesCoalition(exportFileTypes, coalition);
			}
		}

		private void GenerateAllFilesCoalition(List<ElementExportFileType> exportFileTypes, BriefingCoalition coalition)
		{
			string sKneeboardFolder = KneeboardFolders.Images;

			if (exportFileTypes.Contains(ElementExportFileType.Situation))
				AddFileHtml(GenerateFileName(ElementExportFileType.Situation, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlSituation(coalition));
			if (exportFileTypes.Contains(ElementExportFileType.SituationMap))
				AddMapData(GenerateFileName(ElementExportFileType.SituationMap, coalition.CoalitionName), sKneeboardFolder, coalition.MapData);
			if (exportFileTypes.Contains(ElementExportFileType.Operations))
				AddFileHtml(GenerateFileName(ElementExportFileType.Operations, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlOperations(coalition));
			if (exportFileTypes.Contains(ElementExportFileType.Coms) && coalition.ComPresets is object && coalition.ComPresets.Count > 0)
				AddFileHtml(GenerateFileName(ElementExportFileType.Coms, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlComs(coalition));

			foreach (AssetFlight asset in coalition.OwnAssets.OfType<AssetFlight>().Where(_a => _a.MissionData is object))
			{
				sKneeboardFolder = KneeboardFolders.Images; // TODO aircraft specific kneeboard folder

				if (exportFileTypes.Contains(ElementExportFileType.Missions))
					AddFileHtml(GenerateFileName(ElementExportFileType.Missions, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlMission(coalition, asset));
				if (exportFileTypes.Contains(ElementExportFileType.MissionMaps))
					AddMapData(GenerateFileName(ElementExportFileType.MissionMaps, coalition.CoalitionName), sKneeboardFolder, asset.MissionData.MapData);
			}
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlSituation(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.OwnColor);

			//hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("SITUATION", 2);
			hb.AppendParagraphJustified(m_briefingContainer.Mission.Description);
			hb.AppendHeader("TASKS", 3);
			hb.AppendParagraphJustified(coalition.Task);
			hb.AppendHeader("WEATHER", 3);
			hb.AppendParagraphCentered(m_briefingContainer.Mission.Weather.ToString());

			hb.FinalizeDocument();
			return hb;
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlOperations(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.OwnColor);

			//hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("OPERATIONS", 2);
			hb.AppendHeader("BULLSEYE", 3);
			hb.AppendParagraphCentered(coalition.GetBullseyeCoordinatesString());
			hb.AppendParagraphCentered(coalition.BullseyeDescription);
			hb.AppendHeader("ASSETS", 3);

			hb.OpenTable("Name", "Task", "Type", "Radio", "Notes");
			hb.AppendTableRow("Missions");
			foreach (Asset asset in coalition.OwnAssets.Where(_a => _a.Usage == ElementAssetUsage.MissionWithDetail))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.GetRadioString(), asset.Information);
			}
			foreach (Asset asset in coalition.OwnAssets.Where(_a => _a.Usage == ElementAssetUsage.Mission))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.GetRadioString(), asset.Information);
			}

			hb.AppendTableRow("Support");
			foreach (Asset asset in coalition.OwnAssets.Where(_a => _a.Usage == ElementAssetUsage.Support))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.GetRadioString(), asset.Information);
			}

			hb.AppendTableRow("Base");
			foreach (Asset asset in coalition.OwnAssets.Where(_a => _a.Usage == ElementAssetUsage.Base))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.GetRadioString(), asset.Information);
			}
			foreach (Asset asset in coalition.Airdromes.Where(_a => _a.Usage == ElementAssetUsage.Base && _a.Side == ElementAssetSide.Own))
			{
				hb.AppendTableRow(asset.Name, asset.Task, asset.Type, asset.GetRadioString(), asset.Information);
			}

			hb.CloseTable();

			hb.FinalizeDocument();
			return hb;
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlMission(BriefingCoalition coalition, AssetFlight asset)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.OwnColor);

			hb.AppendHeader($"MISSION {asset.Name} | {asset.Task}", 2);
			hb.AppendParagraphCentered(asset.MissionData.MissionInformation);

			hb.AppendHeader("WAYPOINTS", 3);
			hb.OpenTable("#", "Waypoint", "Action", "Alt.");
			foreach (AssetRoutePoint routePoint in asset.MapPoints.OfType<AssetRoutePoint>())
			{
				hb.AppendTableRow(routePoint.Number.ToString(), routePoint.Name, routePoint.Action, routePoint.AltitudeFeet);
			}
			hb.CloseTable();

			hb.AppendHeader("TARGETS", 3);
			hb.OpenTable("Name", "Type", "Localication", "Information");
			foreach (AssetUnit unit in asset.MissionData.GetListTargetUnits())
			{
				hb.AppendTableRow(unit.AssetGroup.Name, unit.Type, unit.GetLocalisation(), unit.Information);
			}
			hb.CloseTable();

			hb.FinalizeDocument();
			return hb;
		}

		private HtmlBuilder.HtmlDocument GenerateHtmlComs(BriefingCoalition coalition)
		{
			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.OwnColor);

			hb.AppendHeader("COMS", 2);

			hb.OpenTable("#", "Label", "Freq.", " ", "#", "Label", "Freq.");

			for (int iNumber = 1; iNumber <= ListComPreset.PresetsCount; iNumber++)
			{
				ComPreset presetRadio1 = coalition.ComPresets.GetPreset(1, iNumber);
				ComPreset presetRadio2 = coalition.ComPresets.GetPreset(2, iNumber);

				hb.AppendTableRow(presetRadio1.PresetNumber.ToString(), presetRadio1.Label, presetRadio1.Radio.ToString(), " ", presetRadio2.PresetNumber.ToString(), presetRadio2.Label, presetRadio2.Radio.ToString());
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
			m_listFiles.Add(bf);
		}

		private void AddMapData(string sFileName, string sKneeboardFolder, BriefopCustomMap mapData)
		{
			BriefingFile bf = new BriefingFile() { FileName = sFileName, KneeboardFolder = sKneeboardFolder, BitmapContent = ToolsMap.GenerateMapImage(mapData) };
			m_listFiles.Add(bf);
		}

		private void ExportFilesToPath(string sPath, bool bKeepHtml)
		{
			if (!Directory.Exists(sPath))
				throw new ExceptionDcsBriefop($"Path does not exists : {sPath}");

			CleanFilesToPath(sPath);

			foreach (BriefingFile briefingFile in m_listFiles)
			{
				string sFilePath;
				if (briefingFile is BriefingFileHtml briefingFileHtml)
				{
					string sHtmlFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.html");
					if (File.Exists(sHtmlFilePath))
						File.Delete(sHtmlFilePath);

					File.WriteAllText(sHtmlFilePath, briefingFileHtml.HtmlContent);

					sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.jpg");
					if (File.Exists(sFilePath))
						File.Delete(sFilePath);

					briefingFile.BitmapContent.Save(sFilePath, ImageFormat.Jpeg);
					
					if (!bKeepHtml)
					{
						File.Delete(sHtmlFilePath);
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

		private void CleanFilesToPath(string sPath)
		{
			DirectoryInfo di = new DirectoryInfo(sPath);
			foreach (FileInfo file in di.GetFiles().Where(_f => _f.Name.StartsWith(m_sFilePrefix)))
			{
				file.Delete();
			}
		}

		private void ExportFilesToMiz()
		{
			if (!File.Exists(m_missionManager.MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {m_missionManager.MizFilePath}");

			CleanFilesToMiz();

			using (ZipArchive za = ZipFile.Open(m_missionManager.MizFilePath, ZipArchiveMode.Update))
			{
				foreach (BriefingFile briefingFile in m_listFiles)
				{
					string sZipEntry = briefingFile.GetKneeboardZipEntryName();
					string sTempPath = Path.GetTempFileName();
					briefingFile.BitmapContent.Save(sTempPath);

					ToolsZip.RemoveZipEntries(za, sZipEntry);
					za.CreateEntryFromFile(sTempPath, sZipEntry);
				}
			}
		}

		private void CleanFilesToMiz()
		{
			using (ZipArchive za = ZipFile.Open(m_missionManager.MizFilePath, ZipArchiveMode.Update))
			{
				List<string> listToDelete = new List<string>();
				foreach (ZipArchiveEntry entry in za.Entries.Where(_ze =>_ze.Name.StartsWith(m_sFilePrefix)))
				{
					listToDelete.Add(entry.FullName);
				}

				foreach(string sEntry in listToDelete)
				{
					ToolsZip.RemoveZipEntries(za, sEntry);
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
