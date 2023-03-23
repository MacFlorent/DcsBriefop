namespace DcsBriefop.DataMiz
{
	internal class MizBopRoutePoint : BaseMizBopWithDefault
	{
		#region Properties
		public int GroupId { get; set; }
		public int Number { get; set; }
		public string Notes { get; set; }
		#endregion

		#region Methods
		public override bool IsDefaultData()
		{
			return string.IsNullOrEmpty(Notes);
		}
		#endregion
	}
}
