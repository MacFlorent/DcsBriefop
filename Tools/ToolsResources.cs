using DcsBriefop.Data;
using DcsBriefop.Properties;
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
				try { sContent = Resources.ResourceManager.GetString(sResourceName); }
				catch (Exception) { sContent = null; }
			}

			return sContent;
		}

		public static Icon GetIconResource(string sResourceName, string sExtension)
		{
			Icon iconFinal = null;
			object oResource = Resources.ResourceManager.GetObject(sResourceName);
			if (oResource is Icon icon)
				iconFinal = icon;
			else if (oResource is Bitmap bitmap)
			{
				IntPtr icH = bitmap.GetHicon();
				iconFinal = Icon.FromHandle(icH);
				//DestroyIcon(icH);
			}

			return iconFinal;
		}

		public static Image GetImageResource(string sResourceName, string sExtension)
		{
			Image imageFinal = null;
			string sResourceFilePath = GetResourceFileFullPath(sResourceName, sExtension);

			if (!string.IsNullOrEmpty(sExtension) && File.Exists(sResourceFilePath))
			{
				imageFinal = Image.FromFile(sResourceFilePath);
			}
			else
			{
				object oResource = Resources.ResourceManager.GetObject(sResourceName, Resources.Culture);
				if (oResource is Image image)
					imageFinal = image;
				else if (oResource is Icon icon)
				{
					imageFinal = new Icon(icon, 800, 800).ToBitmap(); // ask for a large size so the largest icon size will be used
				}
			}

			return imageFinal;
		}
	}
}
