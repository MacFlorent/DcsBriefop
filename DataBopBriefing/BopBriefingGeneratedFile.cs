namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingGeneratedFile : IDisposable
	{
		public string FileName { get; set; }
		public List<string> Kneeboards { get; set; } = new List<string>();
		public Image Image { get; set; }
		public string Html { get; set; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Image.Dispose();
			}
		}
	}

	internal class ListBopBriefingGeneratedFile : List<BopBriefingGeneratedFile>, IDisposable
	{
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach(BopBriefingGeneratedFile f in this)
					f.Dispose();
			}
		}
	}
}