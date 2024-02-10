using CoordinateSharp;
using DcsBriefop.Data;
using GMap.NET;
using System.Text;

namespace DcsBriefop.Tools
{
	internal static class ToolsCoordinate
	{
		public static string ToStringDMS(this Coordinate c)
		{
			CoordinateFormatOptions cfo = new CoordinateFormatOptions();
			cfo.Format = CoordinateFormatType.Degree_Minutes_Seconds;
			cfo.Round = 2;
			cfo.Display_Leading_Zeros = true;
			return c.ToString(cfo);
		}

		public static string ToStringDDM(this Coordinate c)
		{
			CoordinateFormatOptions cfo = new CoordinateFormatOptions();
			cfo.Format = CoordinateFormatType.Degree_Decimal_Minutes;
			cfo.Display_Leading_Zeros = true;
			cfo.Round = 4;
			return c.ToString(cfo);
		}

		public static string ToStringMGRS(this Coordinate c)
		{
			return c.MGRS.ToString();
		}

		public static string ToString(this Coordinate coordinate, ElementCoordinateDisplay coordinateDisplay)
		{
			StringBuilder sb = new StringBuilder();
			if ((coordinateDisplay & ElementCoordinateDisplay.Mgrs) > 0)
				sb.AppendWithSeparator(coordinate.ToStringMGRS(), Environment.NewLine);
			if ((coordinateDisplay & ElementCoordinateDisplay.Dms) > 0)
				sb.AppendWithSeparator(coordinate.ToStringDMS(), Environment.NewLine);
			if ((coordinateDisplay & ElementCoordinateDisplay.Ddm) > 0)
				sb.AppendWithSeparator(coordinate.ToStringDDM(), Environment.NewLine);

			return sb.ToString();
		}

		public static double NormalizeBearing(double dBearing)
		{
			double dNormalizedBearing = dBearing % 360;

			if (dNormalizedBearing < 0)
				dNormalizedBearing += 360;
			else if (dNormalizedBearing == 0)
				dNormalizedBearing = 360;

			return dNormalizedBearing;
		}

		public static Tuple<double, double> ReprojectPoint(DotSpatial.Projections.ProjectionInfo piSource, DotSpatial.Projections.ProjectionInfo piDestination, Tuple<double, double> pointInput)
		{
			// here as per DotSpatial, X is horizontal and Y vertical (which is the opposite of DCS)
			double[] xy = { pointInput.Item1, pointInput.Item2 };
			double[] z = { 0 };

			DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, piSource, piDestination, 0, 1);

			return new Tuple<double, double>(xy[0], xy[1]);
		}
	}
}
