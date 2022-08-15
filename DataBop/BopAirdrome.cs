using CoordinateSharp;
using DcsBriefop.Data;
using System.Collections.Generic;

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
		public Coordinate Coordinate { get; set; }
		public string Information { get; set; }
		public List<Radio> Radios { get; set; }
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopAirdrome(BopManager parentManager, Airdrome airdrome) : base(parentManager)
		{
			m_airdrome = airdrome;

			Id = m_airdrome.Id;
			Name = m_airdrome.Name;
			Coordinate = new Coordinate(m_airdrome.Latitude, m_airdrome.Longitude);
			Information = m_airdrome.Tacan?.ToString();
			Tacan = m_airdrome.Tacan;
		}
		#endregion

		#region Initialize & Persist
		public override void Persist()
		{
		}
		#endregion

		#region Methods
		public virtual string ToStringLocalisation()
		{
			return ParentManager.BopMain.ToStringLocalisation(Coordinate);
		}
		#endregion
	}
}
