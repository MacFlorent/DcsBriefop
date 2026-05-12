using DcsBriefop.Data;
using DcsBriefop.Tools;
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
			SKImage skImage = marker.GetSkImage();
			if (skImage is null)
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

			canvas.DrawImage(skImage, new SKRect(-sizeW / 2f, -sizeH / 2f, sizeW / 2f, sizeH / 2f), new SKSamplingOptions(SKCubicResampler.Mitchell));

			if (!string.IsNullOrEmpty(marker.Label))
			{
				using SKTypeface typeface = SKTypeface.FromFamilyName(ElementMapValue.DefaultFont.FontFamily.Name);
				using SKFont textFont = new()
				{
					Size = ElementMapValue.DefaultFont.Size,
					Typeface = typeface,
					Edging = SKFontEdging.SubpixelAntialias,
				};

				float textX = 0;
				float textY = sizeH / 2f + textFont.Size + 1f;

				/*
				float textWidth = textFont.MeasureText(marker.Label);
				float hPad = 4f;
				float vPad = 2f;
				SKRect bgRect = new(
					textX - textWidth / 2f - hPad,
					textY - textFont.Size - vPad,
					textX + textWidth / 2f + hPad,
					textY + vPad + 1f);
				using SKPaint bgPaint = new() { Color = new SKColor(0, 0, 0, 110), IsAntialias = true };
				canvas.DrawRoundRect(bgRect, 4f, 4f, bgPaint);
				*/
				System.Drawing.Color textColor = System.Drawing.Color.FromArgb(
					marker.TintColor?.R ?? 255,
					marker.TintColor?.G ?? 255,
					marker.TintColor?.B ?? 255);
				System.Drawing.Color outlineColor = textColor.Lerp(
					ToolsImage.PerceivedBrightness(textColor) > 128 ? System.Drawing.Color.Black : System.Drawing.Color.White, 0.8f);

				using SKPaint outlinePaint = new()
				{
					Style = SKPaintStyle.Stroke,
					Color = new SKColor(outlineColor.R, outlineColor.G, outlineColor.B, 210),
					StrokeWidth = 2f,
					StrokeJoin = SKStrokeJoin.Round,
					IsAntialias = true,
				};
				canvas.DrawText(marker.Label, textX, textY, SKTextAlign.Center, textFont, outlinePaint);

				using SKPaint textPaint = new()
				{
					Color = new SKColor(textColor.R, textColor.G, textColor.B),
					IsAntialias = true,
				};
				canvas.DrawText(marker.Label, textX, textY, SKTextAlign.Center, textFont, textPaint);
			}

			if (marker.IsSelected || marker.IsHovered)
			{
				SKColor borderColor = marker.IsSelected
					? ElementMapValue.SKColorSelected
					: ElementMapValue.SKColorMouseOver;

				using SKPaint glowPaint = new()
				{
					Style = SKPaintStyle.Stroke,
					Color = borderColor.WithAlpha(110),
					StrokeWidth = 5f,
					IsAntialias = true,
					MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 4f),
				};
				canvas.DrawRect(-sizeW / 2f - 1f, -sizeH / 2f - 1f, sizeW + 2f, sizeH + 2f, glowPaint);

				using SKPaint borderPaint = new()
				{
					Style = SKPaintStyle.Stroke,
					Color = borderColor,
					StrokeWidth = 1.5f,
					IsAntialias = true,
				};
				canvas.DrawRect(-sizeW / 2f, -sizeH / 2f, sizeW, sizeH, borderPaint);
			}

			canvas.Restore();
			return true;
		}
	}
}
