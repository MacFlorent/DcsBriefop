using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopMission : BaseBop
	{
		#region Fields
		#endregion

		#region Properties
		public string Sortie { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public MizBopMap MapData { get { return Miz.MizBopCustom.MapData; } }
		public BopWeather Weather { get; private set; }
		public Dictionary<string, BopCoalition> Coalitions { get; private set; }
		//public List<BopAsset> Assets { get; private set; }
		//public List<BopAirdrome> Airdromes { get; private set; }

		public PreferencesMission PreferencesMission { get { return Miz.MizBopCustom.PreferencesMission; } }
		public PreferencesMap PreferencesMap { get { return Miz.MizBopCustom.PreferencesMap; } }
		#endregion

		#region CTOR
		public BopMission(Miz miz, Theatre theatre) : base(miz, theatre)
		{
			Weather = new BopWeather(Miz, Theatre, Date);

			Coalitions = new Dictionary<string, BopCoalition>
			{
				{ ElementCoalition.Red, new BopCoalition(Miz, Theatre, ElementCoalition.Red) },
				{ ElementCoalition.Blue, new BopCoalition(Miz, Theatre, ElementCoalition.Blue) },
				{ ElementCoalition.Neutral, new BopCoalition(Miz, Theatre, ElementCoalition.Neutral) }
			};
		}
		#endregion

		#region Miz
		public override void FromMiz()
		{
			base.FromMiz();

			Sortie = Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.Description);
			Date = new DateTime(Miz.RootMission.Date.Year, Miz.RootMission.Date.Month, Miz.RootMission.Date.Day).AddSeconds(Miz.RootMission.StartTime);

			Weather.FromMiz();

			foreach (BopCoalition coalition in Coalitions.Values)
			{
				coalition.FromMiz();
			}
		}

		public override void ToMiz()
		{
			base.ToMiz();

			Miz.RootDictionary.Sortie = Sortie;
			Miz.RootDictionary.Description = ToolsLua.DisplayToDcsText(Description);
			Miz.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
			Miz.RootMission.StartTime = Convert.ToInt32((Date - Miz.RootMission.Date).TotalSeconds);

			foreach (BopCoalition coalition in Coalitions.Values)
			{
				coalition.ToMiz();
			}
		}
		#endregion

		#region Methods
		public string ToStringWeather()
		{
			return Weather.ToString(PreferencesMission.WeatherDisplay);
		}

		//public void SetMapProvider(string sProviderName)
		//{
		//	BopGeneral.SetMapProvider(sProviderName);
		//}

		public string ToStringLocalisation(CoordinateSharp.Coordinate coordinate)
		{
			return coordinate.ToStringLocalisation(PreferencesMission.CoordinateDisplay);
		}
		#endregion
	}
}