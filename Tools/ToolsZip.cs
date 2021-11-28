using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DcsBriefop.Tools
{
	internal static class ToolsZip
	{
		public static string PathToZip(string sPath)
		{
			return sPath.Replace(@"\", "/");
		}

		public static void ReplaceZipEntry(ZipArchive za, string sEntryName, string sEntryContent)
		{
			ZipArchiveEntry zpe = za.GetEntry(sEntryName);
			if (zpe is object)
				zpe.Delete();

			zpe = za.CreateEntry(sEntryName);
			using (StreamWriter writer = new StreamWriter(zpe.Open()))
			{
				writer.Write(sEntryContent);
			}
		}

		public static void RemoveZipEntries(ZipArchive za, string sEntryName)
		{
			foreach(ZipArchiveEntry ze in za.Entries.Where(_ze => _ze.FullName == sEntryName).ToList())
				ze.Delete();
		}
	}
}
