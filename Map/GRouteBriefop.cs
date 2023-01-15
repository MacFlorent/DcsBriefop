using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GRouteBriefop : GMapRoute, ISerializable, IDeserializationCallback
	{
		#region Fields
		private MapTemplateRoute m_template;
		private Bitmap m_bitmap;
		#endregion

		#region Properties
		public string RouteTemplate
		{
			get { return m_template?.Name; }
		}

		public Color? TintColor { get; set; }
		public string Label
		{
			get { return Name; }
		}
		public int Thickness { get; set; }
		#endregion

		#region CTOR
		public GRouteBriefop(List<PointLatLng> points, string sName, MapTemplateRoute template, Color? tintColor, int iThickness) : base(points, sName)
		{
			m_template = template;
			TintColor = tintColor;
			Thickness = iThickness;
			if (m_template.ThicknessCorrection is object)
			{
				Thickness = (int)(Thickness * m_template.ThicknessCorrection.Value);
			}

			LoadBitmap();

			Stroke = new Pen(TintColor.GetValueOrDefault(Color.Black), Thickness);
			Stroke.DashStyle = m_template.DashOverride.GetValueOrDefault(DashStyle.Solid);
		}

		public static GRouteBriefop NewFromTemplateName(List<PointLatLng> points, string sName, string sTemplateName, Color? tintColor, int iThickness)
		{
			MapTemplateRoute template = MapTemplateRoute.GetTemplate(sTemplateName);
			return new GRouteBriefop(points, sName, template, tintColor, iThickness);
		}

		public static GRouteBriefop NewFromMizStyleName(List<PointLatLng> points, string sName, string sMizStyleName, Color? tintColor, int iThickness)
		{
			MapTemplateRoute template = MapTemplateRoute.GetTemplateFromDcsMizStyle(sMizStyleName);
			return new GRouteBriefop(points, sName, template, tintColor, iThickness);
		}
		#endregion

		#region Methods
		public void LoadTemplate(string sTemplate)
		{
			m_template = MapTemplateRoute.GetTemplate(sTemplate);
		}

		public void LoadBitmap()
		{
			if (m_template.Bitmap is null)
				return;

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
			Render(g, LocalPoints);
		}

		public void Render(Graphics g, List<GPoint> gPoints)
		{
			for (int i = 0; i < gPoints.Count - 1; i++)
			{
				Point pointStart = new Point((int)gPoints[i].X, (int)gPoints[i].Y);
				Point pointEnd = new Point((int)gPoints[i + 1].X, (int)gPoints[i + 1].Y);

				DrawSegment(g, pointStart, pointEnd);
			}
		}

		private void DrawSegment(Graphics g, Point pointStart, Point pointEnd)
		{
			//g.DrawString("*", m_font, Brushes.Red, pointStart.X, pointStart.Y);
			//g.DrawString("+", m_font, Brushes.Red, pointEnd.X, pointEnd.Y);
			//g.DrawLine(new Pen(Color.Red, 1), pointStart, pointEnd);

			if (m_bitmap is object && m_template.DashOverride is null)
				DrawSegmentBitmap(g, pointStart, pointEnd);
			else
				DrawSegmentDash(g, pointStart, pointEnd);

			if (!string.IsNullOrEmpty(Label))
			{
				DrawStringAngledCentered(g, pointStart, pointEnd, Label);
			}
		}

		private void DrawSegmentDash(Graphics g, Point pointStart, Point pointEnd)
		{
			g.DrawLine(Stroke, pointStart, pointEnd);
		}

		private void DrawStringAngledCentered(Graphics g, Point pointStart, Point pointEnd, string sLabel)
		{
			double dAngleRad = ComputeAngleRad(pointStart, pointEnd);
			float fAngle = (float)(dAngleRad * 180 / Math.PI);
			Point pointCenter = new Point(pointStart.X + (pointEnd.X - pointStart.X) / 2, pointStart.Y + (pointEnd.Y - pointStart.Y) / 2);

			ToolsImage.DrawStringAngledCentered(g, pointCenter, sLabel, ElementMapValue.DefaultFont, TintColor.GetValueOrDefault(Color.Black), true, fAngle, Thickness/2);
		}


		private void DrawSegmentBitmap(Graphics g, Point pointStart, Point pointEnd)
		{
			double dHeight = Thickness;

			//Point pEndTest = new Point(pointStart.X, pointStart.Y + 100); // horizontal forward
			//g.DrawString($"{ComputeAngle(pointStart, pointEnd) * 180 / Math.PI}°", m_font, Brushes.Red, pointStart);
			//g.DrawString($"{ComputeAngle(pointStart, pointEnd)} rad", m_font, Brushes.Red, pointEnd.X, pointEnd.Y + 15);
			//g.DrawPolygon(new Pen(Brushes.Red, 1), ComputeAngledPoints(pointStart, pointEnd, dHeight));

			// drawing the image multiple times to fill the segment
			lock (m_bitmap)
			{
				double dFullDrawWidth = m_bitmap.Width * dHeight / m_bitmap.Height;

				bool bFinished = false;
				Point p1 = pointStart;
				Point p2;
				while (!bFinished)
				{
					double dDrawWidth = dFullDrawWidth;
					double dDistanceRemaining = ComputePointDistance(p1, pointEnd);
					if (dDistanceRemaining < dDrawWidth)
					{
						p2 = pointEnd;
						dDrawWidth = dDistanceRemaining;
						bFinished = true;
					}
					else
						p2 = TranslatePoint(p1, pointEnd, dDrawWidth);

					//g.DrawPolygon(new Pen(Brushes.Chartreuse, 1), ComputeAngledPoints(p1, p2, dHeight));
					DrawBitmapBetweenPoints(g, m_bitmap, p1, p2, dHeight, dDrawWidth);

					p1 = p2;
				}
			}
		}

		private void DrawBitmapBetweenPoints(Graphics g, Bitmap bitmap, Point p1, Point p2, double dHeight, double dDistance)
		{
			Point[] points = ComputeAngledPoints(p1, p2, dHeight);

			// points from ComputeAngledPoints are topLeft, topRight, bottomRight, bottomLeft
			// points for DrawImage are topLeft, topRight, bottomLeft
			Point[] pointsDest = { points[0], points[1], points[3] };
			Rectangle rectSource = new Rectangle(0, 0, (int)(dDistance * m_bitmap.Height / dHeight), bitmap.Height);
			g.DrawImage(bitmap, pointsDest, rectSource, GraphicsUnit.Pixel);
		}

		private double ComputePointDistance(Point p1, Point p2)
		{
			return Math.Sqrt((Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
		}

		private Point TranslatePoint(Point pointOrigin, Point pointTowards, double dDistance)
		{
			double dAngle = ComputeAngleRad(pointOrigin, pointTowards);
			return new Point(pointOrigin.X + (int)Math.Round(dDistance * Math.Cos(dAngle), MidpointRounding.AwayFromZero),
												pointOrigin.Y + (int)Math.Round(dDistance * Math.Sin(dAngle), MidpointRounding.AwayFromZero));
		}

		private Point[] ComputeAngledPoints(Point pointStart, Point pointEnd, double dHeight)
		{
			// get array of points defining a rectangle angled along the vector running from pointStart to pointEnd
			// with dHeight being the size of the sides crossing the vector perpendicularly
			// points returned are topLeft, topRight, bottomRight, bottomLeft
			double dAngle = ComputeAngleRad(pointStart, pointEnd) - Math.PI / 2; // get the perpendicular angle
			double dHalfHeight = dHeight / 2;

			double dXTranslate = dHalfHeight * Math.Cos(dAngle);
			double dYTranslate = dHalfHeight * Math.Sin(dAngle);

			Point pointTopLeft = new Point(pointStart.X - (int)dXTranslate, pointStart.Y - (int)dYTranslate);
			Point pointTopRight = new Point(pointEnd.X - (int)dXTranslate, pointEnd.Y - (int)dYTranslate);
			Point pointBottomRight = new Point(pointEnd.X + (int)dXTranslate, pointEnd.Y + (int)dYTranslate);
			Point pointBottomLeft = new Point(pointStart.X + (int)dXTranslate, pointStart.Y + (int)dYTranslate);

			return new Point[] { pointTopLeft, pointTopRight, pointBottomRight, pointBottomLeft };
		}

		private double ComputeAngleRad(Point pointStart, Point pointEnd)
		{
			int iSpacingX = pointEnd.X - pointStart.X;
			int iSpacingY = pointEnd.Y - pointStart.Y;
			// Atan2 with these parameters, will return the angle of the vector running from pointStart to pointEnd
			// The angle is trigonometric, in radian, but as the drawing referential is inverted on Y (Y increments when going down) it is clockwise
			return Math.Atan2(iSpacingY, iSpacingX);
		}
		#endregion

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("route_type", RouteTemplate);
			info.AddValue("thickness", Thickness);

		}

		protected GRouteBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string sRouteType = Extensions.GetValue(info, "route_type", "");
			LoadTemplate(sRouteType);

			Thickness = Extensions.GetStruct<int>(info, "thickness", 1);
		}
		#endregion

		#region IDeserializationCallback Members
		public override void OnDeserialization(object sender)
		{
			base.OnDeserialization(sender);
			LoadBitmap();
		}
		#endregion
	}
}
