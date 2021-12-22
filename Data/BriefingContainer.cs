using DcsBriefop.DataMiz;
using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class BriefingContainer : BaseBriefing
	{
		#region Properties
		public BriefingMission Mission { get; set; }
		public List<BriefingCoalition> BriefingCoalitions { get; private set; } = new List<BriefingCoalition>();

		public BriefopCustomMap MapData { get { return Core.Miz.BriefopCustom.MapData; } }
		#endregion

		#region CTOR
		public BriefingContainer(Miz miz) : base (new BaseBriefingCore(miz))
		{
			Mission = new BriefingMission(Core);

			if (!string.IsNullOrEmpty(miz.RootDictionary.RedTask))
				BriefingCoalitions.Add(new BriefingCoalition(Core, ElementCoalition.Red));
			if (!string.IsNullOrEmpty(miz.RootDictionary.BlueTask))
				BriefingCoalitions.Add(new BriefingCoalition(Core, ElementCoalition.Blue));
			if (!string.IsNullOrEmpty(miz.RootDictionary.NeutralTask))
				BriefingCoalitions.Add(new BriefingCoalition(Core, ElementCoalition.Neutral));
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			Mission.Persist();

			foreach (BriefingCoalition coalition in BriefingCoalitions)
				coalition.Persist();
		}
		#endregion
	}
}
