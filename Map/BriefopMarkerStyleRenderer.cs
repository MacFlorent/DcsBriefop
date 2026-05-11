using DcsBriefop.Data;
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
		public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature mapFeature, IStyle style, RenderService renderService, long iteration)
		{
			if (mapFeature is not PointFeature pointMapFeature || style is not BriefopMarkerStyle markerStyle)
				return false;

			BriefopMarker marker = markerStyle.Marker;
			SKBitmap skBitmap = marker.GetSkBitmap();
			if (skBitmap is null)
				return false;

			Mapsui.Manipulations.ScreenPosition screenPos = viewport.WorldToScreen(pointMapFeature.Point);
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
				using SKTypeface typeface = SKTypeface.FromFamilyName(ElementMapValue.DefaultFont.FontFamily.Name);
				using SKFont textFont = new() { Size = ElementMapValue.DefaultFont.Size, Typeface = typeface };
				float textX = 0;
				float textY = sizeH / 2f + textFont.Size;
				using SKPaint shadowPaint = new()
				{
					Color = new SKColor(0, 0, 0, 120),
					IsAntialias = true,
				};
				canvas.DrawText(marker.Label, textX + 1, textY + 1, SKTextAlign.Center, textFont, shadowPaint);
				using SKPaint textPaint = new()
				{
					Color = new SKColor(
						marker.TintColor?.R ?? 0,
						marker.TintColor?.G ?? 0,
						marker.TintColor?.B ?? 0),
					IsAntialias = true,
				};
				canvas.DrawText(marker.Label, textX, textY, SKTextAlign.Center, textFont, textPaint);
			}

			if (marker.IsSelected || marker.IsHovered)
			{
				SKColor borderColor = marker.IsSelected
					? ElementMapValue.SKColorSelected
					: ElementMapValue.SKColorMouseOver;
				
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
