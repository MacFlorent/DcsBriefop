using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.DataMiz
{
	internal abstract class BaseBriefopCustomSerializable
	{
		#region Fields
		protected static JsonConverter m_converterGMapOverlays = new GMapOverlayJsonConverter();
		protected static JsonConverter m_converterGMarkerBriefop = new GMarkerBriefopJsonConverter();
		protected static JsonConverter m_converterListCustomDataAssetGroup = new ListBriefopCustomGroupJsonConverter();
		protected static JsonConverter m_converterListCustomDataAssetAirdrome = new ListBriefopCustomAirdromeJsonConverter();

		protected static JsonConverter[] m_serializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop, m_converterListCustomDataAssetGroup, m_converterListCustomDataAssetAirdrome };
		protected static JsonConverter[] m_deserializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop };
		#endregion
	}

	internal abstract class BaseBriefopCustomWithDefault : BaseBriefopCustomSerializable
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

