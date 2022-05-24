﻿using DcsBriefop.Tools;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace DcsBriefop
{
	static class Program
	{
		[STAThread]
		static int Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			InitializeCulture();

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			if (!ToolsCommandLine.ParseCommandLine(args))
			{
				return -1;
			}

			Log.ApplicationStart();

			FrmMain f = new FrmMain();
			f.Show(null);
			f.FormClosed += MainFormClosed;
			Application.Run(f);

			return 1;
		}

		private static void InitializeCulture()
		{
			CultureInfo cultureInfo = CultureInfo.InvariantCulture;
			//CultureInfo cultureInfo = new CultureInfo("fr-FR"); ;
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
		}

		private static void ManageException(Exception ex, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			Log.Exception(ex, sMemberName, iLineNumber);
			string sMessage = $"Unhandled error.{Environment.NewLine}{ex?.Message}{Environment.NewLine}{Environment.NewLine}{ex?.InnerException}";
			ToolsMisc.ShowMessageBoxError(sMessage);
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
