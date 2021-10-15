using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingShip : BriefingGroup
	{
		#region Fields
		private GroupShip GroupShip { get { return m_group as GroupShip; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.ship.ToString();
		public override string Category { get { return "Ship"; } }

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

		public string Type { get { return MainUnit.Type; } }
		
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
		public BriefingShip(BriefingPack bp, GroupShip gs, BriefingCoalition bc) : base(bp, gs, bc)
		{
			if (BriefingStatus == ElementGroupStatusId.NotSet)
			{
				BriefingStatus = ElementGroupStatusId.Point;
			}

			InitializeMapData();
		}
		#endregion

		#region Methods
		public string GetRadioString()
		{
			return ToolsMasterData.GetRadioString(RadioFrequency, RadioModulation);
		}
		#endregion
	}
}
