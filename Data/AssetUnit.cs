using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;

namespace DcsBriefop.Data
{
	internal class AssetUnit : BaseBriefing
	{
		#region Fields
		private MizUnit m_unit;
		private BriefopCustomUnit m_briefopCustomUnit;
		#endregion

		#region Properties
		public AssetGroup AssetGroup { get; set; }
		public bool Included { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string GroupName { get; set; }
		public string Type { get; set; }
		public Coordinate Coordinate { get; set; }
		public string Description { get; set; }
		public string Information { get; set; }
		#endregion

		#region CTOR
		public AssetUnit(BaseBriefingCore core, MizUnit unit, AssetGroup group) : base(core)
		{
			m_unit = unit;
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
			Id = m_unit.Id;
			Name = m_unit.Name;
			Type = m_unit.Type;
			Description = m_unit.Type;
			Coordinate = Core.Theatre.GetCoordinate(m_unit.Y, m_unit.X);

			DcsUnit dcsUnit = DcsUnitManager.GetUnit(Type);
			if (dcsUnit is object)
			{
				Description = dcsUnit.Description;
				Information = dcsUnit.Information;
			}
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

			if (m_unit.Radios is object && m_unit.Radios.Length > 0 && AssetGroup.Coalition.ComPresets is object && AssetGroup.Coalition.ComPresets.Count > 0)
			{
				foreach(ComPreset comPreset in AssetGroup.Coalition.ComPresets)
				{
					if (m_unit.Radios.Length > comPreset.PresetRadio)
					{
						MizRadio mizRadio = m_unit.Radios[comPreset.PresetRadio];
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
			return $"{Coordinate.ToStringDMS()}{Environment.NewLine}{Coordinate.ToStringDDM()}{Environment.NewLine}{Coordinate.ToStringMGRS()}";
		}
		#endregion
	}
}
