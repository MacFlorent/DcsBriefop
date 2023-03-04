using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace DcsBriefop
{
	internal class BriefopManager
	{
		#region Fields
		private static readonly string m_sBriefingFilePrefix = "bop_";
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

		public void MizSave(string sFilePath)
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

			//ToolsBriefop.MizCheck(MizFilePath);
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
		public async Task GenerateBriefingFiles(ElementBriefingGeneration briefingGeneration)
		{
			CleanBriefingFilesMiz();
			CleanBriefingFilesDirectory();

			List<BopBriefingGeneratedFile> files = await BopMission.GenerateBriefingFiles();
			foreach (BopBriefingGeneratedFile file in files)
			{
				string sFinalFileName = $"{m_sBriefingFilePrefix}{file.FileName}";

				if (briefingGeneration.HasFlag(ElementBriefingGeneration.Miz))
					GenerateBriefingFileMiz(file, sFinalFileName);
				if (briefingGeneration.HasFlag(ElementBriefingGeneration.Directory))
					GenerateBriefingFileDirectory(file, sFinalFileName);
			}
		}

		private void GenerateBriefingFileMiz(BopBriefingGeneratedFile file, string sFinalFileName)
		{
		}

		private void CleanBriefingFilesMiz()
		{
		}

		private void GenerateBriefingFileDirectory(BopBriefingGeneratedFile file, string sFinalFileName)
		{
			string sPath = MizFileDirectory;

			string sFileFullPath = Path.Combine(sPath, $"{sFinalFileName}.jpg");
			if (File.Exists(sFileFullPath))
				File.Delete(sFileFullPath);
			file.Image.Save(sFileFullPath, ImageFormat.Jpeg);

			if (!string.IsNullOrEmpty(file.Html) && PreferencesManager.Preferences.Briefing.GenerateDirectoryHtml)
			{
				sFileFullPath = Path.Combine(sPath, $"{sFinalFileName}.html");
				if (File.Exists(sFileFullPath))
					File.Delete(sFileFullPath);
				File.WriteAllText(sFileFullPath, file.Html);
			}
		}

		private void CleanBriefingFilesDirectory()
		{
			DirectoryInfo di = new DirectoryInfo(MizFileDirectory);
			foreach (FileInfo file in di.GetFiles().Where(_f => _f.Name.StartsWith(m_sBriefingFilePrefix)))
			{
				file.Delete();
			}
		}
		#endregion
	}
}
