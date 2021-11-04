using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetShip : AssetGroup
	{
		#region Fields
		private GroupShip GroupShip { get { return m_group as GroupShip; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.ship.ToString();
		public override string Task { get { return ""; } }
		public override string Type { get { return MainUnit.Type; } }
		public override string Radio { get { return ToolsMisc.GetRadioString(RadioFrequency, RadioModulation); } }

		private UnitShip MainUnit
		{
			get
			{
				UnitShip us = GroupShip.Units.OfType<UnitShip>().Where(_us => _us.Type.StartsWith("CVN")).FirstOrDefault();
				if (us is null)
					us = GroupShip.Units.OfType<UnitShip>().FirstOrDefault();

				return us;
			}
		}
		
		public string UnitName { get { return MainUnit.Name; } }
		
		public decimal RadioFrequency
		{
			get { return MainUnit.RadioFrequency / 1000000; }
			set { MainUnit.RadioFrequency = value * 1000000; }
		}
		public int RadioModulation
		{
			get { return MainUnit.RadioModulation; }
			set { MainUnit.RadioModulation = value; }
		}
		#endregion

		#region CTOR
		public AssetShip(BriefingPack briefingPack, BriefingCoalition briefingCoalition, GroupShip group) : base(briefingPack, briefingCoalition, group) { }
		#endregion

		#region Methods
		protected override string GetDefaultInformation()
		{
			return $"TCN={GetTacanString()} ICLS={"xxx"}";
		}

		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.AssetGroups?.Where(_f => _f.Id == Id).FirstOrDefault();
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id);
			RootCustom.AssetGroups.Add(CustomData);

			if (Type.StartsWith("CVN"))
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

		public string GetRadioString()
		{
			return ToolsMisc.GetRadioString(RadioFrequency, RadioModulation);
		}
		#endregion
	}
}
