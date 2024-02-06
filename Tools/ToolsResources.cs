using DcsBriefop.Data;
using DcsBriefop.Properties;
using System.Reflection;

namespace DcsBriefop.Tools
{
	internal static class ToolsResources
	{
		public static string GetResourceDirectoryPath(string sResourceDirectory)
		{
			if (string.IsNullOrEmpty(sResourceDirectory))
				sResourceDirectory = ElementBopResource.DirectoryDefault;

			string sBaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(sBaseDirectory, sResourceDirectory);
		}

		public static string GetResourceFilePath(string sResourceName, string sExtension, string sResourceDirectory)
		{
			string sResourceDirectoryPath = GetResourceDirectoryPath(sResourceDirectory);
			string sResourceFilePath = Path.Combine(sResourceDirectoryPath, $"{sResourceName}.{sExtension}");
			return sResourceFilePath;
		}

		public static string GetJsonResourceContent(string sResourceName, string sResourceDirectory)
		{
			return GetTextResourceContent(sResourceName, "json", sResourceDirectory);
		}

		public static string GetTextResourceContent(string sResourceName, string sExtension, string sResourceDirectory)
		{
			string sResourceFilePath = GetResourceFilePath(sResourceName, sExtension, sResourceDirectory);
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

		public static Icon GetIconResource(string sResourceName, string sResourceDirectory)
		{
			Icon iconFinal = null;
			string sResourceFilePath = GetResourceFilePath(sResourceName, "ico", sResourceDirectory);

			if (File.Exists(sResourceFilePath))
			{
				iconFinal = Icon.ExtractAssociatedIcon(sResourceFilePath);
			}
			else
			{
				object oResource = Resources.ResourceManager.GetObject(sResourceName);
				if (oResource is Icon icon)
					iconFinal = icon;
				else if (oResource is Bitmap bitmap)
				{
					IntPtr icH = bitmap.GetHicon();
					iconFinal = Icon.FromHandle(icH);
					//DestroyIcon(icH);
				}
			}

			return iconFinal;
		}

		public static Image GetImageResource(string sResourceName, string sExtension, string sResourceDirectory)
		{
			Image imageFinal = null;
			string sResourceFilePath = GetResourceFilePath(sResourceName, sExtension, sResourceDirectory);

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
