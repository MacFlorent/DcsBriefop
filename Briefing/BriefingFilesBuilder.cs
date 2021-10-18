using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace DcsBriefop.Briefing
{
	//https://github.com/itext/itext7-dotnet
	//HtmlConverter.ConvertToPdf(new FileInfo(sFilePath), new FileInfo(sPdfFileName));

	internal abstract class BriefingFile
	{
		public string FileName { get; set; }
		public string KneeboardFolder { get; set; }
	}

	internal class BriefingFileHtml : BriefingFile
	{
		public string HtmlContent { get; set; }
	}

	internal class BriefingFileImage : BriefingFile
	{
		public Bitmap BitmapContent { get; set; }
	}


	internal class BriefingFilesBuilder
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
		}

		public void GenerateFileSituation(BriefingCoalition coalition)
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

			m_listFiles.Add(new BriefingFileHtml() { FileName = sFileName, KneeboardFolder = KneeboardFolders.Images, HtmlContent = hb.ToString() });
		}

		public void GenerateFilesOperations(BriefingCoalition coalition)
		{
			string sFileName = $"{coalition.Name}_02_OPERATIONS";

			HtmlBuilder.HtmlDocument hb = new HtmlBuilder.HtmlDocument(coalition.Color);

			hb.OpenTag("html", "");
			hb.OpenTag("body", "");

			hb.AppendHeader(m_briefingPack.Sortie, 3);
			hb.AppendHeader("OPERATIONS", 2);
			hb.AppendHeader("BULSEYE", 3);
			hb.AppendParagraphCentered(coalition.BullseyeCoordinates);
			hb.AppendParagraphCentered(coalition.BullseyeDescription);
			hb.AppendHeader("OPERATIONS", 3);
			
			foreach(BriefingGroup groupFlight in coalition.GroupFlights)
			{
				//hb.AppendTableHeader(m_briefingPack.Sortie, 3);
			}
			// tab with groups

			hb.CloseTag();
			hb.CloseTag();

			m_listFiles.Add(new BriefingFileHtml() { FileName = sFileName, KneeboardFolder = KneeboardFolders.Images, HtmlContent = hb.ToString() });
		}

		private void ExportFilesToPath(string sPath)
		{
			if (!Directory.Exists(sPath))
				throw new ExceptionDcsBriefop($"Path does not exists : {sPath}");
			
			foreach (BriefingFile briefingFile in m_listFiles)
			{
				string sFilePath = Path.Combine(sPath, $"{briefingFile.FileName}.bmp");
				if (File.Exists(sFilePath))
					File.Delete(sFilePath);

				if (briefingFile is BriefingFileHtml bfHtml)
				{
					//Bitmap b = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImageGdiPlus(hb.ToString(), new Size(800, 1200)) as Bitmap;
					using (Bitmap b = HtmlRender.RenderToImage(bfHtml.HtmlContent, new Size(800, 1200), Color.LightGray) as Bitmap)
					{
						b.Save(sFilePath);
					}
				}
				else if(briefingFile is BriefingFileImage bfImage) { }
			}

		}

		private void ExportFilesToMiz()
		{
			if (!File.Exists(m_missionManager.MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {m_missionManager.MizFilePath}");

			using (ZipArchive za = ZipFile.Open(m_missionManager.MizFilePath, ZipArchiveMode.Update))
			{
				foreach(BriefingFile briefingFile in m_listFiles)
				{
					string sZipEntry = $@"KNEEBOARD/{briefingFile.KneeboardFolder}/{briefingFile.FileName}";

					if (briefingFile is BriefingFileHtml bfHtml)
					{
						using (Bitmap b = HtmlRender.RenderToImage(bfHtml.HtmlContent, new Size(800, 1200), Color.LightGray) as Bitmap)
						{
							string sTempPath = Path.GetTempFileName();
							b.Save(sTempPath);

							ToolsZip.RemoveZipEntries(za, sZipEntry);
							za.CreateEntryFromFile(sTempPath, sZipEntry);
						}
					}
					else if (briefingFile is BriefingFileImage bfImage) { }
				}
				
			}
		}
		#endregion

	}
}
