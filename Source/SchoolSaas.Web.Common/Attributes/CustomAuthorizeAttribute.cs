using Microsoft.AspNetCore.Authorization;

namespace SchoolSaas.Web.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //Using User Roles 
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
    //Using Permissions

    //public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    //{
    //    private readonly string _permission;

    //    public CustomAuthorizeAttribute(string permission)
    //    {
    //        _permission = permission;
    //    }

    //    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //    {
    //        var user = context.HttpContext.User;

    //        if (!user.Identity.IsAuthenticated)
    //        {
    //            Log.Error("User is not authenticated.");
    //            context.Result = new UnauthorizedResult();
    //            return;
    //        }

    //        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //        if (string.IsNullOrEmpty(userId))
    //        {
    //            Log.Error("User ID is null or empty.");
    //            context.Result = new ForbidResult();
    //            return;
    //        }

    //        var token = context.HttpContext.Request.Headers["Authorization"].ToString();
    //        if (string.IsNullOrEmpty(token))
    //        {
    //            Log.Error("Authorization token is missing.");
    //            context.Result = new UnauthorizedResult();
    //            return;
    //        }

    //        try
    //        {
    //            token = token.Replace("Bearer ", string.Empty);
    //            var permissions = await GetPermissions(userId, token, context);
    //            var hasPermission = permissions.Any(p => p.Name == _permission);
    //            if (!hasPermission)
    //            {
    //                Log.Error("User does not have the required permission.");
    //                context.Result = new UnauthorizedResult();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error($"Error while fetching permissions: {ex.Message}");
    //            context.Result = new ForbidResult();
    //        }
    //    }

    //    private async Task<List<PermissionDto>> GetPermissions(string userId, string token, AuthorizationFilterContext context)
    //    {
    //        // Try to get permissions using the direct method first
    //        try
    //        {
    //            var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
    //            if (permissionService != null)
    //            {
    //                var permissions = await permissionService.GetUserPermissionsAsync(userId);
    //                if (permissions != null)
    //                {
    //                    return permissions;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error($"Error while using direct method: {ex.Message}");
    //        }

    //        // Fallback to API request if direct method fails
    //        return await GetUserPermissionsViaApi(userId, token);
    //    }

    //    private async Task<List<PermissionDto>> GetUserPermissionsViaApi(string userId, string token)
    //    {
    //        using var client = new HttpClient();
    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    //        var response = await client.GetAsync($"http://localhost:4480/permission/getuserpermissions?userId={userId}");
    //        response.EnsureSuccessStatusCode();

    //        var jsonResponse = await response.Content.ReadAsStringAsync();
    //        return JsonSerializer.Deserialize<List<PermissionDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    //    }
    //}
    //

}