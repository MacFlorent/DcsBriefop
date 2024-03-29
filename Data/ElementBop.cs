﻿namespace DcsBriefop.Data
{
	internal enum ElementBullseyeWaypoint
	{
		None,
		One,
		Last
	}

	internal enum ElementGroupClass
	{
		None,
		Air,
		Ground,
		Sea,
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

	internal enum ElementMeasurementSystem
	{
		Metric,
		Imperial,
		Hybrid
	}

	[Flags]
	internal enum ElementCoordinateDisplay
	{
		None = 0,
		Dms = 1,
		Ddm = 2,
		Mgrs = 4,
		All = ~None
	}

	[Flags]
	internal enum ElementMapOverlayRouteDisplay
	{
		None = 0,
		NoMarkerFirstPoint = 1,
		PointLabelFull = 2,
		PointLabelLight = 4,
		RouteLabel = 8,
	}

	internal enum ElementAirbaseType
	{
		None = 0,
		Airdrome = 1,
		Ship = 2,
		Farp = 3
	}

	internal enum ElementDatalinkType
	{
		None,
		Link16,
		Sadl,
		Idm
	}

	[Flags]
	internal enum ElementBriefingPageRender
	{
		None = 0,
		Html = 1,
		Map = 2
	}

	internal enum ElementBriefingPartType
	{
		Bullseye,
		Paragraph,
		Sortie,
		Description,
		Task,
		Airbases,
		Groups,
		Waypoints,
		Image,
		Weather,
		TableText
	}

	internal enum ElementBriefingPartGroupType
	{
		GroupsAndUnits,
		GroupsOnly,
		UnitsOnly
	}

	[Flags]
	internal enum ElementBriefingOutput
	{
		None = 0,
		Miz = 1,
		Directory = 2
	}

	internal enum ElementGroupOrUnit
	{
		Group = 1,
		Unit = 2
	}

	internal static class ElementGlobalData
	{
		public static readonly string DcsFileFilter = "DCS mission files (*.miz)|*.miz|All files (*.*)|*.*";
		public static readonly string ImageFileFilter = "Image files |*.bmp;*.jpg;*.png";
		public static readonly string BullseyeRoutePointName = "BULLS";
		public static readonly string GenerateDirectoryNameDefault = "BriefopGenerated";
		public static readonly string GenerateDirectoryNameVeaf = @"src\mission\KNEEBOARD";
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
