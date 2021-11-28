using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetStatic : AssetGroup
	{
		#region Fields
		private MizGroupStatic GroupStatic { get { return m_group as MizGroupStatic; } }
		#endregion

		#region Properties
		protected override string MapMarker { get; set; } = MarkerBriefopType.dot.ToString();
		public override string Task { get { return ""; } }
		public override string Type { get { return GroupStatic.Units.FirstOrDefault()?.Type; ; } }
		public override string RadioString { get { return ""; } }
		#endregion

		#region CTOR
		public AssetStatic(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, MizGroupVehicle group) : base(briefingPack, briefingCoalition, side, group) { }
		#endregion

		#region Methods
		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.GetAssetGroup(Id, BriefingCoalition.Name);
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id, BriefingCoalition.Name);
			RootCustom.AssetGroups.Add(CustomData);

			CustomData.SetDefaultData();
		}
		#endregion
	}
}
