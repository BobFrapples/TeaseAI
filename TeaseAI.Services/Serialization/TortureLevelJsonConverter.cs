using Newtonsoft.Json;
using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.Serialization
{
    public class TortureLevelJsonConverter : JsonConverter<TortureLevel>
    {
        public override void WriteJson(JsonWriter writer, TortureLevel value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override TortureLevel ReadJson(JsonReader reader, Type objectType, TortureLevel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
                return TortureLevel.Create(3).Value;

            var s = (int)reader.Value;
            return s == 0 ? TortureLevel.Create(3).Value : TortureLevel.Create(s).Value;
        }
    }
}
