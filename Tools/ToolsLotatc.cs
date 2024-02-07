using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using Newtonsoft.Json;

namespace DcsBriefop.Tools
{
	internal class LotatcDrawingFile
	{
		public bool enable;
		public string version;
		public List<LotatcDrawingLayer> drawings;
	}

	internal class LotatcDrawingLayer
	{
		public string author;
		public bool enable;
		public string name;
		public bool shared;
		public string version;
		public string type;
		public bool visible;
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

		public static void DrawingsFileJsonToMiz(string sJson, BriefopManager briefopManager)
		{
			MizDrawingLayer mizDrawingLayerCommon = briefopManager.BopMission.Miz.RootMission.DrawingLayers.Where(_l => _l.Name == ElementDrawingLayer.Common).FirstOrDefault();
			if (mizDrawingLayerCommon is null)
				throw new ExceptionBop("No common drawing layer in mission. The layer must exist in the mission to allow for external drawings import.");
			
			mizDrawingLayerCommon.Objects.RemoveAll(_o => _o.Name.StartsWith("lotatc_"));

			LotatcDrawingLayer lotatcDrawingLayer = JsonConvert.DeserializeObject<LotatcDrawingLayer>(sJson);
			if ("layer".Equals(lotatcDrawingLayer.type))
			{
				DrawingsLayerToMiz(lotatcDrawingLayer, briefopManager, mizDrawingLayerCommon);
			}
			else
			{ 
				LotatcDrawingFile lotatcDrawingFile = JsonConvert.DeserializeObject<LotatcDrawingFile>(sJson);
				if (lotatcDrawingFile.drawings is null)
					throw new ExceptionBop("Cannot parse input file, please check that it is a correctly formatted lotac drawings file.");

				foreach (LotatcDrawingLayer layer in lotatcDrawingFile.drawings)
				{
					DrawingsLayerToMiz(layer, briefopManager, mizDrawingLayerCommon);
				}
			}
		}

		public static void DrawingsLayerToMiz(LotatcDrawingLayer lotatcDrawingsAuthor, BriefopManager briefopManager, MizDrawingLayer mizDrawingLayer)
		{
			foreach (LotatcDrawing lotatcDrawing in lotatcDrawingsAuthor.drawings)
			{
				MizDrawingObject mizDrawing = MizDrawingObject.NewFromLuaTemplate();
				mizDrawingLayer.Objects.Add(mizDrawing);
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
				theatre.GetDcsXYNew(out double dX, out double dY, c);

				if (bFirst)
				{
					mizDrawing.MapY = dY;
					mizDrawing.MapX = dX;
					bFirst = false;
				}

				mizDrawingPoint.Y = dY - mizDrawing.MapY;
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
			theatre.GetDcsXYNew(out double dX, out double dY, c);
			mizDrawing.MapY = dY;
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
			theatre.GetDcsXYNew(out double dX, out double dY, c);
			mizDrawing.MapY = dY;
			mizDrawing.MapX = dX;
		}
	}
}
