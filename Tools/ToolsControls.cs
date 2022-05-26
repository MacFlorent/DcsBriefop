using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DcsBriefop.Tools
{
	internal static class ToolsControls
	{
		#region MessageBox
		public static void ShowMessageBoxInfo(string sMessage)
		{
			MessageBox.Show(sMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static bool ShowMessageBoxQuestion(string sMessage)
		{
			return MessageBox.Show(sMessage, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
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
		#endregion

		#region DataGridView
		public static void SetDataGridViewProperties(DataGridView dgv)
		{
			dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgv.AllowUserToResizeColumns = true;
			dgv.AllowUserToAddRows = false;
			dgv.RowHeadersVisible = false;
			dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
		}
		#endregion

		#region Redraw Suspend/Resume
		[DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
		private const int WM_SETREDRAW = 0xB;

		public static void SuspendDrawing(this Control target)
		{
			SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
		}

		public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
		public static void ResumeDrawing(this Control target, bool redraw)
		{
			SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

			if (redraw)
			{
				target.Refresh();
			}
		}
		#endregion

		public static void DisposeAndClearTabs(this TabControl tc)
		{
			while (tc.TabCount > 0)
				tc.TabPages[0].Dispose();
			tc.TabPages.Clear();

		}
	}
}

