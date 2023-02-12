using DcsBriefop.net.Tools;
using GMap.NET.MapProviders;
using Maroontress.Html;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class FrmBriefingPage : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;
		#endregion

		#region CTOR
		public FrmBriefingPage(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
		{
			InitializeComponent();

			//https://github.com/maroontress/HtmlBuilder
			//https://github.com/hardkoded/puppeteer-sharp

			var nodeOf = Nodes.NewFactory();
			var html = nodeOf.Html.Add(
					nodeOf.Head.Add(
							nodeOf.Title.Add("My first web page")),
					nodeOf.Body.Add(
							nodeOf.P.Add("Hello, World!")));
			var result = html.ToString();
			textBox1.Text = result;
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmBriefingPage f = new FrmBriefingPage(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		private async void Test(string sHtml)
		{
			using (HtmlImageRenderer i = new HtmlImageRenderer())
			{
				Image img = await i.RenderImageAsync(sHtml, new ScreenshotOptions() { Type = ScreenshotType.Png });
				pictureBox1.Image = img;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Test(textBox1.Text);
		}
	}


}
