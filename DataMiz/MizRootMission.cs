using LsonLib;
using System;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizRootMission : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Date = "date";
			public static readonly string Year = "Year";
			public static readonly string Month = "Month";
			public static readonly string Day = "Day";
			public static readonly string StartTime = "start_time";
			public static readonly string Theater = "theatre";
			public static readonly string Map = "map";
			public static readonly string Weather = "weather";
			public static readonly string Coalition = "coalition";
			public static readonly string Drawings = "drawings";
			public static readonly string Layers = "layers";
		}

		public Dictionary<string, LsonValue> RootLua { get; private set; }

		public DateTime Date { get; set; } // Date is in local timezone
		public int StartTime { get; set; } // Seconds to add to the mission date
		public string Theatre { get; set; }
		public MizMap Map { get; set; }
		public MizWeather Weather { get; set; }
		public List<MizCoalition> Coalitions { get; set; } = new List<MizCoalition>();
		public List<MizDrawingLayer> DrawingLayers { get; set; } = new List<MizDrawingLayer>();

		public MizRootMission(LsonDict lsd) : base(lsd) { }

		public MizRootMission(Dictionary<string, LsonValue> rootLua) : base(rootLua["mission"].GetDict())
		{
			RootLua = rootLua;
		}

		public override void FromLua()
		{
			Date = new DateTime(Lsd[LuaNode.Date][LuaNode.Year].GetInt(), Lsd[LuaNode.Date][LuaNode.Month].GetInt(), Lsd[LuaNode.Date][LuaNode.Day].GetInt());
			StartTime = Lsd[LuaNode.StartTime].GetInt();
			Theatre = Lsd[LuaNode.Theater].GetString();
			Map = new MizMap(Lsd[LuaNode.Map].GetDict());
			Weather = new MizWeather(Lsd[LuaNode.Weather].GetDict());

			LsonDict lsdCoalitions = Lsd[LuaNode.Coalition].GetDict();
			foreach (LsonValue lsv in lsdCoalitions.Values)
			{
				Coalitions.Add(new MizCoalition(lsv.GetDict()));
			}

			LsonDict lsdDrawings = Lsd[LuaNode.Drawings][LuaNode.Layers].GetDict();
			foreach (LsonValue lsv in lsdDrawings.Values)
			{
				DrawingLayers.Add(new MizDrawingLayer(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Date][LuaNode.Year] = Date.Year;
			Lsd[LuaNode.Date][LuaNode.Month] = Date.Month;
			Lsd[LuaNode.Date][LuaNode.Day] = Date.Day;
			Lsd[LuaNode.StartTime] = StartTime;
			Lsd[LuaNode.Theater] = Theatre;
			Map.ToLua();
			Weather.ToLua();

			foreach (MizCoalition c in Coalitions)
			{
				c.ToLua();
			}
		}
	}
}
