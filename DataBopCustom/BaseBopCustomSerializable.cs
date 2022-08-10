using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.DataBopCustom
{
	internal abstract class BaseBopCustomSerializable
	{
		#region Fields
		protected static JsonConverter m_converterGMapOverlays = new GMapOverlayJsonConverter();
		protected static JsonConverter m_converterGMarkerBriefop = new GMarkerBriefopJsonConverter();
		protected static JsonConverter m_listBopCustomGroupJsonConverter = new ListBopCustomGroupJsonConverter();
		protected static JsonConverter m_listBopCustomUnitJsonConverter = new ListBopCustomUnitJsonConverter();
		protected static JsonConverter m_listBopCustomAirdromeJsonConverter = new ListBopCustomAirdromeJsonConverter();

		protected static JsonConverter[] m_serializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop, m_listBopCustomGroupJsonConverter, m_listBopCustomUnitJsonConverter, m_listBopCustomAirdromeJsonConverter };
		protected static JsonConverter[] m_deserializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop };
		#endregion
	}

	internal abstract class BaseBopCustomWithDefault : BaseBopCustomSerializable
	{
		protected string m_defaultDataJson;

		public void SetDefaultData()
		{
			m_defaultDataJson = JsonConvert.SerializeObject(this, Formatting.None, m_serializeConverters);
		}

		public bool IsDefaultData()
		{
			string sJson = JsonConvert.SerializeObject(this, Formatting.None, m_serializeConverters);
			return sJson == m_defaultDataJson;
		}
	}
}

