using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnitShip : BopUnit
	{
		#region Fields
		#endregion

		#region Properties
		public Radio Radio { get; set; }
		public Tacan Tacan { get; set; }
		public int? Icls { get; set; }
		public decimal? Link4 { get; set; }
		#endregion

		#region CTOR
		public BopUnitShip(Miz miz, Theatre theatre, BopGroupShip bopGroup, MizUnit mizUnit) : base(miz, theatre, bopGroup, mizUnit)
		{
			if (m_mizUnit.RadioFrequency is object && m_mizUnit.RadioModulation is object)
				Radio = new Radio(m_mizUnit.RadioFrequency.Value / ElementRadio.UnitFrequencyRatio, m_mizUnit.RadioModulation.Value);

			Tacan = bopGroup.GetTacanFromRouteTaskAction(Id);
			Icls = bopGroup.GetIclsFromRouteTaskAction(Id);
			Link4 = bopGroup.GetLink4FromRouteTaskAction(Id);
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizUnit.RadioFrequency = Radio?.Frequency;
			m_mizUnit.RadioModulation = Radio?.Modulation;
		}
		#endregion

		#region Methods
		#endregion
	}
}
