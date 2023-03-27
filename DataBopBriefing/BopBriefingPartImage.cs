using DcsBriefop.Data;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartImage : BaseBopBriefingPart
	{
		#region Properties
		public string Header { get; set; }
		public string ImagePath { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartImage() : base(ElementBriefingPartType.Image, "content") { }
		#endregion

		#region Methods
		protected override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}

			string sImageFullPath = GetImageFullPath(bopManager);
			if(Path.Exists(sImageFullPath))
			{
				tags.Add(new HtmlTag("img").Attr("src", sImageFullPath));
			}

			return tags;
		}

		private string GetImageFullPath(BriefopManager bopManager)
		{
			if (string.IsNullOrEmpty(ImagePath))
				return null;
			else
				return Path.Join(bopManager.MizFileDirectory, ImagePath);
		}
		#endregion
	}
}
