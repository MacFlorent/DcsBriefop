using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
using DcsBriefop.Map;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetVehicle : AssetGroup
	{
		#region Fields
		private GroupVehicle GroupVehicle { get { return m_group as GroupVehicle; } }
		#endregion

		#region Properties
		protected override string MapMarker { get; set; } = MarkerBriefopType.tank.ToString();
		public override string Task { get { return ""; } }
		public override string Type { get { return GroupVehicle.Units.FirstOrDefault()?.Type; ; } }
		public override string RadioString { get { return ""; } }
		#endregion

		#region CTOR
		public AssetVehicle(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, GroupVehicle group) : base(briefingPack, briefingCoalition, side, group) { }
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
