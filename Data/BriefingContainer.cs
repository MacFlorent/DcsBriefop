using DcsBriefop.DataMiz;
using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class BriefingContainer
	{
		#region Properties
		public BriefingContext MissionContext { get; private set; }
		public List<BriefingCoalition2> BriefingFolders { get; private set; } = new List<BriefingCoalition2>();
		#endregion

		#region CTOR
		public BriefingContainer(Miz miz)
		{
			MissionContext = new BriefingContext(miz);
		}
		#endregion

		#region Methods
		public void Persist()
		{
			MissionContext.Persist();
		}
		#endregion
	}
}
