using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingShip : BriefingGroup
	{
		private GroupShip GroupShip { get { return m_group as GroupShip; } }
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

		public string UnitName { get { return MainUnit.Name; } }

		#region CTOR
		public BriefingShip(BriefingPack bp, GroupShip gs, BriefingCoalition bc) : base(bp, gs, bc)
		{
			if (BriefingCategory == ElementGroupBriefingCategory.NotSet)
			{
				BriefingCategory = ElementGroupBriefingCategory.Point;
			}

			InitializeMapOverlay();
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
