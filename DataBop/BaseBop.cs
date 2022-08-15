namespace DcsBriefop.DataBop
{
	internal abstract class BaseBop
	{
		#region Properties
		public BopManager ParentManager { get; private set; }
		#endregion

		#region CTOR
		public BaseBop(BopManager parentManager)
		{
			ParentManager = parentManager;
		}
		#endregion

		#region Initialize & Persist
		public virtual void PostInitialize() { }
		public virtual void Persist() { }
		#endregion
	}
}
