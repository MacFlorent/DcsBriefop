using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopCallsign
	{
		#region Fields
		private static Dictionary<int, string> m_callsignsJtac;// TODO replace by a json resource
		private static Dictionary<int, string> m_callsignsHeliport;// TODO replace by a json resource
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

			m_callsignsHeliport = new Dictionary<int, string>();
			i = 0;
			m_callsignsHeliport.Add(++i, "London");
			m_callsignsHeliport.Add(++i, "Dallas");
			m_callsignsHeliport.Add(++i, "Paris");
			m_callsignsHeliport.Add(++i, "Moscow");
			m_callsignsHeliport.Add(++i, "Berlin");
			m_callsignsHeliport.Add(++i, "Rome");
			m_callsignsHeliport.Add(++i, "Madrid");
			m_callsignsHeliport.Add(++i, "Warsaw");
			m_callsignsHeliport.Add(++i, "Dublin");
			m_callsignsHeliport.Add(++i, "Perth");
		}

		public BopCallsign() { }

		public static BopCallsign NewFromMizCallsign(MizCallsign mizCallsign)
		{
			BopCallsign bopCallsign = null;
			if (mizCallsign is not null)
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
			if (iCallsignNumber is not null)
			{
				bopCallsign = new BopCallsign() { Number = iCallsignNumber };
			}

			return bopCallsign;
		}

		public static BopCallsign NewFromJtacId(int? iCallsignId, int? iCallsignGroup)
		{
			BopCallsign bopCallsign = null;
			if (iCallsignId is not null && iCallsignGroup is not null)
			{
				bopCallsign = new BopCallsign()
				{
					Name = GetCallsignFromJtacId(iCallsignId),
					Group = iCallsignGroup.Value
				};
			}

			return bopCallsign;
		}

		public static BopCallsign NewFromHeliportId(int? iCallsignId)
		{
			BopCallsign bopCallsign = null;
			if (iCallsignId is not null)
			{
				bopCallsign = new BopCallsign()
				{
					Name = GetCallsignFromHeliportId(iCallsignId),
				};
			}

			return bopCallsign;
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			string sCallsign = null;
			if (Number is not null)
				sCallsign = Number.ToString();
			else
			{
				StringBuilder sb = new StringBuilder(Name);
				if (Group is not null)
					sb.AppendWithSeparator(Group.ToString(), "-");
				if (Element is not null)
					sb.AppendWithSeparator(Element.ToString(), "-");

				sCallsign = sb.ToString();
			}

			return sCallsign;
		}

		private static string GetCallsignFromJtacId(int? iCallsignId)
		{
			string sCallsign = null;

			if (iCallsignId is not null)
			{
				if (m_callsignsJtac.ContainsKey(iCallsignId.Value))
					sCallsign = m_callsignsJtac[iCallsignId.Value];
				else
					sCallsign = $"{iCallsignId.Value}";
			}

			return sCallsign;
		}

		private static string GetCallsignFromHeliportId(int? iCallsignId)
		{
			string sCallsign = null;

			if (iCallsignId is not null)
			{
				if (m_callsignsHeliport.ContainsKey(iCallsignId.Value))
					sCallsign = m_callsignsHeliport[iCallsignId.Value];
				else
					sCallsign = $"{iCallsignId.Value}";
			}

			return sCallsign;
		}
		#endregion
	}
}
