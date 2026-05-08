using DcsBriefop.Data;
using DcsBriefop.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DcsBriefop
{
	internal class BriefopMarkerJsonConverter : JsonConverter<BriefopMarker>
	{
		private static class JsonNode
		{
			public static readonly string Latitude = "lat";
			public static readonly string Longitude = "lng";
			public static readonly string Template = "template";
			public static readonly string Label = "label";
			public static readonly string Scale = "scale";
			public static readonly string Angle = "angle";
			public static readonly string Color = "color";
		}

		public override void WriteJson(JsonWriter writer, BriefopMarker value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			jo.Add(new JProperty(JsonNode.Latitude, value.Position.Latitude));
			jo.Add(new JProperty(JsonNode.Longitude, value.Position.Longitude));
			jo.Add(new JProperty(JsonNode.Template, value.TemplateName));
			jo.Add(new JProperty(JsonNode.Scale, value.Scale));
			jo.Add(new JProperty(JsonNode.Angle, value.Angle));

			if (value.TintColor is object)
				jo.Add(new JProperty(JsonNode.Color, ColorTranslator.ToHtml(value.TintColor.Value)));

			jo.Add(new JProperty(JsonNode.Label, value.Label));

			jo.WriteTo(writer);
		}

		public override BriefopMarker ReadJson(JsonReader reader, Type objectType, BriefopMarker existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			double dLat = token[JsonNode.Latitude].Value<double>();
			double dLng = token[JsonNode.Longitude].Value<double>();
			string sMarkerType = token[JsonNode.Template].Value<string>();
			int iScale = token[JsonNode.Scale].Value<int>();
			int iAngle = token[JsonNode.Angle].Value<int>();

			Color? tintColor = null;
			if (token[JsonNode.Color] is object)
				tintColor = ColorTranslator.FromHtml(token[JsonNode.Color].Value<string>());

			string sLabel = token[JsonNode.Label].Value<string>();

			return BriefopMarker.NewFromTemplateName(new GeoPoint(dLat, dLng), sMarkerType, tintColor, sLabel, iScale, iAngle);
		}
	}
}
