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
			string sJsonStream = null;

			if (File.Exists(sResourceFilePath))
			{
				sJsonStream = File.ReadAllText(sResourceFilePath);
			}
			else
			{
				try { sJsonStream = Properties.Resources.ResourceManager.GetString(sResourceName, Properties.Resources.Culture); }
				catch (Exception) { sJsonStream = null; }
			}

			return sJsonStream;
		}
	}
}
