using System;
using System.Text.RegularExpressions;

namespace DcsBriefop.Data
{
	internal class Tacan : IEquatable<Tacan>
	{
		#region Properties
		public int Channel { get; set; }
		public string Mode { get; set; }
		public string Identifier { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			string sIdentifier = "";
			if (!string.IsNullOrEmpty(Identifier))
				sIdentifier = $" [{Identifier}]";

			return $"{Channel}{Mode}{sIdentifier}";
		}

		public static Tacan NewFromString(string sTacan)
		{
			Tacan tacan = null;
			Regex regex = new Regex(@"^(?<chan>[0-9]+)\s*(?<mode>[XxYy])\s*(?<id>.*)");
			Match match = regex.Match(sTacan);

			if (match.Success)
			{
				string sChannel = match.Groups["chan"].Value;
				string sMode = match.Groups["mode"].Value;
				string sId = match.Groups["id"].Value;

				int? iChannel = null;
				if (int.TryParse(sChannel, out int i))
					iChannel = i;

				if (iChannel is object)
				{
					tacan = new Tacan() { Channel = iChannel.Value, Mode = sMode, Identifier = sId };
					tacan.Normalize();
				}
			}

			return tacan;
		}

		public void Normalize()
		{
			if (Channel <= 0)
				Channel = 1;
			else if (Channel > 99)
				Channel = 99;

			Mode = Mode.ToUpper();
			if (Mode != "X" && Mode != "Y")
				Mode = "X";

			Identifier = Identifier.Replace("[", "").Replace("]", "").Trim();
		}

		public bool Equals(Tacan other)
		{
			if (other is null)
				return false;

			return (Channel == other.Channel && Mode == other.Mode && Identifier == other.Identifier);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Tacan);
		}

		public override int GetHashCode() => (Channel, Mode, Identifier).GetHashCode();
		#endregion
	}
}
