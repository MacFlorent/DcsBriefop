using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.net.Tools;
using DcsBriefop.Tools;
using Maroontress.Html;
using PuppeteerSharp;

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
		public ElementBriefingPageContent PageContent { get; set; } = (ElementBriefingPageContent.Map | ElementBriefingPageContent.Html);
		public List<BopBriefingPart> Parts { get; set; } = new List<BopBriefingPart>();
		#endregion

		#region CTOR
		public BopBriefingPage(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;
		}
		#endregion

		#region Methods
		public async Task<Image> BuildHtmlImage()
		{
			Image image = null;
			string sHtml = BuildHtmlContentString();
			using (HtmlImageRenderer i = new HtmlImageRenderer())
			{
				ScreenshotOptions screenshotOptions = new ScreenshotOptions() { Type = ScreenshotType.Png };
				ViewPortOptions viewPortOptions = new ViewPortOptions() { Height = m_bopBriefingFolder.ImageSize.Height, Width = m_bopBriefingFolder.ImageSize.Width };

				image = await i.RenderImageAsync(sHtml, screenshotOptions, viewPortOptions);
			}

			return image;
		}

		public string BuildHtmlContentString()
		{
			Tag tagHtml = BuildHtmlContent();
			return tagHtml.ToString(FormatOptions.DefaultIndent);
		}

		public Tag BuildHtmlContent()
		{
			NodeFactory nodeOf = Nodes.NewFactory();
			return nodeOf.Html.Add
			(
				BuildHtmlContentHead(nodeOf),
				BuildHtmlContentBody(nodeOf)
			);
		}

		private Tag BuildHtmlContentHead(NodeFactory nodeOf)
		{
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "css");
			return nodeOf.Head.Add (nodeOf.Style.Add(sStyle));
		}

		private Tag BuildHtmlContentBody(NodeFactory nodeOf)
		{
			return nodeOf.Body.Add (BuildHtmlContentWrapper(nodeOf));
		}

		private Tag BuildHtmlContentWrapper(NodeFactory nodeOf)
		{
			Tag tagWrapper = nodeOf.Div.WithId("wrapper");
			if (DisplayTitle)
			{
				tagWrapper = tagWrapper.Add
				(
					nodeOf.Div.WithClass("header").Add(nodeOf.H1).Add(Title)
				);
			}

			foreach (BopBriefingPart part in Parts)
			{
				Tag tagPart = part.BuildHtml(nodeOf);
				if (tagPart is object)
				{
					tagWrapper = tagWrapper.Add(tagPart);
				}
			}

			return tagWrapper;
		}
		#endregion

	}
}
