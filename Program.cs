﻿using DcsBriefop.Data;
using DcsBriefop.FormBop;
using DcsBriefop.Tools;
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

			OptionsCommon commandLineOptions = ToolsCommandLine.ParseCommandLine(args);
			if (commandLineOptions is OptionsBatch optionsBatch)
			{
				return MainBatch(optionsBatch);
			}
			else if (commandLineOptions is OptionsApp optionsApp)
			{
				return MainApp(optionsApp);
			}
			else
			{
				return -1;
			}
		}

		private static int MainApp(OptionsApp optionsApp)
		{
			Log.ApplicationStart();
			Log.Info("Starting app verb");

			FrmBop f = new FrmBop();
			f.Show(null);
			f.FormClosed += MainFormClosed;
			Application.Run(f);

			return 1;
		}

		private static int MainBatch(OptionsBatch optionsBatch)
		{
			Log.ApplicationStart();
			Log.Info($"Starting batch verb for {optionsBatch.MizFile}");

			try
			{
				//Log.Info($"Reading miz file content");
				//BriefingManager missionManager = new BriefingManager(optionsBatch.MizFile);
				//Log.Info($"Building DcsBriefop data for miz file");
				//BriefingContainer briefingContainer = new BriefingContainer(missionManager.Miz);

				//Log.Info($"Generating kneeboard briefing files");
				//using (BriefingFilesBuilder builder = new BriefingFilesBuilder(briefingContainer, missionManager))
				//	builder.Generate();

				Log.ApplicationEnd();
				return 1;
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
				return -1;
			}
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
			ToolsControls.ShowMessageBoxError(sMessage);
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
