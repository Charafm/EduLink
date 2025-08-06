using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Web.Common.Controllers;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Web.IdentityFrontal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("identity/profile")]
    public class ProfileController : ApiController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("EdulinkIdentity");
            if (Request.Headers.TryGetValue("Authorization", out var authHeader))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authHeader);
            return client;
        }
        //private bool IsFrontOfficeUser()
        //{
        //    /*
        //     */
        //}
        private bool IsFrontOfficeUser()
            => User.FindAll("role").Any(r =>
                r.Value.Equals("Parent", StringComparison.OrdinalIgnoreCase) ||
                r.Value.Equals("Student", StringComparison.OrdinalIgnoreCase));

        [HttpGet]
        public async Task<ActionResult<ProfileDto>> GetProfile(CancellationToken ct)
        {
            try
            {
                var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                using var client = CreateClient();
                var BOclient = _httpClientFactory.CreateClient("EdulinkBackoffice");
                if (Request.Headers.TryGetValue("Authorization", out var authHeader))
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authHeader);
                // Get base user data from FO/BO endpoints
                //var userEndpoint = IsFrontOfficeUser()
                //    ? $"/api/fousers/{userId}"
                //    : $"/api/users/{userId}";
                var test = User.FindFirst("client_id").Value;
                var userEndpoint = string.Empty;
                if (User.FindFirst("client_id").Value == "m2m.SchoolSaas.Frontoffice")
                {
                     userEndpoint = $" /api/fousers/{userId}";
                }
                else
                {
                   userEndpoint = $" /api/users/{userId}";
                }

                    var userResponse = await client.GetAsync(userEndpoint, ct);
                if (!userResponse.IsSuccessStatusCode)
                    return StatusCode((int)userResponse.StatusCode);

                var userData = await JsonSerializer.DeserializeAsync<UserDto>(
                    await userResponse.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                // Get role details for extended profile
                var rolesResponse = await client.GetAsync(
                    IsFrontOfficeUser() ? "/api/fousers/getallroles" : "/api/roles/getallroles",
                    ct
                );

                var rolesData = rolesResponse.IsSuccessStatusCode
                    ? await JsonSerializer.DeserializeAsync<RoleDto[]>(
                        await rolesResponse.Content.ReadAsStreamAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                      )
                    : Array.Empty<RoleDto>();

                // Build composite profile DTO
                var profile = new ProfileDto
                {
                    Id = Guid.Parse(userData.Id),
                    Email = userData.Email,
                    UserName = userData.UserName,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                     
                    Roles = rolesData.Select(r => r.Name).ToArray(),
                   
                };

                // Add parent/staff specific data
                if (User.FindFirst("client_id").Value == "m2m.SchoolSaas.Frontoffice")
                {
                    if(userData.RoleNames.FirstOrDefault() == UserType.Parent.ToString())
                    {
                        var parentData = await BOclient.GetFromJsonAsync<ParentDTO>(
                        $"/parents/byuserid/{userId}",
                        ct
                    );
                        profile.Parent = parentData;
                    }
                    else
                    {
                        var studentData = await BOclient.GetFromJsonAsync<StudentDTO>(
                        $"/students/byuserid/{userId}",
                        ct
                    );
                        profile.Student = studentData;
                    }
                }
                else
                {
                  if(userData.RoleNames.FirstOrDefault() == UserType.Staff.ToString())
                    {
                        var staffData = await BOclient.GetFromJsonAsync<StaffDTO>(
                      $"/staffs/byuserid/{userId}",
                      ct
                  );
                        profile.Staff = staffData;
                    }else if (userData.RoleNames.FirstOrDefault() == UserType.Instructure.ToString())
                    {
                        var teacherData = await BOclient.GetFromJsonAsync<TeacherDTO>(
                   $"/teachers/byuserid/{userId}",
                   ct
               );
                        profile.Teacher = teacherData;
                    }
                    //else
                    //{
                    //    var staffData = await client.GetFromJsonAsync<StaffDTO>(
                    //$"/api/staff/{userId}",
                    //ct
                    //);
                    //    profile.Staff = staffData;
                    //}
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Profile loading failed: {ex.Message}");
            }
        }

        //private async Task<string[]> GetUserPermissions(HttpClient client, string userId, CancellationToken ct)
        //{
        //    var response = await client.GetAsync($"/permission/getuserpermissions?userId={userId}", ct);
        //    return response.IsSuccessStatusCode
        //        ? await JsonSerializer.DeserializeAsync<string[]>(
        //            await response.Content.ReadAsStreamAsync(),
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        //          )
        //        : Array.Empty<string>();
        //}

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto dto,
            CancellationToken ct)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            using var client = CreateClient();
            var endpoint = IsFrontOfficeUser()
                ? "/api/fousers/changepassword"
                : "/api/users/changepassword";

            var response = await client.PutAsJsonAsync(endpoint, dto, ct);
            return response.IsSuccessStatusCode
                ? NoContent()
                : StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
