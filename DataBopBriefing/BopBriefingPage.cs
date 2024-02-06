using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.net.Tools;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using HtmlTags;
using PuppeteerSharp;
using System.Text.RegularExpressions;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPage : IEquatable<BopBriefingPage>
	{
		#region Fields
		#endregion

		#region Properties
		public Guid Guid { get; set; }
		public string Title { get; set; }
		public bool DisplayTitle { get; set; }
		public ElementBriefingPageRender Render { get; set; } = (ElementBriefingPageRender.Map | ElementBriefingPageRender.Html);
		public bool MapIncludeBaseOverlays { get; set; } = true;
		public int HtmlFontSize { get; set; } = 16;

		public List<BaseBopBriefingPart> Parts { get; set; } = new List<BaseBopBriefingPart>();
		public MizBopMap MapData { get; set; } = new MizBopMap();
		#endregion

		#region CTOR
		public BopBriefingPage()
		{
			Guid = Guid.NewGuid();
		}
		#endregion

		#region Methods
		public BaseBopBriefingPart AddPart(ElementBriefingPartType briefingPartType)
		{
			BaseBopBriefingPart bopBriefingPart = null;

			if (briefingPartType == ElementBriefingPartType.Paragraph)
				bopBriefingPart = new BopBriefingPartParagraph();
			else if (briefingPartType == ElementBriefingPartType.Sortie)
				bopBriefingPart = new BopBriefingPartSortie();
			else if (briefingPartType == ElementBriefingPartType.Description)
				bopBriefingPart = new BopBriefingPartDescription();
			else if (briefingPartType == ElementBriefingPartType.Task)
				bopBriefingPart = new BopBriefingPartTask();
			else if (briefingPartType == ElementBriefingPartType.Airbases)
				bopBriefingPart = new BopBriefingPartAirbases();
			else if (briefingPartType == ElementBriefingPartType.Groups)
				bopBriefingPart = new BopBriefingPartGroups();
			else if (briefingPartType == ElementBriefingPartType.Waypoints)
				bopBriefingPart = new BopBriefingPartWaypoints();
			else if (briefingPartType == ElementBriefingPartType.Image)
				bopBriefingPart = new BopBriefingPartImage();
			else if (briefingPartType == ElementBriefingPartType.TableText)
				bopBriefingPart = new BopBriefingPartTableText();

			if (bopBriefingPart is not null)
			{
				bopBriefingPart.InitializeDefault();
				Parts.Add(bopBriefingPart);
			}

			return bopBriefingPart;
		}

		public void OrderPart(BaseBopBriefingPart bopBriefingPart, int iWay)
		{
			if (iWay < 0)
				iWay = -1;
			else
				iWay = 1;

			int iCurrentIndex = Parts.IndexOf(bopBriefingPart);
			if (iCurrentIndex < 0)
				return;

			int iNewIndex = iCurrentIndex + iWay;
			if (iNewIndex >= 0 && iNewIndex < Parts.Count)
			{
				Parts.Remove(bopBriefingPart);
				Parts.Insert(iNewIndex, bopBriefingPart);
			}
		}
		#endregion

		#region Html
		public async Task<Image> BuildHtmlImage(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			Image image = null;
			string sHtml = BuildHtmlString(bopManager, bopBriefingFolder);
			using (HtmlImageRenderer renderer = new HtmlImageRenderer())
			{
				ScreenshotOptions screenshotOptions = new ScreenshotOptions() { Type = ScreenshotType.Png };
				ViewPortOptions viewPortOptions = new ViewPortOptions() { Height = bopBriefingFolder.ImageSize.Height, Width = bopBriefingFolder.ImageSize.Width };

				image = await renderer.RenderImageAsync(sHtml, screenshotOptions, viewPortOptions); // ConfigureAwait(false); https://devblogs.microsoft.com/dotnet/configureawait-faq/
			}

			return image;
		}

		public string BuildHtmlString(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			return BuildHtml(bopManager, bopBriefingFolder)?.ToString();
		}

		public HtmlTags.HtmlDocument BuildHtml(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTags.HtmlDocument html = new();
			html.Head.Append(BuildHtmlStyle(bopBriefingFolder));
			html.Head.Append(BuildHtmlScript(ElementBopResource.JsCharts));
			html.Head.Append(BuildHtmlScript(ElementBopResource.JsChartsWaypointBuilder));

			if (DisplayTitle)
				html.Body.Append(BuildHtmlBodyHeader(bopManager, bopBriefingFolder));

			html.Body.Append(BuildHtmlBodyMain(bopManager, bopBriefingFolder));

			return html;
		}

		private HtmlTag BuildHtmlStyle(BopBriefingFolder bopBriefingFolder)
		{
			HtmlTag tag = new("style");

			string sStyle = BopBriefingStyle.GetElement(bopBriefingFolder.HtmlCssStyle)?.GetCss() ?? BopBriefingStyle.GetCssDefault();
			if (!string.IsNullOrEmpty(sStyle))
			{
				sStyle = Regex.Replace(sStyle, @"--base-font-size *: *12 *;", $"--base-font-size:{HtmlFontSize}px;");
				tag.AppendHtml(sStyle);
			}

			return tag;
		}

		private HtmlTag BuildHtmlScript(string sResourceJs)
		{
			HtmlTag tag = new("script");
			string sStyle = ToolsResources.GetTextResourceContent(sResourceJs, "js", ElementBopResource.DirectoryHtml);
			return tag.AppendHtml(sStyle);
		}

		private HtmlTag BuildHtmlBodyHeader(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTag tag = new HtmlTag("header");
			tag.Add("h1").AppendText(Title);
			return tag;
		}

			private HtmlTag BuildHtmlBodyMain(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTag tag = new("main");

			bool bHr = false;
			foreach (BaseBopBriefingPart part in Parts)
			{
				IEnumerable<HtmlTag> partTags = part.BuildHtmlContent(bopManager, bopBriefingFolder);
				if (partTags is not null && partTags.Any())
				{
					if (bHr)
						tag.Add("hr");
					else
						bHr = true;

					tag.Append(partTags);

				}
			}

			return tag;
		}
		#endregion

		#region Map
		public Image BuildMapImage(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			GMapProvider mapProvider = GMapProviders.TryGetProvider(bopManager.BopMission.PreferencesMap.ProviderName);
			return ToolsMap.GenerateMapImage(MapData, mapProvider, GetMapAdditionalOverlays(bopManager, bopBriefingFolder), bopBriefingFolder.ImageSize);
		}

		public IEnumerable<GMapOverlay> GetMapAdditionalOverlays(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> additionalOverlays = new List<GMapOverlay>();

			if (MapIncludeBaseOverlays)
			{
				additionalOverlays.Add(bopManager.BopMission.BuildStaticMapOverlay());
				additionalOverlays.Add(bopManager.BopMission.MapData.BuildCustomMapOverlay());
				if (bopManager.BopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName ?? "", out BopCoalition bopCoalition))
				{
					additionalOverlays.Add(bopCoalition.BuildStaticMapOverlay());
					additionalOverlays.Add(bopCoalition.MapData.BuildCustomMapOverlay());
				}
			}

			foreach (BaseBopBriefingPart bopBriefingPart in Parts)
			{
				IEnumerable<GMapOverlay> partOverlays = bopBriefingPart.BuildMapOverlays(bopManager, bopBriefingFolder);
				if (partOverlays is not null)
					additionalOverlays.AddRange(partOverlays);
			}

			return additionalOverlays;
		}
		#endregion

		#region IEquatable
		public bool Equals(BopBriefingPage other)
		{
			if (other is null)
				return false;

			return (Guid == other.Guid);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BopBriefingPage);
		}

		public override int GetHashCode() => Guid.GetHashCode();
		#endregion
	}
}
