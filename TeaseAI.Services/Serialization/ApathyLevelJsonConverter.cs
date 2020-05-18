using Newtonsoft.Json;
using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.Serialization
{
    public class ApathyLevelJsonConverter : JsonConverter<ApathyLevel>
    {
        public override void WriteJson(JsonWriter writer, ApathyLevel value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override ApathyLevel ReadJson(JsonReader reader, Type objectType, ApathyLevel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
                return ApathyLevel.Moderate;

            var s = (string)reader.Value;

            return string.IsNullOrWhiteSpace(s) ? ApathyLevel.Moderate : ApathyLevel.Create(s).Value;
        }
    }
}
