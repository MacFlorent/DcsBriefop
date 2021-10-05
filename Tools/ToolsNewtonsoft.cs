using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace DcsBriefop.Tools
{
	//https://dotnetfiddle.net/sVWsE4
	internal class GMapOverlayJsonConverter : JsonConverter<GMapOverlay>
	{
		private static class JsonNode
		{
			public static readonly string Markers = "markers";
		}

		public override void WriteJson(JsonWriter writer, GMapOverlay value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			if (value.Markers is object && value.Markers.Count > 0)
			{
				JArray ja = new JArray();
				foreach (GMarkerGoogle gmm in value.Markers)
					ja.Add(JToken.FromObject(gmm, serializer));

				jo[JsonNode.Markers] = ja;
			}

			jo.WriteTo(writer, new GMarkerGoogleJsonConverter());
		}

		public override GMapOverlay ReadJson(JsonReader reader, Type objectType, GMapOverlay existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			List<GMarkerGoogle> markers = token[JsonNode.Markers].ToObject<List<GMarkerGoogle>>(serializer);
			return null;
		}
	}

	public class GMarkerGoogleJsonConverter : JsonConverter<GMarkerGoogle>
	{
		private static class JsonNode
		{
			public static readonly string Latitude = "lat";
			public static readonly string Longitude = "lng";
			public static readonly string MarkerType = "marker_type";
		}

		public override void WriteJson(JsonWriter writer, GMarkerGoogle value, JsonSerializer serializer)
		{
			JObject jo = new JObject();
			jo.Add(new JProperty(JsonNode.Latitude, value.Position.Lat));
			jo.WriteTo(writer);
		}

		public override GMarkerGoogle ReadJson(JsonReader reader, Type objectType, GMarkerGoogle existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			double lat = token[JsonNode.Latitude].Value<double>();
			//return new GMarkerGoogle(new PointLatLng(token[JsonNode.Latitude]))

			return null;
		}
	}
}