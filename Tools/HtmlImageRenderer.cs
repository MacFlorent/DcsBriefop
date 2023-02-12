using PuppeteerSharp;
using System.Windows.Forms;

namespace DcsBriefop.net.Tools
{
	public class HtmlImageRenderer : IDisposable
	{
		private BrowserFetcher m_browserFetcher;

		public HtmlImageRenderer()
		{
			m_browserFetcher = new BrowserFetcher();
		}

		//ScreenshotStreamAsync
		public async Task<byte[]> RenderImageDataAsync(string sHtml, ScreenshotOptions options)
		{
			byte[] resultBytes = null;
			try
			{
				// Download chrome (headless) browser (first time takes a while).
				await m_browserFetcher.DownloadAsync();

				// Launch the browser and set the given html.
				await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
				await using var page = await browser.NewPageAsync();
				await page.SetContentAsync(sHtml);
				resultBytes = await page.ScreenshotDataAsync(options);

				await browser.CloseAsync();
				return resultBytes;
			}
			catch (Exception ex)
			{
				// Log ex $"{nameof(RenderImageDataAsync)}: Unable to render image from {nameof(html)}={html}");
			}

			return null;
		}

		public async Task<Image> RenderImageAsync(string sHtml, ScreenshotOptions options)
		{
			Image resultImage = null;
			byte[] byteArrayIn = await RenderImageDataAsync(sHtml, options);
			using (var ms = new MemoryStream(byteArrayIn))
			{
				resultImage = Image.FromStream(ms);
			}

			return resultImage;
		}

		public void Dispose()
		{
			m_browserFetcher = null;
		}
	}
}
