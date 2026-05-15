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
		public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature mapFeature, IStyle style, RenderService renderService, long iteration)
		{
			if (mapFeature is not PointFeature pointMapFeature || style is not BriefopLabelStyle labelStyle)
				return false;

			BriefopLabel label = labelStyle.Label;
			if (string.IsNullOrEmpty(label.Text))
				return true;

			Mapsui.Manipulations.ScreenPosition sp = viewport.WorldToScreen(pointMapFeature.Point);

			using SKFont font = new()
			{
				Size = label.FontSize,
				Typeface = string.IsNullOrEmpty(label.FontFamily) ? null : SKTypeface.FromFamilyName(label.FontFamily),
			};
			using SKPaint textPaint = new()
			{
				Color = label.ForeColor == Color.Empty ? SKColors.Black : new SKColor(label.ForeColor.R, label.ForeColor.G, label.ForeColor.B, label.ForeColor.A),
				IsAntialias = true,
			};

			float textW = font.MeasureText(label.Text);
			font.GetFontMetrics(out SKFontMetrics fm);
			float ascent = -fm.Ascent;
			float textH = ascent + fm.Descent;

			const float padding = 4f;

			canvas.Save();
			canvas.Translate((float)sp.X, (float)sp.Y);
			if (label.Angle != 0)
				canvas.RotateDegrees(label.Angle);

			// In DCS, textboxes are anchored at bottom-left; text body grows upward
			SKRect bgRect = new(-padding, -(textH + padding), textW + padding, padding);

			if (label.BackColor != Color.Empty)
			{
				using SKPaint bgPaint = new()
				{
					Style = SKPaintStyle.Fill,
					Color = new SKColor(label.BackColor.R, label.BackColor.G, label.BackColor.B, label.BackColor.A),
				};
				canvas.DrawRect(bgRect, bgPaint);
			}

			canvas.DrawText(label.Text, 0f, -fm.Descent, SKTextAlign.Left, font, textPaint);

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
