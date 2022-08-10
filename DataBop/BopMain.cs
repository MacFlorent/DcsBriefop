using DcsBriefop.DataBopCustom;
using DcsBriefop.DataMiz;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Collections.Generic;

namespace DcsBriefop.DataBop
{
	internal class BopMain : BaseBop
	{
		#region Fields
		#endregion

		#region Properties
		public BopGeneral BopGeneral { get; set; }
		public List<BopAsset> Assets { get; private set; }
		#endregion

		#region CTOR
		public BopMain(BopManager parentManager) : base(parentManager)
		{
			InitializeCustom();

			BopGeneral = new BopGeneral(ParentManager);

			Assets = new List<BopAsset>();
			foreach (MizCoalition mizCoalition in ParentManager.Miz.RootMission.Coalitions)
			{
				foreach (MizCountry mizCountry in mizCoalition.Countries)
				{
					foreach (MizGroupFlight g in mizCountry.GroupFlights)
					{
						Assets.Add(new BopAssetFlight(ParentManager, mizCoalition.Name, mizCountry.Name, g));
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

		#region Initialize & Persist
		private void InitializeCustom()
		{
			if (ParentManager.BopCustomMain is null)
			{
				ParentManager.BopCustomMain = new BopCustomMain();
				ParentManager.BopCustomMain.InitializeDefault();
			}
		}

		public override void PostInitialize()
		{
			base.PostInitialize();

			BopGeneral.PostInitialize();
			foreach (BopAsset asset in Assets)
				asset.PostInitialize();

		}

		public override void Persist()
		{
			base.Persist();

			BopGeneral.Persist();
			foreach (BopAsset asset in Assets)
				asset.Persist();
		}
		#endregion

		#region Methods
		public void SetMapProvider(string sProviderName)
		{
			BopGeneral.SetMapProvider(sProviderName);
		}
		#endregion
	}
}