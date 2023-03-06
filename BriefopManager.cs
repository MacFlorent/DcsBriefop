using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Text;

namespace DcsBriefop
{
	internal class BriefopManager
	{
		#region Fields
		private static readonly string m_sBriefingMizPrefix = "bop_";
		private static readonly string m_sBriefingDirectory = "BriefopGenerated";
		#endregion

		#region Properties
		public string MizFilePath { get; set; }
		public string MizFileDirectory { get { return Path.GetDirectoryName(MizFilePath); } }
		public string MizFileName { get { return Path.GetFileName(MizFilePath); } }

		public BopMission BopMission { get; set; }
		#endregion

		#region CTOR
		public BriefopManager(string sMizFilePath)
		{
			MizFilePath = sMizFilePath;
			MizLoad();
		}
		#endregion

		#region Miz
		public void MizLoad()
		{
			Log.Debug($"Start for miz file : {MizFilePath}");
			string sMissionFilePath = null, sDictionaryFilePath = null, sCustomFilePath = null;

			if (!File.Exists(MizFilePath))
				throw new ExceptionBop($"Miz file not found : {MizFilePath}");

			Log.Debug("Opening zip archive");
			using (ZipArchive za = ZipFile.OpenRead(MizFilePath))
			{
				foreach (ZipArchiveEntry entry in za.Entries)
				{
					if (!string.IsNullOrEmpty(sMissionFilePath) && !string.IsNullOrEmpty(sDictionaryFilePath) && !string.IsNullOrEmpty(sCustomFilePath))
						break;

					if (entry.FullName.Equals(Miz.MissionFileName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{Miz.MissionFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath, true);
						sMissionFilePath = sTempPath;


					}
					if (entry.FullName.Equals(Miz.DictionaryZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.DictionaryFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath, true);
						sDictionaryFilePath = sTempPath;
					}
					if (entry.FullName.Equals(Miz.BopCustomZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{Miz.BopCustomFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath, true);
						sCustomFilePath = sTempPath;
					}
				}
			}

			if (!File.Exists(sMissionFilePath))
			{
				throw new ExceptionBop($"Mission lua file not found : {sMissionFilePath}");
			}
			if (!File.Exists(sDictionaryFilePath))
			{
				throw new ExceptionBop($"Dictionary lua file not found : {sDictionaryFilePath}");
			}

			Log.Debug($"Reading lua data");
			string sLuaMission = ToolsLua.ReadLuaFileContent(sMissionFilePath);
			string sLuaDictionnary = ToolsLua.ReadLuaFileContent(sDictionaryFilePath);
			string sJsonBopCustom = "";
			if (File.Exists(sCustomFilePath))
			{
				Log.Debug($"Reading json data custom");
				sJsonBopCustom = File.ReadAllText(sCustomFilePath);
			}

			Log.Debug($"Building DataMiz objects");
			Miz miz = new Miz(sLuaMission, sLuaDictionnary, sJsonBopCustom);
			Theatre theatre = new Theatre(miz.RootMission.Theatre);
			BopMission = new BopMission(miz, theatre);

			PreferencesManager.Preferences.Application.WorkingDirectory = Path.GetDirectoryName(MizFilePath);
			PreferencesManager.Preferences.Application.AddRecentMiz(MizFilePath);
			PreferencesManager.Save();
		}

		public async void MizSave(string sFilePath)
		{
			if (!File.Exists(MizFilePath))
				throw new ExceptionBop($"Original mission miz file not found : {MizFilePath}");

			if (string.IsNullOrEmpty(sFilePath))
				sFilePath = MizFilePath;

			if (MizFilePath.Equals(sFilePath))
			{
				// saving as the loaded file
				if (PreferencesManager.Preferences.Application.BackupBeforeOverwrite)
				{
					string sArchivePath = $"{sFilePath}.{DateTime.Now:yyyyMMdd_HHmmss}";
					Log.Info($"Archiving {sFilePath} as {sArchivePath}");
					File.Copy(sFilePath, sArchivePath);
				}
			}
			else
			{
				// saving as another file, initialize with a copy of the loaded file
				File.Copy(MizFilePath, sFilePath, true);
				MizFilePath = sFilePath;
			}

			BopMission.ToMiz();
			BopMission.Miz.ToLua();

			using (ZipArchive za = ZipFile.Open(sFilePath, ZipArchiveMode.Update))
			{
				ToolsZip.ReplaceZipEntry(za, Miz.DictionaryZipEntryFullName, ToolsLua.LsonRootToDcs(BopMission.Miz.RootDictionary.RootLua));
				ToolsZip.ReplaceZipEntry(za, Miz.MissionFileName, ToolsLua.LsonRootToDcs(BopMission.Miz.RootMission.RootLua));
				ToolsZip.ReplaceZipEntry(za, Miz.BopCustomZipEntryFullName, BopMission.Miz.MizBopCustom.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
			}

			// DEBUG
			string sDebugFilePath = Path.Combine(Path.GetDirectoryName(sFilePath), $"{MizFileName}_{Miz.BopCustomFileName}.json");
			File.WriteAllText(sDebugFilePath, BopMission.Miz.MizBopCustom.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
			//			

			if (PreferencesManager.Preferences.Briefing.GenerateOnSave)
				await GenerateBriefing(ElementBriefingOutput.Miz);

			ToolsBriefop.MizCheck(MizFilePath);
		}

		public string MizBatchCommandFileName()
		{
			string sMizName = Path.GetFileNameWithoutExtension(MizFileName);
			return Path.Combine(MizFileDirectory, $"{sMizName}.cmd");
		}

		public void MizBatchCommand()
		{
			string sCommandFilePath = MizBatchCommandFileName();
			string sCommandFileContent = ToolsResources.GetTextResourceContent("DcsBriefopBatch", "cmd");
			sCommandFileContent = sCommandFileContent.Replace("%1", MizFileName);
			sCommandFileContent = sCommandFileContent.Replace("%2", System.Reflection.Assembly.GetExecutingAssembly().Location);
			StreamWriter sw = File.CreateText(sCommandFilePath);
			sw.Write(sCommandFileContent);
			sw.Close();
		}
		#endregion

		#region Briefing generation
		public async Task GenerateBriefing(ElementBriefingOutput briefingOutput)
		{
			using (ListBopBriefingGeneratedFile files = await BopMission.GenerateBriefingFiles())
			{
				if (briefingOutput.HasFlag(ElementBriefingOutput.Miz))
					GenerateBriefingMiz(files);
				if (briefingOutput.HasFlag(ElementBriefingOutput.Directory))
					GenerateBriefingDirectory(files);
			}
		}

		public void GenerateBriefingMiz(ListBopBriefingGeneratedFile files)
		{
			if (!File.Exists(MizFilePath))
				throw new ExceptionBop($"Miz file not found : {MizFilePath}");

			using (ZipArchive za = ZipFile.Open(MizFilePath, ZipArchiveMode.Update))
			{
				CleanBriefingFilesMiz(za);

				foreach (BopBriefingGeneratedFile file in files.Where(_f => _f.Kneeboards is not null))
				{
					foreach (string sUnitDirectory in file.Kneeboards)
					{
						string sUnitDirectoryWithSeparator = sUnitDirectory;
						if (!string.IsNullOrEmpty(sUnitDirectoryWithSeparator))
							sUnitDirectoryWithSeparator = $"{sUnitDirectoryWithSeparator}/";

						string sZipEntry = $@"KNEEBOARD/{sUnitDirectoryWithSeparator}IMAGES/{m_sBriefingMizPrefix}{file.FileName}.jpg"; ;
						string sTempPath = Path.GetTempFileName();
						file.Image.Save(sTempPath, ImageFormat.Jpeg);

						ToolsZip.RemoveZipEntries(za, sZipEntry);
						za.CreateEntryFromFile(sTempPath, sZipEntry);
					}
				}
			}
		}

		private void CleanBriefingFilesMiz(ZipArchive za)
		{
			List<string> listToDelete = new List<string>();
			foreach (ZipArchiveEntry entry in za.Entries.Where(_ze => _ze.Name.StartsWith(m_sBriefingMizPrefix)))
			{
				listToDelete.Add(entry.FullName);
			}

			foreach (string sEntry in listToDelete)
			{
				ToolsZip.RemoveZipEntries(za, sEntry);
			}
		}

		private void GenerateBriefingDirectory(ListBopBriefingGeneratedFile files)
		{
			string sDirectoryRoot = Path.Combine(MizFileDirectory, m_sBriefingDirectory);

			if (!Path.Exists(sDirectoryRoot))
				Directory.CreateDirectory(sDirectoryRoot);
			else
				CleanBriefingDirectory(sDirectoryRoot);

			foreach (BopBriefingGeneratedFile file in files.Where(_f => _f.Kneeboards is not null))
			{
				foreach (string sUnitDirectory in file.Kneeboards)
				{
					string sDirectory = Path.Combine(sDirectoryRoot, sUnitDirectory);
					if (!Path.Exists(sDirectory))
						Directory.CreateDirectory(sDirectory);

					string sFilePath = Path.Combine(sDirectory, $"{file.FileName}.jpg");
					if (File.Exists(sFilePath))
						File.Delete(sFilePath);
					file.Image.Save(sFilePath, ImageFormat.Jpeg);

					if (!string.IsNullOrEmpty(file.Html) && PreferencesManager.Preferences.Briefing.GenerateDirectoryHtml)
					{
						sFilePath = Path.Combine(sDirectory, $"{file.FileName}.html");
						if (File.Exists(sFilePath))
							File.Delete(sFilePath);
						File.WriteAllText(sFilePath, file.Html);
					}
				}
			}
		}

		private void CleanBriefingDirectory(string sDirectoryRoot)
		{
			DirectoryInfo di = new DirectoryInfo(sDirectoryRoot);
			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in di.GetDirectories())
			{
				dir.Delete(true);
			}
		}
		#endregion
	}
}
