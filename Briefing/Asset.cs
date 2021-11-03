﻿using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal abstract class Asset : BaseBriefing
	{
		#region Fields
		#endregion

		#region Properties
		public CustomDataAsset CustomData;

		protected virtual string DefaultMarker { get; set; } = MarkerBriefopType.dot.ToString();

		public BriefingCoalition BriefingCoalition { get; protected set; }

		public ElementAssetCategory Category
		{
			get { return (ElementAssetCategory)CustomData.Category; }
			set { CustomData.Category = (int)value; }
		}

		public ElementAssetMapDisplay MapDisplay
		{
			get { return (ElementAssetMapDisplay)CustomData.MapDisplay; }
			set { CustomData.MapDisplay = (int)value; }
		}

		public abstract int Id { get; }
		public abstract string Name { get; }
		public abstract string Task { get; }
		public abstract string Type { get; }
		public abstract string Radio { get; }
		public string Information
		{
			get
			{
				if (CustomInformation is null)
					return GetDefaultInformation();
				else
					return CustomInformation;
			}
			set
			{
				CustomInformation = value;
			}
		}

		public string CustomInformation
		{
			get { return CustomData.Information; }
			set { CustomData.Information = value; }
		}

		public string MissionInformation
		{
			get { return CustomData.MissionInformation; }
			set { CustomData.MissionInformation = value; }
		}

		public CustomDataMap MapDataMission
		{
			get { return CustomData.MapDataMission; }
			set { CustomData.MapDataMission = value; }
		}

		public List<AssetMapPoint> MapPoints { get; private set; } = new List<AssetMapPoint>();
		public GMapOverlay MapOverlayStatic { get; set; }
		#endregion

		#region CTOR
		public Asset(BriefingPack briefingPack, BriefingCoalition briefingCoalition) : base(briefingPack)
		{
			BriefingCoalition = briefingCoalition;
		}
		#endregion

		#region Methods
		protected abstract string GetDefaultInformation();
		protected abstract void InitializeCustomData();
		protected abstract void InitializeMapPoints(BriefingPack briefingPack);
		
		protected void InitializeData(BriefingPack briefingPack)
		{
			CustomData = RootCustom.Assets?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (CustomData is null)
			{
				CustomData = new CustomDataAsset(Id);
				RootCustom.Assets.Add(CustomData);

				InitializeCustomData();
			}

			InitializeMapPoints(briefingPack);
			InitializeMapOverlay();
			InitializeMapDataMission();
		}

		public void InitializeMapOverlay()
		{
			if (MapOverlayStatic is null)
				MapOverlayStatic = new GMapOverlay(ElementMapValue.OverlayStatic);
			
			MapOverlayStatic.Clear();

			List<PointLatLng> points = null;
			
			if (MapDisplay == ElementAssetMapDisplay.Point)
			{
				points = InitializeMapDataPoint(MapOverlayStatic);
			}
			else if (MapDisplay == ElementAssetMapDisplay.Orbit)
			{
				points = InitializeMapDataOrbit(MapOverlayStatic);
			}
			else if(MapDisplay == ElementAssetMapDisplay.FullRoute)
			{
				points = InitializeMapDataFullRoute(MapOverlayStatic);
			}
		}

		public void InitializeMapDataMission()
		{
			GMapOverlay staticOverlay = new GMapOverlay(ElementMapValue.OverlayStatic);
			List<PointLatLng> points = InitializeMapDataFullRoute(staticOverlay);

			if (MapDataMission is null)
			{
				MapDataMission = new CustomDataMap();

				PointLatLng? centerPoint = ToolsMap.GetPointsCenter(points);
				if (centerPoint is object)
				{
					MapDataMission.CenterLatitude = centerPoint.Value.Lat;
					MapDataMission.CenterLongitude = centerPoint.Value.Lng;
				}
				else
				{
					MapDataMission.CenterLatitude = BriefingCoalition.Bullseye.Latitude.DecimalDegree;
					MapDataMission.CenterLongitude = BriefingCoalition.Bullseye.Longitude.DecimalDegree;
				}
				MapDataMission.Zoom = ElementMapValue.DefaultZoom;
				MapDataMission.MapOverlayCustom = new GMapOverlay();
			}


			MapDataMission.AdditionalMapOverlays.Clear();
			MapDataMission.AdditionalMapOverlays.Add(staticOverlay);
			MapDataMission.AdditionalMapOverlays.Add(RootCustom.MapData.MapOverlayCustom);
			MapDataMission.AdditionalMapOverlays.Add(BriefingCoalition.MapData.MapOverlayCustom);
		}

		protected List<PointLatLng> InitializeMapDataPoint(GMapOverlay staticOverlay)
		{
			PointLatLng p = new PointLatLng(MapPoints[0].Coordinate.Latitude.DecimalDegree, MapPoints[0].Coordinate.Longitude.DecimalDegree);
			GMarkerBriefop marker = new GMarkerBriefop(p, DefaultMarker, BriefingCoalition.Color, Name);
			staticOverlay.Markers.Add(marker);

			return new List<PointLatLng>() { p };
		}

		protected List<PointLatLng> InitializeMapDataOrbit(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint routePoint in MapPoints)
			{
				if (points.Count > 1)
					break;
				else if (points.Count <= 0 && routePoint.IsOrbitStart())
				{
					PointLatLng p = new PointLatLng(routePoint.Coordinate.Latitude.DecimalDegree, routePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), BriefingCoalition.Color, Name);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
				else if (points.Count == 1)
				{
					PointLatLng p = new PointLatLng(routePoint.Coordinate.Latitude.DecimalDegree, routePoint.Coordinate.Longitude.DecimalDegree);
					GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), BriefingCoalition.Color, null);
					staticOverlay.Markers.Add(marker);
					points.Add(p);
				}
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(BriefingCoalition.Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}

		protected List<PointLatLng> InitializeMapDataFullRoute(GMapOverlay staticOverlay)
		{
			List<PointLatLng> points = new List<PointLatLng>();

			foreach (AssetMapPoint mapPoint in MapPoints)
			{
				PointLatLng p = new PointLatLng(mapPoint.Coordinate.Latitude.DecimalDegree, mapPoint.Coordinate.Longitude.DecimalDegree);
				GMarkerBriefop marker = new GMarkerBriefop(p, MarkerBriefopType.triangle.ToString(), BriefingCoalition.Color, $"{mapPoint.Number}:{mapPoint.Name}");
				staticOverlay.Markers.Add(marker);
				points.Add(p);
			}

			if (points.Count > 1)
			{
				GMapRoute route = new GMapRoute(points, "route");
				route.Stroke = new Pen(BriefingCoalition.Color, 2);
				staticOverlay.Routes.Add(route);
			}

			return points;
		}
		#endregion
	}
}
