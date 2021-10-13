using System.Drawing;

namespace DcsBriefop.MasterData
{
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
		public static readonly int DefaultZoom = 9;
		public static readonly string OverlayStatic = "static";
	}
}
