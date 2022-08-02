using DcsBriefop.DataMiz;
using System.Collections.Generic;

namespace DcsBriefop.DataBriefop
{
	internal class BriefopMain : BaseBriefop
	{
		#region Properties
		public BriefopGeneral GeneralData { get; set; }
		public List<BriefopAsset> Assets { get; private set; }
		#endregion

		#region CTOR
		public BriefopMain(BriefopManager parentManager) : base(parentManager)
		{
			GeneralData = new BriefopGeneral(ParentManager);

			Assets = new List<BriefopAsset>();
			foreach (MizCoalition mizCoalition in ParentManager.Miz.RootMission.Coalitions)
			{
				foreach (MizCountry mizCountry in mizCoalition.Countries)
				{
					foreach (MizGroupFlight g in mizCountry.GroupFlights)
					{
						Assets.Add(new BriefopAssetFlight(ParentManager, mizCoalition.Name, mizCountry.Name, g));
					}
					//foreach (MizGroupShip g in mizCountry.GroupShips)
					//{
					//	Assets.Add(new AssetShip(Core, this, side, g));
					//}
					//foreach (MizGroupVehicle g in mizCountry.GroupVehicles)
					//{
					//	Assets.Add(new AssetVehicle(Core, this, side, g));
					//}
				}
			}

			PostInitialize();
		}
		#endregion

		#region Methods
		public override void PostInitialize()
		{
			base.PostInitialize();

			GeneralData.PostInitialize();
			foreach (BriefopAsset asset in Assets)
				asset.PostInitialize();

		}

		public override void Persist()
		{
			base.Persist();

			GeneralData.Persist();
			foreach (BriefopAsset asset in Assets)
				asset.Persist();
		}
		#endregion
	}
}