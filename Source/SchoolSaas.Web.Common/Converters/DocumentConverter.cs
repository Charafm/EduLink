using SchoolSaas.Domain.Common.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SchoolSaas.Web.Common.Converters
{
    public class DocumentConverter<TDocument> : BaseConverter<TDocument>
        where TDocument : AbstractDocument
    {
        public override bool CanRead => false;
        public override void WriteJson(JsonWriter writer, TDocument? value, JsonSerializer serializer)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter>() { new StringEnumConverter() }
            };

            writer.WriteRawValue(JsonConvert.SerializeObject(new
            {
                id = value?.Id,
                type = value?.Type,
                spec = value?.Spec,
                mimeType = value?.MimeType,
                value?.Uri,
                status = value?.Status,
                created = value?.Created,
                lastModified = value?.LastModified,
                comment = value?.Comment,
                data = "",
            }));
        }
    }
}
