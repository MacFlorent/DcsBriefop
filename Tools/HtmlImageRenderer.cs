using PuppeteerSharp;
using DcsBriefop.Tools;

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
		public async Task<byte[]> RenderImageDataAsync(string sHtml, ScreenshotOptions options, ViewPortOptions viewPortOptions)
		{
			byte[] resultBytes = null;
			try
			{
				// Download chrome (headless) browser (first time takes a while).
				await m_browserFetcher.DownloadAsync();

				// Launch the browser and set the given html.

				//const browser = await puppeteer.launch({
				//ignoreDefaultArgs: ['--disable-extensions'],
				//await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, Args = new[] { "--disable-extensions" } });
				await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
				await using var page = await browser.NewPageAsync();
				await page.SetContentAsync(sHtml);

				if (viewPortOptions is object)
				{
					await page.SetViewportAsync(viewPortOptions);
				}

				resultBytes = await page.ScreenshotDataAsync(options);

				await browser.CloseAsync();
				return resultBytes;
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
				throw;
			}
		}

		public async Task<Image> RenderImageAsync(string sHtml, ScreenshotOptions options, ViewPortOptions viewPortOptions)
		{
			Image resultImage = null;
			byte[] byteArrayIn = await RenderImageDataAsync(sHtml, options, viewPortOptions);
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
