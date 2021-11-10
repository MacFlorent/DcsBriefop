using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
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
		public override string RadioString { get { return Radio.ToString(); } }

		public UnitShip MainUnit
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

		private Radio m_radio;
		public Radio Radio
		{
			get
			{
				if (m_radio is null)
					m_radio = new Radio() { Frequency = MainUnit.RadioFrequency / 1000000, Modulation = MainUnit.RadioModulation };
				return m_radio;
			}
			set
			{
				MainUnit.RadioFrequency = value.Frequency * 1000000;
				MainUnit.RadioModulation = value.Modulation;
			}
		}
		#endregion

		#region CTOR
		public AssetShip(BriefingPack briefingPack, BriefingCoalition briefingCoalition, ElementAssetSide side, GroupShip group) : base(briefingPack, briefingCoalition, side, group) { }
		#endregion

		#region Methods
		protected override string GetDefaultInformation()
		{
			return $"TCN={GetTacanString()}";
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
				Usage = ElementAssetUsage.Base;
				MapDisplay = ElementAssetMapDisplay.Point;
			}
			else
			{
				Usage = ElementAssetUsage.Excluded;
				MapDisplay = ElementAssetMapDisplay.None;
			}
		}
		#endregion
	}
}
