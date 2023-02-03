using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBopMission
{
	internal class BopCallsign
	{
		#region Fields
		private static Dictionary<int, string> m_callsignsJtac;// TODO replace by a json resource
		#endregion

		#region Properties
		public string Name { get; set; }
		public int? Group { get; set; }
		public int? Element { get; set; }
		public int? Number { get; set; }
		#endregion

		#region CTOR
		static BopCallsign()
		{
			m_callsignsJtac = new Dictionary<int, string>();
			int i = 0;
			m_callsignsJtac.Add(++i, "Axeman");
			m_callsignsJtac.Add(++i, "Darknight");
			m_callsignsJtac.Add(++i, "Warrior");
			m_callsignsJtac.Add(++i, "Pointer");
			m_callsignsJtac.Add(++i, "Eyeball");
			m_callsignsJtac.Add(++i, "Moonbeam");
			m_callsignsJtac.Add(++i, "Whiplash");
			m_callsignsJtac.Add(++i, "Finger");
			m_callsignsJtac.Add(++i, "Pinpoint");
			m_callsignsJtac.Add(++i, "Ferret");
			m_callsignsJtac.Add(++i, "Shaba");
			m_callsignsJtac.Add(++i, "Playboy");
			m_callsignsJtac.Add(++i, "Hammer");
			m_callsignsJtac.Add(++i, "Jaguar");
			m_callsignsJtac.Add(++i, "Deathstar");
			m_callsignsJtac.Add(++i, "Anvil");
			m_callsignsJtac.Add(++i, "Firefly");
			m_callsignsJtac.Add(++i, "Mantis");
			m_callsignsJtac.Add(++i, "Badger");
		}

		public BopCallsign() { }

		public static BopCallsign NewFromMizCallsign(MizCallsign mizCallsign)
		{
			BopCallsign bopCallsign = null;
			if (mizCallsign is object)
			{
				bopCallsign = new BopCallsign()
				{
					Name = new string(mizCallsign.Name.TakeWhile(_c => !char.IsDigit(_c)).ToArray()),
					Group = mizCallsign.Flight,
					Element = mizCallsign.Element
				};
			}

			return bopCallsign;
		}

		public static BopCallsign NewFromNumber( int? iCallsignNumber)
		{
			BopCallsign bopCallsign = null;
			if (iCallsignNumber is object)
			{
				bopCallsign = new BopCallsign() { Number = iCallsignNumber };
			}

			return bopCallsign;
		}

		public static BopCallsign NewFromJtacId(int? iCallsignId, int? iCallsignGroup)
		{
			BopCallsign bopCallsign = null;
			if (iCallsignId is object && iCallsignGroup is object)
			{
				bopCallsign = new BopCallsign()
				{
					Name = GetCallsignFromJtacId(iCallsignId),
					Group = iCallsignGroup.Value
				};
			}

			return bopCallsign;
		}

		#endregion

		#region Methods
		public override string ToString()
		{
			string sCallsign = null;
			if (Number is object)
				sCallsign = Number.ToString();
			else
			{
				sCallsign = $"{Name}-{Group}";
				if (Element is object)
					sCallsign = $"{sCallsign}-{Element}";
			}

			return sCallsign;
		}

		private static string GetCallsignFromJtacId(int? iCallsignId)
		{
			string sCallsign = null;

			if (iCallsignId is object)
			{
				if (m_callsignsJtac.ContainsKey(iCallsignId.Value))
					sCallsign = m_callsignsJtac[iCallsignId.Value];
				else
					sCallsign = $"{iCallsignId.Value}";
			}

			return sCallsign;
		}
		#endregion
	}
}
