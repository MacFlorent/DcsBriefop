namespace DcsBriefop.Data
{
	internal class Radio
	{
		#region Properties
		public decimal Frequency { get; set; }
		public int Modulation { get; set; }
		#endregion

		#region CTOR
		public Radio()
		{
			Frequency = 250;
			Modulation = ElementRadioModulation.AM;
		}

		public Radio(decimal dFrequency, int iModulation)
		{
			Frequency = dFrequency;
			Modulation = iModulation;
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			string sModulation = MasterDataRepository.GetById(MasterDataType.RadioModulation, Modulation)?.Label;
			return $"{Frequency:###.00}{sModulation}";
		}

		public Radio GetCopy()
		{
			return new Radio(Frequency, Modulation);
		}
		#endregion
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
