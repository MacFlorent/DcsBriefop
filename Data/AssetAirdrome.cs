using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using System.Linq;
using System.Text;

namespace DcsBriefop.Data
{
	internal class AssetAirdrome : Asset
	{
		#region Fields
		protected Airdrome m_airdrome;
		protected BriefopCustomAirdrome m_briefopCustomAirdrome;
		#endregion

		#region Properties
		public Coordinate Coordinate { get; set; }
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public AssetAirdrome(BaseBriefingCore core, BriefingCoalition coalition, Airdrome airdrome) : base(core, coalition, ElementAssetSide.None)
		{
			m_airdrome = airdrome;
			Initialize();
		}
		#endregion

		#region Initialization
		protected override void InitializeDataCustom()
		{
			m_briefopCustomAirdrome = Core.Miz.BriefopCustom.GetAirdrome(Id, Coalition.CoalitionName);

			if (m_briefopCustomAirdrome is null)
			{
				m_briefopCustomAirdrome = new BriefopCustomAirdrome(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustom.AssetAirdromes.Add(m_briefopCustomAirdrome);

				if (IsAssetBase())
				{
					m_briefopCustomAirdrome.Usage = (int)ElementAssetUsage.Base;
					m_briefopCustomAirdrome.MapDisplay = (int)ElementAssetMapDisplay.Point;
				}
				else
				{
					m_briefopCustomAirdrome.Usage = (int)ElementAssetUsage.Excluded;
					m_briefopCustomAirdrome.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomAirdrome.SetDefaultData();
			}
		}

		protected override void InitializeData()
		{
			base.InitializeData();
			
			MapMarker = MarkerBriefopType.airport.ToString();
			Usage = (ElementAssetUsage)m_briefopCustomAirdrome.Usage;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomAirdrome.MapDisplay;

			if (IsAssetBase())
			{
				Side = ElementAssetSide.Own;
				Color = Coalition.OwnColor;
			}

			Id = m_airdrome.Id;
			Name = m_airdrome.Name;
			Type = "Airbase";

			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
			Tacan = m_airdrome.Tacan;
		}

		protected override void InitializeMapPoints()
		{
			MapPoints.Add(new AssetMapPoint(Core, 0, Name, Coordinate));
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			m_briefopCustomAirdrome.Usage = (int)Usage;
			m_briefopCustomAirdrome.MapDisplay = (int)MapDisplay;
			m_briefopCustomAirdrome.Information = m_customInformation;
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";
			if (Side == ElementAssetSide.Own)
				sInformation = Tacan?.ToString();

			return sInformation;
		}

		public override string GetRadioString()
		{
			if (m_airdrome.Radios is null)
				return "";
			else
			{
				StringBuilder sb = new StringBuilder();
				foreach (Radio radio in m_airdrome.Radios)
					sb.AppendWithSeparator(radio.ToString(), " ");

				return sb.ToString();
			}
		}

		private bool IsAssetBase()
		{
			return Coalition.OwnAssets.OfType<AssetFlight>().Where(_a => _a.GetAirdromeIds().Contains(Id)).Any();
		}
		#endregion
	}
}
