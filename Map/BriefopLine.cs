using DcsBriefop.Data;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Nts;
using NetTopologySuite.Geometries;
using SkiaSharp;
using System.Drawing.Drawing2D;

namespace DcsBriefop.Map
{
	internal class BriefopLine
	{
		#region Fields
		private SKBitmap m_skBitmap;
		#endregion

		#region Properties
		public List<GeoPoint> Points { get; }
		public MapTemplateLine Template { get; }
		public Color LineColor { get; }
		public int Thickness { get; }
		public Color TextColor { get; }
		public string Text { get; }
		public List<string> SegmentTexts { get; }
		public bool PanelArrow { get; }
		public bool Closed { get; }
		public Color FillColor { get; }
		#endregion

		#region CTOR
		private BriefopLine(List<GeoPoint> points, MapTemplateLine template,
			Color lineColor, int iThickness,
			Color textColor, string sText, List<string> segmentTexts, bool bPanelArrow,
			bool bClosed, Color fillColor)
		{
			Points = points;
			Template = template;
			LineColor = lineColor;
			Thickness = iThickness;
			if (template.ThicknessCorrection is not null)
				Thickness = (int)(iThickness * template.ThicknessCorrection.Value);
			TextColor = textColor;
			Text = sText;
			SegmentTexts = segmentTexts;
			PanelArrow = bPanelArrow;
			Closed = bClosed;
			FillColor = fillColor;
			LoadSkBitmap();
		}

		public static BriefopLine NewLineFromTemplateName(List<GeoPoint> points, string sTemplateName, Color lineColor, int iThickness, Color textColor, string sText)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplate(sTemplateName);
			return new BriefopLine(points, template, lineColor, iThickness, textColor, sText, null, false, false, Color.Empty);
		}

		public static BriefopLine NewRouteFromTemplateName(List<GeoPoint> points, string sTemplateName, Color lineColor, int iThickness, Color textColor, List<string> segmentTexts)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplate(sTemplateName);
			return new BriefopLine(points, template, lineColor, iThickness, textColor, null, segmentTexts, true, false, Color.Empty);
		}

		public static BriefopLine NewFromMizStyleName(List<GeoPoint> points, string sMizStyleName, Color lineColor, int iThickness, bool bClosed, Color fillColor)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplateFromDcsMizStyle(sMizStyleName);
			return new BriefopLine(points, template, lineColor, iThickness, Color.Empty, null, null, false, bClosed, fillColor);
		}
		#endregion

		#region Methods
		public void LoadSkBitmap()
		{
			m_skBitmap?.Dispose();
			m_skBitmap = null;

			Bitmap bitmap = Template.GetBitmap();
			if (bitmap is not null)
			{
				if (LineColor != Color.Empty)
					ToolsImage.ColorTint(ref bitmap, LineColor);
				using MemoryStream ms = new();
				bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ms.Position = 0;
				m_skBitmap = SKBitmap.Decode(ms);
				bitmap.Dispose();
			}
		}

		public SKBitmap GetSkBitmap() => m_skBitmap;

		public GeometryFeature ToGeometryFeature()
		{
			if (Points.Count < 2)
				return null;

			GeometryFactory gf = new();
			Coordinate[] coords = [.. Points.Select(p => { MPoint mp = MapProjection.ToMPoint(p); return new Coordinate(mp.X, mp.Y); })];
			Geometry geometry = gf.CreateLineString(coords);
			GeometryFeature feature = new(geometry);
			feature.Styles.Add(new BriefopLineStyle(this));
			return feature;
		}
		#endregion
	}
}
