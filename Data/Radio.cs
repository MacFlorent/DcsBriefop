namespace DcsBriefop.Data
{
	internal class Radio
	{
		public decimal Frequency { get; set; }
		public int Modulation { get; set; }
	}

	internal class Tacan
	{
		public int Channel { get; set; }
		public string Mode { get; set; }
		public string Identifier { get; set; }
	}
}
