using DcsBriefop.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class DcsUnit
	{
		public string Type { get; set; }
		public string Description { get; set; }
		public string MapMarker { get; set; }
		public string Information { get; set; }
	}

	internal static class DcsUnitManager
	{
		static List<DcsUnit> DcsUnits = new List<DcsUnit>();

		static DcsUnitManager()
		{
			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent($"Units");
				DcsUnits = JsonConvert.DeserializeObject<List<DcsUnit>>(sJsonStream);
			}
			catch (Exception e)
			{
				Log.Error("Failed to build unit data. Unit informations will not be available");
				Log.Exception(e);
				DcsUnits = null;
			}
		}

		public static DcsUnit GetUnit(string sType)
		{
			return DcsUnits.Where(_u => _u.Type == sType).FirstOrDefault();
		}
	}
}
