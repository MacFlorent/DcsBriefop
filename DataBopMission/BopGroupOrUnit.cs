using CoordinateSharp;
using DcsBriefop.Data;
using GMap.NET.WindowsForms;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupOrUnit : IEquatable<BopGroupOrUnit>
	{
		#region Properties
		public BopGroup BopGroup { get; set; }
		public BopUnit BopUnit { get; set; }

		public int Id { get { return BopUnit?.Id ?? BopGroup.Id; } }
		public string DisplayName { get { return BopUnit?.ToStringDisplayName() ?? BopGroup.ToStringDisplayName(); } }
		public string Coalition { get { return BopGroup.CoalitionName; } }
		public string Country { get { return BopGroup.CountryName; } }
		public ElementGroupOrUnit GroupOrUnit { get { return BopUnit is not null ? ElementGroupOrUnit.Unit : ElementGroupOrUnit.Group; } }
		public string Group { get { return BopGroup.Name; } }
		public string Type { get { return BopUnit?.Type ?? BopGroup.Type; } }
		public ElementGroupClass GroupClass { get { return BopUnit?.GroupClass ?? BopGroup.GroupClass; } }
		public ElementDcsObjectAttribute Attributes { get { return BopUnit?.Attributes ?? BopGroup.Attributes; } }
		public Radio Radio { get { return BopGroup.Radio; } }
		public string Additional { get { return BopUnit?.ToStringAdditional() ?? BopGroup.ToStringAdditional(); } }
		public Coordinate Coordinate { get { return BopUnit?.Coordinate ?? BopGroup.Coordinate; } }
		#endregion

		#region CTOR
		#endregion

		#region Methods
		public void FinalizeFromMiz()
		{
			BopGroup?.FinalizeFromMiz();
			BopUnit?.FinalizeFromMiz();
		}

		public GMapOverlay GetMapOverlay()
		{
			if (BopUnit is not null)
				return BopUnit.GetMapOverlay();
			else
				return BopGroup.GetMapOverlay();
		}

		public string ToStringLocalisation(ElementCoordinateDisplay coordinateDisplay, ElementMeasurementSystem? measurementSystem)
		{
			if (BopUnit is not null)
				return BopUnit.ToStringLocalisation(coordinateDisplay, measurementSystem);
			else
				return BopGroup.ToStringLocalisation(coordinateDisplay, measurementSystem);
		}
		#endregion

		#region IEquatable
		public bool Equals(BopGroupOrUnit other)
		{
			if (other is null)
				return false;

			return ((BopGroup?.Id).GetValueOrDefault() == (other.BopGroup?.Id).GetValueOrDefault()
						&&
						(BopUnit?.Id).GetValueOrDefault() == (other.BopUnit?.Id).GetValueOrDefault());
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BopGroupOrUnit);
		}

		public override int GetHashCode() => ((BopGroup?.Id).GetValueOrDefault(), (BopUnit?.Id).GetValueOrDefault()).GetHashCode();
		#endregion
	}
}
