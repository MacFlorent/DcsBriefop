using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnit : BaseBop
	{
		#region Fields
		protected MizUnit m_mizUnit;
		protected MizBopUnit m_mizBopUnit;
		protected DcsObject m_dcsObject;
		#endregion

		#region Properties
		public ElementDcsObjectClass ObjectClass { get; set; }
		public ElementDcsObjectAttribute Attributes { get; set; }
		public BopGroup BopGroup { get; private set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public bool MainInGroup { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region CTOR
		public BopUnit(Miz miz, Theatre theatre, BopGroup bopGroup, MizUnit mizUnit) : base(miz, theatre)
		{
			BopGroup = bopGroup;
			m_mizUnit = mizUnit;
			ObjectClass = BopGroup.ObjectClass;
			m_dcsObject = DcsObjectManager.GetObject(m_mizUnit.Type);
			
			if (m_dcsObject is object)
			{
				ObjectClass = m_dcsObject.ObjectClass;
				Attributes = m_dcsObject.Attributes;
				MainInGroup = m_dcsObject.MainInGroup;
			}

			InitializeMizBopCustom();

			Id = m_mizUnit.Id;
			Name = m_mizUnit.Name;
			Type = m_mizUnit.Type;
			Playable = (m_mizUnit.Skill == ElementSkill.Player || m_mizUnit.Skill == ElementSkill.Client);
			
			MapMarker = m_mizBopUnit.MapMarker;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizUnit.Id = Id;
			m_mizUnit.Name = Name;
			m_mizUnit.Type = Type;
			m_mizBopUnit.MapMarker = MapMarker;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Coordinate = Theatre.GetCoordinate(m_mizUnit.Y, m_mizUnit.X);
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopUnit = Miz.MizBopCustom.MizBopUnits.Where(_u => _u.Id == m_mizUnit.Id).FirstOrDefault();
			if (m_mizBopUnit is null)
			{
				m_mizBopUnit = new MizBopUnit() { Id = Id };
				m_mizBopUnit.MapMarker = m_dcsObject?.MapMarker ?? ToolsBriefop.GetDefaultMapMarker(ObjectClass);
				m_mizBopUnit.SetDefaultData();
				Miz.MizBopCustom.MizBopUnits.Add(m_mizBopUnit);
			}
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

		public virtual string ToStringLocalisation()
		{
			return Coordinate.ToStringLocalisation(Miz.MizBopCustom.PreferencesMission.CoordinateDisplay);
		}

		public GMarkerBriefop GetMarkerBriefop(Color? color)
		{
			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), MapMarker, color ?? ToolsBriefop.GetCoalitionColor(BopGroup.CoalitionName), ToStringDisplayName(), 1, 0);
		}
		#endregion
	}
}
