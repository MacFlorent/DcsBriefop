using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Map;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBop
{
	internal class BopAirdrome : BaseBop
	{
		#region Fields
		private Airdrome m_airdrome;
		#endregion

		#region Properties
		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Information { get; set; }
		public string Type { get; set; }
		public string Task { get; set; }

		public string MapMarker { get; set; }

		public Coordinate Coordinate { get; set; }
	

		#endregion

		#region CTOR
		public BopAirdrome(BopManager parentManager, Airdrome airdrome) : base(parentManager)
		{
			m_airdrome = airdrome;
			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
}
		#endregion

		#region Methods
		public virtual string ToStringLocalisation()
		{
			string sLocalisation = "";
			//BopMapPoint point = MapPoints.FirstOrDefault();
			//if (point is object)
			//{
			//	//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			//	sLocalisation = point.ToStringLocalisation();
			//}

			return sLocalisation;
		}
		#endregion
	}
}
