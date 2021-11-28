using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizInitialPoint : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "id";
			public static readonly string Callsign = "callsignStr";
			public static readonly string Y = "y";
			public static readonly string X = "x";
			public static readonly string Comment = "comment";
		}

		public int Id { get; set; }
		public string Callsign { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }
		public string Comment { get; set; }

		public MizInitialPoint(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetInt();
			Callsign = m_lsd[LuaNode.Callsign].GetString();
			Y = m_lsd[LuaNode.Y].GetDecimal();
			X = m_lsd[LuaNode.X].GetDecimal();
			Comment = m_lsd[LuaNode.Comment].GetString();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Callsign] = Callsign;
			m_lsd[LuaNode.Y] = Y;
			m_lsd[LuaNode.X] = X;
			m_lsd[LuaNode.Comment] = Comment;
		}
	}
}
