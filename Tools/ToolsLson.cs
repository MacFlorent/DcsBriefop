using LsonLib;
using System.Collections.Generic;
using System.Linq;

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

		public static decimal? IfExistsDecimal(this LsonDict lsd, string sKey)
		{
			return lsd.IfExists(sKey)?.GetDecimalSafe();
		}


		public static string IfExistsString(this LsonDict lsd, string sKey)
		{
			return lsd.IfExists(sKey)?.GetString();
		}

		public static void SetIfExists(this LsonDict lsd, string sKey, LsonValue value)
		{
			if (lsd.ContainsKey(sKey))
				lsd[sKey] = value;
		}

		public static void SetOrAdd(this LsonDict lsd, string sKey, LsonValue value)
		{
			if (lsd.ContainsKey(sKey))
				lsd[sKey] = value;
			else if (value is object)
				lsd.Add(sKey, value);
		}

		public static void SetOrAddBool(this LsonDict lsd, string sKey, bool? bValue)
		{
			if (bValue is object)
				lsd.SetOrAdd(sKey, bValue.Value);
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

		public static List<LsonValue> GetOrderedValueList(LsonDict lsd)
		{
			var kvpList = lsd.ToList();
			kvpList.Sort((kvp1, kvp2) => kvp1.Key.GetInt().CompareTo(kvp2.Key.GetInt()));
			return kvpList.Select(_kvp => _kvp.Value).ToList();
		}
	}
}
