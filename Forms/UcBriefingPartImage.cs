using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartImage : UcBriefingPartBase
	{
		#region CTOR
		public UcBriefingPartImage(BaseBopBriefingPart bopBriefingPart, BriefopManager bopManager, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopManager, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartImage briefingPart = m_bopBriefingPart as BopBriefingPartImage;
			TbHeader.Text = briefingPart.Header;
			TbImagePath.Text = briefingPart.ImagePath;
			DisplayCurrentImage();
		}

		public override void ScreenToData()
		{
			BopBriefingPartImage briefingPart = m_bopBriefingPart as BopBriefingPartImage;
			briefingPart.Header = TbHeader.Text;
			briefingPart.ImagePath = TbImagePath.Text;
		}

		private void DisplayCurrentImage()
		{
			string sImageFullPath = GetImageFullPath();
			if (Path.Exists(sImageFullPath))
			{
				TbImageFullPath.Text = sImageFullPath;
				PbImage.Image = new Bitmap(sImageFullPath);
			}
			else
			{
				TbImageFullPath.Text = "";
				PbImage.Image = null;
			}
		}

		private string GetImageFullPath()
		{
			if (string.IsNullOrEmpty(TbImagePath.Text))
				return null;
			else
				return Path.Join(m_bopManager.MizFileDirectory, TbImagePath.Text);
		}

		private string ExtractImagePath(string sFullPath)
		{
			if (sFullPath.StartsWith(m_bopManager.MizFileDirectory))
				return sFullPath.Replace(m_bopManager.MizFileDirectory, "");
			else
				return null;
		}

		#endregion

		#region Events
		private void BtImagePath_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				string sImagePath = GetImageFullPath();
				ofd.Filter = ElementGlobalData.ImageFileFilter;
				if (Path.Exists(sImagePath))
					ofd.FileName = sImagePath;
				else
					ofd.InitialDirectory = m_bopManager.MizFileDirectory;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					TbImagePath.Text = ExtractImagePath(ofd.FileName);
					DisplayCurrentImage();
				}
			}
		}
		#endregion
	}
}
