using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
using BruTile;
using BruTile.Web;
using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Rendering.Skia;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using Mapsui.UI.WindowsForms;
using SkiaSharp;
using System.Drawing.Drawing2D;
using System.Net;

namespace DcsBriefop.Tools
{
	internal static class ToolsMap
	{
		#region Fields
		private static readonly HttpClient s_tileHttpClient = BuildTileHttpClient();
		#endregion

		private static HttpClient BuildTileHttpClient()
		{
			HttpClient httpClient = new();
			httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("DcsBriefop/1.0");
			return httpClient;
		}

		#region MapControl
		public static void InitializeMapControl(this MapControl mapControl, string sProviderName)
		{
			if (string.IsNullOrEmpty(sProviderName))
				sProviderName = PreferencesManager.Preferences.Map.ProviderName;

			mapControl.Map.Layers.Clear();
			mapControl.Map.Layers.Add(MapProviders.CreateTileLayer(sProviderName));

			MapRenderer.RegisterStyleRenderer(typeof(BriefopMarkerStyle), new BriefopMarkerStyleRenderer());
			MapRenderer.RegisterStyleRenderer(typeof(BriefopLineStyle), new BriefopLineStyleRenderer());
			MapRenderer.RegisterStyleRenderer(typeof(BriefopLabelStyle), new BriefopLabelStyleRenderer());
		}

		// TODO Phase 5: remove once UcGroup and UcAirbase are migrated to Mapsui
		public static void InitializeGMaps()
		{
			if (!string.IsNullOrEmpty(PreferencesManager.Preferences.Application.InternetProxyHost))
			{
				WebProxy proxy = new WebProxy(PreferencesManager.Preferences.Application.InternetProxyHost, PreferencesManager.Preferences.Application.InternetProxyPort.GetValueOrDefault(80));
				if (!string.IsNullOrEmpty(PreferencesManager.Preferences.Application.InternetProxyUser))
					proxy.Credentials = new NetworkCredential(PreferencesManager.Preferences.Application.InternetProxyUser, PreferencesManager.Preferences.Application.InternetProxyPassword);

				GMapProvider.WebProxy = proxy;
			}

			GMaps.Instance.Mode = AccessMode.ServerOnly;
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
		public static MemoryLayer BuildMizDrawingLayer(Theatre theatre, List<MizDrawingLayer> drawingLayers)
		{
			List<IFeature> features = [];
			foreach (MizDrawingLayer drawingLayer in drawingLayers)
				AddMizDrawingLayerFeatures(theatre, features, drawingLayer);
			return new MemoryLayer { Style = null, Features = features };
		}

		private static void AddMizDrawingLayerFeatures(Theatre theatre, List<IFeature> features, MizDrawingLayer drawingLayer)
		{
			foreach (MizDrawingObject drawingObject in drawingLayer.Objects)
			{
				if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Line)
					AddMizDrawingObjectLine(theatre, features, drawingObject, drawingObject.Closed.GetValueOrDefault(false));
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Icon)
					AddMizDrawingObjectIcon(theatre, features, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.TextBox)
					AddMizDrawingObjectText(theatre, features, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Polygon)
					AddMizDrawingObjectPolygon(theatre, features, drawingObject);
			}
		}

