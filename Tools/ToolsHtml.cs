using HtmlTags;

namespace DcsBriefop.Tools
{
	internal static class ToolsHtml
	{
		public static List<HtmlTag> HtmlLineBreaks(this string sText)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			string[] strings = sText.Split(Environment.NewLine);
			foreach (string s in strings)
			{
				HtmlTag tag = new HtmlTag("div").AppendText(s);
				tags.Add(tag);
			}

			return tags;
		}
	}
}
