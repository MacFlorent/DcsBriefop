using DcsBriefop.Data;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Rendering;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using Color = System.Drawing.Color;

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

			using SKFont font = ToolsImage.GetSKFontOrDefault(label.FontFamily, label.FontSize);
			using SKPaint textPaint = new()
			{
				Color = label.ForeColor == Color.Empty ? ElementMapValue.ForeColorDefault.ToSKColor() : label.ForeColor.ToSKColor(),
				IsAntialias = true,
			};

			float fTextW = font.MeasureText(label.Text);
			font.GetFontMetrics(out SKFontMetrics fm);
			float fAscent = -fm.Ascent;
			float fTextH = fAscent + fm.Descent;

			const float fPadding = 4f;

			canvas.Save();
			canvas.Translate((float)sp.X, (float)sp.Y);
			if (label.Angle != 0)
				canvas.RotateDegrees(label.Angle);

			// In DCS, textboxes are anchored at bottom-left; text body grows upward
			SKRect bgRect = new(-fPadding, -(fTextH + fPadding), fTextW + fPadding, fPadding);

			if (label.BackColor != Color.Empty)
			{
				using SKPaint bgPaint = new()
				{
					Style = SKPaintStyle.Fill,
					Color = label.BackColor.ToSKColor(),
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
