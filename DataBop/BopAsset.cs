using DcsBriefop.Data;
using DcsBriefop.Map;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBop
{
	internal abstract class BopAsset : BaseBop
	{
		#region Properties
		public string CoalitionName { get; protected set; }
		public string CountryName { get; protected set; }

		public virtual ElementDcsObjectClass Class { get; protected set; }
		public ElementAssetFunction Function { get; set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Information { get; set; }
		public string Type { get; set; }
		public string Task { get; set; }

		public string MapMarker { get; set; }
		public List<BopMapPoint> MapPoints { get; private set; }
		#endregion

		#region CTOR
		public BopAsset(BopManager parentManager, string sCoalitionName, string sCountryName) : base(parentManager)
		{
			CoalitionName = sCoalitionName;
			CountryName = sCountryName;

			Class = ElementDcsObjectClass.None;
			Function = ElementAssetFunction.Other;
			
			MapMarker = ElementMapTemplateMarker.Mark;
			MapPoints = new List<BopMapPoint>();
		}
		#endregion

		#region Methods
		public virtual string ToStringLocalisation()
		{
			string sLocalisation = "";
			BopMapPoint point = MapPoints.FirstOrDefault();
			if (point is object)
			{
				//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
				sLocalisation = point.ToStringLocalisation();
			}

			return sLocalisation;
		}
		#endregion
	}
}
