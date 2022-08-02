using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
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
		public static void Error(string sMessage, [CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			m_logger.Error(FormatWithCallerInformation(sCallerFilePath, iLineNumber, sCallerMemberName, sMessage));
		}
		public static void Exception(Exception ex, [CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			m_logger.Error(FormatWithCallerInformation(sCallerFilePath, iLineNumber, sCallerMemberName, ex.Message));
			m_logger.Error(ex.StackTrace);
		}
		public static void Warning(string sMessage, [CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			m_logger.Warn(FormatWithCallerInformation(sCallerFilePath, iLineNumber, sCallerMemberName, sMessage));
		}

		public static void Info(string sMessage, [CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			m_logger.Info(FormatWithCallerInformation(sCallerFilePath, iLineNumber, sCallerMemberName, sMessage));
		}
		public static void Debug(string sMessage, [CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			m_logger.Debug(FormatWithCallerInformation(sCallerFilePath, iLineNumber, sCallerMemberName, sMessage));
		}

		////// Application events
		public static void ApplicationStart([CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			ApplicationEvent(true, sCallerFilePath, iLineNumber, sCallerMemberName);
		}

		public static void ApplicationEnd([CallerFilePath] string sCallerFilePath = null, [CallerLineNumber] int iLineNumber = 0, [CallerMemberName] string sCallerMemberName = null)
		{
			ApplicationEvent(false, sCallerFilePath, iLineNumber, sCallerMemberName);
		}

		private static void ApplicationEvent(bool bStart, string sCallerFilePath, int iLineNumber, string sCallerMemberName)
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
				Info("", sCallerFilePath, iLineNumber, sCallerMemberName);
			}
			Info($"{sMainLineStatic}{sMainLineVariable}{sMainLineStatic}", sCallerFilePath, iLineNumber, sCallerMemberName);
		}

		////// Tools
		private static string FormatWithCallerInformation(string sFilePath, int iLineNumber, string sMemberName, string sMessage)
		{
			string sFileInformation = "", sMemberInformation = "";

			if (!string.IsNullOrEmpty(sFilePath))
				sFileInformation = Path.GetFileName(sFilePath);
			if (iLineNumber > 0)
				sFileInformation = $"{sFileInformation}:{iLineNumber:d3}";

			if (!string.IsNullOrEmpty(sFileInformation))
				sFileInformation = sFileInformation.PadRight(30, ' ');

			if (!string.IsNullOrEmpty(sMemberName))
				sMemberInformation = sMemberName.PadRight(15, ' ');

			string sCallerInformation = ($"{sFileInformation}{sMemberInformation}");
			return $"{sCallerInformation} {sMessage}";
		}
		#endregion
	}
}
