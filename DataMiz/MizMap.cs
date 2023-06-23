using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizMap : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string CenterY = "centerY";
			public static readonly string CenterX = "centerX";
			public static readonly string Zoom = "zoom";
		}

		public double CenterY { get; set; }
		public double CenterX { get; set; }
		public double Zoom { get; set; }

		public MizMap(LsonDict lsd) : base(lsd) { }
		
		public override void FromLua()
		{
			CenterY = Lsd[LuaNode.CenterY].GetDouble();
			CenterX = Lsd[LuaNode.CenterX].GetDouble();
			Zoom = Lsd[LuaNode.Zoom].GetDouble();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.CenterY] = CenterY;
			Lsd[LuaNode.CenterX] = CenterX;
			Lsd[LuaNode.Zoom] = Zoom;
		}
	}
}
