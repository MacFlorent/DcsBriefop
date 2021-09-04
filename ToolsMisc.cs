using System;
using System.Drawing;
using System.Text;

namespace DcsBriefop
{
	internal static class ToolsMisc
	{
		public static decimal MetersToFeet { get; } = 3.281M;
		public static decimal MsToKnots { get; } = 1.944M;
		public static decimal MmHgToHpa { get; } = 1.333M;
		public static decimal MmHgToInH { get; } = 1 / 25.4M;

		public static void AppendWithSeparator(this StringBuilder sb, string value, string sSeparator)
		{
			if (sb.Length > 0)
				sb.Append(sSeparator);
			sb.Append(value);
		}

		public static string GetLuaStringCleaned(string sString)
		{
			return sString.Replace("\\\n", "\n");
		}

		public static float Lerp(this float start, float end, float amount)
		{
			if (amount > 1)
				amount = 1;
			if (amount < 0)
				amount = 0;

			float difference = end - start;
			float adjusted = difference * amount;
			return start + adjusted;
		}

		public static Color Lerp(this Color color, Color colorTo, float amount)
		{
			// start colours as lerp-able floats
			float sr = color.R, sg = color.G, sb = color.B;

			// end colours as lerp-able floats
			float er = colorTo.R, eg = colorTo.G, eb = colorTo.B;

			// lerp the colours to get the difference
			byte r = (byte)sr.Lerp(er, amount),
						g = (byte)sg.Lerp(eg, amount),
						b = (byte)sb.Lerp(eb, amount);

			// return the new colour
			return Color.FromArgb(r, g, b);
		}

		public static int PerceivedBrightness(Color c)
		{
			return (int)Math.Sqrt(
			c.R * c.R * .299 +
			c.G * c.G * .587 +
			c.B * c.B * .114);
		}
	}
}
