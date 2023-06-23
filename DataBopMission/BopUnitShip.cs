using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Text;

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
		public double? Link4 { get; set; }
		#endregion

		#region CTOR
		public BopUnitShip(Miz miz, Theatre theatre, BopGroupShip bopGroup, MizUnit mizUnit) : base(miz, theatre, bopGroup, mizUnit)
		{
			if (m_mizUnit.RadioFrequency is object && m_mizUnit.RadioModulation is object)
				Radio = new Radio(m_mizUnit.RadioFrequency.Value / ElementRadio.UnitFrequencyRatio, m_mizUnit.RadioModulation.Value);

			Tacan = bopGroup.GetTacanFromRouteTask(Id);
			Icls = bopGroup.GetIclsFromRouteTaskAction(Id);
			Link4 = bopGroup.GetLink4FromRouteTaskAction(Id);
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();
		}
		#endregion

		#region Methods
		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditional());

			if (Radio is object)
				sb.AppendWithSeparator($"Radio:{Radio}", " ");
			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");
			if (Icls is object)
				sb.AppendWithSeparator($"ICLS:{Icls}", " ");
			if (Link4 is object)
				sb.AppendWithSeparator($"LNK4:{Link4:###.000}", " ");

			return sb.ToString();
		}
		#endregion
	}
}
