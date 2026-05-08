using Color = System.Drawing.Color;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Rendering;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using SkiaSharp;
using System.Drawing.Drawing2D;

namespace DcsBriefop.Map
{
	internal class BriefopLineStyleRenderer : ISkiaStyleRenderer
	{
		public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature mapFeature, IStyle style, RenderService renderService, long iteration)
		{
			if (mapFeature is not GeometryFeature geometryMapFeature || style is not BriefopLineStyle lineStyle)
				return false;

			BriefopLine line = lineStyle.Line;
			Coordinate[] coords = geometryMapFeature.Geometry?.Coordinates;
			if (coords is null || coords.Length < 2)
				return false;

			SKPoint[] screenPoints = [.. coords.Select(c =>
			{
				Mapsui.Manipulations.ScreenPosition sp = viewport.WorldToScreen(new MPoint(c.X, c.Y));
				return new SKPoint((float)sp.X, (float)sp.Y);
			})];

			if (line.Closed && line.FillColor != Color.Empty && screenPoints.Length >= 3)
				DrawFill(canvas, line, screenPoints);

			for (int i = 1; i < screenPoints.Length; i++)
			{
				string sSegmentText = null;
				if (line.SegmentTexts is not null && i < line.SegmentTexts.Count)
					sSegmentText = line.SegmentTexts[i];

				DrawSegment(canvas, line, screenPoints[i - 1], screenPoints[i], sSegmentText);
			}

			if (line.Closed && screenPoints.Length >= 3)
				DrawSegment(canvas, line, screenPoints[^1], screenPoints[0], null);

			return true;
		}

		private static void DrawFill(SKCanvas canvas, BriefopLine line, SKPoint[] screenPoints)
		{
			using SKPath path = new();
			path.MoveTo(screenPoints[0]);
			for (int i = 1; i < screenPoints.Length; i++)
				path.LineTo(screenPoints[i]);
			path.Close();

			using SKPaint paint = new()
			{
				Style = SKPaintStyle.Fill,
				Color = ToSKColor(line.FillColor),
				IsAntialias = true,
			};
			canvas.DrawPath(path, paint);
		}

		private static void DrawSegment(SKCanvas canvas, BriefopLine line, SKPoint p1, SKPoint p2, string sText)
		{
			SKBitmap skBitmap = line.GetSkBitmap();
			if (skBitmap is not null && line.Template.DashOverride is null)
				DrawSegmentBitmap(canvas, line, p1, p2, skBitmap);
			else
				DrawSegmentDash(canvas, line, p1, p2);

			string sFinalText = sText ?? line.Text;
			if (!string.IsNullOrEmpty(sFinalText))
				DrawPanel(canvas, line, p1, p2, sFinalText);
		}

		private static void DrawSegmentDash(SKCanvas canvas, BriefopLine line, SKPoint p1, SKPoint p2)
		{
			using SKPaint paint = new()
			{
				Style = SKPaintStyle.Stroke,
				Color = line.LineColor == Color.Empty ? SKColors.Black : ToSKColor(line.LineColor),
				StrokeWidth = line.Thickness,
				IsAntialias = true,
				StrokeCap = SKStrokeCap.Round,
			};

			if (line.Template.DashOverride == DashStyle.Dot)
				paint.PathEffect = SKPathEffect.CreateDash([line.Thickness * 1f, line.Thickness * 2f], 0f);
			else if (line.Template.DashOverride == DashStyle.Dash)
				paint.PathEffect = SKPathEffect.CreateDash([line.Thickness * 4f, line.Thickness * 2f], 0f);

			canvas.DrawLine(p1, p2, paint);
		}

		private static void DrawSegmentBitmap(SKCanvas canvas, BriefopLine line, SKPoint p1, SKPoint p2, SKBitmap bitmap)
		{
			float dx = p2.X - p1.X;
			float dy = p2.Y - p1.Y;
			float segLen = MathF.Sqrt(dx * dx + dy * dy);
			if (segLen <= 0 || bitmap.Height == 0)
				return;

			float tileW = (float)bitmap.Width * line.Thickness / bitmap.Height;
			if (tileW <= 0)
				return;

			float angleDeg = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
			float halfH = line.Thickness / 2f;

			canvas.Save();
			canvas.Translate(p1.X, p1.Y);
			canvas.RotateDegrees(angleDeg);

			float x = 0;
			while (x < segLen)
			{
				float drawW = Math.Min(tileW, segLen - x);
				float srcW = drawW * bitmap.Width / tileW;
				canvas.DrawBitmap(bitmap, new SKRect(0, 0, srcW, bitmap.Height), new SKRect(x, -halfH, x + drawW, halfH));
				x += tileW;
			}

			canvas.Restore();
		}

		private static void DrawPanel(SKCanvas canvas, BriefopLine line, SKPoint p1, SKPoint p2, string sText)
		{
			using SKFont textFont = new() { Size = 11f };
			using SKPaint textPaint = new()
			{
				Color = line.TextColor == Color.Empty ? SKColors.Black : ToSKColor(line.TextColor),
				IsAntialias = true,
			};

			float textW = string.IsNullOrEmpty(sText) ? 0f : textFont.MeasureText(sText);
			textFont.GetFontMetrics(out SKFontMetrics fm);
			float ascent = -fm.Ascent;
			float textH = textFont.Spacing;

			float arrowLen = line.PanelArrow ? textH : 0f;
			float totalW = textW + arrowLen;
			const float margin = 15f;

			float dx = p2.X - p1.X;
			float dy = p2.Y - p1.Y;
			float segLen = MathF.Sqrt(dx * dx + dy * dy);

			if ((textW <= 0f && arrowLen <= 0f) || totalW + margin > segLen)
				return;

			float centerX = (p1.X + p2.X) / 2f;
			float centerY = (p1.Y + p2.Y) / 2f;
			float angleDeg = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
			float offsetX = -arrowLen / 2f;

			using SKPaint bgPaint = new() { Style = SKPaintStyle.Fill, Color = SKColors.White, IsAntialias = true };
			using SKPaint borderPaint = new() { Style = SKPaintStyle.Stroke, Color = textPaint.Color, StrokeWidth = 1f, IsAntialias = true };

			// Panel background with optional arrow
			canvas.Save();
			canvas.Translate(centerX, centerY);
			canvas.RotateDegrees(angleDeg);

			using SKPath panelPath = new();
			float half = textH / 2f;
			panelPath.MoveTo(-textW / 2f + offsetX, half);
			panelPath.LineTo(-textW / 2f + offsetX, -half);
			panelPath.LineTo(textW / 2f + offsetX, -half);
			if (line.PanelArrow)
				panelPath.LineTo(textW / 2f + textH + offsetX, 0f);
			panelPath.LineTo(textW / 2f + offsetX, half);
			panelPath.Close();

			canvas.DrawPath(panelPath, bgPaint);
			canvas.DrawPath(panelPath, borderPaint);
			canvas.Restore();

			// Text with reading-direction correction
			float angleText = angleDeg;
			if (angleText < -90f) angleText += 180f;
			else if (angleText > 90f) angleText -= 180f;

			canvas.Save();
			canvas.Translate(centerX, centerY);
			canvas.RotateDegrees(angleText);
			canvas.DrawText(sText, -(textW / 2f) - offsetX, -textH / 2f + ascent, SKTextAlign.Left, textFont, textPaint);
			canvas.Restore();
		}

		private static SKColor ToSKColor(Color c) => new(c.R, c.G, c.B, c.A);
	}
}
