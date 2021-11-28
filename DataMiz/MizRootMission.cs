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
		}

		public Dictionary<string, LsonValue> RootLua { get; private set; }

		public DateTime Date { get; set; } // Date is in local timezone
		public int StartTime { get; set; } // Seconds to add to the mission date
		public string Theatre { get; set; }
		public MizMap Map { get; set; }
		public MizWeather Weather { get; set; }
		public List<MizCoalition> Coalitions { get; set; } = new List<MizCoalition>();

		public MizRootMission(LsonDict lsd) : base(lsd) { }

		public MizRootMission(Dictionary<string, LsonValue> rootLua) : base(rootLua["mission"].GetDict())
		{
			RootLua = rootLua;
		}

		public override void FromLua()
		{
			Date = new DateTime(m_lsd[LuaNode.Date][LuaNode.Year].GetInt(), m_lsd[LuaNode.Date][LuaNode.Month].GetInt(), m_lsd[LuaNode.Date][LuaNode.Day].GetInt());
			StartTime = m_lsd[LuaNode.StartTime].GetInt();
			Theatre = m_lsd[LuaNode.Theater].GetString();
			Map = new MizMap(m_lsd[LuaNode.Map].GetDict());
			Weather = new MizWeather(m_lsd[LuaNode.Weather].GetDict());

			LsonDict lsdCoalitions = m_lsd[LuaNode.Coalition].GetDict();
			foreach (LsonValue lsv in lsdCoalitions.Values)
			{
				Coalitions.Add(new MizCoalition(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Date][LuaNode.Year] = Date.Year;
			m_lsd[LuaNode.Date][LuaNode.Month] = Date.Month;
			m_lsd[LuaNode.Date][LuaNode.Day] = Date.Day;
			m_lsd[LuaNode.StartTime] = StartTime;
			m_lsd[LuaNode.Theater] = Theatre;
			Map.ToLua();
			Weather.ToLua();

			foreach (MizCoalition c in Coalitions)
			{
				c.ToLua();
			}
		}
	}
}
