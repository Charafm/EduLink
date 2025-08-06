using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSwag.Annotations;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Web.Common.Attributes;
using SchoolSaas.Web.Common.Controllers;
using SchoolSaas.Web.IdentityFrontal.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SchoolSaas.Web.IdentityFrontal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class IdentityController : ApiController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IdentityHeaderOptions _identityHeaderOptions;

        public IdentityController(IHttpClientFactory httpClientFactory, IOptions<IdentityHeaderOptions> identityHeaderOptions)
        {
            _httpClientFactory = httpClientFactory;
            _identityHeaderOptions = identityHeaderOptions.Value;
        }

        [AllowAnonymous] //API can be used without JWT Bearer Token since it is a LOGIN API
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            //Building http Client using the appropriate server Base URL or Address ("http://localhost:5196" from appsetting.JSON) and API Endpoint ("connect/token")
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var tokenUrl = $"{httpClient.BaseAddress}connect/token";
            var clientid =  _identityHeaderOptions.GetClient(loginRequest.UserType);

            //Building Form content since api enpoint accepts body POST requests instead of Parameters request for security reasons
            var formContent = new FormUrlEncodedContent(new[]
            {
                //Either Backoffice or Frontoffice so far, based on client_id server determines which identity database to use
                new KeyValuePair<string, string>("client_id", clientid),
                // For login it is Password grandtype
                new KeyValuePair<string, string>("grant_type", _identityHeaderOptions.grant_type),
                // scope is always "EduLink" , can add offline_access to get refresh token as well,according to OpenIddict Documentation
                new KeyValuePair<string, string>("scope", _identityHeaderOptions.scope), 
                new KeyValuePair<string, string>("client_secret", _identityHeaderOptions.client_secret),
                new KeyValuePair<string, string>("username", loginRequest.Username),
                new KeyValuePair<string, string>("password", loginRequest.Password)
            });
 
            // Sending Post request to get JWT and Refresh Tokens 
            var response = await httpClient.PostAsync(tokenUrl, formContent); 

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("Logout")]

        public async Task<IActionResult> Logout()
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var logoutUrl = $"{httpClient.BaseAddress}connect/logout";

            var token = string.Empty;

            // Add the authorization header with the bearer token
            if (Request.Headers.ContainsKey("Authorization"))
            {
                token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            }
            else
            {

                return BadRequest("Authorization header not found in request");
            }

            var logoutRequest = new LogoutRequestDto
            {
                Token = token
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, logoutUrl)
            {
                Content = new StringContent(JsonSerializer.Serialize(logoutRequest), System.Text.Encoding.UTF8, "application/json")
            };



            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {

                return Ok("Logout successful.");
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();

                return BadRequest(errorResponse);
            }
        }
      
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var userCreateUrl = $"{httpClient.BaseAddress}api/fousers/createuser";
            var userName = await GenerateUniqueUsername(httpClient, request.FirstNameFr, request.LastNameFr);
            var userCreateDto = new UserCreateDto
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = request.Email,
                FirstName = request.FirstNameFr,
                LastName = request.LastNameFr,
            
                Password = request.Password,
       
                PhoneNumber = request.Phone
            };

            if (request.type == UserType.SchoolAdmin)
            {
                userCreateDto.RoleName = "SchoolAdministrator";
            }
            //else if (request.type == UserType.Instructure)
            //{
            //    userCreateDto.RoleName = "Instructure";
            //}
            else if (request.type == UserType.Parent)
            {
                userCreateDto.RoleName = request.type.ToString();
                var httpClientBO = _httpClientFactory.CreateClient("EdulinkBackoffice");
                var strangerCheckUrl = $"{httpClientBO.BaseAddress}/parents/exists?email={Uri.EscapeDataString(request.Email)}";
                var strangerAddUrl = $"{httpClientBO.BaseAddress}/parents/create";

                // Check if parent exists
                var checkResponse = await httpClientBO.GetStringAsync(strangerCheckUrl);
                var userExists = bool.Parse(checkResponse);
                if (!userExists || request.type == UserType.Parent)
                {
                    
                    var strangerDto = new ParentDTO
                    {
                        Id = Guid.NewGuid(),
                        UserId = userCreateDto.Id,
                        FirstNameFr = request.FirstNameFr,
                        FirstNameAr = request.FirstNameAr,
                        LastNameFr = request.LastNameFr,
                        LastNameAr = request.LastNameAr,
                        Email = request.Email,
                        Phone = request.Phone,
                        CIN = request.CIN,
                        //BranchId = request.BranchId,
                        IsIdentityVerified = true,
                        AddressFr = request.AddressFr,
                        AddressAr = request.AddressAr,
                        Occupation = request.Occupation,
                        VerificationDate = DateTime.Now,

                    };

                    var addResponse = await httpClientBO.PostAsJsonAsync(strangerAddUrl, strangerDto);
                    if (!addResponse.IsSuccessStatusCode)
                    {
                        return BadRequest("Error adding parent.");
                    }
                }
                else
                {
                    return BadRequest("Parent already exists.");
                }
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(userCreateDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(userCreateUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }


        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var forgotPasswordUrl = $"{httpClient.BaseAddress}api/fousers/PasswordForgotten";

            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(forgotPasswordUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, UserType Type)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var resetPasswordUrl = string.Empty;
            if (Type == UserType.Staff || Type == UserType.Instructure || Type == UserType.SchoolAdmin)
            {
                 resetPasswordUrl = $"{httpClient.BaseAddress}/api/users/resetpassword";
            }else if (Type == UserType.Student || Type == UserType.Parent)
            {
                 resetPasswordUrl = $"{httpClient.BaseAddress}api/fousers/NewPassword";
            }
                

            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(resetPasswordUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [SkipTokenValidation]
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var tokenUrl = $"{httpClient.BaseAddress}connect/token";

            var formContent = new FormUrlEncodedContent(new[]
            {
                
                new KeyValuePair<string, string>("grant_type","refresh_token"),
                new KeyValuePair<string, string>("scope", _identityHeaderOptions.scope),
                new KeyValuePair<string, string>("client_secret", _identityHeaderOptions.client_secret),
                new KeyValuePair<string, string>("refresh_token", request.RefreshToken)
            });
           
            if (request.UserType == UserType.Staff || request.UserType == UserType.Instructure)
            {
                formContent.Headers.Add("client_id", new[] { _identityHeaderOptions.ClientId_Backoffice });
            }
            else if (request.UserType == UserType.Student || request.UserType == UserType.Parent)
            {
                formContent.Headers.Add("client_id", new[] { _identityHeaderOptions.ClientId_Frontoffice });
            }
            var response = await httpClient.PostAsync(tokenUrl, formContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [SkipTokenValidation]
        [AllowAnonymous]
        [HttpPost("ValidateToken")]
        public async Task<IActionResult> CheckValidation([FromBody] string Token)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var tokenUrl = $"{httpClient.BaseAddress}token/validate";

            //if (!Request.Headers.ContainsKey("Authorization"))
            //{
            //    return BadRequest("Authorization header not found in request");
            //}

            // Extract the Authorization header from the incoming request
            var authorizationHeader = Token;

            // Add the Authorization header to the outgoing request
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationHeader.Split(" ").Last());

            var response = await httpClient.GetAsync(tokenUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [AllowAnonymous]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[OpenApiIgnore]
        [HttpPost("CreateStudentUser")]
        public async Task<IActionResult> CreateStudentUser([FromBody] CreateStudentUserDTO request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var userCreateUrl = $"{httpClient.BaseAddress}api/fousers/createuser";
            var userName = await GenerateUniqueUsername(httpClient, request.FirstNameFr, request.LastNameFr);
            var userCreateDto = new UserCreateDto
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = request.Email,
                FirstName = request.FirstNameFr,
                LastName = request.LastNameFr,
                Password = request.Password,
                RoleName = "Student",
                PhoneNumber = request.Phone
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(userCreateDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(userCreateUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [AllowAnonymous]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[OpenApiIgnore]
        [HttpPost("CreateStaffUser")]
        public async Task<IActionResult> CreateStaffUser([FromBody] CreateUserRequestDto request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var userCreateUrl = $"{httpClient.BaseAddress}api/users/createuser";
            var userName = await GenerateUniqueUsername(httpClient, request.FirstNameFr, request.LastNameFr);
            var userCreateDto = new UserCreateDto
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = request.Email,
                FirstName = request.FirstNameFr,
                LastName = request.LastNameFr,

                Password = request.Password,
                RoleName = request.type.ToString(),
                PhoneNumber = request.Phone
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(userCreateDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(userCreateUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        [AllowAnonymous]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[OpenApiIgnore]
        [HttpPost("CreateTeacherUser")]
        public async Task<IActionResult> CreateTeacherUser([FromBody] CreateUserRequestDto request)
        {
            var httpClient = _httpClientFactory.CreateClient("EdulinkIdentity");
            var userCreateUrl = $"{httpClient.BaseAddress}api/users/createuser";
            var userName = await GenerateUniqueUsername(httpClient, request.FirstNameFr, request.LastNameFr);
            var userCreateDto = new UserCreateDto
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Email = request.Email,
                FirstName = request.FirstNameFr,
                LastName = request.LastNameFr,

                Password = request.Password,
                RoleName = request.type.ToString(),
                PhoneNumber = request.Phone
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(userCreateDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(userCreateUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return Ok(responseString);
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return BadRequest(errorResponse);
            }
        }
        private async Task<string> GenerateUniqueUsername(HttpClient httpClient, string firstName, string lastName)
        {
            // Extract the first two characters of the last name
            var shortLastName = lastName.Length >= 2 ? lastName.Substring(0, 2).ToLower() : lastName.ToLower();

            // Remove any spaces from the first name and convert to lowercase
            var formattedFirstName = firstName.Replace(" ", "").ToLower();

            var baseUsername = $"{shortLastName}{formattedFirstName}";
            var username = baseUsername;
            //var count = 1;

            //// Ensure the username is unique by appending numbers if necessary
            //while (await IsUsernameTaken(httpClient, username))
            //{
            //    username = $"{baseUsername}{count}";
            //    count++;
            //}

            return username;
        }
        //private async Task<bool> IsUsernameTaken(HttpClient httpClient, string username)
        //{
        //    var checkUrl = $"{httpClient.BaseAddress}/api/fousers?UserName={username}";
        //    var response = await httpClient.GetAsync(checkUrl);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new HttpRequestException($"Error checking username: {response.StatusCode}");
        //    }

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var users = JsonSerializer.Deserialize<PagedResultOfUser>(responseString);

        //    return users != null && users.Items.Any();
        //}
    }
}
