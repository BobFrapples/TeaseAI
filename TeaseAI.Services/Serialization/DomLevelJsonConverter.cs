using Newtonsoft.Json;
using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.Serialization
{
    public class DomLevelJsonConverter : JsonConverter<DomLevel>
    {
        public override void WriteJson(JsonWriter writer, DomLevel value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override DomLevel ReadJson(JsonReader reader, Type objectType, DomLevel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
                return DomLevel.Tease;

            var s = (string)reader.Value;
            return string.IsNullOrWhiteSpace(s) ? DomLevel.Tease : DomLevel.Create(s).Value;
        }
    }
}
