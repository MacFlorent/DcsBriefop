using CoordinateSharp;
using DcsBriefop.Data;
using System.Text;
using System;

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

		public static string ToStringLocalisation(this Coordinate coordinate, ElementCoordinateDisplay coordinateDisplay)
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
	}
}
