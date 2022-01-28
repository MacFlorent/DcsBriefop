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

		public decimal CenterY { get; set; }
		public decimal CenterX { get; set; }
		public decimal Zoom { get; set; }

		public MizMap(LsonDict lsd) : base(lsd) { }
		
		public override void FromLua()
		{
			CenterY = Lsd[LuaNode.CenterY].GetDecimal();
			CenterX = Lsd[LuaNode.CenterX].GetDecimal();
			Zoom = Lsd[LuaNode.Zoom].GetDecimal();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.CenterY] = CenterY;
			Lsd[LuaNode.CenterX] = CenterX;
			Lsd[LuaNode.Zoom] = Zoom;
		}
	}
}
