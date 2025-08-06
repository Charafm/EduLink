using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Web.Identity.ViewModels.Authorization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;
using MediatR;
using SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries;
using SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Infrastructure.Identity.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Web.Identity.Helpers;

namespace SchoolSaas.Web.Identity.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [OpenApiIgnore]
    public class AuthorizationController : Controller
    {
        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IOpenIddictAuthorizationManager _authorizationManager;
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IContextFactory _contextFactory;
        private readonly IMediator _mediatR;
        private readonly ManagerPicker _managerPicker;

        public AuthorizationController(
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictAuthorizationManager authorizationManager,
            IOpenIddictScopeManager scopeManager,
            JwtTokenHandler jwtTokenHandler,
            IContextFactory contextFactory,
            IMediator mediatR,
            ManagerPicker managerPicker)
        {
            _applicationManager = applicationManager;
            _authorizationManager = authorizationManager;
            _scopeManager = scopeManager;

            _contextFactory = contextFactory;
            _mediatR = mediatR;
            _managerPicker = managerPicker;
            _jwtTokenHandler = jwtTokenHandler;
        }

        private (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager) GetManagers(string clientId)
        {
            var context = _contextFactory.CreateContext(clientId);
            var userManager = _managerPicker.GetUserManager(context);
            var signInManager = _managerPicker.GetSignInManager(userManager);
            var roleManager = _managerPicker.GetRoleManager(context);
            return (userManager, signInManager, roleManager);
        }
        [HttpGet("~/connect/authorize")]
        [HttpPost("~/connect/authorize")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Authorize()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            var clientId = request.ClientId;
            if (clientId == null)
                throw new InvalidOperationException("Client ID cannot be null.");
            var (userManager, signInManager, roleManager) = GetManagers(request.ClientId);
            var context = _contextFactory.CreateContext(clientId);


            // If prompt=login was specified by the client application,
            // immediately return the user agent to the login page.
            if (request.HasPrompt(OpenIddictConstants.Prompts.Login))
            {
                var prompt = string.Join(" ", request.GetPrompts().Remove(OpenIddictConstants.Prompts.Login));
                var parameters = Request.HasFormContentType ?
                    Request.Form.Where(parameter => parameter.Key != Parameters.Prompt).ToList() :
                    Request.Query.Where(parameter => parameter.Key != Parameters.Prompt).ToList();
                parameters.Add(System.Collections.Generic.KeyValuePair.Create(Parameters.Prompt, new StringValues(prompt)));

                return Challenge(
                    authenticationSchemes: IdentityConstants.ApplicationScheme,
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = Request.PathBase + Request.Path + QueryString.Create(parameters)
                    });
            }

            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
            if (result == null || !result.Succeeded || (request.MaxAge != null && result.Properties?.IssuedUtc != null &&
                DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value)))
            {
                if (request.HasPrompt(OpenIddictConstants.Prompts.None))
                {
                    return Forbid(
                        authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.LoginRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is not logged in."
                        }));
                }

                return Challenge(
                    authenticationSchemes: IdentityConstants.ApplicationScheme,
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                            Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                    });
            }

            var user = await userManager.GetUserAsync(result.Principal) ??
                throw new InvalidOperationException("The user details cannot be retrieved.");
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
                throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

            var authorizations = await _authorizationManager.FindAsync(
                subject: await userManager.GetUserIdAsync(user),
                client: await _applicationManager.GetIdAsync(application),
                status: Statuses.Valid,
                type: AuthorizationTypes.Permanent,
                scopes: request.GetScopes()).ToListAsync();

            switch (await _applicationManager.GetConsentTypeAsync(application))
            {
                case ConsentTypes.External when !authorizations.Any():
                    return Forbid(
                        authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "The logged in user is not allowed to access this client application."
                        }));
                case ConsentTypes.Implicit:
                case ConsentTypes.External when authorizations.Any():
                case ConsentTypes.Explicit when authorizations.Any() && !request.HasPrompt(OpenIddictConstants.Prompts.Consent):
                    var principal = await signInManager.CreateUserPrincipalAsync(user);

                    principal.SetScopes(request.GetScopes());
                    principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

                    var authorization = authorizations.LastOrDefault();
                    if (authorization == null)
                    {
                        authorization = await _authorizationManager.CreateAsync(
                            principal: principal,
                            subject: await userManager.GetUserIdAsync(user),
                            client: await _applicationManager.GetIdAsync(application),
                            type: AuthorizationTypes.Permanent,
                            scopes: principal.GetScopes());
                    }

                    principal.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));

                    foreach (var claim in principal.Claims)
                    {
                        claim.SetDestinations(GetDestinations(claim, principal));
                    }

                    return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                case ConsentTypes.Explicit when request.HasPrompt(OpenIddictConstants.Prompts.None):
                case ConsentTypes.Systematic when request.HasPrompt(OpenIddictConstants.Prompts.None):
                    return Forbid(
                        authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "Interactive user consent is required."
                        }));
                default:
                    return View(new AuthorizeViewModel
                    {
                        ApplicationName = await _applicationManager.GetDisplayNameAsync(application),
                        Scope = request.Scope
                    });
            }
        }

        [Authorize, FormValueRequired("submit.Accept")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            var clientId = request.ClientId;
            if (clientId == null)
                throw new InvalidOperationException("Client ID cannot be null.");
            var (userManager, signInManager, roleManager) = GetManagers(request.ClientId);



            var user = await userManager.GetUserAsync(User) ??
                throw new InvalidOperationException("The user details cannot be retrieved.");
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
                throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

            var authorizations = await _authorizationManager.FindAsync(
                subject: await userManager.GetUserIdAsync(user),
                client: await _applicationManager.GetIdAsync(application),
                status: Statuses.Valid,
                type: AuthorizationTypes.Permanent,
                scopes: request.GetScopes()).ToListAsync();

            if (!authorizations.Any() && await _applicationManager.HasConsentTypeAsync(application, ConsentTypes.External))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The logged in user is not allowed to access this client application."
                    }));
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);

            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

            var authorization = authorizations.LastOrDefault();
            if (authorization == null)
            {
                authorization = await _authorizationManager.CreateAsync(
                    principal: principal,
                    subject: await userManager.GetUserIdAsync(user),
                    client: await _applicationManager.GetIdAsync(application),
                    type: AuthorizationTypes.Permanent,
                    scopes: principal.GetScopes());
            }

            principal.SetAuthorizationId(await _authorizationManager.GetIdAsync(authorization));

            foreach (var claim in principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, principal));
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [Authorize, FormValueRequired("submit.Deny")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public IActionResult Deny() => Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        [HttpGet("~/connect/logout")]
        public Task<IActionResult> Logout([FromBody] LogoutRequestDto request) => LogoutPost(request);

        [ActionName(nameof(Logout)), HttpPost("~/connect/logout"), ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost([FromBody] LogoutRequestDto request)
        {
            var token = request.Token;

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var principal = _jwtTokenHandler.DecodeToken(token);

            if (principal == null)
            {
                return BadRequest("Invalid token.");
            }

            var clientId = principal.FindFirst("client_id")?.Value;
            var userId = principal.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Client ID or User ID is missing in the token.");
            }

            // Store the client ID in HttpContext.Items
            HttpContext.Items["ClientId"] = clientId;

            var (_, signInManager, _) = GetManagers(clientId);

            // Revoke the token in the database
            var tokenId = principal.FindFirst("oi_tkn_id")?.Value;
            if (tokenId != null)
            {
                var tokenmanager = _managerPicker.GetTokenManager();
                var tokenEntry = await tokenmanager.FindByIdAsync(tokenId);
                if (tokenEntry != null)
                {
                    await tokenmanager.TryRevokeAsync(tokenEntry);
                    tokenEntry.ExpirationDate = DateTime.UtcNow;
                    await tokenmanager.UpdateAsync(tokenEntry);
                }
            }

            await signInManager.SignOutAsync();
            return Ok("Logout successful.");

        }



        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            var clientId = request.ClientId;
            if (clientId == null)
                throw new InvalidOperationException("Client ID cannot be null.");

            if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                return await HandleExchangeCodeGrantType(clientId);
            }
            else if (request.IsClientCredentialsGrantType())
            {
                return await HandleExchangeClientCredentialsGrantType(request);
            }
            else if (request.IsPasswordGrantType())
            {
                return await HandleExchangePasswordGrantType(request);
            }

            else
            {
                throw new InvalidOperationException("The specified grant type is not supported.");
            }
        }

        private async Task<List<string>> GetUserPermissionsAsync(string userId, string clientId)
        {
            // Instead of a format specifier that expects a numeric value,
            // just use {0} to output the string representation:
            Console.WriteLine("Getting permissions for userId: {0}", userId);

            var context = _contextFactory.CreateContext(clientId);
            List<PermissionDto> permissions;

            try
            {
                if (context is FrontOfficeIdentityContext)
                {
                    permissions = await _mediatR.Send(new GetFoPermissionsQuery { UserId = userId });
                }
                else
                {
                    permissions = await _mediatR.Send(new GetUserPermissionsQuery { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in permission query for userId {userId}: {ex}");
                throw;
            }

            return permissions.Select(p => p.Code).ToList();
        }


        private async Task<IActionResult> HandleExchangeClientCredentialsGrantType(OpenIddictRequest request)
        {
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId);
            if (application == null)
            {
                throw new InvalidOperationException("The application details cannot be found in the database.");
            }
            var (userManager, signInManager, roleManager) = GetManagers(request.ClientId);
            var identity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: Claims.Name,
                roleType: Claims.Role);

            identity.AddClaim(Claims.Subject, await _applicationManager.GetClientIdAsync(application));
            identity.AddClaim(Claims.Name, await _applicationManager.GetDisplayNameAsync(application));


            var user = await userManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                var permissions = await GetUserPermissionsAsync(user.Id, request.ClientId);
                var permissionClaims = permissions.Select(p => new Claim("permissions", p));
                identity.AddClaims(permissionClaims);
            }

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

            foreach (var claim in principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, principal));
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }


        private async Task<IActionResult> HandleExchangeCodeGrantType(string clientId)
        {
            var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            var (userManager, signInManager, roleManager) = GetManagers(clientId);

            // Retrieve the user profile corresponding to the refresh token.
            var user = await userManager.GetUserAsync(principal);
            if (user == null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                    }));
            }

            // Ensure the user is still allowed to sign in.
            if (!await signInManager.CanSignInAsync(user))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
                    }));
            }


            // Add permissions to the identity
            var identity = (ClaimsIdentity)principal.Identity;


            foreach (var claim in principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, principal));
            }

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        private async Task<IActionResult> HandleExchangePasswordGrantType(OpenIddictRequest request)
        {
            var (userManager, signInManager, roleManager) = GetManagers(request.ClientId);
            var context = _contextFactory.CreateContext(request.ClientId);
            //var user = context.Set<ApplicationUser>().AsNoTracking().FirstOrDefaultAsync(u => u.UserName == request.Username).Result ;
            // Find user by username or email
                    //var user = await userManager.Users..FindByNameAsync(request.Username) ??
                    //   await userManager.FindByEmailAsync(request.Username);
            var user = await userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.NormalizedUserName == request.Username.ToUpper()) ?? 
                       await userManager.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.NormalizedEmail == request.Username.ToUpper());
            //if (user == null || !(await CheckPassword(user, request.Password, request.ClientId)))
            //{
            var passMatch = (await userManager.CheckPasswordAsync(user, request.Password.ToString()));
            if (user == null || !passMatch)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidRequest,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Invalid username/email or password."
                    }));
            }

            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id ?? throw new InvalidOperationException());
            identity.AddClaim(OpenIddictConstants.Claims.Username, user.UserName, OpenIddictConstants.Destinations.AccessToken);
            identity.AddClaim(OpenIddictConstants.Claims.Name, $"{user.FirstName} {user.LastName}", OpenIddictConstants.Destinations.AccessToken);

            

            List<Claim> claims = new List<Claim>();

            if (userManager.SupportsUserClaim)
            {
                claims.AddRange(await userManager.GetClaimsAsync(user));
            }

            if (userManager.SupportsUserRole)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim("roles", roleName));
                    if (roleManager.SupportsRoleClaims)
                    {
                        ApplicationRole role = await roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }

            var permissions = await GetUserPermissionsAsync(user.Id, request.ClientId);
            var permissionClaims = permissions.Select(p => new Claim("permissions", p));
            identity.AddClaims(permissionClaims);

            identity.AddClaims(claims);

            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

            foreach (var claim in principal.Claims)
            {
                claim.SetDestinations(GetDestinations(claim, principal));
            }

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private class TokenResult
        {
            public string AccessToken { get; set; }
            public string TokenType { get; set; }
            public int ExpiresIn { get; set; }
            public string RefreshToken { get; set; }
        }

        private IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Profile))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Roles))
                        yield return Destinations.IdentityToken;

                    yield break;

                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}