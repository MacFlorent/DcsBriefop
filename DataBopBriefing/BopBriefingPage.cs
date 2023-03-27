using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.net.Tools;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using HtmlTags;
using log4net;
using PuppeteerSharp;

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

			if (briefingPartType == ElementBriefingPartType.Bullseye)
				bopBriefingPart = new BopBriefingPartBullseye();
			else if (briefingPartType == ElementBriefingPartType.Paragraph)
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
			else if (briefingPartType == ElementBriefingPartType.Weather)
				bopBriefingPart = new BopBriefingPartWeather();

			if (bopBriefingPart is object)
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
			html.Head.Append(BuildHtmlStyle());
			html.Head.Append(BuildHtmlScript());
			html.Body.Append(BuildHtmlWrapper(bopManager, bopBriefingFolder));

			return html;
		}

		private HtmlTag BuildHtmlStyle()
		{
			HtmlTag tag = new("style");
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "css");
			return tag.AppendHtml(sStyle);
		}

		private HtmlTag BuildHtmlScript()
		{
			HtmlTag tag = new("script");
			string sStyle = ToolsResources.GetTextResourceContent("briefingTemplate", "js");
			return tag.AppendHtml(sStyle);
		}

		private HtmlTag BuildHtmlWrapper(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			HtmlTag tag = new HtmlTag("div").Id("wrapper");
			if (DisplayTitle)
			{
				tag.Add("div").AddClass("header").Add("h1").AppendText(Title);
			}

			foreach (BaseBopBriefingPart part in Parts)
			{
				HtmlTag tagPart = part.BuildHtml(bopManager, bopBriefingFolder);
				if (tagPart is object)
				{
					tag.Append(tagPart);
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
				additionalOverlays.Add(bopManager.BopMission.MapOverlay);
				if (bopManager.BopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName ?? "", out BopCoalition bopCoalition))
				{
					additionalOverlays.Add(bopCoalition.MapOverlay);
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
