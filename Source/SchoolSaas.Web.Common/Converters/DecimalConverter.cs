using Newtonsoft.Json;

namespace SchoolSaas.Web.Common.Converters
{
    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?)
                || objectType == typeof(double) || objectType == typeof(double?)
                || objectType == typeof(float) || objectType == typeof(float?);
        }

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(double.Parse(string.Format("{0:0.##}", value)));
        }
    }
}
