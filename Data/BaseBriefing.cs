using DcsBriefop.DataMiz;

namespace DcsBriefop.Data
{
	internal abstract class BaseBriefing
	{
		#region Properties
		public BaseBriefingCore Core { get; private set; }
		#endregion

		#region CTOR
		public BaseBriefing(BaseBriefingCore core)
		{
			Core = core;
		}
		#endregion

		#region Methods
		public virtual void Persist() { }
		#endregion
	}

	internal class BaseBriefingCore
	{
		public Miz Miz { get; private set; }
		public Theatre Theatre { get; private set; }

		public BaseBriefingCore(Miz miz)
		{
			Miz = miz;
			Theatre = new Theatre(Miz.RootMission.Theatre);
		}
	}
}
