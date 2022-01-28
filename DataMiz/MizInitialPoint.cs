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
			Id = Lsd[LuaNode.Id].GetInt();
			Callsign = Lsd[LuaNode.Callsign].GetString();
			Y = Lsd[LuaNode.Y].GetDecimal();
			X = Lsd[LuaNode.X].GetDecimal();
			Comment = Lsd[LuaNode.Comment].GetString();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;
			Lsd[LuaNode.Callsign] = Callsign;
			Lsd[LuaNode.Y] = Y;
			Lsd[LuaNode.X] = X;
			Lsd[LuaNode.Comment] = Comment;
		}
	}
}
