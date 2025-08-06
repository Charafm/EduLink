using Newtonsoft.Json;

namespace SchoolSaas.Infrastructure.Common.Converters
{
    public sealed class DateOnlyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly) || objectType == typeof(DateOnly?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dateStr = (string)reader.Value;
            return DateOnly.Parse(dateStr!);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var isoDate = ((DateOnly)value).ToString("O");
            writer.WriteValue(isoDate);
        }
    }
}
