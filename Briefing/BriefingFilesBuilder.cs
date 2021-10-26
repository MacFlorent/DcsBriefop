using DcsBriefop.Map;
using DcsBriefop.MasterData;
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
		public void Generate(string sFileExportPath, bool bUpdateMiz)
		{
			GenerateAllFiles();

			if (!string.IsNullOrEmpty(sFileExportPath))
				ExportFilesToPath(sFileExportPath);
			if (bUpdateMiz)
				ExportFilesToMiz();
		}

		private void GenerateAllFiles()
		{
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
			GenerateFileSituation(coalition);
			GenerateFilesOperations(coalition);

			AddMapData($"mapDataTest_", KneeboardFolders.Images, coalition.MapData);
			foreach (Asset bg in coalition.GroupFlights.Where(_bf => _bf.Id == 45 || _bf.Id == 68))
				AddMapData($"mapDataTest_{bg.Id}", KneeboardFolders.Images, bg.MapData);

		}

		private void GenerateFileSituation(BriefingCoalition coalition)
		{
			string sFileName = $"{coalition.Name}_00_SITUATION";

			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.OpenTag("html", "");
			hb.OpenTag("body", "");

			hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("SITUATION", 2);
			hb.AppendParagraphJustified(m_briefingPack.Description);
			hb.AppendHeader("TASKS", 3);
			hb.AppendParagraphJustified(coalition.Task);
			hb.AppendHeader("WEATHER", 3);
			hb.AppendParagraphCentered(m_briefingPack.Weather.ToString());

			hb.CloseTag();
			hb.CloseTag();

			AddFileHtml(sFileName, KneeboardFolders.Images, hb.ToString());
		}

		private void GenerateFilesOperations(BriefingCoalition coalition)
		{
			string sFileName = $"{coalition.Name}_02_OPERATIONS";

			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.OpenTag("html", "");
			hb.OpenTag("body", "");

			hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("OPERATIONS", 2);
			hb.AppendHeader("BULLSEYE", 3);
			hb.AppendParagraphCentered(coalition.BullseyeCoordinates);
			hb.AppendParagraphCentered(coalition.BullseyeDescription);
			hb.AppendHeader("OPERATIONS", 3);
			
			foreach(Asset groupFlight in coalition.GroupFlights)
			{
				//hb.AppendTableHeader(m_briefingPack.Sortie, 3);
			}
			// tab with groups

			hb.CloseTag();
			hb.CloseTag();

			AddFileHtml(sFileName, KneeboardFolders.Images, hb.ToString());
		}

		private void AddFileHtml(string sFileName, string sKneeboardFolder, string sHtmlContent)
		{
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

		private void ExportFilesToPath(string sPath)
		{
			if (!Directory.Exists(sPath))
				throw new ExceptionDcsBriefop($"Path does not exists : {sPath}");
			
			foreach (BriefingFile briefingFile in m_listFiles)
			{
				string sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.jpg");
				if (File.Exists(sFilePath))
					File.Delete(sFilePath);

				briefingFile.BitmapContent.Save(sFilePath, ImageFormat.Jpeg);
			}

		}

		private void ExportFilesToMiz()
		{
			return;
			if (!File.Exists(m_missionManager.MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {m_missionManager.MizFilePath}");

			using (ZipArchive za = ZipFile.Open(m_missionManager.MizFilePath, ZipArchiveMode.Update))
			{
				foreach(BriefingFile briefingFile in m_listFiles)
				{
					string sZipEntry = $@"KNEEBOARD/{briefingFile.KneeboardFolder}/{briefingFile.FileName}";

					if (briefingFile is BriefingFileHtml bfHtml)
					{
						using (Bitmap b = HtmlRender.RenderToImage(bfHtml.HtmlContent, new Size(ElementImageSize.Width, ElementImageSize.Height), Color.LightGray) as Bitmap)
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
