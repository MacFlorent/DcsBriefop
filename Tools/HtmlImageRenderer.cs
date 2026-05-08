using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace DcsBriefop.Tools
{
	internal static class HtmlImageRenderer
	{
		public static async Task<Image> RenderImageAsync(string sHtml, Size size)
		{
			TaskCompletionSource tcsNavigation = new();

			using Form hostForm = new()
			{
				FormBorderStyle = FormBorderStyle.None,
				ShowInTaskbar = false,
				StartPosition = FormStartPosition.Manual,
				Width = size.Width,
				Height = size.Height,
				Location = new Point(-32000, -32000)
			};

			WebView2 webView = new() { Dock = DockStyle.Fill };
			hostForm.Controls.Add(webView);
			hostForm.Show();

			await webView.EnsureCoreWebView2Async();

			webView.CoreWebView2.NavigationCompleted += (s, e) =>
			{
				if (e.IsSuccess)
					tcsNavigation.TrySetResult();
				else
					tcsNavigation.TrySetException(new Exception($"WebView2 navigation failed: {e.WebErrorStatus}"));
			};

			webView.CoreWebView2.NavigateToString(sHtml);
			await tcsNavigation.Task;
			await Task.Delay(300);

			using MemoryStream ms = new();
			await webView.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, ms);
			ms.Seek(0, SeekOrigin.Begin);
			return Image.FromStream(ms);
		}
	}
}
