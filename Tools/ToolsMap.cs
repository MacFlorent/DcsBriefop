using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DcsBriefop.Tools
{
	internal static class ToolsMap
	{
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
					AddMizDrawingObjectLine(theatre, overlay, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Icon)
					AddMizDrawingObjectIcon(theatre, overlay, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.TextBox)
					AddMizDrawingObjectText(theatre, overlay, drawingObject);
			}
		}

		private static void AddMizDrawingObjectLine(Theatre theatre, GMapOverlay overlay, MizDrawingObject drawingObject)
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


			GRouteBriefop route = GRouteBriefop.NewFromMizStyleName(points, drawingObject.Name, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5));
			overlay.Routes.Add(route);
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

			Font font = new Font(drawingObject.Font, drawingObject.FontSize.GetValueOrDefault(11));
			GTextBriefop text = new GTextBriefop(p, drawingObject.Text, ColorFromDcsString(drawingObject.ColorString), ColorFromDcsString(drawingObject.FillColorString), font, drawingObject.Angle.GetValueOrDefault(0), drawingObject.BorderThickness.GetValueOrDefault(0));
			overlay.Markers.Add(text);
		}

		private static Color ColorFromDcsString(string sDcsString)
		{
			string sHtmlColor = $"#{sDcsString.Substring(2, 6)}";
			string sAlpha = sDcsString.Substring(8, 2);
			Color color = ColorTranslator.FromHtml(sHtmlColor);
			Color colorAlpha = Color.FromArgb(Convert.ToInt32(sAlpha, 16), color);
			return colorAlpha;
		}
		#endregion

		#region Image Generation
		public static Bitmap GenerateMapImage(BriefopCustomMap mapData)
		{
			GMapProvider mapProvider = ElementMapValue.DefaultMapProvider;
			List<GMapOverlay> overlays = new List<GMapOverlay>();
			overlays.Add(mapData.MapOverlayCustom);
			overlays.AddRange(mapData.AdditionalMapOverlays);
			PointLatLng centerLatLng = new PointLatLng(mapData.CenterLatitude, mapData.CenterLongitude);
			return GenerateMapImage(centerLatLng, (int)mapData.Zoom, mapProvider, overlays, ElementImageSize.Width, ElementImageSize.Height);
		}

		public static Bitmap GenerateMapImage(PointLatLng centerLatLng, int iZoom, GMapProvider mapProvider, List<GMapOverlay> overlays, int iOutputWidth, int iOutputHeight)
		{
			GPoint centerPoint = mapProvider.Projection.FromLatLngToPixel(centerLatLng, iZoom);
			GPoint topLeft = new GPoint(centerPoint.X - iOutputWidth / 2, centerPoint.Y - iOutputHeight / 2);
			GPoint bottomRight = new GPoint(topLeft.X + iOutputWidth, topLeft.Y + iOutputHeight);

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
							if (route is GRouteBriefop routeBriefop)
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

		private static void DrawRouteBriefop(Graphics gfx, GMapProvider mapProvider, int iZoom, GPoint topLeft, GRouteBriefop routeBriefop)
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

