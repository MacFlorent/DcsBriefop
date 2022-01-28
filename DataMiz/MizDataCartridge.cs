//using DcsBriefop.Tools;
//using LsonLib;
//using System.Collections.Generic;
//using System.Linq;

//namespace DcsBriefop.DataMiz
//{
//	internal class MizDataCartridge : BaseMiz
//	{
//		private class LuaNode
//		{
//			public static readonly string GroupsPoints = "GroupsPoints";
//			public static readonly string AaWaypoint = "A/A Waypoint";
//		}

//		public int? AaWaypoint { get; set; }

//		public MizDataCartridge(LsonDict lsd) : base(lsd) { }
		
//		public override void FromLua()
//		{
//			LsonDict lsdGroupsPoints = Lsd[LuaNode.GroupsPoints].GetDict();
//			AaWaypoint = ToolsLson.IfExistsInt (lsdGroupsPoints, LuaNode.AaWaypoint);
//		}

//		public override void ToLua()
//		{
//			LsonDict lsdGroupsPoints = Lsd[LuaNode.GroupsPoints].GetDict();
//			if (AaWaypoint.HasValue)
//				lsdGroupsPoints
//		}
//	}
//}
