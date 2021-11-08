using DcsBriefop.LsonStructure;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetStatic : AssetGroup
	{
		#region Fields
		private GroupStatic GroupStatic { get { return m_group as GroupStatic; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.dot.ToString();
		public override string Task { get { return ""; } }
		public override string Type { get { return GroupStatic.Units.FirstOrDefault()?.Type; ; } }
		public override string RadioString { get { return ""; } }
		#endregion

		#region CTOR
		public AssetStatic(BriefingPack briefingPack, BriefingCoalition briefingCoalition, GroupVehicle group) : base(briefingPack, briefingCoalition, group) { }
		#endregion

		#region Methods
		protected override string GetDefaultInformation()
		{
			return "";
		}

		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.GetAssetGroup(Id, BriefingCoalition.Name);
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id);
			RootCustom.AssetGroups.Add(CustomData);

		}
		#endregion
	}
}
