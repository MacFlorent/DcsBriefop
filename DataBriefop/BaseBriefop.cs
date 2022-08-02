namespace DcsBriefop.DataBriefop
{
	internal abstract class BaseBriefop
	{
		#region Properties
		public BriefopManager ParentManager { get; private set; }
		#endregion

		#region CTOR
		public BaseBriefop(BriefopManager parentManager)
		{
			ParentManager = parentManager;
		}
		#endregion

		#region Methods
		public virtual void PostInitialize() { }
		public virtual void Persist() { }
		#endregion
	}
}
