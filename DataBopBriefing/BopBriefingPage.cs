using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.net.Tools;
using DcsBriefop.Tools;
using HtmlTags;
using PuppeteerSharp;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPage
	{
		#region Fields
		#endregion

		#region Properties
		public string Title { get; set; }
		public bool DisplayTitle { get; set; }
		public ElementBriefingPageRender Render { get; set; } = (ElementBriefingPageRender.Map | ElementBriefingPageRender.Html);
		
		public List<BopBriefingPartBase> Parts { get; set; } = new List<BopBriefingPartBase>();
		public MizBopMap MapData { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPage() { }
		#endregion

		#region Methods
		public BopBriefingPartBase AddPart(string sPartType)
		{
			BopBriefingPartBase bopBriefingPart = null;
			
			if (sPartType == ElementBriefingPartType.Bullseye)
				bopBriefingPart = new BopBriefingPartBullseye();
			else if (sPartType == ElementBriefingPartType.Paragraph)
				bopBriefingPart = new BopBriefingPartParagraph();
			else if (sPartType == ElementBriefingPartType.Sortie)
				bopBriefingPart = new BopBriefingPartSortie();
			else if(sPartType == ElementBriefingPartType.Description)
				bopBriefingPart = new BopBriefingPartDescription();
			else if(sPartType == ElementBriefingPartType.Task)
				bopBriefingPart = new BopBriefingPartTask();

			if (bopBriefingPart is object)
			{
				Parts.Add(bopBriefingPart);
			}

			return bopBriefingPart;
		}
		#endregion

		#region Html
		public async Task<Image> BuildHtmlImage(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			Image image = null;
			string sHtml = BuildHtmlString(bopMission, bopBriefingFolder);
			using (HtmlImageRenderer renderer = new HtmlImageRenderer())
			{
				ScreenshotOptions screenshotOptions = new ScreenshotOptions() { Type = ScreenshotType.Png };
				ViewPortOptions viewPortOptions = new ViewPortOptions() { Height = bopBriefingFolder.ImageSize.Height, Width = bopBriefingFolder.ImageSize.Width };

				image = await renderer.RenderImageAsync(sHtml, screenshotOptions, viewPortOptions);
			}

			return image;
		}

		public string BuildHtmlString(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			return BuildHtml(bopMission, bopBriefingFolder)?.ToString();
		}

		public HtmlTags.HtmlDocument BuildHtml(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTags.HtmlDocument html = new();
			html.Head.Append(BuildHtmlStyle());
			html.Body.Append(BuildHtmlWrapper(bopMission, bopBriefingFolder));

			return html;
		}

		private HtmlTag BuildHtmlStyle()
		{
			HtmlTag tag = new("style");
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "css");
			return tag.AppendHtml(sStyle);
		}

		private HtmlTag BuildHtmlWrapper(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTag tag = new HtmlTag("div").Id("wrapper");
			if (DisplayTitle)
			{
				tag.Add("div").AddClass("header").Add("h1").AppendText(Title);
			}

			foreach (BopBriefingPartBase part in Parts)
			{
				HtmlTag tagPart = part.BuildHtml(bopMission, bopBriefingFolder);
				if (tagPart is object)
				{
					tag.Append(tagPart);
				}
			}

			return tag;
		}
		#endregion

	}
}
