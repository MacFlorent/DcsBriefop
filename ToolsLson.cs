using LsonLib;

namespace DcsBriefop
{
	internal static class ToolsLson
	{
		public static LsonValue IfExists(this LsonDict lsd, string sKey)
		{
			 return lsd.ContainsKey(sKey) ? lsd[sKey] : null;
		}

		public static bool? IfExistsBool(this LsonDict lsd, string sKey)
		{
			return lsd.IfExists(sKey)?.GetBoolSafe();
		}

		public static int? IfExistsInt(this LsonDict lsd, string sKey)
		{
			return lsd.IfExists(sKey)?.GetIntSafe();
		}

		public static string IfExistsString(this LsonDict lsd, string sKey)
		{
			return lsd.IfExists(sKey)?.GetString();
		}
	}
}
