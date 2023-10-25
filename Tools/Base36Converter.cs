﻿namespace DcsBriefop.Tools
{
	internal static class Base36Converter
	{
		private const int Base = 36;
		private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static string ConvertTo(int value)
		{
			string result = "";

			while (value > 0)
			{
				result = Chars[value % Base] + result;
				value /= Base;
			}

			return result;
		}
	}
}
