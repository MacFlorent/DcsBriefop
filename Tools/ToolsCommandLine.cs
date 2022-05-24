using CommandLine;
using System.Collections;
using System.IO;
using System.Text;

namespace DcsBriefop.Tools
{
	internal class OptionsCommon
	{
		[Option('l', "loglevel", Required = false, HelpText = "Override logging level for log4net")]
		public string LogLevel { get; set; }
	}

	[Verb("app", isDefault:true, HelpText = "Launch the windows application")]
	class OptionsApp : OptionsCommon
	{
	}

	[Verb("batch", HelpText = "Process a file without opening the application windows")]
	class OptionsBatch : OptionsCommon
	{
		[Option('m', "mizFile", Required = true, HelpText = "The path of the miz file to be batch processed")]
		public string MizFile { get; set; }

	}

	internal static class ToolsCommandLine
	{
		#region Fields
		private static StringBuilder m_parserTextBuilder = new StringBuilder();
		private static StringWriter m_parserTextWriter = new StringWriter(m_parserTextBuilder);
		#endregion

		#region Methods
		public static bool ParseCommandLine(string[] args)
		{
			Parser parser = new Parser(config =>{	config.HelpWriter = m_parserTextWriter;	});
			return parser.ParseArguments<OptionsApp, OptionsBatch>(args)
					 .MapResult(
							 (OptionsApp o) => { return ApplyOptionsApp(o); },
							 (OptionsBatch o) => { return ApplyOptionsBatch(o); ; },
							 errors => { return ParseError(errors); });
		}

		private static bool ApplyOptionsCommon(OptionsCommon o)
		{
			if (!string.IsNullOrEmpty(o.LogLevel))
				Log.LogLevel = o.LogLevel;
			
			return true;
		}

		private static bool ApplyOptionsApp(OptionsApp o)
		{
			ApplyOptionsCommon(o);
			return true;
		}

		private static bool ApplyOptionsBatch(OptionsBatch o)
		{
			ApplyOptionsCommon(o);
			return true;
		}

		private static bool ParseError(IEnumerable errors)
		{
			Log.Error(m_parserTextBuilder.ToString());
			ToolsMisc.ShowMessageBoxError(m_parserTextBuilder.ToString());
			return false;
		}
		#endregion
	}
}
