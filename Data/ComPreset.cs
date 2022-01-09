using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
{
	/*
	 * For compatibility with all playable airframes we use the following (these are not RM limitations but rather DCS gameplay constraints) :
	 * 
	 *	Radio 1 is UHF only (FM/AM)
	 *	Radio 2 is VHF/UHF (FM/AM)
	 * 
	 * See frequency ranges in Radio class
	 * 
	 * The first preset of one radio must be also the group radio, or it will be overwritten by the editor.
	 * Usually radio 1, but radio 2 for F14-B
	 */
	internal class ComPreset
	{
		#region Fields
		#endregion

		#region Properties
		public int PresetRadio { get; set; }
		public int PresetNumber { get; set; }
		public ElementComPresetMode Mode { get; set; }
		public int AssetId { get; set; }
		public string Label { get; set; }
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		public ComPreset() : this (0, 0) { }

		public ComPreset(int iRadio, int iNumber) : this(iRadio, iNumber, new Radio()) { }

		public ComPreset(int iRadio, int iNumber, Radio radio)
		{
			PresetRadio = iRadio;
			PresetNumber = iNumber;
			Radio = radio;

			Mode = ElementComPresetMode.Free;
			AssetId = 0;
		}
		#endregion

		#region Methods
		public ComPreset GetCopy()
		{
			ComPreset copy = new ComPreset(PresetRadio, PresetNumber, Radio.GetCopy());
			copy.Mode = Mode;
			copy.AssetId = AssetId;
			copy.Label = Label;
			return copy;
		}

		public void Compute(BriefingCoalition coalition)
		{
			if (Mode == ElementComPresetMode.Airdrome)
			{
				AssetAirdrome airdrome = GetAsset(coalition) as AssetAirdrome;
				if (airdrome is object)
				{
					Radio = null;
					if (airdrome.Radios is object && airdrome.Radios.Count > 0)
					{
						Radio = airdrome.Radios.Where(_r => _r.Frequency >= 225m).FirstOrDefault();
						if (Radio is null)
							Radio = airdrome.Radios.FirstOrDefault();
					}

					if (Radio is object)
					{
						Label = airdrome.Name;
					}
					else
					{
						Radio = new Radio();
						Label = $"{airdrome.Name} (A/A)";
					}
				}
				else
				{
					Radio = new Radio();
					Label = "-invalid id-";
				}
			}
			else if (Mode == ElementComPresetMode.Group)
			{
				Asset asset = GetAsset(coalition) as Asset;
				if (asset is AssetFlight flight)
				{
					Radio = flight.Radio;
					Label = flight.Name;
				}
				else if (asset is AssetShip ship)
				{
					Radio = ship.Radio;
					Label = ship.Name;
				}
				else
				{
					Radio = new Radio();
					Label = "-invalid id-";
				}
			}
			else //if (Mode == ElementComPresetMode.Free)
			{
				Mode = ElementComPresetMode.Free;
				AssetId = 0;
				Radio.Normalize();
			}

		}

		public Asset GetAsset(BriefingCoalition coalition)
		{
			if (Mode == ElementComPresetMode.Airdrome)
				return coalition.Airdromes.Where(_a => _a.Id == AssetId).FirstOrDefault();
			else if (Mode == ElementComPresetMode.Group)
				return coalition.OwnAssets.Where(_a => _a.Id == AssetId).FirstOrDefault();
			else
				return null;
		}
		#endregion
	}

	internal class ListComPreset : List<ComPreset>
	{
		#region Properties
		public static readonly int RadiosCount = 2;
		public static readonly int PresetsCount = 15;

		#endregion
		#region CTOR
		#endregion

		#region Methods
		public void InitializeEmpty()
		{
			Clear();
			for (int iRadio = 1; iRadio <= RadiosCount; iRadio++)
			{
				for (int iNumber = 1; iNumber <= PresetsCount; iNumber++)
				{
					Add(new ComPreset(iRadio, iNumber, new Radio(255, ElementRadioModulation.AM)));
				}
			}
		}

		public void InitializeCoalition(BriefingCoalition coalition)
		{
			InitializeEmpty();

			ComPreset preset;

			int iRadio = 0;
			int iNumber = 0;

			iRadio++;

			foreach (AssetAirdrome airdrome in coalition.Airdromes.Where(_a => _a.IsAssetBase()))
			{
				iNumber++;
				preset = GetPreset(iRadio, iNumber);
				if (preset is object)
				{
					preset.Mode = ElementComPresetMode.Airdrome;
					preset.AssetId = airdrome.Id;
					preset.Compute(coalition);
				}
			}

			foreach (AssetShip ship in coalition.OwnAssets.OfType<AssetShip>().Where(_s => _s.Usage == ElementAssetUsage.Base))
			{
				iNumber++;
				preset = GetPreset(iRadio, iNumber);
				if (preset is object)
				{
					preset.Mode = ElementComPresetMode.Group;
					preset.AssetId = ship.Id;
					preset.Compute(coalition);
				}
			}

			foreach (AssetFlight flight in coalition.OwnAssets.OfType<AssetFlight>().Where(_f => _f.Usage == ElementAssetUsage.Support))
			{

				iNumber++;
				preset = GetPreset(iRadio, iNumber);
				if (preset is object)
				{
					preset.Mode = ElementComPresetMode.Group;
					preset.AssetId = flight.Id;
					preset.Compute(coalition);
				}
			}

			iRadio++; iNumber = 0;

			iNumber++;

			preset = GetPreset(iRadio, iNumber);
			if (preset is object)
			{
				preset.Mode = ElementComPresetMode.Free;
				preset.Label = "Guard";
				preset.Radio = new Radio(121.5m, ElementRadioModulation.AM);
				preset.Compute(coalition);
			}

			decimal dFrequency = 122.0m;
			foreach (AssetFlight flight in coalition.OwnAssets.OfType<AssetFlight>().Where(_f => _f.Playable))
			{
				iNumber++;
				preset = GetPreset(iRadio, iNumber);
				if (preset is object)
				{
					dFrequency += 0.1m;

					preset.Mode = ElementComPresetMode.Free;
					preset.Label = flight.GetCallsign();
					preset.Radio = new Radio(dFrequency, ElementRadioModulation.AM);
					preset.Compute(coalition);
				}
			}
		}

		public void Compute(BriefingCoalition coalition)
		{
			foreach(ComPreset preset in this)
			{
				preset.Compute(coalition);
			}

	
			foreach(AssetFlight flight in coalition.OwnAssets.OfType<AssetFlight>().Where (_f => _f.Playable))
			{
				int iRadio = 1;
				if (flight.Type.StartsWith("F-14"))
					iRadio = 2;

				ComPreset defaultPreset = GetPreset(iRadio, 1);
				flight.Radio = defaultPreset.Radio.GetCopy();
			}
		}

		public ComPreset GetPreset(int iRadio, int iNumber)
		{
			return this.Where(_p => _p.PresetRadio == iRadio && _p.PresetNumber == iNumber).FirstOrDefault();
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
