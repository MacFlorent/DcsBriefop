using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.net.Tools;
using DcsBriefop.Tools;
using HtmlTags;
using PuppeteerSharp;
using System.Xml.Linq;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPage
	{
		#region Fields
		private BopMission m_bopMission;
		private BopBriefingFolder m_bopBriefingFolder;
		#endregion

		#region Properties
		public string Title { get; set; }
		public bool DisplayTitle { get; set; }
		public ElementBriefingPageRender Render { get; set; } = (ElementBriefingPageRender.Map | ElementBriefingPageRender.Html);
		public List<BopBriefingPart> Parts { get; set; } = new List<BopBriefingPart>();
		public MizBopMap MapData { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPage(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;
		}
		#endregion

		#region Methods
		public BopBriefingPart AddPart(string sPartType)
		{
			BopBriefingPart bopBriefingPart = null;
			
			if (sPartType == ElementBriefingPartType.Bullseye)
				bopBriefingPart = new BopBriefingPartBullseye(m_bopMission, m_bopBriefingFolder);
			else if (sPartType == ElementBriefingPartType.Paragraph)
				bopBriefingPart = new BopBriefingPartParagraph(m_bopMission, m_bopBriefingFolder);
			else if (sPartType == ElementBriefingPartType.Sortie)
				bopBriefingPart = new BopBriefingPartSortie(m_bopMission, m_bopBriefingFolder);
			else if(sPartType == ElementBriefingPartType.Description)
				bopBriefingPart = new BopBriefingPartDescription(m_bopMission, m_bopBriefingFolder);
			else if(sPartType == ElementBriefingPartType.Task)
				bopBriefingPart = new BopBriefingPartTask(m_bopMission, m_bopBriefingFolder);

			if (bopBriefingPart is object)
			{
				Parts.Add(bopBriefingPart);
			}

			return bopBriefingPart;
		}
		#endregion

		#region Html
		public async Task<Image> BuildHtmlImage()
		{
			Image image = null;
			string sHtml = BuildHtmlString();
			using (HtmlImageRenderer renderer = new HtmlImageRenderer())
			{
				ScreenshotOptions screenshotOptions = new ScreenshotOptions() { Type = ScreenshotType.Png };
				ViewPortOptions viewPortOptions = new ViewPortOptions() { Height = m_bopBriefingFolder.ImageSize.Height, Width = m_bopBriefingFolder.ImageSize.Width };

				image = await renderer.RenderImageAsync(sHtml, screenshotOptions, viewPortOptions);
			}

			return image;
		}

		public string BuildHtmlString()
		{
			return BuildHtml()?.ToString();
		}

		public HtmlTags.HtmlDocument BuildHtml()
		{
			HtmlTags.HtmlDocument html = new();
			html.Head.Append(BuildHtmlStyle());
			html.Body.Append(BuildHtmlWrapper());

			return html;
		}

		private HtmlTag BuildHtmlStyle()
		{
			HtmlTag tag = new("style");
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "css");
			return tag.AppendHtml(sStyle);
		}

		private HtmlTag BuildHtmlWrapper()
		{
			HtmlTag tag = new HtmlTag("div").Id("wrapper");
			if (DisplayTitle)
			{
				tag.Add("div").AddClass("header").Add("h1").AppendText(Title);
			}

			foreach (BopBriefingPart part in Parts)
			{
				HtmlTag tagPart = part.BuildHtml();
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
