using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizCallsign : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
		}

		public int Id { get; set; }
		public int Flight { get; set; }
		public int Element { get; set; }
		public string Name { get; set; }

		public MizCallsign(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = Lsd[1].GetInt();
			Flight = Lsd[2].GetInt();
			Element = Lsd[3].GetInt();
			Name = Lsd[LuaNode.Name].GetString();
		}

		public override void ToLua()
		{
			Lsd[1] = Id;
			Lsd[2] = Flight;
			Lsd[3] = Element;
			Lsd[LuaNode.Name] = Name;
		}
	}
}
