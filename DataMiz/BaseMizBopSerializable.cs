using Newtonsoft.Json;

namespace DcsBriefop.DataMiz
{
	internal abstract class BaseMizBopSerializable
	{
		#region Fields
		protected static JsonConverter m_converterGMapOverlays = new GMapOverlayJsonConverter();
		protected static JsonConverter m_converterGMarkerBriefop = new GMarkerBriefopJsonConverter();
		protected static JsonConverter m_listMizBopGroupJsonConverter = new ListMizBopGroupJsonConverter();
		protected static JsonConverter m_listMizBopUnitJsonConverter = new ListMizBopUnitJsonConverter();
		//protected static JsonConverter m_listBopCustomAirdromeJsonConverter = new ListBopCustomAirdromeJsonConverter();

		protected static JsonConverter[] m_serializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop, m_listMizBopGroupJsonConverter, m_listMizBopUnitJsonConverter };
		protected static JsonConverter[] m_deserializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop };
		#endregion
	}

	internal abstract class BaseMizBopWithDefault : BaseMizBopSerializable
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

