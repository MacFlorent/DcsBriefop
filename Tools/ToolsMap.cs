using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Drawing.Drawing2D;
using System.Net;

namespace DcsBriefop.Tools
{
	internal static class ToolsMap
	{
		#region MapControl
		public static void InitializeGMaps()
		{
			if (!string.IsNullOrEmpty(PreferencesManager.Preferences.Application.InternetProxyHost))
			{
				WebProxy proxy = new WebProxy(PreferencesManager.Preferences.Application.InternetProxyHost, PreferencesManager.Preferences.Application.InternetProxyPort.GetValueOrDefault(80));
				if (!string.IsNullOrEmpty(PreferencesManager.Preferences.Application.InternetProxyUser))
					proxy.Credentials = new NetworkCredential(PreferencesManager.Preferences.Application.InternetProxyUser, PreferencesManager.Preferences.Application.InternetProxyPassword);

				GMapProvider.WebProxy = proxy;
			}

			GMaps.Instance.Mode = AccessMode.ServerOnly; // the program has trouble terminating all its threads in cached mode, don't know why, better stick to server only for now
			GMapImageProxy.Enable();
		}

		public static void InitializeMapControl(this GMapControl mapControl, string sProvider)
		{
			if (string.IsNullOrEmpty(sProvider))
				sProvider = PreferencesManager.Preferences.Map.ProviderName;

			GMapProvider mapProvider = GMapProviders.TryGetProvider(sProvider);
			mapControl.InitializeMapControl(mapProvider);
		}

		public static void InitializeMapControl(this GMapControl mapControl, GMapProvider mapProvider)
		{
			mapControl.MapProvider = mapProvider;
			//mapControl.MapProvider = GMapProviders.BingMap;
			mapControl.ShowCenter = false;
			mapControl.MinZoom = ElementMapValue.MinZoom;
			mapControl.MaxZoom = ElementMapValue.MaxZoom;
			mapControl.Zoom = PreferencesManager.Preferences.Map.Zoom;
		}

		public static void ForceRefresh(this GMapControl mapControl)
		{
			mapControl.Refresh();
			mapControl.Zoom += 1; mapControl.Zoom -= 1;
		}
		#endregion

		#region Miscellaneous
		public static RectLatLng? GetRectOfPoints(List<PointLatLng> points)
		{
			RectLatLng? rect = null;

			double left = double.MaxValue;
			double top = double.MinValue;
			double right = double.MinValue;
			double bottom = double.MaxValue;

			if (points.Count > 0)
			{
				foreach (var p in points)
				{
					// left
					if (p.Lng < left)
					{
						left = p.Lng;
					}

					// top
					if (p.Lat > top)
					{
						top = p.Lat;
					}

					// right
					if (p.Lng > right)
					{
						right = p.Lng;
					}

					// bottom
					if (p.Lat < bottom)
					{
						bottom = p.Lat;
					}
				}

				rect = RectLatLng.FromLTRB(left, top, right, bottom);
			}

			return rect;
		}

		public static PointLatLng? GetRectCenter(RectLatLng? rect)
		{
			if (rect is null)
				return null;
			else
				return new PointLatLng(rect.Value.Lat - rect.Value.HeightLat / 2, rect.Value.Lng + rect.Value.WidthLng / 2);
		}

		public static PointLatLng? GetPointsCenter(List<PointLatLng> points)
		{
			if (points is null || points.Count <= 0)
				return null;
			else if (points.Count == 1)
				return points[0];
			else
				return GetRectCenter(GetRectOfPoints(points));
		}
		#endregion

		#region MizDrawings
		public static void AddMizDrawingLayers(Theatre theatre, GMapOverlay overlay, List<MizDrawingLayer> drawingLayers)
		{
			foreach (MizDrawingLayer drawingLayer in drawingLayers)
			{
				AddMizDrawingLayer(theatre, overlay, drawingLayer);
			}
		}

