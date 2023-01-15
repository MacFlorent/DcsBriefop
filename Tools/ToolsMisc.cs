using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace DcsBriefop.Tools
{
	internal static class ToolsMisc
	{
		public static void AppendWithSeparator(this StringBuilder sb, string value, string sSeparator)
		{
			if (sb.Length > 0)
				sb.Append(sSeparator);
			sb.Append(value);
		}

		public static string GetDirectoryFullPath(string sDirectoryString)
		{
			string sExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (sDirectoryString == ".")
				sDirectoryString = sExecutionPath;
			else if (sDirectoryString.StartsWith(@".\"))
				sDirectoryString = sDirectoryString.Replace(@".\", $@"{sExecutionPath}\");

			return sDirectoryString;
		}

		public static string GetDirectoryDcsSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS");
		}

		public static string GetDirectoryDcsBetaSave()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Saved Games\DCS.openbeta");
		}

		public static T CloneJson<T>(this T source)
		{
			// Don't serialize a null object, simply return the default for that object
			if (ReferenceEquals(source, null))
				return default;

			// initialize inner objects individually
			// for example in default constructor some list property initialized with some values,
			// but in 'source' these items are cleaned -
			// without ObjectCreationHandling.Replace default constructor values will be added to result
			JsonSerializerSettings deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
		}
	}
}
