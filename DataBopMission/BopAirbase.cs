using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal abstract class BopAirbase : BaseBop
	{
		#region Fields
		protected MizBopAirbase m_mizBopAirbase;
		#endregion

		#region Properties
		public int Id { get; set; }
		public ElementAirbaseType AirbaseType { get; set; }
		public string Name { get; set; }
		public List<BopAirbaseRadio> Radios { get; set; }
		public Tacan Tacan { get; set; }
		public Coordinate Coordinate { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region CTOR
		public BopAirbase(Miz miz, Theatre theatre, ElementAirbaseType airbaseType) : base(miz, theatre)
		{
			AirbaseType = airbaseType;
		}

		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizBopAirbase.Radios.Clear();
			foreach (BopAirbaseRadio bopAirbaseRadio in Radios.Where(_r => !_r.Default || !string.IsNullOrEmpty(_r.Label) || !_r.Used))
			{
				m_mizBopAirbase.Radios.Add(new MizBopAirbaseRadio() { Radio = bopAirbaseRadio.Radio, Label = bopAirbaseRadio.Label, Used = bopAirbaseRadio.Used });
			}
		}

		protected void InitializeRadios(IEnumerable<Radio> defaultRadios)
		{
			Radios = new List<BopAirbaseRadio>();
			if (defaultRadios is object)
			{
				foreach (Radio radio in defaultRadios)
				{
					MizBopAirbaseRadio mizBopAirbaseRadio = m_mizBopAirbase.Radios.Where(_r => _r.Radio.Equals(radio)).FirstOrDefault();
					Radios.Add(new BopAirbaseRadio()
					{
						Radio = radio,
						Default = true,
						Label = mizBopAirbaseRadio?.Label,
						Used = (mizBopAirbaseRadio?.Used).GetValueOrDefault(true)
					});
				}
			}
			foreach (MizBopAirbaseRadio mizBopAirbaseRadio in m_mizBopAirbase.Radios.Where(_r => !defaultRadios.Contains(_r.Radio)))
			{
				Radios.Add(new BopAirbaseRadio()
				{
					Radio = mizBopAirbaseRadio.Radio,
					Default = false,
					Label = mizBopAirbaseRadio.Label,
					Used = mizBopAirbaseRadio.Used
				});
			}
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}

		public virtual string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder();
			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");

			return sb.ToString();
		}

		public virtual string ToStringCoordinate()
		{
			return Coordinate.ToString(Miz.MizBopCustom.PreferencesMission.CoordinateDisplay);
		}

		public GMapOverlay GetMapOverlayPosition()
		{
			GMapOverlay mapOverlay = new GMapOverlay();
			mapOverlay.Markers.Add(GetMarkerBriefop(null));
			return mapOverlay;
		}

		public GMarkerBriefop GetMarkerBriefop(Color? color)
		{
			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), MapMarker, color ?? Color.DarkGray, Name, 1, 0);
		}
		#endregion
	}

	internal class BopAirbaseAirdrome : BopAirbase
	{
		#region Fields
		private Airdrome m_airdrome;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public BopAirbaseAirdrome(Miz miz, Theatre theatre, Airdrome airdrome) : base(miz, theatre, ElementAirbaseType.Airdrome)
		{
			m_airdrome = airdrome;
			InitializeMizBopCustom();

			Id = m_airdrome.Id;
			Name = m_airdrome.Name;
			Tacan = m_mizBopAirbase.Tacan ?? m_airdrome.Tacan;
			InitializeRadios(m_airdrome.Radios);
			MapMarker = m_mizBopAirbase.MapMarker ?? ElementMapTemplateMarker.Airdrome;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizBopAirbase.Tacan = null;
			if (Tacan is object && !Tacan.Equals(m_airdrome.Tacan))
				m_mizBopAirbase.Tacan = Tacan;

			if (MapMarker != ElementMapTemplateMarker.Airdrome)
				m_mizBopAirbase.MapMarker = MapMarker;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();
			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopAirbase = Miz.MizBopCustom.MizBopAirbases.Where(_a => _a.Id == m_airdrome.Id && _a.AirbaseType == AirbaseType).FirstOrDefault();
			if (m_mizBopAirbase is null)
			{
				m_mizBopAirbase = new MizBopAirbase() { Id = m_airdrome.Id, AirbaseType = AirbaseType };
				m_mizBopAirbase.SetDefaultData();
				Miz.MizBopCustom.MizBopAirbases.Add(m_mizBopAirbase);
			}
		}
		#endregion

		#region Methods
		#endregion
	}

	internal class BopAirbaseShip : BopAirbase
	{
		#region Fields
		private BopUnitShip m_bopUnitShip;
		#endregion

		#region Properties
		public int? Icls { get; set; }
		public decimal? Link4 { get; set; }
		#endregion

		#region CTOR
		public BopAirbaseShip(Miz miz, Theatre theatre, BopUnitShip bopUnitShip) : base(miz, theatre, ElementAirbaseType.Ship)
		{
			m_bopUnitShip = bopUnitShip;
			InitializeMizBopCustom();

			Id = m_bopUnitShip.Id;
			Name = m_bopUnitShip.ToStringDisplayName();
			Tacan = m_bopUnitShip.Tacan;
			Icls = m_bopUnitShip.Icls;
			Link4 = m_bopUnitShip.Link4;
			InitializeRadios(new List<Radio>() { m_bopUnitShip.Radio });
			MapMarker = m_mizBopAirbase.MapMarker ?? m_bopUnitShip.MapMarker;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			if (MapMarker != m_bopUnitShip.MapMarker)
				m_mizBopAirbase.MapMarker = MapMarker;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			m_bopUnitShip.FinalizeFromMiz();
			Coordinate = m_bopUnitShip.Coordinate;
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopAirbase = Miz.MizBopCustom.MizBopAirbases.Where(_a => _a.Id == m_bopUnitShip.Id && _a.AirbaseType == AirbaseType).FirstOrDefault();
			if (m_mizBopAirbase is null)
			{
				m_mizBopAirbase = new MizBopAirbase() { Id = m_bopUnitShip.Id, AirbaseType = AirbaseType };
				m_mizBopAirbase.SetDefaultData();
				Miz.MizBopCustom.MizBopAirbases.Add(m_mizBopAirbase);
			}
		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder (base.ToStringAdditionnal());
			if (Icls is object)
				sb.AppendWithSeparator($"ICLS:{Icls}", " ");
			if (Link4 is object)
				sb.AppendWithSeparator($"LNK4:{Link4:###.000}", " ");

			return sb.ToString();
		}
		#endregion
	}

	internal class BopAirbaseFarp : BopAirbase
	{
		#region Fields
		private BopUnit m_bopUnit;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public BopAirbaseFarp(Miz miz, Theatre theatre, BopUnit bopUnit) : base(miz, theatre, ElementAirbaseType.Farp)
		{
			m_bopUnit = bopUnit;
			InitializeMizBopCustom();

			Id = m_bopUnit.Id;
			Name = m_bopUnit.ToStringDisplayName();
			Tacan = m_mizBopAirbase.Tacan;
			InitializeRadios(new List<Radio>() { m_bopUnit.HeliportRadio });
			MapMarker = m_mizBopAirbase.MapMarker ?? m_bopUnit.MapMarker;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			if (MapMarker != m_bopUnit.MapMarker)
				m_mizBopAirbase.MapMarker = MapMarker;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			m_bopUnit.FinalizeFromMiz();
			Coordinate = m_bopUnit.Coordinate;
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopAirbase = Miz.MizBopCustom.MizBopAirbases.Where(_a => _a.Id == m_bopUnit.Id && _a.AirbaseType == AirbaseType).FirstOrDefault();
			if (m_mizBopAirbase is null)
			{
				m_mizBopAirbase = new MizBopAirbase() { Id = m_bopUnit.Id, AirbaseType = AirbaseType };
				m_mizBopAirbase.SetDefaultData();
				Miz.MizBopCustom.MizBopAirbases.Add(m_mizBopAirbase);
			}
		}
		#endregion

		#region Methods
		#endregion
	}

	internal class BopAirbaseRadio
	{
		#region Fields
		#endregion

		#region Properties
		public Radio Radio { get; set; }
		public string Label { get; set; }
		public bool Used { get; set; }
		public bool Default { get; set; }
		#endregion

		#region CTOR
		#endregion

		#region Methods
		public override string ToString()
		{
			return Radio.ToString();
		}
		#endregion
	}
}
