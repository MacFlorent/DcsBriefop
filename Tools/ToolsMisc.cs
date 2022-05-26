using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DcsBriefop.Tools
{
	internal static class ToolsMisc
	{
		public static void AppendWithSeparator(this StringBuilder sb, string value, string sSeparator)
		{
			if (sb.Length > 0)
				sb.Append(sSeparator);
			sb.Append(value);
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

		public static string GetDirectoryDcsSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS");
		}

		public static string GetDirectoryDcsBetaSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS.openbeta");
		}
	}
}
