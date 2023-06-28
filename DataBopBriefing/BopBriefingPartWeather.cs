using DcsBriefop.Data;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartWeather : BaseBopBriefingPart
	{
		#region Properties
		#endregion

		#region CTOR
		public BopBriefingPartWeather() : base(ElementBriefingPartType.Weather, "table") { }
		#endregion

		#region Methods
		public override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
			HtmlTag tagTr = tagTable.Add("tr");
			tagTr.Add("th").AppendText("Weather");
			tagTr.Add("td").Append(bopManager.BopMission.Weather.ToString(bopBriefingFolder.WeatherDisplay, bopBriefingFolder.MeasurementSystem).HtmlLineBreaks());
			tags.Add(tagTable);
			return tags;
		}
		#endregion
	}
}
