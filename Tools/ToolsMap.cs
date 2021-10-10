using GMap.NET;
using System.Collections.Generic;

namespace DcsBriefop.Tools
{
	internal static class ToolsMap
	{
		public static RectLatLng? GetRectOfPoints(List<PointLatLng> points)
		{
			RectLatLng? rect = null;

			double left = double.MaxValue;
			double top = double.MinValue;
			double right = double.MinValue;
			double bottom = double.MaxValue;

			if (points.Count > 0)
			{
				foreach (var p in points)
				{
					// left
					if (p.Lng < left)
					{
						left = p.Lng;
					}

					// top
					if (p.Lat > top)
					{
						top = p.Lat;
					}

					// right
					if (p.Lng > right)
					{
						right = p.Lng;
					}

					// bottom
					if (p.Lat < bottom)
					{
						bottom = p.Lat;
					}
				}

				rect = RectLatLng.FromLTRB(left, top, right, bottom);
			}

			return rect;
		}

		public static PointLatLng? GetRectCenter(RectLatLng? rect)
		{
			if (rect is null)
				return null;
			else
				return new PointLatLng(rect.Value.Lat - rect.Value.HeightLat / 2, rect.Value.Lng + rect.Value.WidthLng / 2);
		}

		public static PointLatLng? GetPointsCenter(List<PointLatLng> points)
		{
			if (points.Count <= 0)
				return null;
			else if (points.Count == 1)
				return points[0];
			else
				return GetRectCenter(GetRectOfPoints(points));
		}
	}
}

