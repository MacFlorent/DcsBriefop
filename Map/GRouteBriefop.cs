using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GRouteBriefop : GMapRoute, ISerializable, IDeserializationCallback
	{
		#region Fields
		private Font m_font = new Font("Arial", 11);
		private double m_dRadian90 = 1.5707963268;
		private RouteBriefopTemplate m_template;
		private Bitmap m_bitmap;
		#endregion

		#region Properties
		public string RouteTemplate
		{
			get { return m_template?.Name; }
		}

		public Color? TintColor { get; set; }
		
		public int Thickness { get; set; }
		#endregion

		#region CTOR
		public GRouteBriefop(List<PointLatLng> points, string sName, RouteBriefopTemplate template, Color? tintColor, int iThickness) : base(points, sName)
		{
			m_template = template;
			TintColor = tintColor;
			Thickness = iThickness;
			LoadBitmap();

			Stroke = new Pen(TintColor.GetValueOrDefault(Color.Black), (float)Thickness);
			Stroke.DashStyle = m_template.DashStyle;
		}

		public static GRouteBriefop NewFromTemplateName(List<PointLatLng> points, string sName, string sTemplateName, Color? tintColor, int iThickness)
		{
			RouteBriefopTemplate template = RouteBriefopTemplate.GetTemplate(sTemplateName);
			return new GRouteBriefop(points, sName, template, tintColor, iThickness);
		}

		public static GRouteBriefop NewFromMizStyleName(List<PointLatLng> points, string sName, string sMizStyleName, Color? tintColor, int iThickness)
		{
			RouteBriefopTemplate template = RouteBriefopTemplate.GetTemplateFromDcsMizStyle(sMizStyleName);
			return new GRouteBriefop(points, sName, template, tintColor, iThickness);
		}
		#endregion

		#region Methods
		public void LoadTemplate(string sTemplate)
		{
			m_template = RouteBriefopTemplate.GetTemplate(sTemplate);
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
		#endregion

		#region Render
		public override void OnRender(Graphics g)
		{
			int iCorrectedThickness = Thickness;
			if (m_template.ThicknessCorrection is object)
			{
				iCorrectedThickness = (int)(iCorrectedThickness * m_template.ThicknessCorrection.Value);
			}

			for (int i = 0; i < LocalPoints.Count - 1; i++)
			{
				DrawSegment(g, iCorrectedThickness, i);
			}
		}

		private void DrawSegment(Graphics g, int iCorrectedThickness, int iPointStartIndex)
		{
			if (LocalPoints.Count <= iPointStartIndex + 1)
				return;

			Point pointStart = new Point((int)LocalPoints[iPointStartIndex].X, (int)LocalPoints[iPointStartIndex].Y);
			Point pointEnd = new Point((int)LocalPoints[iPointStartIndex + 1].X, (int)LocalPoints[iPointStartIndex + 1].Y);

			if (m_bitmap is object)
				DrawSegmentBitmap(g, iCorrectedThickness, pointStart, pointEnd);
			else
				DrawSegmentDash(g, pointStart, pointEnd);
		}

		private void DrawSegmentDash(Graphics g, Point pointStart, Point pointEnd)
		{
			g.DrawLine(Stroke, pointStart, pointEnd);
		}

		private void DrawSegmentBitmap(Graphics g, int iCorrectedThickness, Point pointStart, Point pointEnd)
		{
			//double dSegmentAngle = ComputeVerticalAngle(pointStart, pointEnd);
			double dHeight = iCorrectedThickness;
			//double dSegmentWidth = ComputePointDistance(pointStart, pointEnd);

			// drawing a border around the full segment
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
					//g.DrawLine(new Pen(Brushes.Chartreuse, 1), p1, p2);
					//g.DrawString($"{ComputeVerticalAngle(p1, p2) - m_dRadian90}", m_font, Brushes.Aqua, p1);
					DrawBitmapBetweenPoints(g, m_bitmap, p1, p2, dHeight, dDrawWidth);

					p1 = p2;
				}

			}

		}

		private Brush PickBrush()
		{
			Brush result = Brushes.Transparent;

			Random rnd = new Random();

			Type brushesType = typeof(Brushes);

			PropertyInfo[] properties = brushesType.GetProperties();

			int random = rnd.Next(properties.Length);
			result = (Brush)properties[random].GetValue(null, null);

			return result;
		}

		private void DrawBitmapBetweenPoints(Graphics g, Bitmap bitmap, Point p1, Point p2, double dHeight, double dDistance)
		{
			Point[] points = ComputeAngledPoints(p1, p2, dHeight);

			// points from ComputeAngledPoints are topLeft, topRight, bottomRight, bottomLeft
			// points for DrawImage are topLeft, topRight, bottomLeft
			// here they are switched to draw the image in the same orientation as DCS does
			//Point[] pointsDest = { points[0], points[1], points[3] }; // this is not matching the DCS orientation
			Point[] pointsDest = { points[2], points[3], points[1] }; // this is matching the DCS orientation
			Rectangle rectSource = new Rectangle(0, 0, (int)(dDistance * m_bitmap.Height / dHeight), bitmap.Height);
			g.DrawImage(bitmap, pointsDest, rectSource, GraphicsUnit.Pixel);

			g.DrawPolygon(new Pen(PickBrush(), 1), points);
		}

		private Point TranslatePoint(Point pointOrigin, Point pointTowards, double dDistance)
		{
			double dAngle = ComputeVerticalAngle(pointOrigin, pointTowards) - m_dRadian90;

			return new Point(pointOrigin.X + (int)Math.Round(dDistance * Math.Cos(dAngle), MidpointRounding.AwayFromZero),
												pointOrigin.Y + (int)Math.Round(dDistance * Math.Sin(dAngle), MidpointRounding.AwayFromZero));
		}

		private double ComputePointDistance(Point p1, Point p2)
		{
			return Math.Sqrt((Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
		}

		private Point[] ComputeAngledPoints(Point pointStart, Point pointEnd, double dHeight)
		{
			// get array of points defining a rectangle angled along the vector running from pointStart to pointEnd
			// with dHeight being the size of the sides crossing the vector
			// points returned are topLeft, topRight, bottomRight, bottomLeft
			// do not recompute angle each time double dAngle = ComputeHorizontalLineAngle(pointStart, pointEnd) - 1.5707963268; // remove 90° (in radians) for vertical angle
			double dAngle = ComputeVerticalAngle(pointStart, pointEnd);
			double dHalfHeight = dHeight / 2;

			double dXTranslate = dHalfHeight * Math.Cos(dAngle);
			double dYTranslate = dHalfHeight * Math.Sin(dAngle);

			Point pointTopLeft = new Point(pointStart.X - (int)dXTranslate, pointStart.Y - (int)dYTranslate);
			Point pointToRight = new Point(pointEnd.X - (int)dXTranslate, pointEnd.Y - (int)dYTranslate);
			Point pointBottomRight = new Point(pointEnd.X + (int)dXTranslate, pointEnd.Y + (int)dYTranslate);
			Point pointBottomLeft = new Point(pointStart.X + (int)dXTranslate, pointStart.Y + (int)dYTranslate);

			return new Point[] { pointTopLeft, pointToRight, pointBottomRight, pointBottomLeft };
		}

		private double ComputeVerticalAngle(Point pointStart, Point pointEnd)
		{
			// angle clockwise from 12 o'clock to the line from pointStart to pointEnd, in radian
			int iSpacingX = pointStart.X - pointEnd.X;
			int iSpacingY = pointStart.Y - pointEnd.Y;
			return Math.Atan2(iSpacingY, iSpacingX) - m_dRadian90; // remove 90° (in radians) for vertical angle
		}

		public override void Dispose()
		{
			base.Dispose();
		}
		#endregion

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("route_type", RouteTemplate);
			base.GetObjectData(info, context);
		}

		protected GRouteBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string sRouteType = Extensions.GetValue<string>(info, "route_type", "");
			LoadTemplate(sRouteType);
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
