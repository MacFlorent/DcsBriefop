using log4net;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DcsBriefop
{
	internal class Log
	{
		private static readonly ILog logger = LogManager.GetLogger("MainLogger");

		static Log()
		{
			log4net.Config.XmlConfigurator.Configure();
		}

		////// Logs
		public static void Error(string sMessage, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			logger.Error(FormatWithCallerInformation(sMemberName, iLineNumber, sMessage));
		}
		public static void Exception(Exception ex, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			logger.Error(FormatWithCallerInformation(sMemberName, iLineNumber, ex.Message));
			logger.Error(ex.StackTrace);
		}
		public static void Warning(string sMessage)
		{
			logger.Warn(sMessage);
		}

		public static void Info(string sMessage)
		{
			logger.Info(sMessage);
		}
		public static void Debug(string sMessage)
		{
			logger.Debug(sMessage);
		}

		////// Application events
		public static void ApplicationStart([CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			ApplicationEvent(true, sMemberName, iLineNumber);
		}

		public static void ApplicationEnd([CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			ApplicationEvent(false, sMemberName, iLineNumber);
		}

		private static void ApplicationEvent(bool bStart, string sMemberName, int iLineNumber)
		{
			string sEvent = bStart ? "START" : "END";
			char c = bStart ? '>' : '<';
			int iStatic = 15;
			int iVariable = 30;

			AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
			string sApplicationDescription = $"{assemblyName?.Name} {assemblyName?.Version}";
			string sMainLineStatic = new string(c, iStatic);
			string sMainLineVariable = ($" {sApplicationDescription} {sEvent} ").PadRight(iVariable, ' ');

			if (bStart)
			{
				Info("");
			}
			Info($"{sMainLineStatic}{sMainLineVariable}{sMainLineStatic}");
		}

		////// Tools
		private static string FormatWithCallerInformation(string sMemberName, int iLineNumber, string sMessage)
		{
			string sLineNumber = "";
			if (iLineNumber > 0)
				sLineNumber = $":{iLineNumber:d3}";

			string sCallerInformation = ($"{sMemberName}{sLineNumber}").PadRight(10, ' ');
			return $"{sCallerInformation} {sMessage}";
		}
	}
}
