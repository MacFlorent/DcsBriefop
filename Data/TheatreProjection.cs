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

			string sBriefopProjection = GetProjection("Briefop") ?? "+proj=latlong +datum=WGS84 +no_defs";
			BriefopProjection = DotSpatial.Projections.ProjectionInfo.FromProj4String(sBriefopProjection);
		}

		public static string GetProjection(string sTheatre)
		{
			return TheatreProjections.Where(_p => _p.Theatre == sTheatre).Select(_p => _p.Projection).FirstOrDefault();
		}

	}
}

