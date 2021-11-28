namespace DcsBriefop.Data
{
	internal abstract class BaseBriefing
	{
		#region Properties
		protected BriefingContext MissionContext { get; private set; }
		#endregion

		#region CTOR
		public BaseBriefing(BriefingContext missionContext)
		{
			MissionContext = missionContext;
		}
		#endregion

		#region Methods
		public virtual void Persist() { }
		#endregion
	}
}
