using DcsBriefop.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	//https://wiki.hoggitworld.com/view/Threat_Database
	internal class DcsUnit
	{
		public string Type { get; set; }
		public string Description { get; set; }
		public string MapMarker { get; set; }
		public string Information { get; set; }
		public string KneeboardFolder { get; set; }
	}

	internal static class DcsUnitManager
	{
		static List<DcsUnit> DcsUnits = new List<DcsUnit>();

		static DcsUnitManager()
		{
			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent("Units");
				DcsUnits = JsonConvert.DeserializeObject<List<DcsUnit>>(sJsonStream);
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build unit data. Unit informations will not be available", e);
				Log.Exception(e);
				DcsUnits = null;
			}

			if (DcsUnits is null)
				DcsUnits = new List<DcsUnit>();
		}

		public static DcsUnit GetUnit(string sType)
		{
			return DcsUnits.Where(_u => _u.Type == sType).FirstOrDefault();
		}
	}
}
