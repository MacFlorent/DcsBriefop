using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnitSea : BopUnit
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
		public BopUnitSea(Miz miz, Theatre theatre, MizUnit mizUnit, BopGroupShip bopGroup) : base(miz, theatre, mizUnit)
		{
			if (m_mizUnit.RadioFrequency is object && m_mizUnit.RadioModulation is object)
				Radio = new Radio(m_mizUnit.RadioFrequency.Value / ElementRadio.UnitFrequencyRatio, m_mizUnit.RadioModulation.Value);

			Tacan = bopGroup.GetTacanFromTaskAction(Id);
			Icls = bopGroup.GetIclsFromTaskAction(Id);
			Link4 = bopGroup.GetLink4FromTaskAction(Id);
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
