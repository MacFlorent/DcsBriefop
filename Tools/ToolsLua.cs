using LsonLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace DcsBriefop.Tools
{
	internal class ToolsLua
	{
		public static string ReadLuaFileContent(string sFilePath)
		{
			string sFileContent = File.ReadAllText(sFilePath);
			return DcsToLsonRootString(sFileContent);
		}

		public static string DcsToLsonRootString(string sDcsLsonContent)
		{
			// In DCS mission dictionnary, multi-line strings - like the mission description - represent new lines by a backslash and a line feed
			// This is not supported by LsonLib, so we replace them by an escaped line feed which is ok
			sDcsLsonContent = sDcsLsonContent.Replace("\\\n", "\\n");
			return sDcsLsonContent;
		}

		public static string LsonRootToDcs(Dictionary<string, LsonValue> root)
		{
			// When going back from the LsonLib serialized string, we need to :
			// - reposition the multi-line strings as is recognized by DCS
			// - replace the actual new lines, which are put by LsonLib with \r\n by the \n used by DCS
			string s = LsonVars.ToString(root);
			s = s.Replace("\\n", "\\\n");
			s = s.Replace("\r\n", "\n");
			s = s.Replace("\t", "    ");

			if (s.StartsWith("\n"))
				s = s.Substring(1, s.Length - 1);

			return s;
		}

		public static string DcsTextToDisplay(string sDcsText)
		{
			// As in the multi-line strings we have simple \n for new lines, we want to replace them by correct Environment.Newline before putting them to display
			return sDcsText.Replace("\n", Environment.NewLine);
		}
		public static string DisplayToDcsText(string sDisplay)
		{
			// And vice versa
			return sDisplay.Replace(Environment.NewLine, "\n");
		}

	}
}
