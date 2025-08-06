using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Domain.Common.Localization;
using System.Reflection;

namespace SchoolSaas.Web.Common.Controllers
{
    /// <summary>
    /// Resources values/description
    /// </summary>
    [Route("[controller]")]
    public class ResourcesController : ApiController
    {
        private readonly IStringLocalizer<Enums> _localizer;

        public ResourcesController(IStringLocalizer<Enums> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Resources values/description list
        /// </summary>
        /// <returns></returns>
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IDictionary<string, IDictionary<string, string>>> Get()
        {
            var assembly = typeof(DocumentTypeEnum).Assembly;
            var enums = assembly.GetTypes().Where(e => e.IsSubclassOf(typeof(Enum)));
            var method = typeof(ResourcesController).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(GetEnumValues));

            var resources = new Dictionary<string, IDictionary<string, string>>();

            foreach (Type type in enums)
            {
                resources.Add(type.Name, (IDictionary<string, string>)method.MakeGenericMethod(type).Invoke(this, new[] { type }));
            }

            return Ok(resources);
        }

        private IDictionary<string, string> GetEnumValues<T>(Type type) where T : Enum
        {
            return Enum.GetValues(type).Cast<T>().Where(e => e.ToString() != DocumentTypeEnum.Unspecified.ToString()).ToDictionary(e => e.ToString(), e => GetLocalizedValue(e));
        }

        private string GetLocalizedValue(Enum enumValue)
        {
            return _localizer.GetString($"{enumValue.GetType().Name}_{enumValue}");
        }
    }
}
