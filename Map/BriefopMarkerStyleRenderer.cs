using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Rendering;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using SkiaSharp;

namespace DcsBriefop.Map
{
	internal class BriefopMarkerStyleRenderer : ISkiaStyleRenderer
	{
		public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature feature, IStyle style, RenderService renderService, long iteration)
		{
			if (feature is not PointFeature pointFeature || style is not BriefopMarkerStyle markerStyle)
				return false;

			BriefopMarker marker = markerStyle.Marker;
			SKBitmap skBitmap = marker.GetSkBitmap();
			if (skBitmap is null)
				return false;

			var screenPos = viewport.WorldToScreen(pointFeature.Point);
			int sizeW = marker.GetSizeWidth();
			int sizeH = marker.GetSizeHeight();
			float centerX = (float)(screenPos.X + marker.GetOffsetX() + sizeW / 2.0);
			float centerY = (float)(screenPos.Y + marker.GetOffsetY() + sizeH / 2.0);

			canvas.Save();
			canvas.Translate(centerX, centerY);
			if (marker.Angle != 0)
				canvas.RotateDegrees(marker.Angle);

			canvas.DrawBitmap(skBitmap, new SKRect(-sizeW / 2f, -sizeH / 2f, sizeW / 2f, sizeH / 2f));

			if (!string.IsNullOrEmpty(marker.Label))
			{
				using SKPaint textPaint = new()
				{
					TextSize = 11f,
					Color = new SKColor(
						marker.TintColor?.R ?? 0,
						marker.TintColor?.G ?? 0,
						marker.TintColor?.B ?? 0),
					IsAntialias = true,
					TextAlign = SKTextAlign.Center,
				};
				canvas.DrawText(marker.Label, 0, sizeH / 2f + textPaint.TextSize, textPaint);
			}

			if (marker.IsSelected || marker.IsHovered)
			{
				SKColor borderColor = marker.IsSelected
					? new SKColor(0, 0, 255)
					: new SKColor(95, 158, 160); // CadetBlue
				using SKPaint borderPaint = new()
				{
					Style = SKPaintStyle.Stroke,
					Color = borderColor,
					StrokeWidth = 1,
					IsAntialias = true,
				};
				canvas.DrawRect(-sizeW / 2f, -sizeH / 2f, sizeW, sizeH, borderPaint);
			}

			canvas.Restore();
			return true;
		}
	}
}
