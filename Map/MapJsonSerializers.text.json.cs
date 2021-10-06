//using GMap.NET;
//using GMap.NET.WindowsForms;
//using GMap.NET.WindowsForms.Markers;
//using System;
//using System.Collections.Generic;
//using System.Text.Json;
//using System.Text.Json.Serialization;

		public static CustomData DeserializeJson(string sJson)
		{
			JsonSerializerOptions options = new JsonSerializerOptions();
			options.Converters.Add(new GMarkerGoogleJsonConverter());
			options.Converters.Add(new GMapOverlayJsonConverter());
			return JsonSerializer.Deserialize<CustomData>(sJson, options);
		}

		public string SerializeToJson()
		{
			var options = new JsonSerializerOptions();
			options.Converters.Add(new GMarkerGoogleJsonConverter());
			options.Converters.Add(new GMapOverlayJsonConverter());
			options.WriteIndented = true;
			return JsonSerializer.Serialize(this, options);
		}



//namespace DcsBriefop.Tools
//{
//	internal static class ToolsJson
//	{
//		public static string GetConvertedPropertyName(string sPropertyName, JsonSerializerOptions options)
//		{
//			return options.PropertyNamingPolicy?.ConvertName(sPropertyName) ?? sPropertyName;
//		}

//		public static bool CheckPropertyName(string sPropertyName, string sValue)
//		{
//			return sPropertyName == sValue;
//		}
//	}

//	internal class GMapOverlayJsonConverter : JsonConverter<GMapOverlay>
//	{
//		private static class JsonNode
//		{
//			public static readonly string Markers = "markers";
//		}

//		public override GMapOverlay Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//		{
//			if (reader.TokenType != JsonTokenType.StartObject)
//				throw new JsonException();

//			GMapOverlay gmo = new GMapOverlay();
//			List<GMarkerGoogle> markers = null;

//			while (reader.Read())
//			{
//				if (reader.TokenType == JsonTokenType.EndObject)
//				{
//					break;
//				}
//				else if (reader.TokenType == JsonTokenType.PropertyName)
//				{
//					string sPropertyName = reader.GetString();
//					reader.Read();
//					if (ToolsJson.CheckPropertyName(JsonNode.Markers, sPropertyName))
//					{
//						markers = JsonSerializer.Deserialize<List<GMarkerGoogle>>(ref reader, options);
//					}
//				}
//			}

//			if (markers is object && markers.Count > 0)
//			{
//				foreach (GMarkerGoogle gmm in markers)
//				{
//					gmo.Markers.Add(gmm);
//				}
//			}

//			return gmo;
//		}

//		public override void Write(Utf8JsonWriter writer, GMapOverlay value, JsonSerializerOptions options)
//		{
//			writer.WriteStartObject();
//			if (value.Markers is object && value.Markers.Count > 0)
//			{
//				List<GMarkerGoogle> markers = new List<GMarkerGoogle>();
//				foreach (GMarkerGoogle gmm in value.Markers)
//					markers.Add(gmm);

//				writer.WritePropertyName(ToolsJson.GetConvertedPropertyName(JsonNode.Markers, options));
//				JsonSerializer.Serialize(writer, markers, options);
//			}
//			writer.WriteEndObject();
//		}
//	}

//	public class GMarkerGoogleJsonConverter : JsonConverter<GMarkerGoogle>
//	{
//		private static class JsonNode
//		{
//			public static readonly string Latitude = "lat";
//			public static readonly string Longitude = "lng";
//			public static readonly string MarkerType = "marker_type";
//		}

//		public override GMarkerGoogle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//		{
//			if (reader.TokenType != JsonTokenType.StartObject)
//				throw new JsonException();

//			double dLat = 0, dLng = 0;
//			GMarkerGoogleType markerType = GMarkerGoogleType.blue_dot;

//			while (reader.Read())
//			{
//				if (reader.TokenType == JsonTokenType.EndObject)
//				{
//					break;
//				}
//				else if (reader.TokenType == JsonTokenType.PropertyName)
//				{
//					string sPropertyName = reader.GetString();
//					reader.Read();
//					if (ToolsJson.CheckPropertyName(JsonNode.Latitude, sPropertyName))
//						dLat = reader.GetDouble();
//					else if (ToolsJson.CheckPropertyName(JsonNode.Longitude, sPropertyName))
//						dLng = reader.GetDouble();
//					if (ToolsJson.CheckPropertyName(JsonNode.MarkerType, sPropertyName))
//						Enum.TryParse(reader.GetString(), out markerType);

//				}
//			}

//			return new GMarkerGoogle(new PointLatLng(dLat, dLng), markerType);
//		}

//		public override void Write(Utf8JsonWriter writer, GMarkerGoogle value, JsonSerializerOptions options)
//		{
//			writer.WriteStartObject();
//			writer.WriteNumber(ToolsJson.GetConvertedPropertyName(JsonNode.Latitude, options), value.Position.Lat);
//			writer.WriteNumber(ToolsJson.GetConvertedPropertyName(JsonNode.Longitude, options), value.Position.Lng);
//			writer.WriteString(ToolsJson.GetConvertedPropertyName(JsonNode.MarkerType, options), value.Type.ToString());
//			writer.WriteEndObject();
//		}

//	}
//}