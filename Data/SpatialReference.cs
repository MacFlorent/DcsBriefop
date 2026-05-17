using ProjNet.CoordinateSystems;

namespace DcsBriefop.Data
{
	internal class SpatialReference(string sProj4)
	{
		#region Properties
		public CoordinateSystem CoordinateSystem { get; } = ParseProj4(sProj4);
		#endregion

		#region Methods
		public string ToStringProj4() => sProj4;

		private static CoordinateSystem ParseProj4(string sInput)
		{
			// string constants:
            // - Proj4 parameter keys: https://proj.org/en/stable/operations/projections/tmerc.html
            // - OGC WKT parameter names (as expected by ProjNet): OGC 01-009 / ProjNet MapProjection.cs
            Dictionary<string, string> dict = new(StringComparer.OrdinalIgnoreCase);
			foreach (string token in sInput.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				if (!token.StartsWith('+'))
					continue;

				string[] kv = token[1..].Split('=', 2);
				dict[kv[0]] = kv.Length > 1 ? kv[1] : "true";
			}

			string sProjType = dict.GetValueOrDefault("proj", "");

			if (sProjType is "longlat" or "latlong")
				return GeographicCoordinateSystem.WGS84;

			if (sProjType == "tmerc")
			{
				CoordinateSystemFactory cf = new();
				IProjection projection = cf.CreateProjection("Transverse_Mercator", "Transverse_Mercator", new List<ProjectionParameter>
				{
					new("latitude_of_origin", GetParam(dict, "lat_0")),
					new("central_meridian", GetParam(dict, "lon_0")),
					new("scale_factor", GetParam(dict, "k_0", 1.0)),
					new("false_easting", GetParam(dict, "x_0")),
					new("false_northing", GetParam(dict, "y_0")),
				});

				return cf.CreateProjectedCoordinateSystem("DCS Theatre", GeographicCoordinateSystem.WGS84, projection,
					LinearUnit.Metre, new AxisInfo("X", AxisOrientationEnum.East), new AxisInfo("Y", AxisOrientationEnum.North));
			}

			throw new NotSupportedException($"Unsupported projection type: {sProjType}");
		}

		private static double GetParam(Dictionary<string, string> dict, string key, double fallback = 0.0)
		{
			return dict.TryGetValue(key, out string val)
				&& double.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double d)
				? d : fallback;
		}
		#endregion
	}
}
