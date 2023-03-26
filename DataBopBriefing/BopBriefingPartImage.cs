using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartImage : BaseBopBriefingPart
	{
		#region Properties
		public string Header { get; set; }
		public string ImageName { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartImage() : base(ElementBriefingPartType.Image, "content") { }
		#endregion

		#region Methods
		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}

			return tags;
		}

		//private Image LoadImage()
		//{
		//	using (ZipArchive za = ZipFile.OpenRead(MizFilePath))
		//	{
		//		foreach (ZipArchiveEntry entry in za.Entries)

		//}
		#endregion
	}
}
