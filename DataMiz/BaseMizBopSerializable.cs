using Newtonsoft.Json;

namespace DcsBriefop.DataMiz
{
	internal abstract class BaseMizBopSerializable
	{
		#region Fields
		protected static JsonConverter m_converterBriefopMarker = new BriefopMarkerJsonConverter();

		protected static JsonConverter[] m_serializeConverters = new JsonConverter[] { m_converterBriefopMarker };
		protected static JsonConverter[] m_deserializeConverters = new JsonConverter[] { m_converterBriefopMarker };
		#endregion
	}
}
