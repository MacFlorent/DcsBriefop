using DcsBriefop.Tools;
using Newtonsoft.Json;
using OSGeo.OSR;

namespace DcsBriefop.Data
{
	//https://github.com/pydcs/dcs/tree/master/dcs/terrain
	internal class TheatreProjection
	{
		public string Theatre { get; set; }
		public string Projection { get; set; }
	}

	internal static class TheatreProjectionManager
	{
		public static SpatialReference BriefopSpatialReference { get; set; }
		public static List<TheatreProjection> TheatreProjections { get; set; }

		static TheatreProjectionManager()
		{
			try
			{
				Initialize();
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build theatre projection data. Map data informations will not be available", e);
				Log.Exception(e);
			}
		}

		public static void Initialize()
		{
			string sJsonStream = ToolsResources.GetJsonResourceContent(ElementBopResource.TheatreProjection, null);
			if (!string.IsNullOrEmpty(sJsonStream))
				TheatreProjections = JsonConvert.DeserializeObject<List<TheatreProjection>>(sJsonStream);

			string sBriefopProj4 = GetProjection("Briefop") ?? "+proj=longlat +datum=WGS84 +no_defs +type=crs";
			BriefopSpatialReference = new SpatialReference("");
			BriefopSpatialReference.ImportFromProj4(sBriefopProj4);

		}

		public static string GetProjection(string sTheatre)
		{
			return TheatreProjections.Where(_p => _p.Theatre == sTheatre).Select(_p => _p.Projection).FirstOrDefault();
		}

	}
}

