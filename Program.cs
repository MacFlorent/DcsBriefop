using DcsBriefop.Tools;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DcsBriefop
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Log.ApplicationStart();

			FrmMain f = new FrmMain();
			f.Show(null);
			f.FormClosed += MainFormClosed;
			Application.Run(f);
		}

		private static void ManageException(Exception ex)
		{
			Log.Exception(ex);
			string sMessage = $"Unhandled error.{Environment.NewLine}{ex?.Message}{Environment.NewLine}{Environment.NewLine}{ex?.InnerException}";
			ToolsMisc.ShowMessageBoxError (sMessage);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ManageException(e.ExceptionObject as Exception);
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			ManageException(e.Exception);
		}

		private static void MainFormClosed(object sender, FormClosedEventArgs e)
		{
			Log.ApplicationEnd();
			Application.Exit();
		}
	}
}
