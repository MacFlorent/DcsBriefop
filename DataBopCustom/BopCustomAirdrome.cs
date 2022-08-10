using DcsBriefop.Data;
using System.Collections.Generic;

namespace DcsBriefop.DataBopCustom
{
	internal class BopCustomAirdrome : BaseBopCustomWithDefault
	{
		public int Id { get; set; }
		public string Information { get; set; }
		public List<Radio> Radios { get; set; }

		public BopCustomAirdrome(int iId)
		{
			Id = iId;
		}
	}

}
