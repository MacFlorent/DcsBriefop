using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GRouteBriefop : GMapRoute, ISerializable, IDeserializationCallback
	{
		public GRouteBriefop(List<PointLatLng> points, string sName) : base(points, sName) { }

		public override void OnRender(Graphics g)
		{
			for(int i = 0; i < LocalPoints.Count - 1; i++)
			{
				DrawSegment(g, i);
			}


			//base.OnRender(g);
		}

		private void DrawSegment(Graphics g, int iPointStartIndex)
		{
			if (LocalPoints.Count <= iPointStartIndex + 1)
				return;

			Point pointStart = new Point((int)LocalPoints[iPointStartIndex].X, (int)LocalPoints[iPointStartIndex].Y);
			Point pointEnd = new Point((int)LocalPoints[iPointStartIndex + 1].X, (int)LocalPoints[iPointStartIndex + 1].Y);

			double dWidth = Stroke.Width;
			string sBitmapName = "polyline_Bound4.png";

			double dAngle = ComputeHorizontalLineAngle(pointStart, pointEnd) - 1.5707963268; // remove 90° (in radians) for vertical angle
			double dHalfWidth = dWidth / 2;

			double dXTranslate = dHalfWidth * Math.Cos(dAngle);
			double dYTranslate = dHalfWidth * Math.Sin(dAngle);

			Point pointTopLeft = new Point(pointStart.X - (int)dXTranslate, pointStart.Y - (int)dYTranslate);
			Point pointToRight = new Point(pointEnd.X - (int)dXTranslate, pointEnd.Y - (int)dYTranslate);
			Point pointBottomRight = new Point(pointEnd.X + (int)dXTranslate, pointEnd.Y + (int)dYTranslate);
			Point pointBottomLeft = new Point(pointStart.X + (int)dXTranslate, pointStart.Y + (int)dYTranslate);

			Point[] pointsCanvas = { pointTopLeft, pointToRight, pointBottomRight, pointBottomLeft };
			g.DrawPolygon(new Pen(Stroke.Brush, 2), pointsCanvas);

			using (Bitmap bitmap = new Bitmap(sBitmapName))
			{
				//Point[] pointsBitmap = { pointTopLeft, pointToRight, pointBottomLeft };
				//g.DrawImage(bitmap, pointsBitmap);
			}

		}

		private double ComputeHorizontalLineAngle(Point pointStart, Point pointEnd)
		{
			// angle clockwise from 9 o'clock to the line from pointStart to pointEnd, in radian
			int iSpacingX = pointStart.X - pointEnd.X;
			int iSpacingY = pointStart.Y - pointEnd.Y;
			return Math.Atan2(iSpacingY, iSpacingX);
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			//info.AddValue("marker_type", MarkerTemplate);
			base.GetObjectData(info, context);
		}

		protected GRouteBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			//string sMarkerType = Extensions.GetValue<string>(info, "marker_type", "");
			//LoadTemplate(sMarkerType);
		}
		#endregion

		#region IDeserializationCallback Members
		public void OnDeserialization(object sender)
		{
			//LoadBitmap();
		}
		#endregion
	}
}
