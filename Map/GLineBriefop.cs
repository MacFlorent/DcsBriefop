using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GLineBriefop : GMapRoute, ISerializable, IDeserializationCallback
	{
		#region Fields
		private MapTemplateLine m_template;
		private Bitmap m_bitmap;

		private Color m_lineColor;
		private int m_iThickness;
		private Brush m_brushLine;

		private Brush m_brushText;
		private string m_sText;
		private List<string> m_segmentTexts;
		private bool m_bPanelArrow;

		private bool m_bClosed;
		private Brush m_brushFill;

		private Brush m_brushPanel;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		private GLineBriefop(List<PointLatLng> points, string sName, MapTemplateLine template,
			Color lineColor, int iThickness,
			Color textColor, string sText, List<string> segmentTexts, bool bPanelArrow,
			bool bClosed, Color fillColor)
			: base(points, sName)
		{
			m_template = template;
			m_lineColor = lineColor;
			m_brushLine = new SolidBrush(lineColor);
			m_iThickness = iThickness;
			if (m_template.ThicknessCorrection is not null)
			{
				m_iThickness = (int)(m_iThickness * m_template.ThicknessCorrection.Value);
			}

			m_brushText = new SolidBrush(textColor);
			m_sText = sText;
			m_segmentTexts = segmentTexts;
			m_bPanelArrow = bPanelArrow;

			m_bClosed = bClosed;
			if (fillColor != Color.Empty)
				m_brushFill = new SolidBrush(fillColor);
			else
				m_brushFill = null;

			m_brushPanel = new SolidBrush(Color.White);

			LoadBitmap();

			Stroke = new Pen(lineColor, m_iThickness);
			Stroke.DashStyle = m_template.DashOverride.GetValueOrDefault(DashStyle.Solid);
		}

		public static GLineBriefop NewLineFromTemplateName(List<PointLatLng> points, string sName, string sTemplateName, Color lineColor, int iThickness, Color textColor, string sText)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplate(sTemplateName);
			return new GLineBriefop(points, sName, template, lineColor, iThickness, textColor, sText, null, false, false, Color.Empty);
		}

		public static GLineBriefop NewRouteFromTemplateName(List<PointLatLng> points, string sName, string sTemplateName, Color lineColor, int iThickness, Color textColor, List<string> m_segmentTexts)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplate(sTemplateName);
			return new GLineBriefop(points, sName, template, lineColor, iThickness, textColor, null, m_segmentTexts, true, false, Color.Empty);
		}

		public static GLineBriefop NewFromMizStyleName(List<PointLatLng> points, string sName, string sMizStyleName, Color lineColor, int iThickness, bool bClosed, Color fillColor)
		{
			MapTemplateLine template = MapTemplateLine.GetTemplateFromDcsMizStyle(sMizStyleName);
			return new GLineBriefop(points, sName, template, lineColor, iThickness, Color.Empty, null, null, false, bClosed, fillColor);
		}

		#endregion

		#region Methods
		public void LoadTemplate(string sTemplate)
		{
			m_template = MapTemplateLine.GetTemplate(sTemplate);
		}

		public void LoadBitmap()
		{
			if (m_bitmap is not null)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			m_bitmap = m_template.GetBitmap();
			if (m_bitmap is not null && m_lineColor != Color.Empty)
				ToolsImage.ColorTint(ref m_bitmap, m_lineColor);
		}
		#endregion

		#region Render
		public override void OnRender(Graphics g)
		{
			Render(g, LocalPoints);
		}

		public void Render(Graphics g, List<GPoint> gPoints)
		{
			Point? pointFirst = null, pointLast = null;
			Point[] pointsFill = new Point[gPoints.Count];

			for (int i = 0; i < gPoints.Count; i++)
			{
				Point pointCurrent = new Point((int)gPoints[i].X, (int)gPoints[i].Y);
				pointLast = pointCurrent;
				pointFirst ??= pointCurrent;
				pointsFill[i] = pointCurrent;

				if (i > 0)
				{
					string sSegmentText = null;
					if (m_segmentTexts is not null)
						sSegmentText = m_segmentTexts[i];

					Point pointPrevious = new Point((int)gPoints[i - 1].X, (int)gPoints[i - 1].Y);
					DrawSegment(g, pointPrevious, pointCurrent, sSegmentText);
				}
			}

			if (m_bClosed)
			{
				if (pointLast is not null && pointFirst is not null)
					DrawSegment(g, pointLast.Value, pointFirst.Value, null);

				if (m_brushFill is not null)
					g.FillPolygon(m_brushFill, pointsFill);
			}
		}

		private void DrawPanel(Graphics g, Point pointStart, Point pointEnd, string sText)
		{
			GraphicsState state;
			Font font = ElementMapValue.DefaultFont;
			int iMargin = 15;

			int iSegmentLength = (int)ComputePointDistance(pointStart, pointEnd);
			int iPanelHeight = (int)font.GetHeight();
			int iArrowLength = m_bPanelArrow ? iPanelHeight : 0;
			int iTextLength = 0;
			if (!string.IsNullOrEmpty(sText))
				iTextLength = (int)g.MeasureString(sText, font).Width;

			int iFullLength = iTextLength + iArrowLength;
			if (iFullLength + iMargin > iSegmentLength)
			{
				iTextLength = 0;
				iFullLength = iArrowLength;
			}

			if ((iTextLength <= 0 && iArrowLength <= 0) || iFullLength + iMargin > iSegmentLength)
				return;

			int iOffsetX = -iArrowLength / 2;

			Point pointCenter = new(pointStart.X + (pointEnd.X - pointStart.X) / 2, pointStart.Y + (pointEnd.Y - pointStart.Y) / 2);
			double dAngleRad = ComputeAngleRad(pointStart, pointEnd);
			float fAngleSegment = (float)(dAngleRad * 180 / Math.PI);

			// panel background
			state = g.Save();

			g.TranslateTransform(pointCenter.X, pointCenter.Y);
			if (fAngleSegment != 0)
				g.RotateTransform(fAngleSegment);

			Point[] pointList = new Point[m_bPanelArrow ? 5 : 4];
			int i = -1;
			pointList[++i] = new Point(-iTextLength / 2 + iOffsetX, iPanelHeight / 2); // bottom left
			pointList[++i] = new Point(-iTextLength / 2 + iOffsetX, -iPanelHeight / 2); // top left
			pointList[++i] = new Point(iTextLength / 2 + iOffsetX, -iPanelHeight / 2); // top right
			if (m_bPanelArrow)
				pointList[++i] = new Point(iTextLength / 2 + iPanelHeight + iOffsetX, 0); // arrow point
			pointList[++i] = new Point(iTextLength / 2 + iOffsetX, iPanelHeight / 2); // bottom right

			g.FillPolygon(m_brushPanel, pointList);
			g.DrawPolygon(new Pen(m_brushText), pointList);

			g.Restore(state);

			// text
			if (iTextLength > 0)
			{
				float fAngleText = fAngleSegment;
				if (fAngleText < -90)
					fAngleText += 180;
				else if (fAngleText > 90)
					fAngleText -= 180;

				state = g.Save();
				g.TranslateTransform(pointCenter.X, pointCenter.Y);
				if (fAngleText != 0)
					g.RotateTransform(fAngleText);

				g.DrawString(sText, ElementMapValue.DefaultFont, m_brushText, -(iTextLength / 2) - iOffsetX, -(iPanelHeight / 2));

				g.Restore(state);
			}
		}

		private void DrawSegment(Graphics g, Point pointStart, Point pointEnd, string sText)
		{
			//g.DrawString("*", m_font, Brushes.Red, pointStart.X, pointStart.Y);
			//g.DrawString("+", m_font, Brushes.Red, pointEnd.X, pointEnd.Y);
			//g.DrawLine(new Pen(Color.Red, 1), pointStart, pointEnd);

			if (m_bitmap is not null && m_template.DashOverride is null)
				DrawSegmentBitmap(g, pointStart, pointEnd);
			else
				DrawSegmentDash(g, pointStart, pointEnd);

			string sFinalText = sText;
			if (string.IsNullOrEmpty(sFinalText))
				sFinalText = m_sText;

			DrawPanel(g, pointStart, pointEnd, sFinalText);
		}

		private void DrawSegmentDash(Graphics g, Point pointStart, Point pointEnd)
		{
			g.DrawLine(Stroke, pointStart, pointEnd);

		}

		private void DrawSegmentBitmap(Graphics g, Point pointStart, Point pointEnd)
		{
			double dHeight = m_iThickness;

			//Point pEndTest = new Point(pointStart.X, pointStart.Y + 100); // horizontal forward
			//g.DrawString($"{ComputeAngle(pointStart, pointEnd) * 180 / Math.PI}°", m_font, Brushes.Red, pointStart);
			//g.DrawString($"{ComputeAngle(pointStart, pointEnd)} rad", m_font, Brushes.Red, pointEnd.X, pointEnd.Y + 15);
			//g.DrawPolygon(new Pen(Brushes.Red, 1), ComputeAngledPoints(pointStart, pointEnd, dHeight));

			// drawing the image multiple times to fill the segment
			lock (m_bitmap)
			{
				double dFullDrawWidth = m_bitmap.Width * dHeight / m_bitmap.Height;
				if (dFullDrawWidth > 0)
				{
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

			info.AddValue("route_type", m_template.Name);
			info.AddValue("thickness", m_iThickness);

		}

		protected GLineBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string sRouteType = Extensions.GetValue(info, "route_type", "");
			LoadTemplate(sRouteType);

			m_iThickness = Extensions.GetStruct<int>(info, "thickness", 1);
		}
		#endregion

		#region IDeserializationCallback Members
		public override void OnDeserialization(object sender)
		{
			base.OnDeserialization(sender);
			LoadBitmap();
		}
		#endregion

		#region IDisposable
		public override void Dispose()
		{
			base.Dispose();
			m_bitmap?.Dispose();
			m_brushFill?.Dispose();
			m_brushLine?.Dispose();
		}
		#endregion
	}
}
