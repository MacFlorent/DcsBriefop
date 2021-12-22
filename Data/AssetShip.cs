using DcsBriefop.DataMiz;
using System.Linq;
using DcsBriefop.Map;

namespace DcsBriefop.Data
{
	internal class AssetShip : AssetGroup
	{
		#region Fields
		#endregion

		#region Properties
		private MizGroupShip GroupShip { get { return m_mizGroup as MizGroupShip; } }
		public MizUnitShip MainUnit { get; set; }
		public string UnitName { get { return MainUnit.Name; } }
		public Radio Radio { get; set; }

		#endregion

		#region CTOR
		public AssetShip(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupShip group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeDataCustom()
		{
			m_briefopCustomGroup = Core.Miz.BriefopCustom.GetGroup(Id, Coalition.CoalitionName);

			if (m_briefopCustomGroup is null)
			{
				m_briefopCustomGroup = new BriefopCustomGroup(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustom.AssetGroups.Add(m_briefopCustomGroup);

				if (Type.StartsWith("CVN"))
				{
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Base;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.Point;
				}
				else
				{
					m_briefopCustomGroup.Usage = (int)ElementAssetUsage.Excluded;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomGroup.SetDefaultData();
			}
		}

		protected override void InitializeData()
		{
			base.InitializeData();
			
			MapMarker = MarkerBriefopType.ship.ToString();

			MainUnit = GroupShip.Units.OfType<MizUnitShip>().Where(_us => _us.Type.StartsWith("CVN")).FirstOrDefault();
			if (MainUnit is null)
				MainUnit = GroupShip.Units.OfType<MizUnitShip>().FirstOrDefault();

			Type = MainUnit.Type;
			Radio = new Radio() { Frequency = MainUnit.RadioFrequency / 1000000, Modulation = MainUnit.RadioModulation };
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			MainUnit.RadioFrequency = Radio.Frequency * 1000000;
			MainUnit.RadioModulation = Radio.Modulation;
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";
			if (Side == ElementAssetSide.Own)
				sInformation = $"TCN={GetTacanString()}";

			return sInformation;
		}

		public override string GetRadioString()
		{
			return Radio.ToString();
		}
		#endregion
	}
}
