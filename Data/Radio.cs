using System.Text.RegularExpressions;

namespace DcsBriefop.Data
{
	internal class Radio : IEquatable<Radio>
	{
		#region Properties
		public decimal Frequency { get; set; }
		public int Modulation { get; set; }
		#endregion

		#region CTOR
		public Radio()
		{
			Frequency = 0;
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
			return $"{Frequency:###.000}{sModulation}";
		}

		public static Radio NewFromString(string sRadio)
		{
			Radio radio = null;
			Regex regex = new Regex(@"^(?<freq>[0-9]+\.?[0-9]*)\s*(?<mod>[AaFf][Mm]).*");
			Match match = regex.Match(sRadio);

			if (match.Success)
			{
				string sFrequency = match.Groups["freq"].Value;
				string sModulation = match.Groups["mod"].Value;

				decimal? dFrequency = null;
				if (decimal.TryParse(sFrequency, out decimal d))
					dFrequency = d;
				
				int iModulation = ElementRadioModulation.AM;
				if (string.Compare(sModulation, MasterDataRepository.GetById(MasterDataType.RadioModulation, ElementRadioModulation.FM)?.Label, true) == 0)
					iModulation = ElementRadioModulation.FM;

				if (dFrequency is object)
				{
					radio = new Radio(dFrequency.Value, iModulation);
					radio.Normalize();
				}
			}

			return radio;
		}

		//public Radio GetCopy()
		//{
		//	return new Radio(Frequency, Modulation);
		//}

		public void Normalize()
		{
			/*
			* Usable ranges are the following:
			* VHF FM range from: 30.000 MHz to 87.975 MHz
			* VHF AM range from: 118.000 MHz to 135.975 MHz
			* VHF AM / FM range from: 136.000 MHz to 155.975 MHz
			* VHF FM range from: 156.000 MHz to 173.975 MHz
			* UHF AM range from: 225.000 MHz to 399.975 MHz
			* 
			* Increments are 0.025 Mhz
			*/

			Frequency = decimal.Round(Frequency * 1000m / 25m) * (25m / 1000m);

			if (Frequency < 30m)
				Frequency = 30m;
			else if (Frequency > 87.975m && Frequency < 118m)
				Frequency = 118m;
			else if (Frequency > 173.975m && Frequency < 225m)
				Frequency = 225m;
			else if (Frequency > 399.975m)
				Frequency = 399.975m;

			if (Frequency <= 87.975m)
				Modulation = ElementRadioModulation.FM;
			else if (Frequency >= 118m && Frequency <= 135.975m)
				Modulation = ElementRadioModulation.AM;
			else if (Frequency >= 156 && Frequency <= 173.975m)
				Modulation = ElementRadioModulation.FM;
			else if (Frequency >= 225m)
				Modulation = ElementRadioModulation.AM;
		}
		#endregion

		#region IEquatable
		public bool Equals(Radio other)
		{
			if (other is null)
				return false;

			return (Frequency == other.Frequency && Modulation == other.Modulation);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Radio);
		}

		public override int GetHashCode() => (Frequency, Modulation).GetHashCode();
		#endregion
	}
}