		private static void AddMizDrawingObjectLine(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject, bool bClosed)
		{
			List<GeoPoint> points = [];
			foreach (MizDrawingPoint point in drawingObject.Points)
			{
				double dY = drawingObject.MapY + point.Y;
				double dX = drawingObject.MapX + point.X;
				Coordinate coordinate = theatre.GetCoordinate(dX, dY);
				points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));
			}

			BriefopLine line = BriefopLine.NewFromMizStyleName(points, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), bClosed, ColorFromDcsString(drawingObject.FillColorString));
			Mapsui.Nts.GeometryFeature feature = line.ToGeometryFeature();
			if (feature is not null)
				features.Add(feature);
		}

		private static void AddMizDrawingObjectIcon(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapX, drawingObject.MapY);
			GeoPoint p = new(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			BriefopMarker marker = BriefopMarker.NewFromMizStyleName(p, drawingObject.File, ColorFromDcsString(drawingObject.ColorString), drawingObject.Name, drawingObject.Scale.GetValueOrDefault(1), (int)drawingObject.Angle.GetValueOrDefault(0));
			PointFeature feature = new(MapProjection.ToMPoint(marker.Position));
			feature.Styles.Add(new BriefopMarkerStyle(marker));
			features.Add(feature);
		}

		private static void AddMizDrawingObjectText(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapX, drawingObject.MapY);
			GeoPoint p = new(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);

			float fFontSize = 11;
			if (drawingObject.FontSize is not null)
			{
				fFontSize = (float)(drawingObject.FontSize.Value - 3);
				if (fFontSize < 1)
					fFontSize = 1;
			}

			BriefopLabel label = new(p, drawingObject.Text, ColorFromDcsString(drawingObject.ColorString), ColorFromDcsString(drawingObject.FillColorString), drawingObject.Font, fFontSize, drawingObject.Angle.GetValueOrDefault(0), drawingObject.BorderThickness.GetValueOrDefault(0));
			features.Add(label.ToPointFeature());
		}

		private static void AddMizDrawingObjectPolygon(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject)
		{
			if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Rectangle)
				AddMizDrawingObjectRectangle(theatre, features, drawingObject);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Free)
				AddMizDrawingObjectLine(theatre, features, drawingObject, true);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Oval || drawingObject.PolygonMode == ElementDrawingPolygonMode.Circle)
				AddMizDrawingObjectOval(theatre, features, drawingObject);
		}

		private static void AddMizDrawingObjectRectangle(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject)
		{
			double dHalfWidth = drawingObject.Width.GetValueOrDefault() / 2;
			double dHalfHeight = drawingObject.Height.GetValueOrDefault() / 2;

			List<GeoPoint> points = [];
			double dY, dX, dYRotated, dXRotated;
			Coordinate coordinate;

			dY = drawingObject.MapY - dHalfWidth;
			dX = drawingObject.MapX - dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dXRotated, dYRotated);
			points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY + dHalfWidth;
			dX = drawingObject.MapX - dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dXRotated, dYRotated);
			points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY + dHalfWidth;
			dX = drawingObject.MapX + dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dXRotated, dYRotated);
			points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			dY = drawingObject.MapY - dHalfWidth;
			dX = drawingObject.MapX + dHalfHeight;
			RotateDcsYX(out dYRotated, out dXRotated, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
			coordinate = theatre.GetCoordinate(dXRotated, dYRotated);
			points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));

			BriefopLine route = BriefopLine.NewFromMizStyleName(points, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), true, ColorFromDcsString(drawingObject.FillColorString));
			Mapsui.Nts.GeometryFeature feature = route.ToGeometryFeature();
			if (feature is not null)
				features.Add(feature);
		}

		private static void AddMizDrawingObjectOval(Theatre theatre, List<IFeature> features, MizDrawingObject drawingObject)
		{
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

			List<GeoPoint> points = [];
			double dStep = 2 * Math.PI / 30;
			for (double dAngle = 0d; dAngle < 2 * Math.PI; dAngle += dStep)
			{
				double dY = dCenterY + dSquashRatio * dRadius * Math.Cos(dAngle);
				double dX = dCenterX - dRadius * Math.Sin(dAngle);
				RotateDcsYX(out double dYRotated, out double dXRotated, dY, dX, dCenterY, dCenterX, drawingObject.Angle);
				Coordinate coordinate = theatre.GetCoordinate(dXRotated, dYRotated);
				points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));
			}

			BriefopLine route = BriefopLine.NewFromMizStyleName(points, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), true, ColorFromDcsString(drawingObject.FillColorString));
			Mapsui.Nts.GeometryFeature feature = route.ToGeometryFeature();
			if (feature is not null)
				features.Add(feature);
		}

		private static void RotateDcsYX(out double dRotatedY, out double dRotatedX, double dY, double dX, double dCenterY, double dCenterX, double? dAngleDegrees)
		{
			//https://stackoverflow.com/questions/13695317/rotate-a-point-around-another-point
			double dAngleRadians = -dAngleDegrees.GetValueOrDefault() * (Math.PI / 180);
			if (dAngleRadians == 0)
			{
				dRotatedY = dY;
				dRotatedX = dX;
			}
			else
			{
				double dCosTheta = Math.Cos(dAngleRadians);
				double dSinTheta = Math.Sin(dAngleRadians);

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
		public static Bitmap GenerateMapImage(MizBopMap mapData, ITileSource tileSource, IEnumerable<ILayer> overlayLayers, Size outputSize)
		{
			GeoPoint center = new(mapData.CenterLatitude, mapData.CenterLongitude);
			return GenerateMapImage(center, (int)mapData.Zoom, tileSource, overlayLayers, outputSize);
		}

		public static Bitmap GenerateMapImage(GeoPoint center, int iZoom, ITileSource tileSource, IEnumerable<ILayer> overlayLayers, Size outputSize)
		{
			MPoint centerWorld = MapProjection.ToMPoint(center);
			double dResolution = MapProjection.ZoomToResolution(iZoom);
			double dHalfW = outputSize.Width / 2.0 * dResolution;
			double dHalfH = outputSize.Height / 2.0 * dResolution;
			double dWorldLeft = centerWorld.X - dHalfW;
			double dWorldTop = centerWorld.Y + dHalfH;

			BruTile.Extent worldExtent = new(centerWorld.X - dHalfW, centerWorld.Y - dHalfH, centerWorld.X + dHalfW, centerWorld.Y + dHalfH);
			int iLevelId = GetClosestLevelId(tileSource.Schema, dResolution);
			List<TileInfo> tileInfos = tileSource.Schema.GetTileInfos(worldExtent, iLevelId).ToList();

			using SKBitmap skBitmap = new(outputSize.Width, outputSize.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
			using SKCanvas canvas = new(skBitmap);
			canvas.Clear(SKColors.LightGray);

			// ITileSource has no GetTile in BruTile 6.0; HttpTileSource.GetTileAsync is the concrete async fetch method.
			// Tasks must be created and awaited inside Task.Run so continuations run on thread-pool threads,
			// not on the UI SynchronizationContext that is blocked by GetResult() (avoids deadlock).
			if (tileSource is HttpTileSource httpSource)
			{
				byte[][] tileData = Task.Run(async () =>
				{
					Task<byte[]>[] tasks = tileInfos.Select(_ti => httpSource.GetTileAsync(s_tileHttpClient, _ti)).ToArray();
					try { await Task.WhenAll(tasks).ConfigureAwait(false); } catch { }
					return tasks.Select(_t => _t.IsCompletedSuccessfully ? _t.Result : null).ToArray();
				}).GetAwaiter().GetResult();

				for (int i = 0; i < tileInfos.Count; i++)
				{
					if (tileData[i] is null || tileData[i].Length == 0)
						continue;

					using SKBitmap tileBitmap = SKBitmap.Decode(tileData[i]);
					if (tileBitmap is null)
						continue;

					BruTile.Extent e = tileInfos[i].Extent;
					float fX = (float)((e.MinX - dWorldLeft) / dResolution);
					float fY = (float)((dWorldTop - e.MaxY) / dResolution);
					float fW = (float)((e.MaxX - e.MinX) / dResolution);
					float fH = (float)((e.MaxY - e.MinY) / dResolution);
					canvas.DrawBitmap(tileBitmap, new SKRect(fX, fY, fX + fW, fY + fH));
				}
			}

			if (overlayLayers is not null)
			{
				Mapsui.Viewport viewport = new(centerWorld.X, centerWorld.Y, dResolution, 0, outputSize.Width, outputSize.Height);
				RenderOverlayLayers(canvas, viewport, overlayLayers);
			}

			using SKImage skImage = SKImage.FromBitmap(skBitmap);
			using SKData skData = skImage.Encode(SKEncodedImageFormat.Png, 100);
			using MemoryStream ms = new(skData.ToArray());
			return new Bitmap(ms);
		}

		private static int GetClosestLevelId(ITileSchema schema, double dResolution)
		{
			int iBestLevel = schema.Resolutions.Keys.First();
			double dBestDiff = double.MaxValue;

			foreach (KeyValuePair<int, Resolution> kvp in schema.Resolutions)
			{
				double dDiff = Math.Abs(kvp.Value.UnitsPerPixel - dResolution);
				if (dDiff < dBestDiff)
				{
					dBestDiff = dDiff;
					iBestLevel = kvp.Key;
				}
			}

			return iBestLevel;
		}

		private static void RenderOverlayLayers(SKCanvas canvas, Mapsui.Viewport viewport, IEnumerable<ILayer> layers)
		{
			// Custom style renderers don't use RenderService; call them directly to avoid MapRenderer.Render parameter complexity
			Dictionary<Type, ISkiaStyleRenderer> styleRenderers = new()
			{
				[typeof(BriefopMarkerStyle)] = new BriefopMarkerStyleRenderer(),
				[typeof(BriefopLineStyle)] = new BriefopLineStyleRenderer(),
				[typeof(BriefopLabelStyle)] = new BriefopLabelStyleRenderer(),
			};

			foreach (ILayer layer in layers)
			{
				if (layer is not MemoryLayer memoryLayer)
					continue;

				foreach (IFeature feature in memoryLayer.Features ?? [])
				{
					foreach (IStyle style in feature.Styles)
					{
						if (styleRenderers.TryGetValue(style.GetType(), out ISkiaStyleRenderer renderer))
							renderer.Draw(canvas, viewport, layer, feature, style, null, 0);
					}
				}
			}
		}

		// TODO Phase 5: remove — GMap-based overload replaced by ITileSource overload above
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

