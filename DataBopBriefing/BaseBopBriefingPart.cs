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
	internal abstract class BaseBopBriefingPart : IEquatable<BaseBopBriefingPart>
	{
		#region Fields
		protected string m_sCssClass;
		#endregion

		#region Properties
		public Guid Guid { get; set; }
		public ElementBriefingPartType PartType { get; protected set; }
		#endregion

		#region CTOR
		public BaseBopBriefingPart(ElementBriefingPartType partType, string sCssClass)
		{
			Guid = Guid.NewGuid();
			PartType = partType;
			m_sCssClass = sCssClass;
		}
		#endregion

		#region Methods
		public virtual void InitializeDefault() { }
		public virtual string ToStringAdditional() { return null; }

		public HtmlTag BuildHtml(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			return new HtmlTag("div").AddClass(m_sCssClass).Append(BuildHtmlContent(bopManager, bopBriefingFolder));
		}

		protected abstract IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder);

		public virtual IEnumerable<GMapOverlay> BuildMapOverlays(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder) { return null; }
		#endregion

		#region IEquatable
		public bool Equals(BaseBopBriefingPart other)
		{
			if (other is null)
				return false;

			return (Guid == other.Guid);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BaseBopBriefingPart);
		}

		public override int GetHashCode() => Guid.GetHashCode();
		#endregion
	}

	#region Json abstract converter
	internal class BopBriefingPartAbstractResolver : DefaultContractResolver
	{
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			if (typeof(BaseBopBriefingPart).IsAssignableFrom(objectType) && !objectType.IsAbstract)
				return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
			return base.ResolveContractConverter(objectType);
		}
	}

	internal class BopBriefingPartAbstractConverter : JsonConverter
	{
		static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings() { ContractResolver = new BopBriefingPartAbstractResolver() };

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(BaseBopBriefingPart));
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
			else if (partType == ElementBriefingPartType.Waypoints)
				return DeserializeConcreteBriefingPart<BopBriefingPartWaypoints>(jo);
			else if (partType == ElementBriefingPartType.Image)
				return DeserializeConcreteBriefingPart<BopBriefingPartImage>(jo);
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