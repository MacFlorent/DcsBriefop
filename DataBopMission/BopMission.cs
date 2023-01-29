using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;

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
		public List<BopGroup> Groups { get; private set; }
		//public List<BopAirdrome> Airdromes { get; private set; }

		public PreferencesMission PreferencesMission { get { return Miz.MizBopCustom.PreferencesMission; } }
		public PreferencesMap PreferencesMap { get { return Miz.MizBopCustom.PreferencesMap; } }
		#endregion

		#region CTOR
		public BopMission(Miz miz, Theatre theatre) : base(miz, theatre)
		{
			Sortie = Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.Description);
			Date = new DateTime(Miz.RootMission.Date.Year, Miz.RootMission.Date.Month, Miz.RootMission.Date.Day).AddSeconds(Miz.RootMission.StartTime);

			Weather = new BopWeather(Miz, Theatre, Date);

			Coalitions = new Dictionary<string, BopCoalition>
			{
				{ ElementCoalition.Red, new BopCoalition(Miz, Theatre, ElementCoalition.Red) },
				{ ElementCoalition.Blue, new BopCoalition(Miz, Theatre, ElementCoalition.Blue) },
				{ ElementCoalition.Neutral, new BopCoalition(Miz, Theatre, ElementCoalition.Neutral) }
			};

			Groups = new List<BopGroup>();
			foreach (MizCoalition mizCoalition in Miz.RootMission.Coalitions)
			{
				foreach (MizCountry mizCountry in mizCoalition.Countries)
				{
					foreach (MizGroup mizGroup in mizCountry.GroupFlights)
					{
						Groups.Add(new BopGroupFlight(Miz, Theatre, mizCoalition.Name, mizCountry.Name, mizGroup));
					}
					foreach (MizGroup mizGroup in mizCountry.GroupShips)
					{
						Groups.Add(new BopGroupShip(Miz, Theatre, mizCoalition.Name, mizCountry.Name, mizGroup));
					}
					foreach (MizGroup mizGroup in mizCountry.GroupVehicles)
					{
						Groups.Add(new BopGroupVehicle(Miz, Theatre, mizCoalition.Name, mizCountry.Name, mizGroup));
					}
					foreach (MizGroup mizGroup in mizCountry.GroupStatics)
					{
						Groups.Add(new BopGroup(Miz, Theatre, mizCoalition.Name, mizCountry.Name, ElementDcsGroupType.Static, ElementDcsObjectClass.None, mizGroup));
					}
				}
			}
		}
		#endregion

		#region Miz
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
			foreach (BopGroup bopGroup in Groups)
			{
				bopGroup.ToMiz();
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
		#endregion
	}
}