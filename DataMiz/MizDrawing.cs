using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;
using System.Linq;

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
			Visible = m_lsd[LuaNode.Visible].GetBool();
			Name = m_lsd[LuaNode.Name].GetString();

			LsonDict lsdObjects = m_lsd[LuaNode.Objects].GetDict();
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

			public static readonly string Points = "points";
			public static readonly string Closed = "closed";
			public static readonly string Thickness = "thickness";
			public static readonly string Style = "style";
			public static readonly string LineMode = "lineMode";
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

		public bool? Closed { get; set; }
		public int? Thickness { get; set; }
		public string Style { get; set; }
		public string LineMode { get; set; }

		public List<MizDrawingPoint> Points { get; private set; } = new List<MizDrawingPoint>();

		public MizDrawingObject(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Visible = m_lsd[LuaNode.Visible].GetBool();
			Name = m_lsd[LuaNode.Name].GetString();
			PrimitiveType = m_lsd[LuaNode.PrimitiveType].GetString();
			MapY = m_lsd[LuaNode.MapY].GetDecimal();
			MapX = m_lsd[LuaNode.MapX].GetDecimal();
			ColorString = m_lsd[LuaNode.ColorString].GetString();

			Angle = ToolsLson.IfExistsInt(m_lsd, LuaNode.Angle);
			File = ToolsLson.IfExistsString(m_lsd, LuaNode.File);
			Scale = ToolsLson.IfExistsInt(m_lsd, LuaNode.Scale);
			Text = ToolsLson.IfExistsString(m_lsd, LuaNode.Text);
			FillColorString = ToolsLson.IfExistsString(m_lsd, LuaNode.FillColorString);
			Font = ToolsLson.IfExistsString(m_lsd, LuaNode.Font);
			FontSize = ToolsLson.IfExistsInt(m_lsd, LuaNode.FontSize);

			Closed = ToolsLson.IfExistsBool (m_lsd, LuaNode.Closed);
			Thickness = ToolsLson.IfExistsInt(m_lsd, LuaNode.Thickness);
			Style = ToolsLson.IfExistsString(m_lsd, LuaNode.Style);
			LineMode = ToolsLson.IfExistsString(m_lsd, LuaNode.LineMode);

			if (m_lsd.ContainsKey(LuaNode.Points))
			{
				LsonDict lsdPoints = m_lsd[LuaNode.Points].GetDict();
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
			Y = m_lsd[LuaNode.Y].GetDecimal();
			X = m_lsd[LuaNode.X].GetDecimal();
		}

		public override void ToLua() { }
	}
}
