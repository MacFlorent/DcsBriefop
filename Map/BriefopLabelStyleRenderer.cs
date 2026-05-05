using Color = System.Drawing.Color;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Rendering;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using SkiaSharp;

namespace DcsBriefop.Map
{
	internal class BriefopLabelStyleRenderer : ISkiaStyleRenderer
	{
		public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature feature, IStyle style, RenderService renderService, long iteration)
		{
			if (feature is not PointFeature pointFeature || style is not BriefopLabelStyle labelStyle)
				return false;

			BriefopLabel label = labelStyle.Label;
			if (string.IsNullOrEmpty(label.Text))
				return true;

			Mapsui.Manipulations.ScreenPosition sp = viewport.WorldToScreen(pointFeature.Point);

			using SKPaint textPaint = new()
			{
				TextSize = label.FontSize,
				Color = label.ForeColor == Color.Empty ? SKColors.Black : new SKColor(label.ForeColor.R, label.ForeColor.G, label.ForeColor.B, label.ForeColor.A),
				IsAntialias = true,
				Typeface = string.IsNullOrEmpty(label.FontFamily) ? null : SKTypeface.FromFamilyName(label.FontFamily),
			};

			float textW = textPaint.MeasureText(label.Text);
			textPaint.GetFontMetrics(out SKFontMetrics fm);
			float ascent = -fm.Ascent;
			float textH = ascent + fm.Descent;

			canvas.Save();
			canvas.Translate((float)sp.X, (float)sp.Y);
			if (label.Angle != 0)
				canvas.RotateDegrees(label.Angle);

			// In DCS, textboxes are anchored at bottom-left; text body grows upward
			SKRect bgRect = new(0f, -textH, textW, 0f);

			if (label.BackColor != Color.Empty)
			{
				using SKPaint bgPaint = new()
				{
					Style = SKPaintStyle.Fill,
					Color = new SKColor(label.BackColor.R, label.BackColor.G, label.BackColor.B, label.BackColor.A),
				};
				canvas.DrawRect(bgRect, bgPaint);
			}

			canvas.DrawText(label.Text, 0f, -fm.Descent, textPaint);

			if (label.BorderThickness > 0 && label.ForeColor != Color.Empty)
			{
				using SKPaint borderPaint = new()
				{
					Style = SKPaintStyle.Stroke,
					Color = textPaint.Color,
					StrokeWidth = label.BorderThickness,
				};
				canvas.DrawRect(bgRect, borderPaint);
			}

			canvas.Restore();
			return true;
		}
	}
}
