using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
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
		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
			HtmlTag tagTr = tagTable.Add("tr");
			tagTr.Add("td").AddClass("header").AppendText("Weather");
			tagTr.Add("td").Append(bopMission.Weather.ToString(bopBriefingFolder.WeatherDisplay, bopBriefingFolder.MeasurementSystem).HtmlLineBreaks());
			tags.Add(tagTable);
			return tags;
		}
		#endregion
	}
}
