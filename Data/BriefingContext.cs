using DcsBriefop.DataMiz;
using System;

namespace DcsBriefop.Data
{
	internal class BriefingContext
	{
		#region Properties
		public Miz Miz { get; private set; }

		public string Sortie { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }

		public Theatre Theatre { get; private set; }
		public Weather Weather { get; private set; }
		#endregion

		#region CTOR
		public BriefingContext(Miz miz)
		{
			Miz = miz;

			Theatre = new Theatre(Miz.RootMission.Theatre);
			Weather = new Weather(Miz.RootMission.Weather);

			Sortie = Miz.RootDictionary.Sortie;
			Description = Miz.RootDictionary.Description;
			Date = new DateTime(Miz.RootMission.Date.Year, Miz.RootMission.Date.Month, Miz.RootMission.Date.Day).AddSeconds(Miz.RootMission.StartTime);
		}
		#endregion

		#region Methods
		public void Persist()
		{
			Miz.RootDictionary.Sortie = Sortie;
			Miz.RootDictionary.Description = Description;
			Miz.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
			Miz.RootMission.StartTime = Convert.ToInt32((Date - Miz.RootMission.Date).TotalSeconds);
		}
		#endregion
	}
}
