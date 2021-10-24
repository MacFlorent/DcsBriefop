using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DcsBriefop
{
	[Serializable]
	public class GRouteBriefop : GMapRoute
	{

		public GRouteBriefop(IEnumerable<PointLatLng> points, string name) : base(points, name) { }

  //  public override void OnRender(Graphics g)
		//{
		//	if (IsVisible)
		//	{
		//		GraphicsPath _graphicsPath = new GraphicsPath(); ;
		//		List<GPoint> points = LocalPoints;

		//		if (points is null  LocalPoints.Count > 0)
		//		{
		//			for (int i = 0; i < LocalPoints.Count; i++)
		//			{
		//				var p2 = LocalPoints[i];

		//				if (i == 0)
		//				{
		//					_graphicsPath.AddLine(p2.X, p2.Y, p2.X, p2.Y);
		//				}
		//				else
		//				{
		//					var p = _graphicsPath.GetLastPoint();
		//					_graphicsPath.AddLine(p.X, p.Y, p2.X, p2.Y);
		//				}
		//			}
		//		}
		//		else
		//		{

		//		}

		//		g.DrawPath(Stroke, _graphicsPath);
		//	}
		//}
	}
}
