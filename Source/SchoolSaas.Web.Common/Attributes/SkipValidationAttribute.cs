using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolSaas.Web.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SkipTokenValidationAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // This will be handled by middleware, so nothing to do here
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // This will be handled by middleware, so nothing to do here
        }
    }
}
