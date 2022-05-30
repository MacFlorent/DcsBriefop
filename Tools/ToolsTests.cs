using DcsBriefop.Data;
using DcsBriefop.UcBriefing;
using LsonLib;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DcsBriefop.Tools
{
	internal static class ToolsTests
	{
		private static string ToLiteral(string sText)
		{
			using (var writer = new StringWriter())
			{
				using (var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp"))
				{
					provider.GenerateCodeFromExpression(new System.CodeDom.CodePrimitiveExpression(sText), writer, null);
					return writer.ToString();
				}
			}
		}

		public static void Adgv(SplitContainer splitContainer, MissionManager missionManager, BriefingContainer briefingContainer)
		{
			if (missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			splitContainer.Panel1.Controls.Clear();

			Zuby.ADGV.AdvancedDataGridView dgv = new Zuby.ADGV.AdvancedDataGridView();
			dgv.Dock = DockStyle.Fill;
			splitContainer.Panel1.Controls.Add(dgv);

			GridManagerAsset gam = new GridManagerAsset(dgv, briefingContainer.GetCoalition(ElementCoalition.Blue).OpposingAssets, null);
			gam.ColumnsDisplayed = GridManagerAsset.ColumnsDisplayedOpposing;
			gam.Initialize();
		}

		public static void LsonText(SplitContainer splitContainer)
		{
			splitContainer.Panel1.Controls.Clear();
			FlowLayoutPanel f = new FlowLayoutPanel();
			f.FlowDirection = FlowDirection.LeftToRight;
			f.Dock = DockStyle.Fill;
			splitContainer.Panel1.Controls.Add(f);

			string sFilePath = @"D:\Projects\dictionary.txt";
			string sFileContent = File.ReadAllText(sFilePath);
			string sLsonIn = ToolsLua.ReadLuaFileContent(sFilePath);
			Dictionary<string, LsonLib.LsonValue> lsd = LsonLib.LsonVars.Parse(sLsonIn);
			string sLsonOut = ToolsLua.LsonRootToDcs(lsd);

			TextBox AppendTextBox(string sText)
			{
				TextBox tb = new TextBox();
				tb.Multiline = true;
				tb.Height = 100;
				tb.Width = f.Width;
				tb.Text = sText;
				f.Controls.Add(tb);
				return tb;
			}

			AppendTextBox(ToLiteral(sFileContent));
			AppendTextBox(ToLiteral(sLsonIn));
			AppendTextBox(ToLiteral(sLsonOut));
			//System.IO.File.WriteAllText(sFilePath + "_mod", s);
		}

		public static void ReplaceLinebreaks(SplitContainer splitContainer)
		{
			splitContainer.Panel1.Controls.Clear();
			FlowLayoutPanel f = new FlowLayoutPanel();
			f.FlowDirection = FlowDirection.LeftToRight;
			f.Dock = DockStyle.Fill;
			splitContainer.Panel1.Controls.Add(f);

			//string sFilePath = @"D:\Projects\mission";
			string sFilePath = @"D:\Projects\dictionary.txt";
			//string sFileContent = File.ReadAllText(sFilePath);
			string sLsonIn = ToolsLua.ReadLuaFileContent(sFilePath);
			Dictionary<string, LsonValue> lsd = LsonVars.Parse(sLsonIn);
			string sLsonOut = LsonVars.ToString(lsd);
			string sReplaced = ToolsLua.ReplaceDcsStringLineBreaks(sLsonOut, 0);

			TextBox AppendTextBox(string sText)
			{
				TextBox tb = new TextBox();
				tb.Multiline = true;
				tb.Height = 300;
				tb.Width = f.Width;
				tb.Text = sText;
				f.Controls.Add(tb);
				return tb;
			}

			AppendTextBox(sLsonOut.Substring(0, 1000));
			AppendTextBox(sReplaced.Substring(0, 1000));
		}
	}
}
