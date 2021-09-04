using LsonLib;

namespace DcsBriefop.LsonStructure
{
	internal abstract class BaseLsonStructure
	{
		protected LsonDict m_lsd;

		public BaseLsonStructure(LsonDict lsd)
		{
			m_lsd = lsd;
			FromLua();
		}

		public abstract void FromLua();
		public abstract void ToLua();
	}
}
