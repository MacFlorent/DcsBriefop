using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Map
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
				foreach (GMarkerBriefop gmb in value.Markers.OfType<GMarkerBriefop>())
					ja.Add(JToken.FromObject(gmb, serializer));

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

			return gmo;
		}
	}

	public class GMarkerBriefopJsonConverter : JsonConverter<GMarkerBriefop>
	{
		private static class JsonNode
		{
			public static readonly string Latitude = "lat";
			public static readonly string Longitude = "lng";
			public static readonly string MarkerType = "marker_type";
			public static readonly string Label = "label";
		}

		public override void WriteJson(JsonWriter writer, GMarkerBriefop value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			jo.Add(new JProperty(JsonNode.Latitude, value.Position.Lat));
			jo.Add(new JProperty(JsonNode.Longitude, value.Position.Lng));
			jo.Add(new JProperty(JsonNode.MarkerType, value.MarkerType));
			jo.Add(new JProperty(JsonNode.Label, value.Label));
			jo.WriteTo(writer);
		}

		public override GMarkerBriefop ReadJson(JsonReader reader, Type objectType, GMarkerBriefop existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			double lat = token[JsonNode.Latitude].Value<double>();
			double lng = token[JsonNode.Longitude].Value<double>();
			string sMarkerType = token[JsonNode.Longitude].Value<string>();
			string sLabel = token[JsonNode.Label].Value<string>();

			return new GMarkerBriefop(new PointLatLng(lat, lng), sMarkerType, sLabel);
		}
	}
}