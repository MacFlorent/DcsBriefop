using DcsBriefop.Forms;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Globalization;
using System.Runtime.CompilerServices;

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
			InitializeGMaps();

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

			FrmMain f = new FrmMain();
			f.Show(null);
			f.FormClosed += MainFormClosed;
			Application.Run(f);

			return 1;
		}

		private static int MainBatch(OptionsBatch optionsBatch)
		{
			Log.ApplicationStart();
			Log.Info($"Starting batch verb for {optionsBatch.Miz}");

			try
			{
				string sMizFilePath = optionsBatch.Miz;
				if (!File.Exists(sMizFilePath) && Directory.Exists(sMizFilePath))
					sMizFilePath = Directory.GetFiles(sMizFilePath, "*.miz").FirstOrDefault();

				if (!File.Exists(sMizFilePath))
					throw new ExceptionBop($"Miz file not found for batch verb [ {optionsBatch.Miz} ]");

				Log.Info($"Reading miz file content {sMizFilePath}");
				BriefopManager bopManager = new BriefopManager(sMizFilePath);
				Log.Info("Saving updated data");
				bopManager.MizSave(null);

				if (optionsBatch.BriefingOutput.HasFlag(Data.ElementBriefingOutput.Miz))
				{
					Log.Info($"Generating kneeboard in Miz file");
					bopManager.GenerateBriefing(Data.ElementBriefingOutput.Miz);
				}
				if (optionsBatch.BriefingOutput.HasFlag(Data.ElementBriefingOutput.Miz))
				{
					Log.Info($"Generating kneeboard in directory");
					bopManager.GenerateBriefing(Data.ElementBriefingOutput.Directory);
				}

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

		private static void InitializeGMaps()
		{
			GMaps.Instance.Mode = AccessMode.ServerOnly; // the program has trouble terminating all its threads in cached mode, don't know why, better stick to server only for now
			GMapImageProxy.Enable();
			//System.Net.WebProxy myProxy = new System.Net.WebProxy(“XXXXXXX.com”, 3128);
			//GMap.NET.MapProviders.GMapProvider.WebProxy = myProxy;
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
