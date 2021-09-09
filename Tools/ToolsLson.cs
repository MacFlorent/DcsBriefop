using LsonLib;

namespace DcsBriefop.Tools
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

		public static void SetOrAddBool(this LsonDict lsd, string sKey, bool? bValue)
		{
			bool bNotNull = bValue.GetValueOrDefault(false);

			if (lsd.ContainsKey(sKey))
				lsd[sKey] = bNotNull;
			else if (bNotNull)
				lsd.Add(sKey, bNotNull);
		}

		public static void SetOrAddInt(this LsonDict lsd, string sKey, int? iValue)
		{
			if (lsd.ContainsKey(sKey))
				lsd[sKey] = iValue;
			else if (iValue is object)
				lsd.Add(sKey, iValue);
		}

		public static void SetOrAddString(this LsonDict lsd, string sKey, string sValue)
		{
			if (lsd.ContainsKey(sKey))
				lsd[sKey] = sValue;
			else if (!string.IsNullOrEmpty(sValue))
				lsd.Add(sKey, sValue);
		}

	}
}
