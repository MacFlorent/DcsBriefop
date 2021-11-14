using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
using DcsBriefop.Tools;
using System;

namespace DcsBriefop.Briefing
{
	internal class BriefingUnit : BaseBriefing
	{
		#region Fields
		private Unit m_unit;
		#endregion

		#region Properties
		public Asset Asset { get; private set; }
		public int Id { get { return m_unit.Id; } }
		public string Name { get { return m_unit.Name; } }
		public string Type { get { return m_unit.Type; } }
		public Coordinate Coordinate { get { return Theatre.GetCoordinate(m_unit.Y, m_unit.X); } }

		public string Description { get; set; }
		public string Information { get; set; }
		#endregion

		#region CTOR
		public BriefingUnit(BriefingPack briefingPack, AssetGroup asset, Unit unit) : base(briefingPack)
		{
			m_unit = unit;
			Asset = asset;

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
