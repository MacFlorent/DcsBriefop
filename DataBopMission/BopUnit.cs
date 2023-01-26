using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnit : BaseBop
	{
		#region Fields
		protected MizUnit m_mizUnit;
		protected DcsObject m_dcsObject;
		#endregion

		#region Properties
		public ElementDcsObjectClass Class { get; set; }
		public ElementDcsObjectAttribute Attributes { get; set; }
		//public string MapMarker { get { return m_dcsObject?.MapMarker; } }
		public BopGroup BopGroup { get; private set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public bool MainInGroup { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public BopUnit(Miz miz, Theatre theatre, MizUnit mizUnit, BopGroup bopGroup) : base(miz, theatre)
		{
			m_mizUnit = mizUnit;
			m_dcsObject = DcsObjectManager.GetObject(m_mizUnit.Type);
			
			Class = ElementDcsObjectClass.None;
			Attributes = ElementDcsObjectAttribute.None;
			BopGroup = bopGroup;

			if (m_dcsObject is object)
			{
				Class = m_dcsObject.Class;
				Attributes = m_dcsObject.Attributes;
				MainInGroup = m_dcsObject.MainInGroup;
			}

			Id = m_mizUnit.Id;
			Name = m_mizUnit.Name;
			Type = m_mizUnit.Type;
			Playable = (m_mizUnit.Skill == ElementSkill.Player || m_mizUnit.Skill == ElementSkill.Client);
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizUnit.Id = Id;
			m_mizUnit.Name = Name;
			m_mizUnit.Type = Type;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Coordinate = Theatre.GetCoordinate(m_mizUnit.Y, m_mizUnit.X);
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return ToStringDisplayName();
		}

		public virtual string ToStringDisplayName()
		{
			return Name;
		}
		#endregion
	}
}
