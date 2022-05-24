using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DcsBriefop
{
	internal class Log
	{
		#region Fields
		private static readonly ILog m_logger = LogManager.GetLogger("MainLogger");
		#endregion

		#region Properties
		public static string LogLevel
		{
			get
			{
				return ((Logger)m_logger.Logger)?.Level.Name;
			}
			set
			{
				Logger currentLogger = (Logger)m_logger.Logger;
				currentLogger.Level = currentLogger.Hierarchy.LevelMap[value];
			}
		}

		#endregion

		#region CTOR
		static Log()
		{
			log4net.Config.XmlConfigurator.Configure();
		}
		#endregion

		#region Methods
		////// Logs
		public static void Error(string sMessage, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			m_logger.Error(FormatWithCallerInformation(sMemberName, iLineNumber, sMessage));
		}
		public static void Exception(Exception ex, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			m_logger.Error(FormatWithCallerInformation(sMemberName, iLineNumber, ex.Message));
			m_logger.Error(ex.StackTrace);
		}
		public static void Warning(string sMessage)
		{
			m_logger.Warn(sMessage);
		}

		public static void Info(string sMessage)
		{
			m_logger.Info(sMessage);
		}
		public static void Debug(string sMessage)
		{
			m_logger.Debug(sMessage);
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
		#endregion
	}
}
