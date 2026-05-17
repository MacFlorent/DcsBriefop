using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
using BruTile;
using BruTile.Web;
using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Rendering.Skia;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using Mapsui.UI.WindowsForms;
using SkiaSharp;

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

		#endregion

		#region MizDrawings
		public static MemoryLayer BuildMizDrawingMapLayer(Theatre theatre, List<MizDrawingLayer> drawingLayers)
		{
			List<IFeature> mapFeatures = [];
			foreach (MizDrawingLayer drawingLayer in drawingLayers)
				AddMizDrawingObjects(theatre, mapFeatures, drawingLayer);
			return new MemoryLayer { Style = null, Features = mapFeatures };
		}

		private static void AddMizDrawingObjects(Theatre theatre, List<IFeature> mapFeatures, MizDrawingLayer drawingLayer)
		{
			foreach (MizDrawingObject drawingObject in drawingLayer.Objects)
			{
				if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Line)
					AddMizDrawingObjectLine(theatre, mapFeatures, drawingObject, drawingObject.Closed.GetValueOrDefault(false));
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Icon)
					AddMizDrawingObjectIcon(theatre, mapFeatures, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.TextBox)
					AddMizDrawingObjectText(theatre, mapFeatures, drawingObject);
				else if (drawingObject.PrimitiveType == ElementDrawingPrimitive.Polygon)
					AddMizDrawingObjectPolygon(theatre, mapFeatures, drawingObject);
			}
		}

		private static void AddMizDrawingObjectLine(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject, bool bClosed)
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
				mapFeatures.Add(feature);
		}

		private static void AddMizDrawingObjectIcon(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapX, drawingObject.MapY);
			GeoPoint p = new(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			BriefopMarker marker = BriefopMarker.NewFromMizStyleName(p, drawingObject.File, ColorFromDcsString(drawingObject.ColorString), null, drawingObject.Scale.GetValueOrDefault(1), (int)drawingObject.Angle.GetValueOrDefault(0));
			PointFeature mapFeature = new(MapProjection.ToMPoint(marker.Position));
			mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
			mapFeatures.Add(mapFeature);
		}

		private static void AddMizDrawingObjectText(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
		{
			Coordinate coordinate = theatre.GetCoordinate(drawingObject.MapX, drawingObject.MapY);
			GeoPoint p = new(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);

			float fFontSize = 11;
			if (drawingObject.FontSize is not null)
			{
				fFontSize = drawingObject.FontSize.Value - 3;
				if (fFontSize < 1)
					fFontSize = 1;
			}

			BriefopLabel label = new(p, drawingObject.Text, ColorFromDcsString(drawingObject.ColorString), ColorFromDcsString(drawingObject.FillColorString), drawingObject.Font, fFontSize, (int)drawingObject.Angle.GetValueOrDefault(0), drawingObject.BorderThickness.GetValueOrDefault(0));
			mapFeatures.Add(label.ToMapFeature());
		}

		private static void AddMizDrawingObjectPolygon(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
		{
			if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Rectangle)
				AddMizDrawingObjectRectangle(theatre, mapFeatures, drawingObject);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Free)
				AddMizDrawingObjectLine(theatre, mapFeatures, drawingObject, true);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Arrow)
				AddMizDrawingObjectArrow(theatre, mapFeatures, drawingObject);
			else if (drawingObject.PolygonMode == ElementDrawingPolygonMode.Oval || drawingObject.PolygonMode == ElementDrawingPolygonMode.Circle)
				AddMizDrawingObjectOval(theatre, mapFeatures, drawingObject);
		}

		private static void AddMizDrawingObjectArrow(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
		{
			List<GeoPoint> points = [];
			foreach (MizDrawingPoint point in drawingObject.Points)
			{
				double dY = drawingObject.MapY + point.Y;
				double dX = drawingObject.MapX + point.X;
				RotateDcsYX(out double dRotatedY, out double dRotatedX, dY, dX, drawingObject.MapY, drawingObject.MapX, drawingObject.Angle);
				Coordinate coordinate = theatre.GetCoordinate(dRotatedX, dRotatedY);
				points.Add(new GeoPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree));
			}

			BriefopLine line = BriefopLine.NewFromMizStyleName(points, drawingObject.Style, ColorFromDcsString(drawingObject.ColorString), drawingObject.Thickness.GetValueOrDefault(5), true, ColorFromDcsString(drawingObject.FillColorString));
			Mapsui.Nts.GeometryFeature feature = line.ToGeometryFeature();
			if (feature is not null)
				mapFeatures.Add(feature);
		}

		private static void AddMizDrawingObjectRectangle(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
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
				mapFeatures.Add(feature);
		}

		private static void AddMizDrawingObjectOval(Theatre theatre, List<IFeature> mapFeatures, MizDrawingObject drawingObject)
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
				mapFeatures.Add(feature);
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
			List<TileInfo> tileInfos = [.. tileSource.Schema.GetTileInfos(worldExtent, iLevelId)];

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
					Task<byte[]>[] tasks = [.. tileInfos.Select(_ti => httpSource.GetTileAsync(s_tileHttpClient, _ti))];
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

		private static void RenderOverlayLayers(SKCanvas canvas, Mapsui.Viewport viewport, IEnumerable<ILayer> mapLayers)
		{
			// Custom style renderers don't use RenderService; call them directly to avoid MapRenderer.Render parameter complexity
			Dictionary<Type, ISkiaStyleRenderer> styleRenderers = new()
			{
				[typeof(BriefopMarkerStyle)] = new BriefopMarkerStyleRenderer(),
				[typeof(BriefopLineStyle)] = new BriefopLineStyleRenderer(),
				[typeof(BriefopLabelStyle)] = new BriefopLabelStyleRenderer(),
			};

			foreach (ILayer mapLayer in mapLayers)
			{
				if (mapLayer is not MemoryLayer memoryMapLayer)
					continue;

				foreach (IFeature mapFeature in memoryMapLayer.Features ?? [])
				{
					foreach (IStyle style in mapFeature.Styles)
					{
						if (styleRenderers.TryGetValue(style.GetType(), out ISkiaStyleRenderer renderer))
							renderer.Draw(canvas, viewport, mapLayer, mapFeature, style, null, 0);
					}
				}
			}
		}

		#endregion
	}
}

