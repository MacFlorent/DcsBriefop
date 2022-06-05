using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class AssetUnit : BaseBriefing
	{
		#region Fields
		private BriefopCustomUnit m_briefopCustomUnit;
		#endregion

		#region Properties
		public MizUnit MizUnit { get; private set; }
		public AssetGroup AssetGroup { get; private set; }
		public DcsObject DcsObject { get; private set; }

		public bool Included { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public ElementDcsObjectClass Class { get { return DcsObject?.Class ?? ElementDcsObjectClass.None; } }
		public string Type { get; set; }
		public ElementDcsObjectAttribute Attributes { get { return DcsObject?.Attributes ?? ElementDcsObjectAttribute.None; } }
		public Coordinate Coordinate { get; set; }
		public string DisplayName { get { return DcsObject?.DisplayName ?? Name; } }
		public string Information { get { return DcsObject?.Information; } }
		#endregion

		#region CTOR
		public AssetUnit(BaseBriefingCore core, MizUnit unit, AssetGroup group) : base(core)
		{
			MizUnit = unit;
			AssetGroup = group;

			Initialize();
		}
		#endregion

		#region Initialize
		protected void Initialize()
		{
			InitializeData();
			InitializeDataCustom();
		}

		protected void InitializeData()
		{
			Id = MizUnit.Id;
			Name = MizUnit.Name;
			Type = MizUnit.Type;
			Coordinate = Core.Theatre.GetCoordinate(MizUnit.Y, MizUnit.X);

			DcsObject = DcsObjectManager.GetObject(Type);
		}

		protected void InitializeDataCustom()
		{
			m_briefopCustomUnit = Core.Miz.BriefopCustomData.GetUnit(Id, AssetGroup.Coalition.CoalitionName);

			if (m_briefopCustomUnit is null)
			{
				m_briefopCustomUnit = new BriefopCustomUnit(Id, AssetGroup.Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.AssetUnits.Add(m_briefopCustomUnit);

				m_briefopCustomUnit.Included = false;

				m_briefopCustomUnit.SetDefaultData();
			}

			Included = m_briefopCustomUnit.Included;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			m_briefopCustomUnit.Included = Included;

			if (MizUnit.Radios is object && MizUnit.Radios.Length > 0 && AssetGroup.Coalition.ComPresets is object && AssetGroup.Coalition.ComPresets.Count > 0)
			{
				foreach (ComPreset comPreset in AssetGroup.Coalition.ComPresets.Where(_cp => _cp.Radio is object))
				{
					if (MizUnit.Radios.Length > comPreset.PresetRadio)
					{
						MizRadio mizRadio = MizUnit.Radios[comPreset.PresetRadio];
						if (comPreset.PresetNumber < mizRadio.Modulations.Length)
							mizRadio.Modulations[comPreset.PresetNumber] = comPreset.Radio.Modulation;
						if (comPreset.PresetNumber < mizRadio.Channels.Length)
							mizRadio.Channels[comPreset.PresetNumber] = comPreset.Radio.Frequency;
					}
				}
			}
		}

		public virtual string GetLocalisation()
		{
			string sAltitude = "";
			if (AssetGroup.MapPoints.FirstOrDefault() is AssetRoutePoint routePoint)
			{
				sAltitude = $"{Environment.NewLine}{routePoint.AltitudeFeet}";
			}

			return $"{Coordinate.ToStringMGRS()}{sAltitude}";
		}
		#endregion
	}
}
