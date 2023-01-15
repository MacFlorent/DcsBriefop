using System;
using System.Drawing;

namespace DcsBriefop.Data
{
	internal enum ElementAssetMapDisplay
	{
		None = 0,
		Point = 1,
		Orbit = 2,
		FullRoute = 3
	}

	internal enum ElementComPresetMode
	{
		Free = 0,
		Airdrome = 1,
		Group = 2
	}

	internal enum ElementWeatherDisplay
	{
		Plain,
		Metar
	}

	[Flags]
	internal enum ElementCoordinateDisplay
	{
		Dms = 1,
		Ddm = 2,
		Mgrs = 4
	}

	internal static class ElementGlobalData
	{
		public static readonly string ResourcesDirectory = "Resources";
		public static readonly string DcsFileFilter = "DCS mission files (*.miz)|*.miz|All files (*.*)|*.*";
	}

	internal static class ElementCoalitionColor
	{
		public static readonly Color Blue = Color.Blue;
		public static readonly Color Red = Color.Red;
		public static readonly Color Neutral = Color.DarkGray;
	}

	internal static class ElementMapValue
	{
		public static readonly int MinZoom = 1;
		public static readonly int MaxZoom = 18;
		public static readonly string OverlayStatic = "static";

		public static readonly Font DefaultFont = new Font("Arial", 11);
		public static readonly Pen PenSelected = new Pen(Color.Blue, 1);
		public static readonly Pen PenMouseOver = new Pen(Color.CadetBlue, 1);
	}
}
