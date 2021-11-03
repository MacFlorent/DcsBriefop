﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DcsBriefop.HtmlBuilder
{
	internal class HtmlDocument
	{
		private static class HtmlCodes
		{
			public static readonly string Space = "&nbsp;";
		}

		private StringBuilder m_builder = new StringBuilder();
		private List<string> m_openTags = new List<string>();

		private readonly int m_iParagraphPadding = 4;
		private readonly int m_iTablePadding = 5;

		private string m_sForeColorCoalition;
		private string m_sBackColorCoalition;
		private string m_sForeColor;
		private string m_sBackColor;


		public HtmlDocument(Color color)
		{
			m_sForeColorCoalition = ColorTranslator.ToHtml(color);
			m_sBackColorCoalition = ColorTranslator.ToHtml(ControlPaint.Light(color, 75f));

			m_sForeColor = ColorTranslator.ToHtml(Color.DimGray);
			m_sBackColor = $"{ColorTranslator.ToHtml(Color.WhiteSmoke)}";
			m_sBackColorCoalition = ColorTranslator.ToHtml(Color.LightGray);

			OpenTag("html");
			OpenTag("body", $"style=\"background-color:{m_sBackColor};\"");
		}

		public override string ToString()
		{
			return m_builder.ToString();
		}

		public void FinalizeDocument()
		{
			CloseTag(); // body
			CloseTag(); // html
		}

		public void AppendHeader(string sText, int iHeaderLevel)
		{
			string sAttributes = $"style=\"color:{m_sForeColorCoalition}; background-color:{m_sBackColorCoalition}; text-align:center; padding:{m_iParagraphPadding}px;\"";
			OpenTag($"h{iHeaderLevel}", sAttributes);
			AppendText(sText);
			CloseTag();
		}

		public void AppendParagraphJustified(string sText)
		{
			AppendParagraph(sText, "justified");
		}
		public void AppendParagraphCentered(string sText)
		{
			AppendParagraph(sText, "center");
		}

		public void AppendParagraph(string sText, string sTextAlign)
		{
			if (string.IsNullOrEmpty(sText))
				return;

			string sAttributes = $"style=\"color:{m_sForeColor}; background-color:{m_sBackColor}; text-align:{sTextAlign}; padding:{m_iParagraphPadding}px;\"";
			OpenTag("p", sAttributes);
			AppendText(sText);
			CloseTag();
		}

		public void OpenTable(params string[] headers)
		{
			OpenTag("table", "align=\"center\" style=\"border-collapse:collapse;\"");
			OpenTag("tr");
			foreach (string s in headers)
			{
				OpenTag($"th", $"style=\"color:{m_sForeColorCoalition}; background-color:{m_sBackColorCoalition}; padding:{m_iTablePadding}px; border: 1px solid {m_sForeColor};\"");
				AppendText(s);
				CloseTag();
			}
			CloseTag();
		}

		public void AppendTableRow(params string[] values)
		{
			OpenTag("tr");
			foreach (string s in values)
			{
				OpenTag($"td", $"style=\"color:{m_sForeColor}; background-color:{m_sBackColor}; padding:{m_iTablePadding}px; border: 1px solid {m_sForeColor};\"");
				AppendText(s);
				CloseTag();
			}
			CloseTag();
		}

		public void CloseTable()
		{
			CloseTag(); // table
		}

		public void AppendLineBreaks(int iCount)
		{
			for (int i = 0; i < iCount; i++)
			{
				OpenTag("p");
				AppendText(HtmlCodes.Space);
				CloseTag();
			}
		}

		public void AppendText(string sText)
		{
			if (!string.IsNullOrEmpty(sText))
				m_builder.Append(sText.Replace(Environment.NewLine, $"<br>{Environment.NewLine}"));
		}

		public void OpenTag(string sTag)
		{
			OpenTag(sTag, null);
		}
		public void OpenTag(string sTag, string sAttributes)
		{
			string sCorrectedAttributes = string.IsNullOrEmpty(sAttributes) ? "" : $" {sAttributes}";
			m_builder.Append(Environment.NewLine);
			m_builder.Append($"{GetIndentString()}<{sTag}{sCorrectedAttributes}>");
			m_openTags.Add(sTag);
		}

		public void CloseTag()
		{
			string sLastTag = m_openTags.LastOrDefault();
			if (!string.IsNullOrEmpty(sLastTag))
			{
				m_openTags.RemoveAt(m_openTags.Count - 1);
				m_builder.Append(Environment.NewLine);
				m_builder.Append($"{GetIndentString()}</{sLastTag}>");
			}
		}

		private string GetIndentString()
		{
			return new string(' ', m_openTags.Count);
		}
	}
}
