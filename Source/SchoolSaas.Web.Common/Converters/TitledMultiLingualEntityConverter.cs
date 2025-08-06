using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Infrastructure.Common.Converters;


namespace SchoolSaas.Web.Common.Converters
{
    public class TitledMultiLingualEntityConverter<TEntity, TTranslation, TId> : BaseConverter<TEntity>
        where TEntity : TitledMultiLingualEntity<TTranslation, TId>
        where TTranslation : TitledEntityTranslation<TEntity, TId>
    {
        private readonly Type _documentType;

        public TitledMultiLingualEntityConverter(Type documentType)
        {
            _documentType = documentType;
        }

        public override bool CanRead => false;
        public override void WriteJson(JsonWriter writer, TEntity? value, JsonSerializer serializer)
        {
            if (value?.Translations.Any() ?? false)
            {
                var translation = value.Translations.FirstOrDefault(e => e.LanguageCode == Thread.CurrentThread.CurrentUICulture.Name);
                if (translation != null)
                {
                    value.Title = translation.Title;
                    value.Description = translation.Description;
                }

                value.Translations = new List<TTranslation>();
            }

            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = new List<JsonConverter>() { new StringEnumConverter(), new DecimalConverter(), new TimeOnlyJsonConverter(), new DateOnlyJsonConverter() }
                };

                if (_documentType != null)
                {
                    var converterType = typeof(DocumentConverter<>).MakeGenericType(_documentType);
                    settings.Converters.Add((JsonConverter)Activator.CreateInstance(converterType));
                }

                return settings;
            };

            writer.WriteRawValue(JsonConvert.SerializeObject(value));
        }
    }
}