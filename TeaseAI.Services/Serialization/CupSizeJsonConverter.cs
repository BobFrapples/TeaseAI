using Newtonsoft.Json;
using System;
using TeaseAI.Common.Constants;

namespace TeaseAI.Services.Serialization
{
    public class CupSizeJsonConverter : JsonConverter<CupSize>
    {
        public override void WriteJson(JsonWriter writer, CupSize value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override CupSize ReadJson(JsonReader reader, Type objectType, CupSize existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
                return CupSize.CCup;

            var s = (string)reader.Value;

            return string.IsNullOrWhiteSpace(s) ? CupSize.CCup : CupSize.Create(s).Value;
        }
    }
}
