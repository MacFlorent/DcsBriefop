using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizDrawingLayer : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Visible = "visible";
			public static readonly string Name = "name";
			public static readonly string Objects = "objects";
		}

		public bool Visible { get; set; }
		public string Name { get; set; }
		public List<MizDrawingObject> Objects { get; private set; } = new List<MizDrawingObject>();

		public MizDrawingLayer(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Visible = Lsd[LuaNode.Visible].GetBool();
			Name = Lsd[LuaNode.Name].GetString();

			LsonDict lsdObjects = Lsd[LuaNode.Objects].GetDict();
			foreach (LsonValue lsv in lsdObjects.Values)
			{
				Objects.Add(new MizDrawingObject(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Visible] = Visible;
			Lsd[LuaNode.Name] = Name;

			if (Lsd.ContainsKey(LuaNode.Objects))
			{
				LsonDict lsdObjects = Lsd[LuaNode.Objects].GetDict();
				lsdObjects.Clear();

				int i = 1;
				foreach (MizDrawingObject o in Objects)
				{
					o.ToLua();
					lsdObjects.Add(i, o.Lsd);
					i++;
				}
			}
		}
	}

	internal class MizDrawingObject : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Visible = "visible";
			public static readonly string Name = "name";
			public static readonly string LayerName = "layerName";
			public static readonly string PrimitiveType = "primitiveType";
			public static readonly string MapY = "mapY";
			public static readonly string MapX = "mapX";
			public static readonly string ColorString = "colorString";

			public static readonly string Angle = "angle";
			public static readonly string File = "file";
			public static readonly string Scale = "scale";
			public static readonly string Text = "text";
			public static readonly string FillColorString = "fillColorString";
			public static readonly string Font = "font";
			public static readonly string FontSize = "fontSize";
			public static readonly string BorderThickness = "borderThickness";

			public static readonly string Points = "points";
			public static readonly string Closed = "closed";
			public static readonly string Thickness = "thickness";
			public static readonly string Style = "style";
			public static readonly string LineMode = "lineMode";

			public static readonly string PolygonMode = "polygonMode";
			public static readonly string Height = "height";
			public static readonly string Width = "width";
			public static readonly string R1 = "r1";
			public static readonly string R2 = "r2";
			public static readonly string Radius = "radius";

		}

		public bool Visible { get; set; }
		public string Name { get; set; }
		public string LayerName { get; set; }
		public string PrimitiveType { get; set; }
		public double MapY { get; set; }
		public double MapX { get; set; }
		public string ColorString { get; set; }

		public int? Angle { get; set; }
		public string File { get; set; }
		public int? Scale { get; set; }
		public string Text { get; set; }
		public string FillColorString { get; set; }
		public string Font { get; set; }
		public int? FontSize { get; set; }
		public int? BorderThickness { get; set; }

		public bool? Closed { get; set; }
		public int? Thickness { get; set; }
		public string Style { get; set; }
		public string LineMode { get; set; }

		public string PolygonMode { get; set; }
		public double? Height { get; set; }
		public double? Width { get; set; }
		public double? R1 { get; set; }
		public double? R2 { get; set; }
		public double? Radius { get; set; }

		public List<MizDrawingPoint> Points { get; private set; } = new List<MizDrawingPoint>();

		public MizDrawingObject(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Visible = Lsd[LuaNode.Visible].GetBool();
			Name = Lsd[LuaNode.Name].GetString();
			LayerName = Lsd[LuaNode.LayerName].GetString();
			PrimitiveType = Lsd[LuaNode.PrimitiveType].GetString();
			MapY = Lsd[LuaNode.MapY].GetDouble();
			MapX = Lsd[LuaNode.MapX].GetDouble();
			ColorString = Lsd[LuaNode.ColorString].GetString();

			Angle = ToolsLson.IfExistsInt(Lsd, LuaNode.Angle);
			File = ToolsLson.IfExistsString(Lsd, LuaNode.File);
			Scale = ToolsLson.IfExistsInt(Lsd, LuaNode.Scale);
			Text = ToolsLson.IfExistsString(Lsd, LuaNode.Text);
			FillColorString = ToolsLson.IfExistsString(Lsd, LuaNode.FillColorString);
			Font = ToolsLson.IfExistsString(Lsd, LuaNode.Font);
			FontSize = ToolsLson.IfExistsInt(Lsd, LuaNode.FontSize);
			BorderThickness = ToolsLson.IfExistsInt(Lsd, LuaNode.BorderThickness);

			Closed = ToolsLson.IfExistsBool(Lsd, LuaNode.Closed);
			Thickness = ToolsLson.IfExistsInt(Lsd, LuaNode.Thickness);
			Style = ToolsLson.IfExistsString(Lsd, LuaNode.Style);
			LineMode = ToolsLson.IfExistsString(Lsd, LuaNode.LineMode);

			PolygonMode = ToolsLson.IfExistsString(Lsd, LuaNode.PolygonMode);
			Height = ToolsLson.IfExistsDouble(Lsd, LuaNode.Height);
			Width = ToolsLson.IfExistsDouble(Lsd, LuaNode.Width);
			R1 = ToolsLson.IfExistsDouble(Lsd, LuaNode.R1);
			R2 = ToolsLson.IfExistsDouble(Lsd, LuaNode.R2);
			Radius = ToolsLson.IfExistsDouble(Lsd, LuaNode.Radius);

			if (Lsd.ContainsKey(LuaNode.Points))
			{
				LsonDict lsdPoints = Lsd[LuaNode.Points].GetDict();
				foreach (LsonValue lsv in ToolsLson.GetOrderedValueList(lsdPoints))
				{
					Points.Add(new MizDrawingPoint(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Visible] = Visible;
			Lsd[LuaNode.Name] = Name;
			Lsd[LuaNode.LayerName] = LayerName;
			Lsd[LuaNode.PrimitiveType] = PrimitiveType;
			Lsd[LuaNode.MapY] = MapY;
			Lsd[LuaNode.MapX] = MapX;
			Lsd[LuaNode.ColorString] = ColorString;

			Lsd.SetIfExists(LuaNode.Angle, Angle);
			Lsd.SetIfExists(LuaNode.File, File);
			Lsd.SetIfExists(LuaNode.Scale, Scale);
			Lsd.SetIfExists(LuaNode.Text, Text);
			Lsd.SetIfExists(LuaNode.FillColorString, FillColorString);
			Lsd.SetIfExists(LuaNode.Font, Font);
			Lsd.SetIfExists(LuaNode.FontSize, FontSize);
			Lsd.SetIfExists(LuaNode.BorderThickness, BorderThickness);

			Lsd.SetIfExists(LuaNode.Closed, Closed);
			Lsd.SetIfExists(LuaNode.Thickness, Thickness);
			Lsd.SetIfExists(LuaNode.Style, Style);
			Lsd.SetIfExists(LuaNode.LineMode, LineMode);

			Lsd.SetIfExists(LuaNode.PolygonMode, PolygonMode);
			Lsd.SetIfExists(LuaNode.Height, Height);
			Lsd.SetIfExists(LuaNode.Width, Width);
			Lsd.SetIfExists(LuaNode.R1, R1);
			Lsd.SetIfExists(LuaNode.R2, R2);
			Lsd.SetIfExists(LuaNode.Radius, Radius);

			if (Lsd.ContainsKey(LuaNode.Points))
			{
				LsonDict lsdPoints = Lsd[LuaNode.Points].GetDict();
				lsdPoints.Clear();

				int i = 1;
				foreach (MizDrawingPoint dp in Points)
				{
					dp.ToLua();
					lsdPoints.Add(i, dp.Lsd);
					i++;
				}
			}

		}

		public static string GetLuaTemplate()
		{
			return ToolsResources.GetTextResourceContent("MizDrawing", "lua", null);
		}

		public static MizDrawingObject NewFromLuaTemplate()
		{
			string sLuaTemplate = GetLuaTemplate();
			Dictionary<string, LsonValue> l = LsonVars.Parse(sLuaTemplate);
			MizDrawingObject mizDrawingObject = new MizDrawingObject(l["template"].GetDict());

			mizDrawingObject.Visible = true;
			mizDrawingObject.Name = null;
			mizDrawingObject.PrimitiveType = null;
			mizDrawingObject.MapY = 0;
			mizDrawingObject.ColorString = null;
			mizDrawingObject.Angle = 0;
			mizDrawingObject.File = null;
			mizDrawingObject.Scale = 0;
			mizDrawingObject.Text = null;
			mizDrawingObject.FillColorString = null;
			mizDrawingObject.Font = null;
			mizDrawingObject.FontSize = null;
			mizDrawingObject.BorderThickness = null;
			mizDrawingObject.Closed = null;
			mizDrawingObject.Thickness = null;
			mizDrawingObject.Style = null;
			mizDrawingObject.LineMode = null;
			mizDrawingObject.PolygonMode = null;
			mizDrawingObject.Height = null;
			mizDrawingObject.Width = null;
			mizDrawingObject.R1 = null;
			mizDrawingObject.R2 = null;
			mizDrawingObject.Radius = null;

			mizDrawingObject.Points.Clear();

			return mizDrawingObject;
		}
	}

	internal class MizDrawingPoint : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Y = "y";
			public static readonly string X = "x";
		}

		public double Y { get; set; }
		public double X { get; set; }

		public MizDrawingPoint(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Y = Lsd[LuaNode.Y].GetDouble();
			X = Lsd[LuaNode.X].GetDouble();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Y] = Y;
			Lsd[LuaNode.X] = X;
		}

		public static string GetLuaTemplate()
		{
			return ToolsResources.GetTextResourceContent("MizDrawing", "lua", null);
		}

		public static MizDrawingPoint NewFromLuaTemplate()
		{
			string sLuaTemplate = GetLuaTemplate();
			Dictionary<string, LsonValue> l = LsonVars.Parse(sLuaTemplate);
			return new MizDrawingPoint(l["template"].GetDict()["points"].GetDict()[1].GetDict());
		}
	}
}
