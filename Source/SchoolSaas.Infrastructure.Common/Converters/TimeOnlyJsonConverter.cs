using Newtonsoft.Json;

namespace SchoolSaas.Infrastructure.Common.Converters
{
    public sealed class TimeOnlyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeOnly) || objectType == typeof(TimeOnly?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var timeStr = (string)reader.Value;
            return string.IsNullOrEmpty(timeStr) ? null : TimeOnly.Parse(timeStr);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var isoTime = (TimeOnly)value;
            writer.WriteValue(string.Format("{0:D2}:{1:D2}", isoTime.Hour, isoTime.Minute));
        }
    }
}
