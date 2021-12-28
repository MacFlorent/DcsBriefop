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
		#endregion

		#region Properties
		public AssetGroup AssetGroup { get; set; }
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

			Id = m_unit.Id;
			Name = m_unit.Name;
			Type = m_unit.Type;
			Coordinate = Core.Theatre.GetCoordinate(m_unit.Y, m_unit.X);
			Id = m_unit.Id;

			DcsUnit dcsUnit = DcsUnitManager.GetUnit(Type);
			if (dcsUnit is object)
			{
				Description = dcsUnit.Description;
				Information = dcsUnit.Information;
			}
		}
		#endregion

		#region Methods
		public virtual string GetLocalisation()
		{
			return $"{Coordinate.ToStringDMS()}{Environment.NewLine}{Coordinate.ToStringDDM()}{Environment.NewLine}{Coordinate.ToStringMGRS()}";
		}
		#endregion
	}
}
