using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.Tools
{
	internal static class ToolsEPPlus
	{
		public static ExcelWorksheet GetWorksheet(ExcelWorkbook ew, string sWorksheetName)
		{
			if (ew.Worksheets[sWorksheetName] is null)
			{
				ew.Worksheets.Add(sWorksheetName);
			}
			return ew.Worksheets[sWorksheetName];
		}

		public static int? FindRowNum(ExcelWorksheet ews, object oKey, string sColumnKey)
		{
			if (ews is null || oKey is null || string.IsNullOrEmpty(sColumnKey))
				return null;

			if (oKey is int) // numbers are always double in the Excel data
				oKey = Convert.ToDouble(oKey);

			for (int iRowNum = 1; iRowNum <= ews.Dimension.End.Row; iRowNum++)
			{
				if (oKey.Equals(ews.Cells[$"{sColumnKey}{iRowNum}"].Value))
					return iRowNum;
			}

			return null;
		}

		public static ExcelRange GetExcelRange(this ExcelWorksheet ews, int iRowNum, string sColumnFirst, string sColumnLast = null)
		{
			string sRange = $"{sColumnFirst}{iRowNum}";
			if (!string.IsNullOrEmpty(sColumnLast))
				sRange = $"{sRange}:{sColumnLast}{iRowNum}";

			return ews.Cells[sRange];
		}


		public static void FitRowHeight2(ExcelRange er)
		{
			// if this is a new worksheet, the first pass exits after first enumerated for some reason, so let's just call it 2 times for now
			FitRowHeight(er);
			FitRowHeight(er);
		}

		public static void FitRowHeight(ExcelRange er)
		{
			ExcelWorksheet ews = er.Worksheet;

			if (er.Merge)
			{
				ExcelRangeBase cell = er.First();
				double dWidth = er.Sum(c => (ews.Column(c.Start.Column)).Width);
				FitRowHeight(ews, cell, dWidth);
			}
			else
			{
				foreach (ExcelRangeBase cell in er)
				{
					double dWidth = ews.Column(cell.Start.Column).Width;
					FitRowHeight(ews, cell, dWidth);
				}
			}
		}

		private static void FitRowHeight(ExcelWorksheet ews, ExcelRangeBase cell, double dWidth)
		{
			string sValue = cell.Value as string;
			double dHeight = ews.Row(cell.Start.Row).Height;
			double dNewHeight = MeasureTextHeight(sValue, cell.Style.Font, dWidth);

			if (dNewHeight > dHeight)
				ews.Row(cell.Start.Row).Height = dNewHeight;
		}

		public static double MeasureTextHeight(string sText, ExcelFont font, double dWidth)
		{
			if (string.IsNullOrEmpty(sText))
				return 0.0;

			using (Bitmap b = new Bitmap(1, 1))
			using (Graphics g = Graphics.FromImage(b))
			{
				int dWidthPixels = Convert.ToInt32(dWidth * 7);  //7 pixels per excel column width
				float fFontSize = font.Size * 0.97f; // was 1.01f
				Font drawingFont = new Font(font.Name, fFontSize);
				SizeF size = g.MeasureString(sText, drawingFont, dWidthPixels, new StringFormat { FormatFlags = StringFormatFlags.MeasureTrailingSpaces });

				//72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
				return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
			}
		}
	}
}
