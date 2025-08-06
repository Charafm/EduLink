using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using SchoolSaas.Web.Common.Attributes;
using System.Reflection;

namespace SchoolSaas.Web.Common.Middlewares
{
    public class CollectSkipValidationPathsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CollectSkipValidationPathsMiddleware> _logger;
        public static HashSet<string> SkipValidationPaths = new HashSet<string>();

        public CollectSkipValidationPathsMiddleware(RequestDelegate next, ILogger<CollectSkipValidationPathsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var controllers = new List<Type>();

            foreach (var assembly in assemblies)
            {
                controllers.AddRange(assembly.GetTypes()
                    .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract));
            }

            foreach (var controller in controllers)
            {
                var controllerRouteAttributes = controller.GetCustomAttributes<RouteAttribute>(true);
                var controllerTemplate = controllerRouteAttributes.FirstOrDefault()?.Template ?? string.Empty;

                var hasControllerAttribute = controller.GetCustomAttributes(typeof(SkipTokenValidationAttribute), true).Any();
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(method => method.GetCustomAttributes(typeof(SkipTokenValidationAttribute), true).Any() || hasControllerAttribute);

                foreach (var action in actions)
                {
                    var httpMethodAttributes = action.GetCustomAttributes().Where(attr => attr is HttpMethodAttribute).Cast<HttpMethodAttribute>();

                    foreach (var httpMethodAttribute in httpMethodAttributes)
                    {
                        var actionTemplate = httpMethodAttribute.Template ?? action.Name;
                        var fullTemplate = CombineTemplates(controllerTemplate, actionTemplate, controller.Name).ToLower();

                        if (!SkipValidationPaths.Contains(fullTemplate))
                        {
                            _logger.LogInformation($"Adding path to SkipValidationPaths: {fullTemplate}");
                            SkipValidationPaths.Add(fullTemplate);
                        }
                    }
                }
            }

            await _next(context);
        }

        private string CombineTemplates(string controllerTemplate, string actionTemplate, string controllerName)
        {
            if (string.IsNullOrEmpty(controllerTemplate))
                return actionTemplate;

            if (controllerTemplate.Contains("[Controller]", StringComparison.OrdinalIgnoreCase))
            {
                controllerTemplate = controllerTemplate.Replace("[Controller]", controllerName.Replace("Controller", ""), StringComparison.OrdinalIgnoreCase);
            }

            if (string.IsNullOrEmpty(actionTemplate))
                return controllerTemplate;

            return $"/{controllerTemplate}/{actionTemplate}";
        }
    }

    public static class CollectSkipValidationPathsMiddlewareExtensions
    {
        public static IApplicationBuilder UseSkipValidationPathsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CollectSkipValidationPathsMiddleware>();
        }
    }
}
