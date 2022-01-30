using DcsBriefop.Data;
using System;
using System.IO;
using System.Reflection;

namespace DcsBriefop.Tools
{
	internal static class ToolsResources
	{
		public static string GetJsonResourceContent(string sResourceName)
		{
			return GetTextResourceContent(sResourceName, "json");
		}

		public static string GetTextResourceContent(string sResourceName, string sExtension)
		{
			string sBaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string sResourceFilePath = Path.Combine(sBaseDirectory, ElementGlobalData.ResourcesDirectory, $"{sResourceName}.{sExtension}");
			string sContent;

			if (File.Exists(sResourceFilePath))
			{
				sContent = File.ReadAllText(sResourceFilePath);
			}
			else
			{
				try { sContent = Properties.Resources.ResourceManager.GetString(sResourceName, Properties.Resources.Culture); }
				catch (Exception) { sContent = null; }
			}

			return sContent;
		}
	}
}
