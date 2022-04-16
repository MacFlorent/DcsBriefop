using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using System.Collections.Generic;
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
		public override string Class { get; protected set; } = "Airdrome";

		public Coordinate Coordinate { get; set; }
		public Tacan Tacan { get; set; }
		public List<Radio> Radios { get { return m_airdrome?.Radios; } }
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
			m_briefopCustomAirdrome = Core.Miz.BriefopCustomData.GetAirdrome(Id, Coalition.CoalitionName);

			if (m_briefopCustomAirdrome is null)
			{
				m_briefopCustomAirdrome = new BriefopCustomAirdrome(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.AssetAirdromes.Add(m_briefopCustomAirdrome);

				if (IsAssetBase())
				{
					m_briefopCustomAirdrome.Included = true;
					m_briefopCustomAirdrome.MapDisplay = (int)ElementAssetMapDisplay.Point;
				}
				else
				{
					m_briefopCustomAirdrome.Included = false;
					m_briefopCustomAirdrome.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomAirdrome.SetDefaultData();
			}

			Included = m_briefopCustomAirdrome.Included;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomAirdrome.MapDisplay;
		}

		protected override void InitializeData()
		{
			base.InitializeData();
			
			MapMarker = ElementMapTemplateMarker.Airdrome;
			Function = ElementAssetFunction.Base;

			Id = m_airdrome.Id;
			Name = Description = m_airdrome.Name;
			Type = "Airbase";

			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
			Tacan = m_airdrome.Tacan;

			if (IsAssetBase())
			{
				Side = ElementAssetSide.Own;
				Color = Coalition.OwnColor;
			}
		}

		protected override void InitializeMapPoints()
		{
			MapPoints.Add(new AssetMapPoint(Core, 0, Name, Coordinate));
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			m_briefopCustomAirdrome.Included = Included;
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

		public bool IsAssetBase()
		{
			return Coalition.OwnAssets.OfType<AssetFlight>().Where(_a => _a.GetAirdromeIds().Contains(Id)).Any();
		}
		#endregion
	}
}
