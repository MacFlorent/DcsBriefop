using DcsBriefop.Data;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace DcsBriefop.Tools
{
	internal static class ToolsResources
	{
		public static string GetResourceFileFullPath(string sResourceName, string sExtension)
		{
			string sBaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string sResourceFilePath = Path.Combine(sBaseDirectory, ElementGlobalData.ResourcesDirectory, $"{sResourceName}.{sExtension}");
			return sResourceFilePath;
		}

		public static string GetJsonResourceContent(string sResourceName)
		{
			return GetTextResourceContent(sResourceName, "json");
		}

		public static string GetTextResourceContent(string sResourceName, string sExtension)
		{
			string sResourceFilePath = GetResourceFileFullPath(sResourceName, sExtension);
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

		public static Icon GetIconResource(string sResource)
		{
			Icon ico = null;
			object oResource = Properties.Resources.ResourceManager.GetObject(sResource, Properties.Resources.Culture);
			if (oResource is Icon i)
				ico = i;
			else if (oResource is Bitmap b)
			{
				IntPtr icH = b.GetHicon();
				ico = Icon.FromHandle(icH);
				//DestroyIcon(icH);
			}

			return ico;
		}

		public static Image GetImageResource(string sResource)
		{
			return Properties.Resources.ResourceManager.GetObject(sResource, Properties.Resources.Culture) as Image;
		}
	}
}
