using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

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

		public static void ShowMessageBoxError(string sMessage)
		{
			MessageBox.Show(sMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowMessageBoxAndLogException(string sMessage, Exception e, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			ShowMessageBoxError($"{sMessage}{Environment.NewLine}{Environment.NewLine}{e.Message}");
			Log.Error(sMessage, sMemberName, iLineNumber);
			Log.Exception(e, sMemberName, iLineNumber);
		}

		public static void SetDataGridViewProperties(DataGridView dgv)
		{
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgv.AllowUserToResizeColumns = true;
			dgv.AllowUserToAddRows = false;
			dgv.RowHeadersVisible = false;
			dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
