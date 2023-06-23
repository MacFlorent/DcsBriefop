using CommandLine;
using DcsBriefop.Data;
using System.Collections;
using System.Text;

namespace DcsBriefop.Tools
{
	internal class OptionsCommon
	{
		[Option('l', "loglevel", Required = false, HelpText = "Override logging level for log4net")]
		public string LogLevel { get; set; }
		[Option('d', "debug", Required = false, HelpText = "Debug mode")]
		public bool Debug { get; set; }
	}

	[Verb("app", isDefault:true, HelpText = "Launch the windows application")]
	class OptionsApp : OptionsCommon
	{
	}

	[Verb("batch", HelpText = "Process a file without opening the application windows")]
	class OptionsBatch : OptionsCommon
	{
		[Option('m', "miz", Required = true, HelpText = "The path of the miz file to be batch processed, or the directory where to find it")]
		public string Miz { get; set; }
		[Option('b', "briefing-output", Required = false, Default = ElementBriefingOutput.Miz, HelpText = "Combinable briefing files generation options (None,Miz,Directory")]
		public ElementBriefingOutput BriefingOutput { get; set; }
	}

	internal static class ToolsCommandLine
	{
		#region Fields
		private static StringBuilder m_parserTextBuilder = new StringBuilder();
		private static StringWriter m_parserTextWriter = new StringWriter(m_parserTextBuilder);
		#endregion

		#region Methods
		public static OptionsCommon ParseCommandLine(string[] args)
		{
			Parser parser = new Parser(config =>{	config.HelpWriter = m_parserTextWriter;	});
			return parser.ParseArguments<OptionsApp, OptionsBatch>(args)
					 .MapResult(
							 (OptionsApp o) => { ApplyOptionsApp(o); return o; },
							 (OptionsBatch o) => { ApplyOptionsBatch(o); return o; },
							 errors => { ParseError(errors); return null as OptionsCommon; });
		}

		private static void ApplyOptionsCommon(OptionsCommon o)
		{
			if (!string.IsNullOrEmpty(o.LogLevel))
				Log.LogLevel = o.LogLevel;
			
			Globals.Debug = o.Debug;
		}

		private static void ApplyOptionsApp(OptionsApp o)
		{
			ApplyOptionsCommon(o);
		}

		private static void ApplyOptionsBatch(OptionsBatch o)
		{
			ApplyOptionsCommon(o);
		}

		private static void ParseError(IEnumerable errors)
		{
			Log.Error(m_parserTextBuilder.ToString());
			ToolsControls.ShowMessageBoxError(m_parserTextBuilder.ToString());
		}
		#endregion
	}
}
