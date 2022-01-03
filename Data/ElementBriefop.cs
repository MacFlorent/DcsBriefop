using GMap.NET.MapProviders;
using System.Drawing;

namespace DcsBriefop.Data
{
	internal enum ElementAssetSide
	{
		None = 0,
		Own = 1,
		Opposing = 2
	}

	internal enum ElementAssetUsage
	{
		Excluded = 0,
		MissionWithDetail = 1,
		Mission = 2,
		Support = 3,
		Base = 4
	}

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

	internal enum ElementExportFileType
	{
		Situation,
		SituationMap,
		Operations,
		Missions,
		MissionMaps,
	}

	internal static class ElementGlobalData
	{
		public static readonly string ResourcesDirectory = "Resources";
	}

	internal static class ElementCoalitionColor
	{
		public static readonly Color Blue = Color.Blue;
		public static readonly Color Red = Color.Red;
		public static readonly Color Neutral = Color.DarkGray;
	}

	internal static class ElementMapValue
	{
		public static readonly GMapProvider MapProvider = BingMapProvider.Instance;
		public static readonly int MinZoom = 1;
		public static readonly int MaxZoom = 18;
		public static readonly int DefaultZoom = 9;
		public static readonly string OverlayStatic = "static";
	}

	internal static class ElementImageSize
	{
		public static readonly int Width = 800;
		public static readonly int Height = 1200;
	}
}
