using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal abstract class BaseBop
	{
		#region Fields
		private bool m_bIsFinalizedFromMiz = false;
		#endregion

		#region Properties
		public Miz Miz { get; private set; }
		public Theatre Theatre { get; private set; }
		#endregion

		#region CTOR
		public BaseBop(Miz miz, Theatre theatre)
		{
			Miz = miz;
			Theatre = theatre;
		}
		#endregion

		#region Miz
		public virtual void ToMiz() { }
		public void FinalizeFromMiz()
		{
			if (m_bIsFinalizedFromMiz)
				return;

			FinalizeFromMizInternal();
			m_bIsFinalizedFromMiz = true;
		}

		protected virtual void FinalizeFromMizInternal() { }
		#endregion
	}
}
