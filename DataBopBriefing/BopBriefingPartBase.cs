using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using GMap.NET.WindowsForms;
using HtmlTags;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DcsBriefop.DataBopBriefing
{
	[JsonConverter(typeof(BopBriefingPartAbstractConverter))]
	internal abstract class BopBriefingPartBase
	{
		#region Fields
		protected string m_sCssClass;
		#endregion

		#region Properties
		public ElementBriefingPartType PartType { get; protected set; }
		#endregion

		#region CTOR
		public BopBriefingPartBase(ElementBriefingPartType partType, string sCssClass)
		{
			PartType = partType;
			m_sCssClass = sCssClass;
		}
		#endregion

		#region Methods
		public virtual string ToStringAdditional() { return null; }

		public HtmlTag BuildHtml(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			return new HtmlTag("div").AddClass(m_sCssClass).Append(BuildHtmlContent(bopMission, bopBriefingFolder));
		}

		protected abstract IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder);

		public virtual IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission) { return null; }
		#endregion
	}

	#region Json abstract converter
	internal class BopBriefingPartAbstractResolver : DefaultContractResolver
	{
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			if (typeof(BopBriefingPartBase).IsAssignableFrom(objectType) && !objectType.IsAbstract)
				return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
			return base.ResolveContractConverter(objectType);
		}
	}

	internal class BopBriefingPartAbstractConverter : JsonConverter
	{
		static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings() { ContractResolver = new BopBriefingPartAbstractResolver() };

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(BopBriefingPartBase));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jo = JObject.Load(reader);
			ElementBriefingPartType partType = (ElementBriefingPartType)jo["PartType"].Value<int>();

			if (partType == ElementBriefingPartType.Bullseye)
				return DeserializeConcreteBriefingPart<BopBriefingPartBullseye>(jo);
			if (partType == ElementBriefingPartType.Paragraph)
				return DeserializeConcreteBriefingPart<BopBriefingPartParagraph>(jo);
			else if (partType == ElementBriefingPartType.Sortie)
				return DeserializeConcreteBriefingPart<BopBriefingPartSortie>(jo);
			else if (partType == ElementBriefingPartType.Description)
				return DeserializeConcreteBriefingPart<BopBriefingPartDescription>(jo);
			else if (partType == ElementBriefingPartType.Task)
				return DeserializeConcreteBriefingPart<BopBriefingPartTask>(jo);
			else if (partType == ElementBriefingPartType.Airbases)
				return DeserializeConcreteBriefingPart<BopBriefingPartAirbases>(jo);
			else if (partType == ElementBriefingPartType.Groups)
				return DeserializeConcreteBriefingPart<BopBriefingPartGroups>(jo);
			//else if (partType == ElementBriefingPartType.Units)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartAirbases>(jo);
			//else if (partType == ElementBriefingPartType.Waypoints)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartAirbases>(jo);
			//else if (partType == ElementBriefingPartType.Image)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartAirbases>(jo);
			else if (partType == ElementBriefingPartType.Weather)
				return DeserializeConcreteBriefingPart<BopBriefingPartWeather>(jo);

			else
				throw new ExceptionBop("Cannot deserialize Unknown part type");

			throw new NotImplementedException();
		}

		private object DeserializeConcreteBriefingPart<Type>(JObject jo) 
		{
			return JsonConvert.DeserializeObject<Type>(jo.ToString(), SerializerSettings);
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException(); // won't be called because CanWrite returns false
		}
	}
	#endregion
}