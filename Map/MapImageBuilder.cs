using DcsBriefop.Briefing;
using DcsBriefop.MasterData;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcsBriefop.Map
{
	public struct MapInfo
	{
		public RectLatLng Area;
		public int Zoom;
		public GMapProvider Type;

		public MapInfo(RectLatLng area, int zoom, GMapProvider type)
		{
			this.Area = area;
			this.Zoom = zoom;
			this.Type = type;
		}
	}

	internal class MapImageBuilder
	{

		public static Bitmap Generate(CustomDataMap mapData, string sFilePath)
		{
			GMapProvider mapProvider = ElementMapValue.MapProvider;
			int iZoom = (int)mapData.Zoom;


			PointLatLng centerLatLng = new PointLatLng(mapData.CenterLatitude, mapData.CenterLongitude);

			GPoint centerPoint = mapProvider.Projection.FromLatLngToPixel(centerLatLng, iZoom);
			GPoint topLeft = new GPoint(centerPoint.X - ElementImageSize.Width / 2, centerPoint.Y - ElementImageSize.Height / 2);
			GPoint bottomRight = new GPoint(topLeft.X + ElementImageSize.Width, topLeft.Y + ElementImageSize.Height);

			PointLatLng topLeftLatLng = mapProvider.Projection.FromPixelToLatLng(topLeft, iZoom);
			PointLatLng bottomRightLatLng = mapProvider.Projection.FromPixelToLatLng(bottomRight, iZoom);
			RectLatLng rectLatLng = RectLatLng.FromLTRB(topLeftLatLng.Lng, topLeftLatLng.Lat, bottomRightLatLng.Lng, bottomRightLatLng.Lat);
			List<GPoint> _tileArea = new List<GPoint>();
			_tileArea.AddRange(mapProvider.Projection.GetAreaTileList(rectLatLng, iZoom, 1));
			_tileArea.TrimExcess();


			var pxDelta = new GPoint(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
			var maxOfTiles = mapProvider.Projection.GetTileMatrixMaxXY(iZoom);

			int padding = 22;
			{
				var bmpDestination = new Bitmap((int)(pxDelta.X + padding * 2), (int)(pxDelta.Y + padding * 2));
				{
					using (var gfx = Graphics.FromImage(bmpDestination))
					{
						gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
						gfx.SmoothingMode = SmoothingMode.HighQuality;

						// get tiles & combine into one
						lock (_tileArea)
						{
							foreach (var p in _tileArea)
							{
								foreach (var tp in mapProvider.Overlays)
								{
									Exception ex;
									GMapImage tile;

									// tile number inversion(BottomLeft -> TopLeft) for pergo maps
									if (tp.InvertedAxisY)
									{
										tile = GMaps.Instance.GetImageFrom(tp, new GPoint(p.X, maxOfTiles.Height - p.Y), iZoom, out ex) as GMapImage;
									}
									else // ok
									{
										tile = GMaps.Instance.GetImageFrom(tp, p, iZoom, out ex) as GMapImage;
									}

									if (tile != null)
									{
										using (tile)
										{
											long x = p.X * mapProvider.Projection.TileSize.Width - topLeft.X + padding;
											long y = p.Y * mapProvider.Projection.TileSize.Width - topLeft.Y + padding;
											{
												gfx.DrawImage(tile.Img, x, y, mapProvider.Projection.TileSize.Width, mapProvider.Projection.TileSize.Height);
											}
										}
									}
								}
							}
						}

						// draw routes
						{
							foreach (var r in mapData.MapOverlayCustom.Routes)
							{
								if (r.IsVisible)
								{
									using (var rp = new GraphicsPath())
									{
										for (int j = 0; j < r.Points.Count; j++)
										{
											var pr = r.Points[j];
											var px = mapProvider.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, iZoom);

											px.Offset(padding, padding);
											px.Offset(-topLeft.X, -topLeft.Y);

											var p2 = px;

											if (j == 0)
											{
												rp.AddLine(p2.X, p2.Y, p2.X, p2.Y);
											}
											else
											{
												var p = rp.GetLastPoint();
												rp.AddLine(p.X, p.Y, p2.X, p2.Y);
											}
										}

										if (rp.PointCount > 0)
										{
											gfx.DrawPath(r.Stroke, rp);
										}
									}
								}
							}
						}

						//// draw polygons
						//{
						//	foreach (var r in mapData.MapOverlayCustom.Polygons)
						//	{
						//		if (r.IsVisible)
						//		{
						//			using (var rp = new GraphicsPath())
						//			{
						//				for (int j = 0; j < r.Points.Count; j++)
						//				{
						//					var pr = r.Points[j];
						//					var px = info.Type.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, info.Zoom);

						//					px.Offset(padding, padding);
						//					px.Offset(-topLeftPx.X, -topLeftPx.Y);

						//					var p2 = px;

						//					if (j == 0)
						//					{
						//						rp.AddLine(p2.X, p2.Y, p2.X, p2.Y);
						//					}
						//					else
						//					{
						//						var p = rp.GetLastPoint();
						//						rp.AddLine(p.X, p.Y, p2.X, p2.Y);
						//					}
						//				}

						//				if (rp.PointCount > 0)
						//				{
						//					rp.CloseFigure();

						//					gfx.FillPath(r.Fill, rp);

						//					gfx.DrawPath(r.Stroke, rp);
						//				}
						//			}
						//		}
						//	}
						//}

						// draw markers
						{
							foreach (var r in mapData.MapOverlayCustom.Markers)
							{
								if (r.IsVisible)
								{
									var pr = r.Position;
									var px = mapProvider.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, iZoom);

									px.Offset(padding, padding);
									px.Offset(-topLeft.X, -topLeft.Y);
									px.Offset(r.Offset.X, r.Offset.Y);

									gfx.ResetTransform();
									gfx.TranslateTransform(-r.LocalPosition.X, -r.LocalPosition.Y);
									gfx.TranslateTransform((int)px.X, (int)px.Y);

									r.OnRender(gfx);
								}
							}

							// tooltips above
							//foreach (var m in Main.Objects.Markers)
							//{
							//  if (m.IsVisible && m.ToolTip != null && m.IsVisible)
							//  {
							//    if (!string.IsNullOrEmpty(m.ToolTipText))
							//    {
							//      var pr = m.Position;
							//      var px = info.Type.Projection.FromLatLngToPixel(pr.Lat, pr.Lng, info.Zoom);

							//      px.Offset(padding, padding);
							//      px.Offset(-topLeftPx.X, -topLeftPx.Y);
							//      px.Offset(m.Offset.X, m.Offset.Y);

							//      gfx.ResetTransform();
							//      gfx.TranslateTransform(-m.LocalPosition.X, -m.LocalPosition.Y);
							//      gfx.TranslateTransform((int)px.X, (int)px.Y);

							//      m.ToolTip.OnRender(gfx);
							//    }
							//  }
							}
							gfx.ResetTransform();
					}

					//bmpDestination.Save(sFilePath, ImageFormat.Jpeg);
					return bmpDestination; // todo return bitmap on do not dispose it
				}
			}
		}
	}
}
