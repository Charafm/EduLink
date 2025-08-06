using SchoolSaas.Web.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace SchoolSaas.Web.Identity.Helpers
{
    public class SkipValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SkipValidationMiddleware> _logger;
        public static HashSet<string> SkipValidationPaths = new HashSet<string>();

        public SkipValidationMiddleware(RequestDelegate next, ILogger<SkipValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract the route templates from the attributes applied to the controller actions
            var controllers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type));

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
                        //var fullTemplate = CombineTemplates(controllerTemplate, actionTemplate);

                        if (!SkipValidationPaths.Contains(actionTemplate))
                        {
                            //_logger.LogInformation($"Adding path to SkipValidationPaths: {fullTemplate}");
                            SkipValidationPaths.Add(actionTemplate);
                        }
                    }
                }
            }

            await _next(context);
        }
    }

    public static class SkipValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSkipValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SkipValidationMiddleware>();
        }
    }
}
