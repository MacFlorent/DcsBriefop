using LsonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
			sDcsLsonContent = sDcsLsonContent.Replace("\\\r\n", "\\n");
			sDcsLsonContent = sDcsLsonContent.Replace("\\\n", "\\n");

			return sDcsLsonContent;
		}

		public static string LsonRootToDcs(Dictionary<string, LsonValue> root)
		{
			// When going back from the LsonLib serialized string, we need to :
			// - reposition the multi-line strings as is recognized by DCS
			// - replace the actual new lines, which are put by LsonLib with \r\n by the \n used by DCS
			string s = LsonVars.ToString(root);

			s = ReplaceDcsStringLineBreaks(s, 0);
			s = s.Replace("\r\n", "\n");
			s = s.Replace("\t", "    ");

			if (s.StartsWith("\n"))
				s = s.Substring(1, s.Length - 1);

			return s;
		}

		public static string ReplaceDcsStringLineBreaks(string s, int iStartIndex)
		{
			// cannot replace  simply as such s = s.Replace("\\n", "\\\n") because the first backslah may be also escaped
			// eg : [1] = "a_do_script(\"VEAF_DYNAMIC_PATH = [[C:\\\\DEV\\\\MISSIONS\\\\operation-bluestorm\\\\node_modules\\\\veaf-mission-creation-tools\\\\]]\");",
			// so we only want to replace if our "\\n" is following 0 or an even number of \
			int iFoundIndex = s.IndexOf("\\n", iStartIndex);
			if (iFoundIndex < 0)
				return s;
			else
			{
				int iCountBackslashBefore = 0;
				for (int i = iFoundIndex - 1; i > 0; i--)
				{
					if (s[i] == '\\')
						iCountBackslashBefore++;
					else
						break;
				}

				string sReplaced = s;
				if (iCountBackslashBefore % 2 == 0)
				{
					StringBuilder sb = new StringBuilder(s);
					sb[iFoundIndex] = '\\';
					sb[iFoundIndex + 1] = '\n';
					sReplaced = sb.ToString();
					iFoundIndex++;
				}


				return ReplaceDcsStringLineBreaks(sReplaced, iFoundIndex + 1);
			}
		}

		public static string DcsTextToDisplay(string sDcsText)
		{
			// As in the multi-line strings we have simple \n for new lines, we want to replace them by correct Environment.Newline before putting them to display
			return sDcsText?.Replace("\n", Environment.NewLine);
		}
		public static string DisplayToDcsText(string sDisplay)
		{
			// And vice versa
			return sDisplay?.Replace(Environment.NewLine, "\n");
		}

	}
}
