using DcsBriefop.Data;
using DcsBriefop.DataBopCustom;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DcsBriefop.DataBop
{
	internal class BopMain : BaseBop
	{
		#region Fields
		#endregion

		#region Properties
		public BopGeneral BopGeneral { get; set; }
		public List<BopAirdrome> Airdromes { get; private set; }
		public List<BopAsset> Assets { get; private set; }
		public List<BopCoalition> Coalitions { get; private set; }
		#endregion

		#region CTOR
		public BopMain(BopManager parentManager) : base(parentManager)
		{
			InitializeCustom();

			BopGeneral = new BopGeneral(ParentManager);

			Airdromes = new List<BopAirdrome>();
			foreach (Airdrome airdrome in ParentManager.Theatre.Airdromes)
			{
				Airdromes.Add(new BopAirdrome(ParentManager, airdrome));
			}

			Assets = new List<BopAsset>();
			foreach (MizCoalition mizCoalition in ParentManager.Miz.RootMission.Coalitions)
			{
				foreach (MizCountry mizCountry in mizCoalition.Countries)
				{
					foreach (MizGroupFlight mizGroupFlight in mizCountry.GroupFlights)
					{
						Assets.Add(new BopAssetFlight(ParentManager, mizCoalition.Name, mizCountry.Name, mizGroupFlight));
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

			Coalitions = new List<BopCoalition>();
			foreach (MizCoalition mizCoalition in ParentManager.Miz.RootMission.Coalitions)
			{
				Coalitions.Add(new BopCoalition(ParentManager, mizCoalition));
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

		public string ToStringLocalisation(CoordinateSharp.Coordinate coordinate)
		{
			StringBuilder sb = new StringBuilder();
			if ((ParentManager.BopCustomMain.CoordinateDisplay & ElementCoordinateDisplay.Mgrs) > 0)
				sb.AppendWithSeparator(coordinate.ToStringMGRS(), Environment.NewLine);
			if ((ParentManager.BopCustomMain.CoordinateDisplay & ElementCoordinateDisplay.Dms) > 0)
				sb.AppendWithSeparator(coordinate.ToStringDMS(), Environment.NewLine);
			if ((ParentManager.BopCustomMain.CoordinateDisplay & ElementCoordinateDisplay.Ddm) > 0)
				sb.AppendWithSeparator(coordinate.ToStringDDM(), Environment.NewLine);

			return sb.ToString();
		}
		#endregion
	}
}