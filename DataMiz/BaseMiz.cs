using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal abstract class BaseMiz
	{
		protected LsonDict m_lsd;

		public BaseMiz(LsonDict lsd)
		{
			m_lsd = lsd;
			FromLua();
		}

		public abstract void FromLua();
		public abstract void ToLua();
	}
}
