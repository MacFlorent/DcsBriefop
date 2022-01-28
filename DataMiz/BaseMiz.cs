using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal abstract class BaseMiz
	{
		public LsonDict Lsd { get; set; }

		public BaseMiz(LsonDict lsd)
		{
			Lsd = lsd;
			FromLua();
		}

		public abstract void FromLua();
		public abstract void ToLua();
	}
}
