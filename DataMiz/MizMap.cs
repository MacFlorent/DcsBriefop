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
			CenterY = m_lsd[LuaNode.CenterY].GetDecimal();
			CenterX = m_lsd[LuaNode.CenterX].GetDecimal();
			Zoom = m_lsd[LuaNode.Zoom].GetDecimal();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.CenterY] = CenterY;
			m_lsd[LuaNode.CenterX] = CenterX;
			m_lsd[LuaNode.Zoom] = Zoom;
		}
	}
}
