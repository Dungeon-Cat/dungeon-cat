using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
namespace Scripts.Utility
{
    public class JsonSettings
    {
        public static readonly JsonSerializerSettings SerializerSettings = new()
        {
            Converters =
            {
                new Vector2Converter()
            },
            Formatting = Formatting.Indented
        };
    }

    public class Vector2Converter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var vector = (Vector2) value!;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            serializer.Serialize(writer, vector.x);
            writer.WritePropertyName("y");
            serializer.Serialize(writer, vector.y);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var x = jsonObject["x"]!.Value<float>();
            var y = jsonObject["y"]!.Value<float>();
            return new Vector2(x, y);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector2);
        }
    }
}