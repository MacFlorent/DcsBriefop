using CoordinateSharp;
using DcsBriefop.Data;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetAirdrome : Asset
	{
		#region Fields
		protected Airdrome m_airdrome;
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.airport.ToString();
		public CustomDataAssetAirdrome CustomData;

		public override ElementAssetCategory Category
		{
			get { return (ElementAssetCategory)CustomData.Category; }
			set { CustomData.Category = (int)value; }
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
		public override string Radio { get { return "radio todo"; } }

		public override string CustomInformation
		{
			get { return CustomData.Information; }
			set { CustomData.Information = value; }
		}

		public Coordinate Coordinate
		{
			get { return new Coordinate (m_airdrome.Latitude, m_airdrome.Longitude); }
		}
		#endregion

		#region CTOR
		public AssetAirdrome(BriefingPack briefingPack, BriefingCoalition briefingCoalition, Airdrome airdrome) : base(briefingPack, briefingCoalition)
		{
			m_airdrome = airdrome;
			InitializeData(briefingPack);
		}
		#endregion

		#region Methods
		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.AssetAirdromes?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetAirdrome(Id);
			RootCustom.AssetAirdromes.Add(CustomData);

			if (BriefingCoalition.Assets.OfType<AssetFlight>().Where(_a => _a.GetAirdromeIds().Contains(Id)).Any())
			{
				Category = ElementAssetCategory.Base;
				MapDisplay = ElementAssetMapDisplay.Point;
			}
			else
			{
				Category = ElementAssetCategory.Excluded;
				MapDisplay = ElementAssetMapDisplay.None;
			}
		}

		protected override void InitializeMapPoints(BriefingPack briefingPack)
		{
			MapPoints.Add(new AssetMapPoint(briefingPack, 0, Name, Coordinate));
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";

			//if (Task == ElementTask.Refueling)
			//{
			//	sInformation = $"TCN={GetTacanString()} ";
			//}

			//sInformation = $"{sInformation} Base={GetAirdromeNames()}";
			return sInformation;
		}
		#endregion
	}
}
