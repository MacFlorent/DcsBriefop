﻿using DcsBriefop.Tools;
using System;

namespace DcsBriefop.Data
{
	internal class MissionContent : BaseBriefing
	{
		#region Properties
		public string Sortie { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }

		public Weather Weather { get; private set; }
		#endregion

		#region CTOR
		public MissionContent(BaseBriefingCore core) : base(core)
		{
			Initialize();
		}
		#endregion

		#region Initialize
		private void Initialize()
		{
			Weather = new Weather(Core.Miz.RootMission.Weather);
			Sortie = Core.Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay (Core.Miz.RootDictionary.Description);
			Date = new DateTime(Core.Miz.RootMission.Date.Year, Core.Miz.RootMission.Date.Month, Core.Miz.RootMission.Date.Day).AddSeconds(Core.Miz.RootMission.StartTime);
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			Core.Miz.RootDictionary.Sortie = Sortie;
			Core.Miz.RootDictionary.Description = ToolsLua.DisplayToDcsText(Description);
			Core.Miz.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
			Core.Miz.RootMission.StartTime = Convert.ToInt32((Date - Core.Miz.RootMission.Date).TotalSeconds);
		}
		#endregion
	}
}
