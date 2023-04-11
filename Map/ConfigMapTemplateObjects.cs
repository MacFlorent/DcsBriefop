using System.Drawing.Drawing2D;

namespace DcsBriefop.Map
{
	internal class ConfigMapTemplateRoutes
	{
		public string Directory { get; set; }
		public List<ConfigMapTemplateRoute> Templates { get; set; } = new List<ConfigMapTemplateRoute>();
	}

	internal class ConfigMapTemplateRoute
	{
		public string FileName { get; set; }
		public string DcsMizStyle { get; set; }
		public double? ThicknessCorrection { get; set; }
		public DashStyle? DashOverride { get; set; }
	}

	internal class ConfigMapTemplateMarkers
	{
		public string Directory { get; set; }
		public int? DefaultWidth { get; set; }
		public int? DefaultHeight { get; set; }
		public string DefaultMark { get; set; }
		public string DefaultAirdrome { get; set; }
		public string DefaultAircraft { get; set; }
		public string DefaultHelicopter { get; set; }
		public string DefaultGround { get; set; }
		public string DefaultShip { get; set; }
		public string DefaultWaypoint { get; set; }
		public string DefaultBullseye { get; set; }
		public List<ConfigMapTemplateMarker> Templates { get; set; } = new List<ConfigMapTemplateMarker>();
	}

	internal class ConfigMapTemplateMarker
	{
		public string FileName { get; set; }
		public string DcsMizFileName { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public double? OffsetWidth { get; set; }
		public double? OffsetHeight { get; set; }

	}
}