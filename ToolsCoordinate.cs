using CoordinateSharp;

namespace DcsBriefop
{
	internal static class ToolsCoordinate
	{
		public static string ToStringDMS(this CoordinateSharp.Coordinate c)
		{
			CoordinateFormatOptions cfo = new CoordinateFormatOptions();
			cfo.Format = CoordinateFormatType.Degree_Minutes_Seconds;
			cfo.Round = 2;
			cfo.Display_Leading_Zeros = true;
			return c.ToString(cfo);
		}

		public static string ToStringDDM(this CoordinateSharp.Coordinate c)
		{
			CoordinateFormatOptions cfo = new CoordinateFormatOptions();
			cfo.Format = CoordinateFormatType.Degree_Decimal_Minutes;
			cfo.Display_Leading_Zeros = true;
			cfo.Round = 4;
			return c.ToString(cfo);
		}

		public static string ToStringMGRS(this CoordinateSharp.Coordinate c)
		{
			return c.MGRS.ToString();
		}
	}
}
