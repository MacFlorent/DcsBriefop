﻿using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public GMapOverlay MapOverlay { get; set; } = new GMapOverlay();
	}
}
