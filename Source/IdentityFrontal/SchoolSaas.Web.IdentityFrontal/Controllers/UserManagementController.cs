using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Web.Common.Controllers;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Application.Common.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;

namespace SchoolSaas.Web.IdentityFrontal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("identity/usermanagement")]
    public class UserManagementController : ApiController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetClient(string? bearerToken = null)
        {
            var client = _httpClientFactory.CreateClient("EdulinkIdentity");
            if (!string.IsNullOrEmpty(bearerToken))
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            return client;
        }

        private static string GetAreaPath(UserType type, string foPath, string boPath)
        {
            return (type == UserType.Parent || type == UserType.Student) ? foPath : boPath;
        }

        // 🧑 USERS

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] UserType type, [FromQuery] string? username = null, [FromQuery] string? email = null)
        {
            var client = GetClient();
            var area = GetAreaPath(type, "api/fousers", "api/users");
            var url = $"{area}?UserName={username}&Email={email}";
            return await ForwardGet(client, url);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(Guid id, [FromQuery] UserType type)
        {
            var client = GetClient();
            var area = GetAreaPath(type, $"api/fousers/{id}", $"api/users/{id}");
            return await ForwardGet(client, area);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromQuery] UserType type, [FromBody] object dto)
        {
            var client = GetClient();
            var area = GetAreaPath(type, $"api/fousers/{id}", $"api/users/{id}");
            return await ForwardPut(client, area, dto);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, [FromQuery] UserType type)
        {
            var client = GetClient();
            var area = GetAreaPath(type, $"api/fousers/{id}", $"api/users/{id}");
            return await ForwardDelete(client, area);
        }

        [HttpPost("users/activate/{id}")]
        public async Task<IActionResult> ActivateUser(Guid id, [FromQuery] UserType type)
        {
            var client = GetClient();
            var area = GetAreaPath(type, $"api/fousers/{id}/activate", $"api/users/{id}/activate");
            return await ForwardPost(client, area, null);
        }

        [HttpPost("users/deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(Guid id, [FromQuery] UserType type)
        {
            var client = GetClient();
            var area = GetAreaPath(type, $"api/fousers/{id}/deactivate", $"api/users/{id}/deactivate");
            return await ForwardPost(client, area, null);
        }

        [HttpPost("users/resetpassword/{id}")]
        public async Task<IActionResult> ResetPassword(Guid id, [FromQuery] UserType type, [FromBody] ResetPasswordRequest request)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, $"api/fousers/resetpassword", $"api/users/resetpassword");
            return await ForwardPut(client, endpoint, request);
        }

        // 🔐 ROLES

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles([FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "api/fousers/getallroles", "api/roles/getallroles");
            return await ForwardGet(client, endpoint);
        }

        [HttpPut("roles/update")]
        public async Task<IActionResult> UpdateRole([FromQuery] string roleId, [FromQuery] string newName, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "api/fousers/updaterole", "api/roles/updaterole") + $"?newRoleName={Uri.EscapeDataString(newName)}";
            return await ForwardPut(client, endpoint, roleId);
        }

        [HttpPost("roles/create")]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "api/fousers/createrole", "api/roles/createrole");
            return await ForwardPost(client, endpoint, roleName);
        }

        [HttpDelete("roles/delete")]
        public async Task<IActionResult> DeleteRole([FromQuery] string roleId, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, $"api/fousers/deleterole?roleId={roleId}", $"api/roles/deleterole?roleId={roleId}");
            return await ForwardDelete(client, endpoint);
        }

        [HttpPut("roles/assign")]
        public async Task<IActionResult> AssignUserToRole([FromQuery] Guid userId, [FromQuery] string roleName, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, $"api/fousers/{userId}/{roleName}", $"api/users/{userId}/{roleName}");
            return await ForwardPut(client, endpoint, null);
        }

        // 🔑 PERMISSIONS

        [HttpPost("permissions/assign")]
        public async Task<IActionResult> AssignPermissionsToUser([FromBody] AssignPermissionsDto dto, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "permission/assignpermissionstouser", "permission/assignpermissionstouser");
            return await ForwardPost(client, endpoint, dto);
        }

        [HttpPost("permissions/assign-role")]
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] AssignPermissionsToRoleDto dto, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "permission/assignpermissionstorole", "permission/assignpermissionstorole");
            return await ForwardPost(client, endpoint, dto);
        }

        [HttpDelete("permissions/unassign")]
        public async Task<IActionResult> UnassignPermissionsFromUser([FromBody] AssignPermissionsDto dto, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "permission/unassignpermissionsfromuser", "permission/unassignpermissionsfromuser");
            return await ForwardDelete(client, endpoint, dto);
        }

        [HttpDelete("permissions/unassign-role")]
        public async Task<IActionResult> UnassignPermissionsFromRole([FromBody] AssignPermissionsToRoleDto dto, [FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "permission/unassignpermissionsfromrole", "permission/unassignpermissionsfromrole");
            return await ForwardDelete(client, endpoint, dto);
        }

        // 🔧 UTILITY SCOPES

        [HttpGet("utilityscopes")]
        public async Task<IActionResult> GetAllUtilityScopes([FromQuery] UserType type)
        {
            var client = GetClient();
            var endpoint = GetAreaPath(type, "foutilityscope/getallutilityscopes", "utilityscope/getallutilityscopes");
            return await ForwardGet(client, endpoint);
        }

        //[HttpPost("utilityscopes/assign-to-role")]
        //public async Task<IActionResult> AssignUtilityScopeToRole([FromBody] AssignUtilityScopeDto dto, [FromQuery] UserType type)
        //{
        //    var client = GetClient();
        //    var endpoint = GetAreaPath(type, "foutilityscope/assignutilityscopetorole", "utilityscope/assignutilityscopetorole");
        //    return await ForwardPost(client, endpoint, dto);
        //}

        // 🔁 UTILITY: Shared forwarding logic
        private async Task<IActionResult> ForwardGet(HttpClient client, string endpoint)
        {
            var response = await client.GetAsync(endpoint);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<IActionResult> ForwardPost(HttpClient client, string endpoint, object? body)
        {
            var content = body is null ? null : new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content ?? new StringContent(""));
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<IActionResult> ForwardPut(HttpClient client, string endpoint, object? body)
        {
            var content = body is null ? null : new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(endpoint, content ?? new StringContent(""));
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<IActionResult> ForwardDelete(HttpClient client, string endpoint, object? body = null)
        {
            if (body == null)
            {
                var response = await client.DeleteAsync(endpoint);
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(endpoint, UriKind.RelativeOrAbsolute),
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
            var responseFull = await client.SendAsync(request);
            return StatusCode((int)responseFull.StatusCode, await responseFull.Content.ReadAsStringAsync());
        }
    }
}
