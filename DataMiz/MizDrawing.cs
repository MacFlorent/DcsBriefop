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

		public override void ToLua() {}
	}

	internal class MizDrawingObject : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Visible = "visible";
			public static readonly string Name = "name";
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

		}

		public bool Visible { get; set; }
		public string Name { get; set; }
		public string PrimitiveType { get; set; }
		public decimal MapY { get; set; }
		public decimal MapX { get; set; }
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
		public decimal? Height { get; set; }
		public decimal? Width { get; set; }
		public decimal? R1 { get; set; }
		public decimal? R2 { get; set; }

		public List<MizDrawingPoint> Points { get; private set; } = new List<MizDrawingPoint>();

		public MizDrawingObject(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Visible = Lsd[LuaNode.Visible].GetBool();
			Name = Lsd[LuaNode.Name].GetString();
			PrimitiveType = Lsd[LuaNode.PrimitiveType].GetString();
			MapY = Lsd[LuaNode.MapY].GetDecimal();
			MapX = Lsd[LuaNode.MapX].GetDecimal();
			ColorString = Lsd[LuaNode.ColorString].GetString();

			Angle = ToolsLson.IfExistsInt(Lsd, LuaNode.Angle);
			File = ToolsLson.IfExistsString(Lsd, LuaNode.File);
			Scale = ToolsLson.IfExistsInt(Lsd, LuaNode.Scale);
			Text = ToolsLson.IfExistsString(Lsd, LuaNode.Text);
			FillColorString = ToolsLson.IfExistsString(Lsd, LuaNode.FillColorString);
			Font = ToolsLson.IfExistsString(Lsd, LuaNode.Font);
			FontSize = ToolsLson.IfExistsInt(Lsd, LuaNode.FontSize);
			BorderThickness = ToolsLson.IfExistsInt(Lsd, LuaNode.BorderThickness);

			Closed = ToolsLson.IfExistsBool (Lsd, LuaNode.Closed);
			Thickness = ToolsLson.IfExistsInt(Lsd, LuaNode.Thickness);
			Style = ToolsLson.IfExistsString(Lsd, LuaNode.Style);
			LineMode = ToolsLson.IfExistsString(Lsd, LuaNode.LineMode);

			PolygonMode = ToolsLson.IfExistsString(Lsd, LuaNode.PolygonMode);
			Height = ToolsLson.IfExistsDecimal(Lsd, LuaNode.Height);
			Width = ToolsLson.IfExistsDecimal(Lsd, LuaNode.Width);
			R1 = ToolsLson.IfExistsDecimal(Lsd, LuaNode.R1);
			R2 = ToolsLson.IfExistsDecimal(Lsd, LuaNode.R2);

			if (Lsd.ContainsKey(LuaNode.Points))
			{
				LsonDict lsdPoints = Lsd[LuaNode.Points].GetDict();
				foreach (LsonValue lsv in ToolsLson.GetOrderedValueList(lsdPoints))
				{
					Points.Add(new MizDrawingPoint(lsv.GetDict()));
				}
			}
		}

		public override void ToLua() { }
	}

	internal class MizDrawingPoint : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Y = "y";
			public static readonly string X = "x";
		}

		public decimal Y { get; set; }
		public decimal X { get; set; }

		public MizDrawingPoint(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Y = Lsd[LuaNode.Y].GetDecimal();
			X = Lsd[LuaNode.X].GetDecimal();
		}

		public override void ToLua() { }
	}
}
