using DcsBriefop.Data;
using DcsBriefop.Tools;
using SkiaSharp;

namespace DcsBriefop.Map
{
	internal class BriefopMarker
	{
		#region Fields
		private MapTemplateMarker m_template;
		private Bitmap m_bitmap;
		private SKBitmap m_skBitmap;
		#endregion

		#region Properties
		public GeoPoint Position { get; set; }
		public string TemplateName => m_template?.Name;
		public Color? TintColor { get; set; }
		public string Label { get; set; }
		public int Scale { get; set; }
		public int Angle { get; set; }
		public bool IsHovered { get; set; }
		public bool IsPressed { get; set; }
		public bool IsSelected { get; set; }
		#endregion

		#region CTOR
		private BriefopMarker(GeoPoint position, MapTemplateMarker template, Color? tintColor, string sLabel, int iScale, int iAngle)
		{
			Position = position;
			m_template = template;
			TintColor = tintColor;
			Label = sLabel;
			Scale = iScale;
			Angle = iAngle;
			LoadBitmap();
		}

		public static BriefopMarker NewFromTemplateName(GeoPoint position, string sTemplateName, Color? tintColor, string sLabel, int iScale, int iAngle)
		{
			return new BriefopMarker(position, MapTemplateMarker.GetTemplate(sTemplateName), tintColor, sLabel, iScale, iAngle);
		}

		public static BriefopMarker NewFromMizStyleName(GeoPoint position, string sMizStyleName, Color? tintColor, string sLabel, int iScale, int iAngle)
		{
			return new BriefopMarker(position, MapTemplateMarker.GetTemplateFromDcsMizFile(sMizStyleName), tintColor, sLabel, iScale, iAngle);
		}

		public BriefopMarker NewCleanCopy()
		{
			return new BriefopMarker(Position, m_template, TintColor, Label, Scale, Angle);
		}
		#endregion

		#region Methods
		public void LoadTemplate(string sTemplate)
		{
			m_template = MapTemplateMarker.GetTemplate(sTemplate);
			LoadBitmap();
		}

		public void LoadBitmap()
		{
			m_bitmap?.Dispose();
			m_skBitmap?.Dispose();
			m_skBitmap = null;

			m_bitmap = m_template.GetBitmap();
			if (TintColor is not null)
				ToolsImage.ColorTint(ref m_bitmap, TintColor.Value);
		}

		public SKBitmap GetSkBitmap()
		{
			if (m_skBitmap is null && m_bitmap is not null)
			{
				using MemoryStream ms = new();
				m_bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				ms.Position = 0;
				m_skBitmap = SKBitmap.Decode(ms);
			}
			return m_skBitmap;
		}

		public int GetSizeWidth() => m_template.SizeWidth * Scale;
		public int GetSizeHeight() => m_template.SizeHeight * Scale;
		public double GetOffsetX() => m_template.SizeWidth * Scale * m_template.OffsetWidth;
		public double GetOffsetY() => m_template.SizeHeight * Scale * m_template.OffsetHeight;
		#endregion
	}
}
