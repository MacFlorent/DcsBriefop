﻿using GMap.NET.MapProviders;
using System.Drawing;

namespace DcsBriefop.Data
{
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