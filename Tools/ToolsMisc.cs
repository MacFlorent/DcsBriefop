using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DcsBriefop.Tools
{
	internal static class ToolsMisc
	{
		public static void AppendWithSeparator(this StringBuilder sb, string sValue, string sSeparator)
		{
			if (string.IsNullOrEmpty(sValue))
				return;

			if (sb.Length > 0)
				sb.Append(sSeparator);
			sb.Append(sValue);
		}

		public static string GetDirectoryFullPath(string sDirectoryString)
		{
			string sExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (sDirectoryString == ".")
				sDirectoryString = sExecutionPath;
			else if (sDirectoryString.StartsWith(@".\"))
				sDirectoryString = sDirectoryString.Replace(@".\", $@"{sExecutionPath}\");

			return sDirectoryString;
		}

		public static void OpenDirectory(string sDirectoryPath)
		{
			//Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", m_briefopManager.MizFileDirectory);
			if (Path.Exists(sDirectoryPath))
				Process.Start("explorer.exe", sDirectoryPath);
		}

		public static string SanitizeFileName(string sFileName)
		{
			if (string.IsNullOrEmpty(sFileName))
				return sFileName;

			char[] invalids = Path.GetInvalidFileNameChars();
			return String.Join("_", sFileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).Replace(" ", "_").TrimEnd('.');
		}

		public static string Truncate(this string sValue, int iMaxChars)
		{
			return sValue.Length <= iMaxChars ? sValue : sValue.Substring(0, iMaxChars) + "...";
		}

		public static string GetDirectoryDcsSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS");
		}

		public static string GetDirectoryDcsBetaSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS.openbeta");
		}

		public static T CloneJson<T>(this T source)
		{
			// Don't serialize a null object, simply return the default for that object
			if (ReferenceEquals(source, null))
				return default;

			// initialize inner objects individually
			// for example in default constructor some list property initialized with some values,
			// but in 'source' these items are cleaned -
			// without ObjectCreationHandling.Replace default constructor values will be added to result
			JsonSerializerSettings deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
		}
	}
}
