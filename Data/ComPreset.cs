using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class ComPreset
	{
		#region Fields
		private BriefingCoalition m_coalition;
		#endregion

		#region Properties
		public int PresetRadio { get; set; }
		public int PresetNumber { get; set; }
		public ElementComPresetMode Mode { get; set; }
		public int AssetId { get; set; }
		public Radio Radio { get; set; }
		public string Notes { get; set; }
		#endregion

		#region CTOR
		public ComPreset(BriefingCoalition coalition, int iRadio, int iNumber) : this(coalition, iRadio, iNumber, new Radio()) { }

		public ComPreset(BriefingCoalition coalition, int iRadio, int iNumber, Radio radio)
		{
			m_coalition = coalition;

			PresetRadio = iRadio;
			PresetNumber = iNumber;
			Radio = radio;

			Mode = ElementComPresetMode.Free;
			Compute();
		}
		#endregion

		#region Methods
		public ComPreset GetCopy()
		{
			return new ComPreset(m_coalition, PresetRadio, PresetNumber, Radio.GetCopy());
		}

		public void Compute()
		{
			if (Mode == ElementComPresetMode.Airdrome)
			{
				AssetAirdrome airdrome = m_coalition.Airdromes.Where(_a => _a.Id == AssetId).FirstOrDefault();
				if (airdrome is object)
				{
					Radio = airdrome.Radio;
					Notes = airdrome.Information;
				}
				else
				{
					Radio = new Radio();
					Notes = "-no airdrome id selected-";
				}
			}
			else if (Mode == ElementComPresetMode.Group)
			{
				Asset asset = m_coalition.OwnAssets.Where(_a => _a.Id == AssetId).FirstOrDefault();
				if (asset is AssetFlight flight)
				{
					Radio = flight.Radio;
					Notes = flight.Information;
				}
				else if (asset is AssetShip ship)
				{
					Radio = ship.Radio;
					Notes = ship.Information;
				}
				else
				{
					Radio = new Radio();
					Notes = "-no group id selected-";
				}
			}
			else //if (Mode == ElementComPresetMode.Free)
			{
				Mode = ElementComPresetMode.Free;
				AssetId = 0;
				Notes = "";
			}

		}
		#endregion
	}

	internal class ListComPreset : List<ComPreset>
	{
		#region CTOR
		#endregion

		#region Methods
		public void InitializeDefault(BriefingCoalition coalition)
		{
			Clear();
			for (int iRadio = 1; iRadio <= 2; iRadio++)
			{
				for (int iNumber = 1; iNumber <= 10; iNumber++)
				{
					Add(new ComPreset(coalition, iRadio, iNumber, new Radio()));
				}
			}
		}

		public ListComPreset GetCopy()
		{
			ListComPreset copy = new ListComPreset();
			foreach (ComPreset preset in this)
			{
				copy.Add(preset.GetCopy());
			}

			return copy;
		}
		#endregion

	}


}
