namespace DcsBriefop.DataMiz
{
	internal class MizBopUnit : BaseMizBopWithDefault
	{
		#region Properties
		public int Id { get; set; }
		public string MapMarker { get; set; }
		#endregion

		#region Methods
		public override bool IsDefaultData()
		{
			if (!string.IsNullOrEmpty(MapMarker))
				return false;

			return true;
		}
		#endregion
	}
}
