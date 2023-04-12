using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GMarkerBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		#region Fields
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
		private GMarkerBriefop(PointLatLng point, MapTemplateMarker template, Color? tintColor, string sLabel, int iScale, int iAngle) : base(point)
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
			Size = new Size (m_template.SizeWidth * Scale, m_template.SizeHeight * Scale);
			Offset = new Point((int)(Size.Width * m_template.OffsetWidth), (int)(Size.Height * m_template.OffsetHeight));
		}

		public void LoadBitmap()
		{
			if (m_bitmap is not null)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			m_bitmap = m_template.GetBitmap();
			if (TintColor is not null)
				ToolsImage.ColorTint(ref m_bitmap, TintColor.Value);
		}
		#endregion

		#region Render
		public override void OnRender(Graphics g)
		{
			Render2(g);
		}

		private void Render2(Graphics g)
		{
			Point pointCenter = new Point(LocalPosition.X + Size.Width / 2, LocalPosition.Y + Size.Height / 2);

			GraphicsState state = g.Save();
			g.TranslateTransform(pointCenter.X, pointCenter.Y);
			if (Angle != 0)
			{
				g.RotateTransform(Angle);
			}

			Rectangle targetRectangle = new Rectangle(-Size.Width / 2, -Size.Height / 2, Size.Width, Size.Height);

			lock (m_bitmap) { g.DrawImage(m_bitmap, targetRectangle); }

			if (!string.IsNullOrEmpty(Label))
			{
				Point pointCenterString = new Point(0, Size.Height / 2);
				ToolsImage.DrawStringAngledCentered(g, pointCenterString, Label, ElementMapValue.DefaultFont, TintColor.GetValueOrDefault(Color.Black), true, 0, 0);
			}

			if (IsSelected)
			{
				g.DrawRectangle(ElementMapValue.PenSelected, targetRectangle);
			}
			else if (IsHovered)
			{
				g.DrawRectangle(ElementMapValue.PenMouseOver, targetRectangle);
			}

			g.Restore(state);
		}

		//private void Render1(Graphics g)
		//{
		//	Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);

		//	if (Angle != 0)
		//	{
		//		Point[] rotatedRect = RotateRectangle(rect, Angle);
		//		Point[] pointsDest = { rotatedRect[0], rotatedRect[1], rotatedRect[3] };
		//		lock (m_bitmap) { g.DrawImage(m_bitmap, pointsDest); }
		//	}
		//	else
		//	{
		//		lock (m_bitmap) { g.DrawImage(m_bitmap, rect); }
		//	}

		//	if (!string.IsNullOrEmpty(Label))
		//	{
		//		Color textColor = TintColor.GetValueOrDefault(Color.Black);
		//		Color shadowColor = Color.FromArgb(120, textColor);

		//		using (Brush textBrush = new SolidBrush(textColor))
		//		using (Brush shadowBrush = new SolidBrush(shadowColor))
		//		{
		//			g.DrawString(Label, ElementMapValue.Font, shadowBrush, rect.Left + 1, rect.Bottom + 1);
		//			g.DrawString(Label, ElementMapValue.Font, textBrush, rect.Left, rect.Bottom);
		//		}
		//	}

		//	if (IsSelected)
		//	{
		//		g.DrawRectangle(ElementMapValue.PenSelected, rect);
		//	}
		//	else if (IsHovered)
		//	{
		//		g.DrawRectangle(ElementMapValue.PenMouseOver, rect);
		//	}
		//}

		//private Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
		//{
		//	double angleInRadians = angleInDegrees * (Math.PI / 180);
		//	double cosTheta = Math.Cos(angleInRadians);
		//	double sinTheta = Math.Sin(angleInRadians);
		//	return new Point
		//	{
		//		X =
		//					(int)
		//					(cosTheta * (pointToRotate.X - centerPoint.X) -
		//					sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
		//		Y =
		//					(int)
		//					(sinTheta * (pointToRotate.X - centerPoint.X) +
		//					cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
		//	};
		//}

		//private Point[] RotateRectangle(Rectangle rect, int iAngle)
		//{
		//	Point pCenter = new Point((rect.X + rect.Width / 2), (rect.Y + rect.Height / 2));

		//	Point pointTopLeft = RotatePoint(new Point(rect.X, rect.Y), pCenter, iAngle);
		//	Point pointTopRight = RotatePoint(new Point(rect.X + rect.Width, rect.Y), pCenter, iAngle);
		//	Point pointBottomRight = RotatePoint(new Point(rect.X + rect.Width, rect.Y + rect.Height), pCenter, iAngle);
		//	Point pointBottomLeft = RotatePoint(new Point(rect.X, rect.Y + rect.Height), pCenter, iAngle);

		//	return new Point[] { pointTopLeft, pointTopRight, pointBottomRight, pointBottomLeft };
		//}
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

		#region IDisposable
		public override void Dispose()
		{
			base.Dispose();
			m_bitmap?.Dispose();
		}
		#endregion
	}
}
