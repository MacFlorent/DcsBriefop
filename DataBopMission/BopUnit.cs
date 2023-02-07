using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using UnitsNet.Units;

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
		public ElementGroupClass GroupClass { get; set; }
		public ElementDcsObjectAttribute Attributes { get; set; }
		public BopGroup BopGroup { get; private set; }

		public int Id { get; set; }
		public string Category { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public bool MainInGroup { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		public string MapMarker { get; set; }
		public Radio HeliportRadio { get; set; }
		public BopCallsign HeliportCallsign { get; set; }
		#endregion

		#region CTOR
		public BopUnit(Miz miz, Theatre theatre, BopGroup bopGroup, MizUnit mizUnit) : base(miz, theatre)
		{
			BopGroup = bopGroup;
			m_mizUnit = mizUnit;
			GroupClass = BopGroup.GroupClass;
			m_dcsObject = DcsObjectManager.GetObject(m_mizUnit.Type);
			
			if (m_dcsObject is object)
			{
				GroupClass = m_dcsObject.GroupClass;
				Attributes = m_dcsObject.Attributes;
				MainInGroup = m_dcsObject.MainInGroup;
			}

			InitializeMizBopCustom();

			Id = m_mizUnit.Id;
			Category = m_mizUnit.Category;
			Name = m_mizUnit.Name;
			Type = m_mizUnit.Type;
			Playable = (m_mizUnit.Skill == ElementSkill.Player || m_mizUnit.Skill == ElementSkill.Client);
			MapMarker = m_mizBopUnit.MapMarker;

			if (m_mizUnit.HeliportFrequency is object)
				HeliportRadio = new Radio(m_mizUnit.HeliportFrequency.Value, m_mizUnit.HeliportModulation ?? ElementRadioModulation.AM);
			HeliportCallsign = BopCallsign.NewFromHeliportId(m_mizUnit.HeliportCallsignId);
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizUnit.Id = Id;
			m_mizUnit.Category = Category;
			m_mizUnit.Name = Name;
			m_mizUnit.Type = Type;
			m_mizBopUnit.MapMarker = MapMarker;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			if (m_mizUnit.Altitude is object)
				AltitudeFeet = (decimal)UnitsNet.UnitConverter.Convert(m_mizUnit.Altitude.Value, LengthUnit.Meter, LengthUnit.Foot);
			else
				AltitudeFeet = BopGroup.RoutePoints.FirstOrDefault()?.AltitudeFeet;

			Coordinate = Theatre.GetCoordinate(m_mizUnit.Y, m_mizUnit.X);
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopUnit = Miz.MizBopCustom.MizBopUnits.Where(_u => _u.Id == m_mizUnit.Id).FirstOrDefault();
			if (m_mizBopUnit is null)
			{
				m_mizBopUnit = new MizBopUnit() { Id = m_mizUnit.Id };
				m_mizBopUnit.MapMarker = m_dcsObject?.MapMarker ?? ToolsBriefop.GetDefaultMapMarker(GroupClass);
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
			if (HeliportCallsign is object)
				return $"{HeliportCallsign} | {Name}";
			else
				return Name;
		}

		public virtual string ToStringAdditionnal()
		{
			return "";
		}

		public virtual string ToStringCoordinate()
		{
			return Coordinate.ToString(Miz.MizBopCustom.PreferencesMission.CoordinateDisplay);
		}

		public GMarkerBriefop GetMarkerBriefop(Color? color)
		{
			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), MapMarker, color ?? ToolsBriefop.GetCoalitionColor(BopGroup.CoalitionName), ToStringDisplayName(), 1, 0);
		}
		#endregion
	}
}
