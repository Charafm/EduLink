using SchoolSaas.Web.Common;
using Newtonsoft.Json;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Web.IdentityFrontal.Models;

var init = args.Contains("/init");
if (init)
{
    args = args.Except(new[] { "/init" }).ToArray();
}
var swagger = args.Contains("/swagger");
if (swagger)
{
    args = args.Except(new[] { "/swagger" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://+:8080");

var converters = new List<JsonConverter>() { };

builder.AddSchoolWebApplication("IdentityFrontal", services =>
{
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);

    var identityUrl = builder.Configuration["IdentityServiceBaseUrl"] ?? Environment.GetEnvironmentVariable("IdentityServiceBaseUrl");
    if (!Uri.IsWellFormedUriString(identityUrl, UriKind.Absolute))
        throw new InvalidOperationException($"Invalid IdentityServiceBaseUrl: {identityUrl}");

    builder.Services.AddHttpClient("EdulinkIdentity", httpClient =>
    {
        httpClient.BaseAddress = new Uri(identityUrl);
    });

    var backofficeUrl = builder.Configuration["BackofficeServiceBaseUrl"] ?? Environment.GetEnvironmentVariable("BackofficeServiceBaseUrl");
    if (!Uri.IsWellFormedUriString(backofficeUrl, UriKind.Absolute))
        throw new InvalidOperationException($"Invalid BackofficeServiceBaseUrl: {backofficeUrl}");

    builder.Services.AddHttpClient("EdulinkBackoffice", httpClient =>
    {
        httpClient.BaseAddress = new Uri(backofficeUrl);
    });

    builder.Services.Configure<IdentityHeaderOptions>(builder.Configuration.GetSection("IdentityHeader"));

}, converters);

var app = builder.Build();
app.UseExceptionHandler("/Home/Error");
app.UseOpenApi();
app.UseSwaggerUi();
if (app.Environment.IsDevelopment())
{
    if (init)
    {
        return;
    }
   
}
else
{
    var isContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    if (!app.Environment.IsDevelopment() && !isContainer)
    {
        app.UseHttpsRedirection();
    }
}
app.MapGet("/healthz", () => Results.Ok("Healthy"));
app.UseSchoolSaasWebApplication(swagger);

app.Run();

public partial class Program { }
