﻿using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DcsBriefop
{
	internal class GMapOverlayJsonConverter : JsonConverter<GMapOverlay>
	{
		private static class JsonNode
		{
			public static readonly string Markers = "markers";
			public static readonly string Routes = "routes";
		}

		public override void WriteJson(JsonWriter writer, GMapOverlay value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			if (value.Markers is object && value.Markers.Count > 0)
			{
				JArray ja = new JArray();
				foreach (var marker in value.Markers)
				{
					if (marker is GMarkerBriefop markerBriefop)
						ja.Add(JToken.FromObject(markerBriefop, serializer));
				}

				jo[JsonNode.Markers] = ja;
			}

			if (value.Routes is object && value.Routes.Count > 0)
			{
				JArray ja = new JArray();
				foreach (GMapRoute gmr in value.Routes)
					ja.Add(JToken.FromObject(gmr, serializer));

				jo[JsonNode.Routes] = ja;
			}

			jo.WriteTo(writer, new GMarkerBriefopJsonConverter());
		}

		public override GMapOverlay ReadJson(JsonReader reader, Type objectType, GMapOverlay existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			GMapOverlay gmo = new GMapOverlay();

			JToken token = JToken.Load(reader);
			if (token.HasValues)
			{
				if (token[JsonNode.Markers] is object)
				{
					foreach (GMarkerBriefop gmb in token[JsonNode.Markers].ToObject<List<GMarkerBriefop>>(serializer))
						gmo.Markers.Add(gmb);
				}
				if (token[JsonNode.Routes] is object)
				{
					foreach (GMapRoute gmr in token[JsonNode.Routes].ToObject<List<GMapRoute>>(serializer))
						gmo.Routes.Add(gmr);
				}
			}
			return gmo;
		}
	}

	public class GMarkerBriefopJsonConverter : JsonConverter<GMarkerBriefop>
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

		public override void WriteJson(JsonWriter writer, GMarkerBriefop value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			jo.Add(new JProperty(JsonNode.Latitude, value.Position.Lat));
			jo.Add(new JProperty(JsonNode.Longitude, value.Position.Lng));
			jo.Add(new JProperty(JsonNode.Template, value.MarkerTemplate));
			jo.Add(new JProperty(JsonNode.Scale, value.Scale));
			jo.Add(new JProperty(JsonNode.Angle, value.Angle));

			if (value.TintColor is object)
				jo.Add(new JProperty(JsonNode.Color, ColorTranslator.ToHtml(value.TintColor.Value)));

			jo.Add(new JProperty(JsonNode.Label, value.Label));

			jo.WriteTo(writer);
		}

		public override GMarkerBriefop ReadJson(JsonReader reader, Type objectType, GMarkerBriefop existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			double lat = token[JsonNode.Latitude].Value<double>();
			double lng = token[JsonNode.Longitude].Value<double>();
			string sMarkerType = token[JsonNode.Template].Value<string>();
			int iScale = token[JsonNode.Scale].Value<int>();
			int iAngle = token[JsonNode.Angle].Value<int>();

			Color? tintColor = null;
			if (token[JsonNode.Color] is object)
				tintColor = ColorTranslator.FromHtml(token[JsonNode.Color].Value<string>());

			string sLabel = token[JsonNode.Label].Value<string>();

			return GMarkerBriefop.NewFromTemplateName(new PointLatLng(lat, lng), sMarkerType, tintColor, sLabel, iScale, iAngle);
		}
	}
}