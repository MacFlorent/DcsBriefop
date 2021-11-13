namespace DcsBriefop.Briefing
{
	internal class CustomDataMission : CustomDataBase
	{
		public int Id { get; set; }
		public string Coalition { get; set; }
		public string MissionInformation { get; set; }

		public CustomDataMap MapData { get; set; }

		public CustomDataMission(int iId, string sCoalition)
		{
			Id = iId;
			Coalition = sCoalition;
		}
	}
}
