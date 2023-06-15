using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;

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
		public List<BopAirbase> Airbases { get; private set; }
		public List<BopGroup> Groups { get; private set; }
		public PreferencesMap PreferencesMap { get { return Miz.MizBopCustom.PreferencesMap; } }
		public List<BopBriefingFolder> BopBriefingFolders { get { return Miz.MizBopCustom.BopBriefingFolders; } }
		//public GMapOverlay StaticMapOverlay { get; private set; }
		#endregion

		#region CTOR
		public BopMission(Miz miz, Theatre theatre) : base(miz, theatre)
		{
			InitializeMizBopCustom();

			Sortie = Miz.RootDictionary.Sortie;
			Description = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.Description);
			Date = new DateTime(Miz.RootMission.Date.Year, Miz.RootMission.Date.Month, Miz.RootMission.Date.Day).AddSeconds(Miz.RootMission.StartTime);

			Weather = new BopWeather(Miz, Theatre, Date);

			//StaticMapOverlay = new GMapOverlay();
			//ToolsMap.AddMizDrawingLayers(Theatre, StaticMapOverlay, Miz.RootMission.DrawingLayers.Where(_dl => string.Compare(_dl.Name, ElementDrawingLayer.Common, true) == 0).ToList());

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
						Groups.Add(new BopGroup(Miz, Theatre, mizCoalition.Name, mizCountry.Name, ElementDcsGroupType.Static, ElementGroupClass.None, mizGroup));
					}
				}
			}

			Airbases = new List<BopAirbase>();
			foreach (Airdrome airdrome in Theatre.Airdromes)
			{
				Airbases.Add(new BopAirbaseAirdrome(Miz, Theatre, airdrome));
			}
			foreach (BopGroup bopGroup in Groups)
			{
				foreach (BopUnit bopUnit in bopGroup.Units)//.OfType<BopUnitShip>().Where(_u => (_u.Attributes & ElementDcsObjectAttribute.AircraftCarrier) != 0))
				{
					if (bopUnit is BopUnitShip bopUnitShip && (bopUnitShip.Attributes & ElementDcsObjectAttribute.AircraftCarrier) != 0)
						Airbases.Add(new BopAirbaseShip(Miz, Theatre, bopUnitShip));
					else if (bopUnit.Category == ElementUnitCategory.Heliport)
						Airbases.Add(new BopAirbaseFarp(Miz, Theatre, bopUnit));
				}
			}

			SetBullseyeRoutePoint();
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

			Miz.MizBopCustom.MizBopAirbases.Clear();
			Miz.MizBopCustom.MizBopGroups.Clear();
			Miz.MizBopCustom.MizBopUnits.Clear();
			Miz.MizBopCustom.MizBopRoutePoints.Clear();

			foreach (BopCoalition coalition in Coalitions.Values)
			{
				coalition.ToMiz();
			}
			foreach (BopGroup bopGroup in Groups)
			{
				bopGroup.ToMiz();
			}
			foreach (BopAirbase bopAirbase in Airbases)
			{
				bopAirbase.ToMiz();
			}
		}

		private void InitializeMizBopCustom()
		{
			if (Miz.MizBopCustom.MapData is null)
			{
				Miz.MizBopCustom.MapData = new MizBopMap();
				Airdrome firstAirdrome = Theatre.Airdromes.FirstOrDefault();
				if (firstAirdrome is object)
				{
					Miz.MizBopCustom.MapData.CenterLatitude = firstAirdrome.Latitude;
					Miz.MizBopCustom.MapData.CenterLongitude = firstAirdrome.Longitude;
				}
				Miz.MizBopCustom.MapData.Zoom = PreferencesManager.Preferences.Map.Zoom;
			}
		}
		#endregion

		#region Methods
		public GMapOverlay BuildStaticMapOverlay()
		{
			GMapOverlay staticMapOverlay = new GMapOverlay();
			ToolsMap.AddMizDrawingLayers(Theatre, staticMapOverlay, Miz.RootMission.DrawingLayers.Where(_dl => string.Compare(_dl.Name, ElementDrawingLayer.Common, true) == 0).ToList());
			return staticMapOverlay;
		}

		public void SetBullseyeRoutePoint()
		{
			foreach (BopGroupFlight bopGroupFlight in Groups.OfType<BopGroupFlight>())
			{
				bopGroupFlight.SetBullseyeWaypoint(Coalitions[bopGroupFlight.CoalitionName]);
			}
		}

		public async Task<ListBopBriefingGeneratedFile> GenerateBriefingFiles(BriefopManager bopManager)
		{
			ListBopBriefingGeneratedFile files = new();
			foreach (BopBriefingFolder folder in BopBriefingFolders.Where(_bf => !_bf.Inactive))
			{
				files.AddRange(await folder.GenerateFiles(bopManager));
			}

			return files;
		}

		public IEnumerable<string> GetKneeboards(string sCoalitionName)
		{
			return Groups.Where(_g => _g.Playable && (string.IsNullOrEmpty(sCoalitionName) || _g.CoalitionName == sCoalitionName)).Select(_g => _g.Type).Distinct();
		}

		public List<BopGroupOrUnit> GetGroupOrUnits()
		{
			List<BopGroupOrUnit> missionGroupOrUnits = new List<BopGroupOrUnit>();
			foreach (BopGroup group in Groups)
			{
				missionGroupOrUnits.Add(new BopGroupOrUnit() { BopGroup = group });
				foreach (BopUnit unit in group.Units)
				{
					missionGroupOrUnits.Add(new BopGroupOrUnit() { BopGroup = group, BopUnit = unit });
				}
			}

			return missionGroupOrUnits;
		}
		#endregion
	}
}