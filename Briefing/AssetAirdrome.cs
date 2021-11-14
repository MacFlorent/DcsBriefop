using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using System.Linq;
using System.Text;

namespace DcsBriefop.Briefing
{
	internal class AssetAirdrome : Asset
	{
		#region Fields
		protected DcsAirdrome m_airdrome;
		#endregion

		#region Properties
		protected override string MapMarker { get; set; } = MarkerBriefopType.airport.ToString();
		public CustomDataAssetAirdrome CustomData;

		public override ElementAssetUsage Usage
		{
			get { return (ElementAssetUsage)CustomData.Usage; }
			set { CustomData.Usage = (int)value; }
		}

		public override ElementAssetMapDisplay MapDisplay
		{
			get { return (ElementAssetMapDisplay)CustomData.MapDisplay; }
			set { CustomData.MapDisplay = (int)value; }
		}

		public override int Id { get { return m_airdrome.Id; } }
		public override string Name { get { return m_airdrome.Name; } }
		public override string Task { get { return ""; } }
		public override string Type { get { return "Airbase"; } }
		
		private string m_sRadioString;
		public override string RadioString
		{
			get
			{
				if (m_sRadioString is null)
				{
					if (m_airdrome.Radios is null)
						m_sRadioString = "";
					else
					{
						StringBuilder sb = new StringBuilder();
						foreach (Radio radio in m_airdrome.Radios)
							sb.AppendWithSeparator(radio.ToString(), " ");

						m_sRadioString = sb.ToString();
					}
				}

				return m_sRadioString;
			}
		}

		public override string CustomInformation
		{
			get { return CustomData.Information; }
			set { CustomData.Information = value; }
		}

		public Coordinate Coordinate
		{
			get { return new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude); }
		}

		public Tacan Tacan { get { return m_airdrome.Tacan; } }
		#endregion

		#region CTOR
		public AssetAirdrome(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, DcsAirdrome airdrome) : base(briefingPack, briefingCoalition, side)
		{
			m_airdrome = airdrome;
			InitializeData(briefingPack);
		}
		#endregion

		#region Methods
		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.GetAssetAirdrome(Id, BriefingCoalition.Name);
			bool bIsAssetBase = BriefingCoalition.OwnAssets.OfType<AssetFlight>().Where(_a => _a.GetAirdromeIds().Contains(Id)).Any();

			if (CustomData is null)
			{
				CustomData = new CustomDataAssetAirdrome(Id, BriefingCoalition.Name);
				RootCustom.AssetAirdromes.Add(CustomData);

				if (bIsAssetBase)
				{
					Side = ElementAssetSide.Own;
					Usage = ElementAssetUsage.Base;
					MapDisplay = ElementAssetMapDisplay.Point;
				}
				else
				{
					Usage = ElementAssetUsage.Excluded;
					MapDisplay = ElementAssetMapDisplay.None;
				}
			}

			if (bIsAssetBase)
				Color = BriefingCoalition.OwnColor;

			CustomData.SetDefaultData();
		}

		protected override void InitializeMapPoints(BriefingPack briefingPack)
		{
			MapPoints.Add(new AssetMapPoint(briefingPack, 0, Name, Coordinate));
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";
			if (Side == ElementAssetSide.Own)
				sInformation = Tacan?.ToString();

			return sInformation;
		}
		#endregion
	}
}
