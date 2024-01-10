using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using Newtonsoft.Json;

namespace DcsBriefop.Tools
{
	internal class LotatcDrawingsFile
	{
		public bool enable;
		public string version;
		public List<LotatcDrawingsAuthor> drawings;
	}

		internal class LotatcDrawingsAuthor
	{
		public string author;
		public List<LotatcDrawing> drawings;
	}

	internal class LotatcDrawing
	{
		public string author;
		public int brushStyle;
		public string color;
		public string colorBg;
		public Guid id;
		public int lineWidth;
		public string name;
		public double radius;
		public string text;
		public bool shared;
		public string timestamp;
		public string type;
		public bool visible;
		public double latitude;
		public double longitude;
		public LotatcDrawingFont font;
		public List<LotatcDrawingPoint> points;
	}

	internal class LotatcDrawingFont
	{
		public string color;
		public string font;
	}

	internal class LotatcDrawingPoint
	{
		public double latitude;
		public double longitude;
	}

	internal static class ToolsLotatc
	{
		private static string HtmlToDcsColor(string sHtmlColor)
		{
			return $"0x{sHtmlColor.Substring(3, 6)}{sHtmlColor.Substring(1, 2)}";
		}

		public static void DrawingsJsonToMiz(string sJson, BriefopManager briefopManager)
		{
			LotatcDrawingsFile lotatcDrawingsFile = JsonConvert.DeserializeObject<LotatcDrawingsFile>(sJson);

			MizDrawingLayer mizDrawingLayerCommon = briefopManager.BopMission.Miz.RootMission.DrawingLayers.Where(_l => _l.Name == ElementDrawingLayer.Common).FirstOrDefault();
			if (mizDrawingLayerCommon is null)
				throw new ExceptionBop("No common drawing layer in mission. The layer must exist in the mission to allow for external drawings import.");

			mizDrawingLayerCommon.Objects.RemoveAll(_o => _o.Name.StartsWith("lotatc_"));

			foreach (LotatcDrawingsAuthor lotatcDrawingsAuthor in lotatcDrawingsFile.drawings)
			{
				foreach (LotatcDrawing lotatcDrawing in lotatcDrawingsAuthor.drawings)
				{
					MizDrawingObject mizDrawing = MizDrawingObject.NewFromLuaTemplate();
					mizDrawingLayerCommon.Objects.Add(mizDrawing);
					mizDrawing.Name = $"lotatc_{lotatcDrawing.name}";
					mizDrawing.LayerName = ElementDrawingLayer.Common;
					mizDrawing.Visible = lotatcDrawing.visible;

					if (lotatcDrawing.type == "polygon")
					{
						FillMizDrawingPolygon(lotatcDrawing, mizDrawing, briefopManager.BopMission.Theatre);
					}
					else if (lotatcDrawing.type == "circle")
					{
						FillMizDrawingCircle(lotatcDrawing, mizDrawing, briefopManager.BopMission.Theatre);
					}
					else if (lotatcDrawing.type == "text")
					{
						FillMizDrawingText(lotatcDrawing, mizDrawing, briefopManager.BopMission.Theatre);
					}
				}
			}
		}

		private static void FillMizDrawingPolygon(LotatcDrawing lotatcDrawing, MizDrawingObject mizDrawing, Theatre theatre)
		{
			mizDrawing.PrimitiveType = ElementDrawingPrimitive.Polygon;
			mizDrawing.PolygonMode = ElementDrawingPolygonMode.Free;
			mizDrawing.Style = "solid";
			mizDrawing.Thickness = lotatcDrawing.lineWidth * 2;

			mizDrawing.ColorString = HtmlToDcsColor(lotatcDrawing.color);
			mizDrawing.FillColorString = HtmlToDcsColor(lotatcDrawing.colorBg);

			bool bFirst = true;
			foreach (LotatcDrawingPoint lotatcPoint in lotatcDrawing.points)
			{
				MizDrawingPoint mizDrawingPoint = MizDrawingPoint.NewFromLuaTemplate();
				mizDrawing.Points.Add(mizDrawingPoint);

				CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate(lotatcPoint.latitude, lotatcPoint.longitude);
				theatre.GetDcsZX(out double dZ, out double dX, c);

				if (bFirst)
				{
					mizDrawing.MapY = dZ;
					mizDrawing.MapX = dX;
					bFirst = false;
				}

				mizDrawingPoint.Y = dZ - mizDrawing.MapY;
				mizDrawingPoint.X = dX - mizDrawing.MapX;
			}

			MizDrawingPoint mizDrawingPointClose = MizDrawingPoint.NewFromLuaTemplate();
			mizDrawingPointClose.Y = mizDrawingPointClose.X = 0;
			mizDrawing.Points.Add(mizDrawingPointClose);
		}

		private static void FillMizDrawingCircle(LotatcDrawing lotatcDrawing, MizDrawingObject mizDrawing, Theatre theatre)
		{
			mizDrawing.PrimitiveType = ElementDrawingPrimitive.Polygon;
			mizDrawing.PolygonMode = ElementDrawingPolygonMode.Circle;
			mizDrawing.Style = "solid";
			mizDrawing.Thickness = lotatcDrawing.lineWidth * 2;
			mizDrawing.Radius = lotatcDrawing.radius;

			mizDrawing.ColorString = HtmlToDcsColor(lotatcDrawing.color);
			mizDrawing.FillColorString = HtmlToDcsColor(lotatcDrawing.colorBg);

			CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate(lotatcDrawing.latitude, lotatcDrawing.longitude);
			theatre.GetDcsZX(out double dZ, out double dX, c);
			mizDrawing.MapY = dZ;
			mizDrawing.MapX = dX;
		}

		private static void FillMizDrawingText(LotatcDrawing lotatcDrawing, MizDrawingObject mizDrawing, Theatre theatre)
		{
			mizDrawing.PrimitiveType = ElementDrawingPrimitive.TextBox;
			mizDrawing.Style = "solid";
			mizDrawing.Text = lotatcDrawing.text;
			mizDrawing.Thickness = lotatcDrawing.lineWidth;

			mizDrawing.ColorString = HtmlToDcsColor(lotatcDrawing.font.color);
			mizDrawing.FillColorString = HtmlToDcsColor(lotatcDrawing.colorBg);
			mizDrawing.Font = "DejaVuLGCSansCondensed.ttf";//lotatcDrawing.font.font;
			mizDrawing.FontSize = 16;

			CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate(lotatcDrawing.latitude, lotatcDrawing.longitude);
			theatre.GetDcsZX(out double dZ, out double dX, c);
			mizDrawing.MapY = dZ;
			mizDrawing.MapX = dX;
		}
	}
}
