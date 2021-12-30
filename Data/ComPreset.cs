using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class ComPreset
	{
		#region Properties
		public int PresetRadio { get; set; }
		public int PresetNumber { get; set; }
		public Radio Radio { get; set; }
		public int GroupId { get; set; }
		public string Notes { get; set; }
		#endregion

		#region CTOR
		public ComPreset(int iRadio, int iNumber) : this(iRadio, iNumber, new Radio()) { }

		public ComPreset(int iRadio, int iNumber, Radio radio)
		{
			PresetRadio = iRadio;
			PresetNumber = iNumber;
			Radio = radio;

			GroupId = 0;
			Notes = "";
		}
		#endregion

		#region Methods
		public ComPreset GetCopy()
		{
			return new ComPreset(PresetRadio, PresetNumber, Radio.GetCopy());
		}
		#endregion
	}

	internal class ListComPreset : List<ComPreset>
	{
		#region CTOR
		#endregion

		#region Methods
		public void InitializeDefault()
		{
			Clear();
			for (int iRadio = 1; iRadio <= 2; iRadio++)
			{
				for (int iNumber = 1; iNumber <= 10; iNumber++)
				{
					Add(new ComPreset(iRadio, iNumber, new Radio()));
				}
			}
		}

		public ListComPreset GetCopy()
		{
			ListComPreset copy = new ListComPreset();
			foreach(ComPreset preset in this)
			{
				copy.Add(preset.GetCopy());
			}
			
			return copy;
		}
		#endregion

	}


}
