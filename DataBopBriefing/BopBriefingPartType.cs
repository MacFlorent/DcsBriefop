namespace DcsBriefop.DataBopBriefing
{
	internal static class ElementBriefingPartType
	{
		public static readonly string Bullseye = "Bullseye";
		public static readonly string Paragraph = "Paragraph";
		public static readonly string Sortie = "Sortie";
		public static readonly string Description = "Description";
		public static readonly string Task = "Task";
	}

	internal class BopBriefingPartType
	{
		public string Name { get; set; }
		public Type ClassType { get; set; }

		public static List<BopBriefingPartType> BopBriefingPartTypes { get; set; }

		static BopBriefingPartType()
		{
			BopBriefingPartTypes = new List<BopBriefingPartType>()
			{
				new BopBriefingPartType() { Name = ElementBriefingPartType.Bullseye, ClassType = typeof(BopBriefingPartBullseye) },
				new BopBriefingPartType() { Name = ElementBriefingPartType.Paragraph, ClassType = typeof(BopBriefingPartParagraph) },
				new BopBriefingPartType() { Name = ElementBriefingPartType.Sortie, ClassType = typeof(BopBriefingPartSortie) },
				new BopBriefingPartType() { Name = ElementBriefingPartType.Description, ClassType = typeof(BopBriefingPartDescription) },
				new BopBriefingPartType() { Name = ElementBriefingPartType.Task, ClassType = typeof(BopBriefingPartTask) }
			};
		
		}
	}
}