		public static void AddMizDrawingLayer(Theatre theatre, GMapOverlay overlay, MizDrawingLayer drawingLayer)
		{
			foreach (MizDrawingObject drawingObject in drawingLayer.Objects)
			{
				if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Line)
					AddMizDrawingObjectLine(theatre, overlay, drawingObject, false);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Icon)
					AddMizDrawingObjectIcon(theatre, overlay, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.TextBox)
					AddMizDrawingObjectText(theatre, overlay, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Polygon)
					AddMizDrawingObjectPolygon(theatre, overlay, drawingObject);
			}
		}

		private static void AddMizDrawingObjectLine(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject, bool bClosed)
		{
			List<PointLatLng> points = new List<PointLatLng>();
			foreach (MizDrawingPoint point in drawingObject.Points)
			{
				decimal dY = drawingObject.MapY + point.Y;
				decimal dX = drawingObject.MapX + point.X;
				Coordinate coordinate = theatre.GetCoordinate(dY, dX);
				PointLatLng p = new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
				points.Add(p);
			}

			GLineBriefop line = GLineBriefop.NewFromMizStyleName(points, null, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), bClosed, ColorFromDcsString(drawingObject.FillColorString));
			overlay.Routes.Add(line);
		}

		private static void AddMizDrawingObjectIcon(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapY, drawingObject.MapX);
			PointLatLng p = new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);

			GMarkerBriefop marker = GMarkerBriefop.NewFromMizStyleName(p, drawingObject.File, ColorFromDcsString(drawingObject.ColorString), drawingObject.Name, drawingObject.Scale.GetValueOrDefault(1), drawingObject.Angle.GetValueOrDefault(0));
			overlay.Markers.Add(marker);
		}

		private static void AddMizDrawingObjectText(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapY, drawingObject.MapX);
			PointLatLng p = new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);

			float fFontSize = 11;
			if (drawingObject.FontSize is object)
			{
				fFontSize = drawingObject.FontSize.Value - 3;
				if (fFontSize < 1)
					fFontSize = 1;
			}
			Font font = new Font(drawingObject.Font, fFontSize);

			GTextBriefop text = new GTextBriefop(p, drawingObject.Text, ColorFromDcsString(drawingObject.ColorString), ColorFromDcsString(drawingObject.FillColorString), font, drawingObject.Angle.GetValueOrDefault(0), drawingObject.BorderThickness.GetValueOrDefault(0));
			overlay.Markers.Add(text);
		}

		private static void AddMizDrawingObjectPolygon(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
		{
			//http://www.independent-software.com/gmap-net-tutorial-maps-markers-and-polygons.html/
			//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp

			if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Rectangle)
				AddMizDrawingObjectRectangle(theatre, overlay, drawingObject);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Free)
				AddMizDrawingObjectLine(theatre, overlay, drawingObject, true);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Oval || drawingObject.PolygonMode == ElementDrawingPolygonMode.Circle)
				AddMizDrawingObjectOval(theatre, overlay, drawingObject);
		}

		private static void AddMizDrawingObjectRectangle(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
		{
			decimal dHalfWidth = drawingObject.Width.GetValueOrDefault() / 2;
			decimal dHalfHeight = drawingObject.Height.GetValueOrDefault() / 2;

			List<PointLatLng> points = new List<PointLatLng>();

			decimal dY, dX, dYRotated, dXRotated;
			Coordinate coordinate;

			dY = drawingObject.MapY - dHalfWidth;
			dX = drawingObject.MapX - dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dYRotated, dXRotated);
			points.Add(new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY + dHalfWidth;
			dX = drawingObject.MapX - dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dYRotated, dXRotated);
			points.Add(new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY + dHalfWidth;
			dX = drawingObject.MapX + dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dYRotated, dXRotated);
			points.Add(new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY - dHalfWidth;
			dX = drawingObject.MapX + dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dYRotated, dXRotated);
			points.Add(new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			GLineBriefop route = GLineBriefop.NewFromMizStyleName(points, null, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), true, ColorFromDcsString(drawingObject.FillColorString));
			overlay.Routes.Add(route);
		}

		private static void AddMizDrawingObjectOval(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
		{
			//https://www.mathopenref.com/coordcirclealgorithm.html
			double dCenterY = (double)drawingObject.MapY;
			double dCenterX = (double)drawingObject.MapX;

			double dRadius, dSquashRatio;
			if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Oval)
			{
				dRadius = (double)drawingObject.R1.Value;
				dSquashRatio = (double)drawingObject.R2.Value / dRadius;
			}
			else
			{
				dRadius = (double)drawingObject.Radius.Value;
				dSquashRatio = 1;
			}

			List<PointLatLng> points = new List<PointLatLng>();
			double dStep = 2 * Math.PI / 30;
			for (double dAngle = 0d; dAngle < 2 * Math.PI; dAngle += dStep)
			{
				double dY = dCenterY + dSquashRatio * dRadius * Math.Cos(dAngle);
				double dX = dCenterX - dRadius * Math.Sin(dAngle);    //note 2.
				RotateDcsYX(out decimal dYRotated, out decimal dXRotated, (decimal)dY, (decimal)dX, (decimal)dCenterY, (decimal)dCenterX, drawingObject.Angle);
				Coordinate coordinate = theatre.GetCoordinate(dYRotated, dXRotated);
				points.Add(new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));
			}

			GLineBriefop route = GLineBriefop.NewFromMizStyleName(points, null, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), true, ColorFromDcsString(drawingObject.FillColorString));
			overlay.Routes.Add(route);
		}

		private static void RotateDcsYX(out decimal dRotatedY, out decimal dRotatedX, decimal dY, decimal dX, decimal dCenterY, decimal dCenterX, decimal? dAngleDegrees)
		{
			//https://stackoverflow.com/questions/13695317/rotate-a-point-around-another-point
			double dAngleRadians = -(double)dAngleDegrees.GetValueOrDefault() * (Math.PI / 180);
			if (dAngleRadians == 0)
			{
				dRotatedY = dY;
				dRotatedX = dX;
			}
			else
			{
				decimal dCosTheta = (decimal)Math.Cos(dAngleRadians);
				decimal dSinTheta = (decimal)Math.Sin(dAngleRadians);

				dRotatedY = (dCosTheta * (dY - dCenterY) - dSinTheta * (dX - dCenterX) + dCenterY);
				dRotatedX = (dSinTheta * (dY - dCenterY) + dCosTheta * (dX - dCenterX) + dCenterX);
			}
		}

		private static Color ColorFromDcsString(string sDcsString)
		{
			Color colorAlpha = Color.Empty;
			if (!string.IsNullOrEmpty(sDcsString) && sDcsString.Length >= 8)
			{
				string sHtmlColor = $"#{sDcsString.Substring(2, 6)}";
				string sAlpha = sDcsString.Substring(8, 2);
				Color color = ColorTranslator.FromHtml(sHtmlColor);
				colorAlpha = Color.FromArgb(Convert.ToInt32(sAlpha, 16), color);
			}
			return colorAlpha;
		}
		#endregion

		#region Image Generation
		public static Bitmap GenerateMapImage(MizBopMap mapData, GMapProvider mapProvider, IEnumerable<GMapOverlay> additionalOverlays, Size outputSize)
		{
			List<GMapOverlay> overlays = new List<GMapOverlay> { mapData.BuildCustomMapOverlay() };
			if (additionalOverlays is not null && additionalOverlays.Any())
				overlays.AddRange(additionalOverlays);

			PointLatLng centerLatLng = new PointLatLng(mapData.CenterLatitude, mapData.CenterLongitude);
			return GenerateMapImage(centerLatLng, (int)mapData.Zoom, mapProvider, overlays, outputSize);
		}

		public static Bitmap GenerateMapImage(PointLatLng centerLatLng, int iZoom, GMapProvider mapProvider, List<GMapOverlay> overlays, Size outputSize)
		{
			GPoint centerPoint = mapProvider.Projection.FromLatLngToPixel(centerLatLng, iZoom);
			GPoint topLeft = new GPoint(centerPoint.X - outputSize.Width / 2, centerPoint.Y - outputSize.Height / 2);
			GPoint bottomRight = new GPoint(topLeft.X + outputSize.Width, topLeft.Y + outputSize.Height);

			PointLatLng topLeftLatLng = mapProvider.Projection.FromPixelToLatLng(topLeft, iZoom);
			PointLatLng bottomRightLatLng = mapProvider.Projection.FromPixelToLatLng(bottomRight, iZoom);
			RectLatLng rectLatLng = RectLatLng.FromLTRB(topLeftLatLng.Lng, topLeftLatLng.Lat, bottomRightLatLng.Lng, bottomRightLatLng.Lat);

			List<GPoint> tileArea = new List<GPoint>();
			tileArea.AddRange(mapProvider.Projection.GetAreaTileList(rectLatLng, iZoom, 1));
			tileArea.TrimExcess();

			GPoint pxDelta = new GPoint(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
			GSize maxOfTiles = mapProvider.Projection.GetTileMatrixMaxXY(iZoom);

			Bitmap bmpDestination = new Bitmap((int)(pxDelta.X), (int)(pxDelta.Y));

			using (var gfx = Graphics.FromImage(bmpDestination))
			{
				gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gfx.SmoothingMode = SmoothingMode.HighQuality;

				//get tiles &combine into one
				lock (tileArea)
				{
					foreach (var p in tileArea)
					{
						foreach (var tp in mapProvider.Overlays)
						{
							Exception ex;
							GMapImage tile;

							// tile number inversion(BottomLeft -> TopLeft) for pergo maps
							if (tp.InvertedAxisY)
							{
								tile = GMaps.Instance.GetImageFrom(tp, new GPoint(p.X, maxOfTiles.Height - p.Y), iZoom, out ex) as GMapImage;
							}
							else // ok
							{
								tile = GMaps.Instance.GetImageFrom(tp, p, iZoom, out ex) as GMapImage;
							}

							if (tile != null)
							{
								using (tile)
								{
									long x = p.X * mapProvider.Projection.TileSize.Width - topLeft.X;
									long y = p.Y * mapProvider.Projection.TileSize.Width - topLeft.Y;
									{
										gfx.DrawImage(tile.Img, x, y, mapProvider.Projection.TileSize.Width, mapProvider.Projection.TileSize.Height);
									}
								}
							}
						}
					}
				}

				foreach (GMapOverlay overlay in overlays)
				{
					// draw routes
					// we cannot use the route render method as there is too much private/internal data and interconnections with the MapControl to do so
					// so we just redraw the lines specifically here
					foreach (GMapRoute route in overlay.Routes.Where(_r => _r.IsVisible))
					{
						if (route.Points is object && route.Points.Count > 0)
						{
							if (route is GLineBriefop routeBriefop)
							{
								DrawRouteBriefop(gfx, mapProvider, iZoom, topLeft, routeBriefop);
							}
							else
							{
								DrawRoute(gfx, mapProvider, iZoom, topLeft, route);
							}
						}
					}

					//// draw polygons
					//foreach (var r in overlay.Polygons)
					//{
					//	if (r.IsVisible)
					//	{
					//		using (var rp = new GraphicsPath())
					//		{
					//			for (int j = 0; j < r.Points.Count; j++)
					//			{
					//				var pr = r.Points[j];
					//				var px = mapProvider.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, iZoom);

					//				px.Offset(iPadding, iPadding);
					//				px.Offset(-topLeft.X, -topLeft.Y);

					//				var p2 = px;

					//				if (j == 0)
					//				{
					//					rp.AddLine(p2.X, p2.Y, p2.X, p2.Y);
					//				}
					//				else
					//				{
					//					var p = rp.GetLastPoint();
					//					rp.AddLine(p.X, p.Y, p2.X, p2.Y);
					//				}
					//			}

					//			if (rp.PointCount > 0)
					//			{
					//				rp.CloseFigure();

					//				gfx.FillPath(r.Fill, rp);

					//				gfx.DrawPath(r.Stroke, rp);
					//			}
					//		}
					//	}
					//}


					// draw markers
					foreach (GMapMarker marker in overlay.Markers.Where(_m => _m.IsVisible))
					{
						DrawMarker(gfx, mapProvider, iZoom, topLeft, marker);
					}
				}

				gfx.ResetTransform();
			}

			return bmpDestination;
		}

		private static void TranslateGraphics(Graphics gfx, GPoint topLeft)
		{
			gfx.ResetTransform(); // need to reset before transforming, if not sometimes will be drawn in the wrong position
			gfx.TranslateTransform(-topLeft.X, -topLeft.Y);
		}

		private static void DrawRouteBriefop(Graphics gfx, GMapProvider mapProvider, int iZoom, GPoint topLeft, GLineBriefop routeBriefop)
		{
			List<GPoint> gPoints = new List<GPoint>();
			foreach (PointLatLng routePoint in routeBriefop.Points)
			{
				GPoint routePointPixel = mapProvider.Projection.FromLatLngToPixel(routePoint.Lat, routePoint.Lng, iZoom);
				gPoints.Add(routePointPixel);
			}

			TranslateGraphics(gfx, topLeft);
			routeBriefop.Render(gfx, gPoints);
		}

		private static void DrawRoute(Graphics gfx, GMapProvider mapProvider, int iZoom, GPoint topLeft, GMapRoute route)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				GPoint? lastPointPixel = null;

				foreach (PointLatLng routePoint in route.Points)
				{
					GPoint routePointPixel = mapProvider.Projection.FromLatLngToPixel(routePoint.Lat, routePoint.Lng, iZoom);

					if (lastPointPixel is object)
					{
						graphicsPath.AddLine(lastPointPixel.Value.X, lastPointPixel.Value.Y, routePointPixel.X, routePointPixel.Y);
					}

					lastPointPixel = routePointPixel;
				}

				if (graphicsPath.PointCount > 0)
				{
					TranslateGraphics(gfx, topLeft);
					gfx.DrawPath(route.Stroke, graphicsPath);
				}
			}
		}

		private static void DrawMarker(Graphics gfx, GMapProvider mapProvider, int iZoom, GPoint topLeft, GMapMarker marker)
		{
			GPoint markerPointPixel = mapProvider.Projection.FromLatLngToPixel(marker.Position.Lat, marker.Position.Lng, iZoom);
			TranslateGraphics(gfx, topLeft);
			gfx.TranslateTransform(markerPointPixel.X, markerPointPixel.Y); // account for marker position within the global map as the render method will draw at this postion
			gfx.TranslateTransform(-marker.LocalPosition.X, -marker.LocalPosition.Y); // account for (nullify) local position of relative to displayed map control if any, as it will be used in the render
			gfx.TranslateTransform(marker.Offset.X, marker.Offset.Y); // account for marker offset positioning
			marker.OnRender(gfx);
		}
		#endregion
	}
}

