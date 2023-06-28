using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;
using System.Buffers.Text;
using System.Drawing;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartImage : BaseBopBriefingPart
	{
		#region Properties
		public string Header { get; set; }
		public string ImagePath { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartImage() : base(ElementBriefingPartType.Image, "content") { }
		#endregion

		#region Methods
		public override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new();
			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}
		
			string sImageFullPath = GetImageFullPath(bopManager);
			if (Path.Exists(sImageFullPath))
			{
				Bitmap bitmap = new(sImageFullPath);
				MemoryStream stream = new MemoryStream();
				bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
				byte[] imageBytes = stream.ToArray();
				string sImage64 = $"data:image/jpeg;base64,{Convert.ToBase64String(imageBytes)}";
				tags.Add(new HtmlTag("img").Attr("src", sImage64).Attr("width", bopBriefingFolder.ImageSize.Width).Attr("height", bitmap.Height * bopBriefingFolder.ImageSize.Width / bitmap.Width));
			}

			return tags;
		}

		private string GetImageFullPath(BriefopManager bopManager)
		{
				if (string.IsNullOrEmpty(ImagePath))
					return null;
				else
					return Path.Join(bopManager.MizFileDirectory, ImagePath); ;
				//return $@"file:///{Path.Join(bopManager.MizFileDirectory, ImagePath).Replace(@"\", @"/")}";
			}
		#endregion
	}
}
