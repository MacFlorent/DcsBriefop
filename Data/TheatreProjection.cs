using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.Data
{
	internal class TheatreProjection
	{
		public string Theatre { get; set; }
		public string Projection { get; set; }
	}

	internal static class TheatreProjectionManager
	{
		public static DotSpatial.Projections.ProjectionInfo BriefopProjection { get; set; }
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

			string sBriefopProjection = "+title=long/lat:WGS84 +proj=longlat +a=6378137.0 +b=6356752.31424518 +ellps=WGS84 +datum=WGS84 +units=degrees6"; // https://proj.org/en/9.3/operations/projections/tmerc.html
			BriefopProjection = DotSpatial.Projections.ProjectionInfo.FromProj4String(sBriefopProjection);
		}

		public static string GetProjection(string sTheatre)
		{
			return TheatreProjections.Where(_p => _p.Theatre == sTheatre).Select(_p => _p.Projection).FirstOrDefault();
		}

	}
}

