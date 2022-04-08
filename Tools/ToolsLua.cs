using LsonLib;
using System.Collections.Generic;
using System.IO;

namespace DcsBriefop.Tools
{
	internal class ToolsLua
	{
		public static string ReadLuaFileContent(string sFilePath)
		{
			string sFileContent = File.ReadAllText(sFilePath);
			sFileContent = sFileContent.Replace("\r\n", "\n");
			return sFileContent;
		}

		public static string LsonRootToCorrectedString(Dictionary<string, LsonValue> root)
		{
			string s = LsonVars.ToString(root);
			s = s.Replace("\r\n", "\n");
			s = s.Replace("\t", "    ");

			if (s.StartsWith("\n"))
				s = s.Substring(1, s.Length - 1);

			return s;
		}
	}
}
