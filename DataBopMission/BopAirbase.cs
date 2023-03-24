using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal abstract class BopAirbase : BaseBop, IEquatable<BopAirbase>
	{
		#region Fields
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

			ToMizBopCustom();
		}

		protected void ToMizBopCustom()
		{
			MizBopAirbase mizBopAirbase = GetMizBopCustom();
			if (mizBopAirbase is null)
			{
				mizBopAirbase = new MizBopAirbase() { Id = Id, AirbaseType = AirbaseType };
				Miz.MizBopCustom.MizBopAirbases.Add(mizBopAirbase);
			}

			mizBopAirbase.MapMarker = null;
			mizBopAirbase.Tacan = null;
			mizBopAirbase.Radios.Clear();
			bool bMizBopCustomModified = false;

			if (MapMarker != GetDefaultMapMarker())
			{
				mizBopAirbase.MapMarker = MapMarker;
				bMizBopCustomModified = true;
			}
			if (Tacan != GetDefaultTacan())
			{
				mizBopAirbase.Tacan = Tacan;
				bMizBopCustomModified = true;
			}
			foreach (BopAirbaseRadio bopAirbaseRadio in Radios.Where(_r => !_r.Default || !string.IsNullOrEmpty(_r.Label) || !_r.Used))
			{
				mizBopAirbase.Radios.Add(new MizBopAirbaseRadio() { Radio = bopAirbaseRadio.Radio, Label = bopAirbaseRadio.Label, Used = bopAirbaseRadio.Used });
				bMizBopCustomModified = true;
			}

			if (!bMizBopCustomModified)
				Miz.MizBopCustom.MizBopAirbases.Remove(mizBopAirbase);
		}

		protected MizBopAirbase GetMizBopCustom()
		{
			return Miz.MizBopCustom.MizBopAirbases.Where(_a => _a.Id == Id && _a.AirbaseType == AirbaseType).FirstOrDefault();
		}

		protected void InitializeRadios(MizBopAirbase mizBopAirbase, IEnumerable<Radio> defaultRadios)
		{
			Radios = new List<BopAirbaseRadio>();
			if (defaultRadios is object)
			{
				foreach (Radio radio in defaultRadios)
				{
					MizBopAirbaseRadio mizBopAirbaseRadio = mizBopAirbase?.Radios.Where(_r => _r.Radio.Equals(radio)).FirstOrDefault();
					Radios.Add(new BopAirbaseRadio()
					{
						Radio = radio,
						Default = true,
						Label = mizBopAirbaseRadio?.Label,
						Used = (mizBopAirbaseRadio?.Used).GetValueOrDefault(true)
					});
				}
			}

			if (mizBopAirbase is not null && mizBopAirbase.Radios is not null)
			{
				foreach (MizBopAirbaseRadio mizBopAirbaseRadio in mizBopAirbase.Radios?.Where(_r => !defaultRadios.Contains(_r.Radio)))
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
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}

		public string ToStringRadios()
		{
			StringBuilder sb = new();
			foreach (BopAirbaseRadio radio in Radios.Where(_r => _r.Used))
			{
				sb.AppendWithSeparator(radio.ToString(), " ");
			}

			return sb.ToString();
		}

		public virtual string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");

			return sb.ToString();
		}

		protected virtual string GetDefaultMapMarker()
		{
			return null;
		}

		protected virtual Tacan GetDefaultTacan()
		{
			return null;
		}

		public GMapOverlay GetMapOverlay(Color? color)
		{
			GMapOverlay mapOverlay = new GMapOverlay();
			mapOverlay.Markers.Add(GetMarkerBriefop(color));
			return mapOverlay;
		}

		public GMarkerBriefop GetMarkerBriefop(Color? color)
		{
			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(Coordinate.Latitude.DecimalDegree, Coordinate.Longitude.DecimalDegree), MapMarker, color ?? Color.DarkGray, Name, 1, 0);
		}
		#endregion

		#region IEquatable
		public bool Equals(BopAirbase other)
		{
			if (other is null)
				return false;

			return (Id == other.Id && AirbaseType == other.AirbaseType);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BopAirbase);
		}

		public override int GetHashCode() => (Id, AirbaseType).GetHashCode();
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

			Id = m_airdrome.Id;
			Name = m_airdrome.Name;

			MizBopAirbase mizBopAirbase = GetMizBopCustom();
			Tacan = mizBopAirbase?.Tacan ?? GetDefaultTacan();
			MapMarker = mizBopAirbase?.MapMarker ?? GetDefaultMapMarker();
			InitializeRadios(mizBopAirbase, m_airdrome.Radios);
		}
		#endregion

		#region Miz
		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();
			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
		}
		#endregion

		#region Methods
		protected override string GetDefaultMapMarker()
		{
			return ElementMapTemplateMarker.Airdrome;
		}

		protected override Tacan GetDefaultTacan()
		{
			return m_airdrome.Tacan;
		}
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

			Id = m_bopUnitShip.Id;
			Name = m_bopUnitShip.ToStringDisplayName();
			Icls = m_bopUnitShip.Icls;
			Link4 = m_bopUnitShip.Link4;

			MizBopAirbase mizBopAirbase = GetMizBopCustom();
			Tacan = mizBopAirbase?.Tacan ?? GetDefaultTacan();
			MapMarker = mizBopAirbase?.MapMarker ?? GetDefaultMapMarker();
			InitializeRadios(mizBopAirbase, new List<Radio>() { m_bopUnitShip.Radio });
		}
		#endregion

		#region Miz
		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			m_bopUnitShip.FinalizeFromMiz();
			Coordinate = m_bopUnitShip.Coordinate;
		}
		#endregion

		#region Methods
		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditional());
			if (Icls is object)
				sb.AppendWithSeparator($"ICLS:{Icls}", " ");
			if (Link4 is object)
				sb.AppendWithSeparator($"LNK4:{Link4:###.000}", " ");

			return sb.ToString();
		}

		protected override string GetDefaultMapMarker()
		{
			return m_bopUnitShip.MapMarker;
		}

		protected override Tacan GetDefaultTacan()
		{
			return m_bopUnitShip.Tacan;
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

			Id = m_bopUnit.Id;
			Name = m_bopUnit.ToStringDisplayName();

			MizBopAirbase mizBopAirbase = GetMizBopCustom();
			Tacan = mizBopAirbase?.Tacan;
			MapMarker = mizBopAirbase?.MapMarker ?? GetDefaultMapMarker();
			InitializeRadios(mizBopAirbase, new List<Radio>() { m_bopUnit.HeliportRadio });
		}
		#endregion

		#region Miz
		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			m_bopUnit.FinalizeFromMiz();
			Coordinate = m_bopUnit.Coordinate;
		}
		#endregion

		#region Methods
		protected override string GetDefaultMapMarker()
		{
			return m_bopUnit.MapMarker;
		}
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
			string sLabel = "";
			if (!string.IsNullOrEmpty(Label))
			{
				sLabel = $"{Label}:";
			}
			return $"{sLabel}{Radio}";
		}
		#endregion
	}
}
