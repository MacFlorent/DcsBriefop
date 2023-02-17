using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
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
		public string PartType { get; protected set; }
		#endregion

		#region CTOR
		public BopBriefingPartBase(string sPartType, string sCssClass)
		{
			PartType = sPartType;
			m_sCssClass = sCssClass;
		}
		#endregion

		#region Methods
		public HtmlTag BuildHtml(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			return new HtmlTag("div").AddClass(m_sCssClass).Append(BuildHtmlContent(bopMission, bopBriefingFolder));
		}

		protected abstract IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder);
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
			string sPartType = jo["PartType"].Value<string>();
			Type concreteType = BopBriefingPartType.BopBriefingPartTypes.Where(_b => _b.Name == sPartType).Select(_b => _b.ClassType).FirstOrDefault();

			if (concreteType is not null)
				return DeserializeConcreteBriefingPart<concreteType>(jo);
			//if (sPartType == ElementBriefingPartType.Bullseye)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartBullseye>(jo);
			//if (sPartType == ElementBriefingPartType.Paragraph)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartParagraph>(jo);
			//else if (sPartType == ElementBriefingPartType.Sortie)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartSortie>(jo);
			//else if (sPartType == ElementBriefingPartType.Description)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartDescription>(jo);
			//else if (sPartType == ElementBriefingPartType.Task)
			//	return DeserializeConcreteBriefingPart<BopBriefingPartTask>(jo);
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