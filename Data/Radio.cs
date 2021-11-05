namespace DcsBriefop.Data
{
	internal class Radio
	{
		public decimal Frequency { get; set; }
		public int Modulation { get; set; }

		public override string ToString()
		{
			string sModulation = "";
			if (Modulation == ElementRadioModulation.AM)
				sModulation = "AM";
			if (Modulation == ElementRadioModulation.FM)
				sModulation = "FM";

			return $"{Frequency:###.00}{sModulation}";
		}
	}

	internal class Tacan
	{
		public int Channel { get; set; }
		public string Mode { get; set; }
		public string Identifier { get; set; }

		public override string ToString()
		{
			string sIdentifier = "";
			if (!string.IsNullOrEmpty(Identifier))
				sIdentifier = $" [{Identifier}]";

			return $"{Channel}{Mode}{sIdentifier}";
		}
	}
}
