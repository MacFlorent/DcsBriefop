using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GMarkerBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		#region Fields
		private Font m_font = new Font("Arial", 11);
		private Pen m_penSelected = new Pen(Color.Blue, 1);
		private Pen m_penMouseOver = new Pen(Color.CadetBlue, 1);

		private MapTemplateMarker m_template;
		private Bitmap m_bitmap;
		#endregion

		#region Properties
		public string MarkerTemplate
		{
			get { return m_template?.Name; }
		}

		public Color? TintColor { get; set; }
		public string Label { get; set; }
		public int Scale { get; set; }
		public int Angle { get; set; }
		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;
		public bool IsSelected { get; set; } = false;
		#endregion

		#region CTOR
		public GMarkerBriefop(PointLatLng point, MapTemplateMarker template, Color? tintColor, string sLabel, int iScale, int iAngle) : base(point)
		{
			m_template = template;
			TintColor = tintColor;
			Label = sLabel;
			Scale = iScale;
			Angle = iAngle;
			LoadTemplateContent();
			LoadBitmap();
		}

		public static GMarkerBriefop NewFromTemplateName(PointLatLng point, string sTemplateName, Color? tintColor, string sLabel, int iScale, int iAngle)
		{
			MapTemplateMarker template = MapTemplateMarker.GetTemplate(sTemplateName);
			return new GMarkerBriefop(point, template, tintColor, sLabel, iScale, iAngle);
		}

		public static GMarkerBriefop NewFromMizStyleName(PointLatLng point, string sMizStyleName, Color? tintColor, string sLabel, int iScale, int iAngle)
		{
			MapTemplateMarker template = MapTemplateMarker.GetTemplateFromDcsMizFile(sMizStyleName);
			return new GMarkerBriefop(point, template, tintColor, sLabel, iScale, iAngle);
		}
		#endregion

		#region Methods
		public void LoadTemplate(string sTemplate)
		{
			m_template = MapTemplateMarker.GetTemplate(sTemplate);
			LoadTemplateContent();
		}

		private void LoadTemplateContent()
		{
			Size = new Size (m_template.SizeDisplay.Width * Scale, m_template.SizeDisplay.Height * Scale);
			Offset = new Point((int)(Size.Width * m_template.OffsetWidth), (int)(Size.Height * m_template.OffsetHeight));
		}

		public void LoadBitmap()
		{
			if (m_bitmap != null && m_bitmap != m_template.Bitmap)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			if (TintColor is object)
				m_bitmap = m_template.Bitmap.ColorTint(TintColor.Value);
			else
				m_bitmap = m_template.Bitmap;
		}

		public override void Dispose()
		{
			if (m_bitmap != null && m_bitmap != m_template.Bitmap)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			base.Dispose();
		}
		#endregion

		#region Render
		public override void OnRender(Graphics g)
		{
			Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);

			if (Angle != 0)
			{
				Point[] rotatedRect = RotateRectangle(rect, Angle);
				Point[] pointsDest = { rotatedRect[0], rotatedRect[1], rotatedRect[3] };
				lock (m_bitmap) { g.DrawImage(m_bitmap, pointsDest); }
			}
			else
			{
				lock (m_bitmap) { g.DrawImage(m_bitmap, rect); }
			}

			if (!string.IsNullOrEmpty(Label))
				{
					Color textColor = TintColor.GetValueOrDefault(Color.Black);
					Color shadowColor = Color.FromArgb(120, textColor);

					using (Brush textBrush = new SolidBrush(textColor))
					using (Brush shadowBrush = new SolidBrush(shadowColor))
					{
						g.DrawString(Label, m_font, shadowBrush, rect.Left + 1, rect.Bottom + 1);
						g.DrawString(Label, m_font, textBrush, rect.Left, rect.Bottom);
					}
				}

			if (IsSelected)
			{
				g.DrawRectangle(m_penSelected, rect);
			}
			else if (IsHovered)
			{
				g.DrawRectangle(m_penMouseOver, rect);
			}
		}

		static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
		{
			double angleInRadians = angleInDegrees * (Math.PI / 180);
			double cosTheta = Math.Cos(angleInRadians);
			double sinTheta = Math.Sin(angleInRadians);
			return new Point
			{
				X =
							(int)
							(cosTheta * (pointToRotate.X - centerPoint.X) -
							sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
				Y =
							(int)
							(sinTheta * (pointToRotate.X - centerPoint.X) +
							cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
			};
		}

		private Point[] RotateRectangle(Rectangle rect, int iAngle)
		{
			Point pCenter = new Point((rect.X + rect.Width / 2), (rect.Y + rect.Height / 2));

			Point pointTopLeft = RotatePoint(new Point(rect.X, rect.Y), pCenter, iAngle);
			Point pointTopRight = RotatePoint(new Point(rect.X + rect.Width, rect.Y), pCenter, iAngle);
			Point pointBottomRight = RotatePoint(new Point(rect.X + rect.Width, rect.Y + rect.Height), pCenter, iAngle);
			Point pointBottomLeft = RotatePoint(new Point(rect.X, rect.Y + rect.Height), pCenter, iAngle);

			return new Point[] { pointTopLeft, pointTopRight, pointBottomRight, pointBottomLeft };
		}
		#endregion

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("marker_type", MarkerTemplate);
			base.GetObjectData(info, context);
		}

		protected GMarkerBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string sMarkerType = Extensions.GetValue<string>(info, "marker_type", "");
			LoadTemplate(sMarkerType);
		}
		#endregion

		#region IDeserializationCallback Members
		public void OnDeserialization(object sender)
		{
			LoadBitmap();
		}
		#endregion
	}
}
