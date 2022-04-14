﻿using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
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
		}

		#region Fields
		private BriefingContainer m_briefingContainer;
		private MissionManager m_missionManager;
		private List<BriefingFile> m_listFiles = new List<BriefingFile>();
		private Color BackColor = Color.WhiteSmoke;
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
			foreach (BriefingCoalition coalition in m_briefingContainer.BriefingCoalitions)
			{
				GenerateAllFilesCoalition(exportFileTypes, coalition);
			}
		}

		private void GenerateAllFilesCoalition(List<ElementExportFileType> exportFileTypes, BriefingCoalition coalition)
		{
			string sKneeboardFolder = KneeboardFolders.Images;

			if (exportFileTypes.Contains(ElementExportFileType.SituationMap))
				AddMapData(GenerateFileName(ElementExportFileType.SituationMap, coalition.CoalitionName), sKneeboardFolder, coalition.MapData);
			if (exportFileTypes.Contains(ElementExportFileType.Operations))
				AddFileHtml(GenerateFileName(ElementExportFileType.Operations, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlOperations(coalition));
			if (exportFileTypes.Contains(ElementExportFileType.Opposition))
				AddFileHtml(GenerateFileName(ElementExportFileType.Opposition, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlOpposition(coalition));

			if (exportFileTypes.Contains(ElementExportFileType.Coms) && coalition.ComPresets is object && coalition.ComPresets.Count > 0)
				AddFileHtml(GenerateFileName(ElementExportFileType.Coms, coalition.CoalitionName), sKneeboardFolder, GenerateHtmlComs(coalition));

			foreach (AssetFlight asset in coalition.OwnAssets.OfType<AssetFlight>().Where(_a => _a.MissionData is object))
			{
				DcsUnit dcsUnit = DcsUnitManager.GetUnit(asset.Type);
				if (dcsUnit is object && !string.IsNullOrEmpty(dcsUnit.KneeboardFolder))
					sKneeboardFolder = dcsUnit.KneeboardFolder;
				else
					sKneeboardFolder = asset.Type;

				sKneeboardFolder = $@"{sKneeboardFolder}/{KneeboardFolders.Images}";

				if (exportFileTypes.Contains(ElementExportFileType.Missions))
					AddFileHtml($"{GenerateFileName(ElementExportFileType.Missions, coalition.CoalitionName)}_{asset.Id}", sKneeboardFolder, GenerateHtmlMissionCard(coalition, asset));
				if (exportFileTypes.Contains(ElementExportFileType.MissionMaps))
					AddMapData($"{GenerateFileName(ElementExportFileType.MissionMaps, coalition.CoalitionName)}_{asset.Id}", sKneeboardFolder, asset.MissionData.MapData);
			}
		}

		private string PlaceholderGet(string sPlaceholder)
		{
			return $"__{sPlaceholder}__";
		}

		private void PlaceholderReplace(ref string sString, string sPlaceholder, string sValue)
		{
			sString = sString.Replace(PlaceholderGet(sPlaceholder), sValue?.Replace(Environment.NewLine, "<br>"));
		}

		private void PlaceholderReplaceStyle(ref string sString, Color color)
		{
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "css");
			PlaceholderReplace(ref sStyle, "colorDark", ColorTranslator.ToHtml(color));
			PlaceholderReplace(ref sStyle, "colorLight", ColorTranslator.ToHtml(color.Lerp(Color.White, 0.85f)));
			PlaceholderReplace(ref sStyle, "colorBack", ColorTranslator.ToHtml(BackColor));

			sString = sString.Replace("<link href=\"briefingTemplate.css\" rel=\"stylesheet\">", $"<style>{sStyle}</style>");
		}

		private bool PlaceholderGetBlock(out string sBlockRaw, out string sBlockCleaned, string sString, string sPlaceholder)
		{
			sBlockRaw = sBlockCleaned = null;

			string sStart = $"<!--{PlaceholderGet($"{sPlaceholder}Start")}-->";
			string sEnd = $"<!--{PlaceholderGet($"{sPlaceholder}End")}-->";

			int iStart = sString.IndexOf(sStart);
			int iEnd = sString.IndexOf(sEnd);

			if (iStart >= 0 && iEnd > iStart)
			{
				sBlockRaw = sString.Substring(iStart, iEnd - iStart + sEnd.Length);
				sBlockCleaned = sBlockRaw.Replace(sStart, "").Replace(sEnd, "");
				return true;
			}
			else
				return false;
		}

		private void AddFileHtml(string sFileName, string sKneeboardFolder, string sHtml)
		{
			BriefingFileHtml bf = new BriefingFileHtml() { FileName = sFileName, KneeboardFolder = sKneeboardFolder, HtmlContent = sHtml };
			bf.BitmapContent = HtmlRender.RenderToImage(sHtml, new Size(ElementImageSize.Width, ElementImageSize.Height), BackColor) as Bitmap;
			//bf.BitmapContent = HtmlRender.RenderToImageGdiPlus(sHtml, new Size(ElementImageSize.Width, ElementImageSize.Height), System.Drawing.Text.TextRenderingHint.AntiAlias) as Bitmap;
			m_listFiles.Add(bf);
		}

		private void AddFileHtml(string sFileName, string sKneeboardFolder, HtmlBuilder.HtmlDocument hb)
		{
			AddFileHtml(sFileName, sKneeboardFolder, hb.ToString());
		}

		private void AddMapData(string sFileName, string sKneeboardFolder, BriefopCustomMap mapData)
		{
			BriefingFile bf = new BriefingFile() { FileName = sFileName, KneeboardFolder = sKneeboardFolder, BitmapContent = ToolsMap.GenerateMapImage(mapData) };
			m_listFiles.Add(bf);
		}
		#endregion

		#region Operations
		private void GenerateHtmlOperationsMissionLine(StringBuilder sbAssets, string sLineCleaned, Asset asset)
		{
			string sAssetLine = sLineCleaned;
			PlaceholderReplace(ref sAssetLine, "missionDescription", asset.Description);
			PlaceholderReplace(ref sAssetLine, "missionTask", asset.Task);
			PlaceholderReplace(ref sAssetLine, "missionType", asset.Type);
			PlaceholderReplace(ref sAssetLine, "missionNotes", asset.Information);
			sbAssets.Append(sAssetLine);
		}

		private void GenerateHtmlOperationsSupportLine(StringBuilder sbAssets, string sLineCleaned, Asset asset)
		{
			string sAssetLine = sLineCleaned;
			PlaceholderReplace(ref sAssetLine, "supportDescription", asset.Description);
			PlaceholderReplace(ref sAssetLine, "supportTask", asset.Task);
			PlaceholderReplace(ref sAssetLine, "supportType", asset.Type);
			PlaceholderReplace(ref sAssetLine, "supportRadio", asset.GetRadioString());
			PlaceholderReplace(ref sAssetLine, "supportNotes", asset.Information);
			sbAssets.Append(sAssetLine);
		}

		private string GenerateHtmlOperations(BriefingCoalition coalition)
		{
			string sHtml = ToolsResources.GetTextResourceContent("briefingTemplateOperations", "html");

			PlaceholderReplaceStyle(ref sHtml, coalition.OwnColor);
			PlaceholderReplace(ref sHtml, "coalition", coalition.CoalitionName);
			PlaceholderReplace(ref sHtml, "weather", m_briefingContainer.Mission.Weather.ToString());
			PlaceholderReplace(ref sHtml, "bullseye", coalition.GetBullseyeCoordinatesString());
			PlaceholderReplace(ref sHtml, "generalSortie", m_briefingContainer.Mission.Sortie);
			PlaceholderReplace(ref sHtml, "generalSituation", m_briefingContainer.Mission.Description);
			PlaceholderReplace(ref sHtml, "generalTask", coalition.Task);

			StringBuilder sb = new StringBuilder();
			string sBlockRaw, sBlockCleaned;

			if (PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "mission"))
			{
				IEnumerable<Asset> assetsMission = coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Other);
				if (assetsMission.Count() <= 0)
				{
					sHtml = sHtml.Replace(sBlockRaw, "");
				}
				else
				{
					sb.Clear();
					PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "missionLine");
					foreach (Asset asset in assetsMission)
					{
						GenerateHtmlOperationsMissionLine(sb, sBlockCleaned, asset);
					}
					sHtml = sHtml.Replace(sBlockRaw, sb.ToString());
				}
			}

			if (PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "supportLine"))
			{
				IEnumerable<Asset> assetsSupport = coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Support);
				IEnumerable<Asset> assetsBase = coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base);
				IEnumerable<Asset> assetsAirdrome = coalition.Airdromes.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base && _a.Side == ElementAssetSide.Own);

				if (assetsSupport.Count() <= 0 || assetsBase.Count() <= 0 || assetsAirdrome.Count() <= 0)
				{
					sHtml = sHtml.Replace(sBlockRaw, "");
				}
				else
				{
					sb.Clear();
					foreach (Asset supportAsset in assetsSupport)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					foreach (Asset supportAsset in assetsBase)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					foreach (Asset supportAsset in assetsAirdrome)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					sHtml = sHtml.Replace(sBlockRaw, sb.ToString());
				}
			}
			return sHtml;
		}
		#endregion

		#region Opposition
		private void GenerateHtmlOppositionThreatLine(StringBuilder sbAssets, string sLineCleaned, Asset asset)
		{
			string sType = asset.Type;
			if (asset is AssetGroup group && group.Units.Count > 0)
				sType = group.Units.FirstOrDefault()?.Type;

			string sAssetLine = sLineCleaned;
			PlaceholderReplace(ref sAssetLine, "threatDescription", $"{asset.Description}<br>{sType}");
			PlaceholderReplace(ref sAssetLine, "threatLocalisation", asset.GetLocalisation());
			PlaceholderReplace(ref sAssetLine, "threatNotes", asset.Information);
			sbAssets.Append(sAssetLine);
		}

		private void GenerateHtmlOppositionThreatLineUnit(StringBuilder sbAssets, string sLineCleaned, AssetUnit unit)
		{
			string sAssetLine = sLineCleaned;
			PlaceholderReplace(ref sAssetLine, "threatDescription", $"{unit.AssetGroup.Description}<br>{unit.Description}");
			PlaceholderReplace(ref sAssetLine, "threatLocalisation", unit.GetLocalisation());
			PlaceholderReplace(ref sAssetLine, "threatNotes", unit.Information);
			sbAssets.Append(sAssetLine);
		}

		private string GenerateHtmlOpposition(BriefingCoalition coalition)
		{
			string sHtml = ToolsResources.GetTextResourceContent("briefingTemplateOpposition", "html");

			PlaceholderReplaceStyle(ref sHtml, coalition.OwnColor);

			StringBuilder sb = new StringBuilder();
			string sBlockRaw, sBlockCleaned;

			if (PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "threat"))
			{
				IEnumerable<Asset> assetsThreat = coalition.OpposingAssets.Where(_a => ToolsBriefop.AssetOrUnitIncluded(_a));
				if (assetsThreat.Count() <= 0)
				{
					sHtml = sHtml.Replace(sBlockRaw, "");
				}
				else
				{
					sb.Clear();
					PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "threatLine");
					foreach (Asset asset in assetsThreat)
					{
						if (asset.Included)
							GenerateHtmlOppositionThreatLine(sb, sBlockCleaned, asset);

						if (asset is AssetGroup group)
						{
							foreach (AssetUnit unit in (group.Units.Where(_u => _u.Included)))
							{
								GenerateHtmlOppositionThreatLineUnit(sb, sBlockCleaned, unit);
							}
						}
					}
					sHtml = sHtml.Replace(sBlockRaw, sb.ToString());
				}
			}

			if (PlaceholderGetBlock(out sBlockRaw, out sBlockCleaned, sHtml, "supportLine"))
			{
				IEnumerable<Asset> assetsSupport = coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Support);
				IEnumerable<Asset> assetsBase = coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base);
				IEnumerable<Asset> assetsAirdrome = coalition.Airdromes.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base && _a.Side == ElementAssetSide.Own);

				if (assetsSupport.Count() <= 0 || assetsBase.Count() <= 0 || assetsAirdrome.Count() <= 0)
				{
					sHtml = sHtml.Replace(sBlockRaw, "");
				}
				else
				{
					sb.Clear();
					foreach (Asset supportAsset in assetsSupport)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					foreach (Asset supportAsset in assetsBase)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					foreach (Asset supportAsset in assetsAirdrome)
					{
						GenerateHtmlOperationsSupportLine(sb, sBlockCleaned, supportAsset);
					}
					sHtml = sHtml.Replace(sBlockRaw, sb.ToString());
				}
			}
			return sHtml;
		}
		#endregion

		#region Coms
		private void GenerateHtmlComsLine(StringBuilder sbComs, string sLineCleaned, ComPreset preset)
		{
			string sRadioLine = sLineCleaned;
			PlaceholderReplace(ref sRadioLine, "radioNumber", preset.PresetNumber.ToString());
			PlaceholderReplace(ref sRadioLine, "radioLabel", preset.Label);
			PlaceholderReplace(ref sRadioLine, "radioFrequency", preset.Radio.ToString());
			sbComs.Append(sRadioLine);
		}

		private void GenerateHtmlComsTable(ref string sString, BriefingCoalition coalition, int iRadio)
		{
			//PlaceholderGetLine(out string sLineRaw, out string sLineCleaned, sString, $"radio{iRadio}Line");
			//StringBuilder sb = new StringBuilder();
			//for (int iNumber = 1; iNumber <= ListComPreset.PresetsCount; iNumber++)
			//{
			//	ComPreset preset = coalition.ComPresets.GetPreset(iRadio, iNumber);
			//	GenerateHtmlComsLine(sb, sLineCleaned, preset);
			//}

			//sString = sString.Replace(sLineRaw, sb.ToString());
		}

		private string GenerateHtmlComs(BriefingCoalition coalition)
		{
			string sHtml = ToolsResources.GetTextResourceContent("briefingTemplateComs", "html");

			PlaceholderReplaceStyle(ref sHtml, coalition.OwnColor);
			PlaceholderReplace(ref sHtml, "coalition", coalition.CoalitionName);

			GenerateHtmlComsTable(ref sHtml, coalition, 1);
			GenerateHtmlComsTable(ref sHtml, coalition, 2);

			return sHtml;
		}
		#endregion

		#region MissionCard
		private void GenerateHtmlMissionCardAssetLine(StringBuilder sbAssets, string sLineCleaned, Asset asset)
		{
			string sAssetLine = sLineCleaned;
			PlaceholderReplace(ref sAssetLine, "supportDescription", asset.Description);
			PlaceholderReplace(ref sAssetLine, "supportTask", asset.Task);
			PlaceholderReplace(ref sAssetLine, "supportType", asset.Type);
			PlaceholderReplace(ref sAssetLine, "supportRadio", asset.GetRadioString());
			PlaceholderReplace(ref sAssetLine, "supportNotes", asset.Information);
			sbAssets.Append(sAssetLine);
		}

		private string GenerateHtmlMissionCard(BriefingCoalition coalition, AssetFlight asset)
		{
			string sHtml = ToolsResources.GetTextResourceContent("briefingTemplateMissionCard", "html");

			//PlaceholderReplaceStyle(ref sHtml, coalition.OwnColor);
			//PlaceholderReplace(ref sHtml, "coalition", coalition.CoalitionName);
			//PlaceholderReplace(ref sHtml, "assetDescription", asset.Description);
			//PlaceholderReplace(ref sHtml, "task", asset.Task);
			//PlaceholderReplace(ref sHtml, "taskDetail", asset.MissionData?.MissionInformation);
			//PlaceholderReplace(ref sHtml, "weather", m_briefingContainer.Mission.Weather.ToString());
			//PlaceholderReplace(ref sHtml, "bullseye", coalition.GetBullseyeCoordinatesString());

			//string sLineRaw, sLineCleaned;
			//PlaceholderGetLine(out sLineRaw, out sLineCleaned, sHtml, "supportLine");
			//StringBuilder sb = new StringBuilder();
			//foreach (Asset supportAsset in coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Support))
			//{
			//	GenerateHtmlMissionCardAssetLine(sb, sLineCleaned, supportAsset);
			//}
			//foreach (Asset supportAsset in coalition.OwnAssets.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base))
			//{
			//	GenerateHtmlMissionCardAssetLine(sb, sLineCleaned, supportAsset);
			//}
			//foreach (Asset supportAsset in coalition.Airdromes.Where(_a => _a.Included && _a.Function == ElementAssetFunction.Base && _a.Side == ElementAssetSide.Own))
			//{
			//	GenerateHtmlMissionCardAssetLine(sb, sLineCleaned, supportAsset);
			//}
			//sHtml = sHtml.Replace(sLineRaw, sb.ToString());

			//PlaceholderGetLine(out sLineRaw, out sLineCleaned, sHtml, "waypointLine");
			//sb.Clear();
			//foreach (AssetRoutePoint routePoint in asset.MapPoints.OfType<AssetRoutePoint>())
			//{
			//	string sWaypointLine = sLineCleaned;
			//	PlaceholderReplace(ref sWaypointLine, "waypointNumber", routePoint.Number.ToString());
			//	PlaceholderReplace(ref sWaypointLine, "waypointName", routePoint.Name);
			//	PlaceholderReplace(ref sWaypointLine, "waypointAction", routePoint.Action);
			//	PlaceholderReplace(ref sWaypointLine, "waypointAltitude", routePoint.AltitudeFeet);
			//	sb.Append(sWaypointLine);
			//}
			//sHtml = sHtml.Replace(sLineRaw, sb.ToString());

			//PlaceholderGetLine(out sLineRaw, out sLineCleaned, sHtml, "targetLine");
			//sb.Clear();
			//foreach (AssetUnit unit in asset.MissionData.GetListTargetUnits())
			//{
			//	string sTargetLine = sLineCleaned;
			//	PlaceholderReplace(ref sTargetLine, "targetName", unit.AssetGroup.Name);
			//	PlaceholderReplace(ref sTargetLine, "targetType", unit.Type);
			//	PlaceholderReplace(ref sTargetLine, "targetLocalisation", unit.GetLocalisation());
			//	PlaceholderReplace(ref sTargetLine, "targetNotes", unit.Information);
			//	sb.Append(sTargetLine);
			//}
			//sHtml = sHtml.Replace(sLineRaw, sb.ToString());

			return sHtml;
		}
		#endregion

		#region Export
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
				foreach (ZipArchiveEntry entry in za.Entries.Where(_ze => _ze.Name.StartsWith(m_sFilePrefix)))
				{
					listToDelete.Add(entry.FullName);
				}

				foreach (string sEntry in listToDelete)
				{
					ToolsZip.RemoveZipEntries(za, sEntry);
				}
			}
		}
		#endregion

		#region Dispose
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
		#endregion
	}
}
