using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.Linq;

namespace DcsBriefop.DataBriefop
{
	internal class BriefopAssetUnit : BaseBriefop
	{
		#region Fields
		private MizUnit m_mizUnit;
		private BriefopAssetGroup m_parentAssetGroup;
		private DcsObject m_dcsObject;
		#endregion

		#region Properties
		public ElementDcsObjectClass Class { get { return m_dcsObject?.Class ?? ElementDcsObjectClass.None; } }
		public ElementDcsObjectAttribute Attributes { get { return m_dcsObject?.Attributes ?? ElementDcsObjectAttribute.None; } }
		public bool MainInGroup { get { return (m_dcsObject?.MainInGroup).GetValueOrDefault(); } }
		public string MapMarker { get { return m_dcsObject?.MapMarker; } }


		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get { return m_dcsObject?.DisplayName ?? Name; } }
		public string Type { get; set; }
		public bool Playable { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public BriefopAssetUnit(BriefopManager parentManager, MizUnit mizUnit, BriefopAssetGroup parentGroup) : base(parentManager)
		{
			m_mizUnit = mizUnit;
			m_dcsObject = DcsObjectManager.GetObject(m_mizUnit.Type);
			m_parentAssetGroup = parentGroup;

			Id = m_mizUnit.Id;
			Name = m_mizUnit.Name;
			Type = m_mizUnit.Type;
			Playable = (m_mizUnit.Skill == ElementSkill.Player || m_mizUnit.Skill == ElementSkill.Client);

			Coordinate = ParentManager.Theatre.GetCoordinate(m_mizUnit.Y, m_mizUnit.X);
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			//if (MizUnit.Radios is object && MizUnit.Radios.Length > 0 && AssetGroup.Coalition.ComPresets is object && AssetGroup.Coalition.ComPresets.Count > 0)
			//{
			//	foreach (ComPreset comPreset in AssetGroup.Coalition.ComPresets.Where(_cp => _cp.Radio is object))
			//	{
			//		if (MizUnit.Radios.Length > comPreset.PresetRadio)
			//		{
			//			MizRadio mizRadio = MizUnit.Radios[comPreset.PresetRadio];
			//			if (comPreset.PresetNumber < mizRadio.Modulations.Length)
			//				mizRadio.Modulations[comPreset.PresetNumber] = comPreset.Radio.Modulation;
			//			if (comPreset.PresetNumber < mizRadio.Channels.Length)
			//				mizRadio.Channels[comPreset.PresetNumber] = comPreset.Radio.Frequency;
			//		}
			//	}
			//}
		}

		public virtual string ToStringLocalisation()
		{
			return $"{Coordinate.ToStringMGRS()}{Environment.NewLine}{AltitudeFeet} ft";
		}
		#endregion
	}
}
