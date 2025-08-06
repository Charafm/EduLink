using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;
using SchoolSaas.Application.Backoffice;
using SchoolSaas.Infrastructure.Backoffice;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Infrastructure.Identity;
using SchoolSaas.Web.Common;
using Serilog;

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



// Use your common extension method to set up the web application:
var converters = new List<JsonConverter>();
// (Insert your converter registration code here, as needed)

builder.AddSchoolWebApplication("backoffice", services =>
{
    builder.Services.AddBackofficeApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);
    builder.Services.AddIdentityInfrastructure(builder.Configuration);
    builder.Services.AddBackofficeInfrastructure(builder.Configuration);

    // (Optional) Additional configuration, e.g. logging:
    // builder.Logging.AddDbLogger(options =>
    // {
    //     builder.Configuration.GetSection("Logging")
    //         .GetSection("Database")
    //         .GetSection("Options")
    //         .Bind(options);
    // });

    // HttpClient registrations:
    builder.Services.AddHttpClient("SchoolSaasIdentity", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("IdentityServiceBaseUrl"));
    });
    builder.Services.AddHttpClient("SchoolSaasEdulink", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("EdulinkServiceBaseUrl"));
    });
    builder.Services.AddHttpClient("EdulinkIdentityFrontal", httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("IdentityFrontalServiceBaseUrl"));
    });
    // Optionally, add additional HTTP client registrations.
}, converters);

// ***** REMOVE any remaining Autofac-specific configuration *****


var app = builder.Build();
var runMigrationsAtStartup = app.Configuration.GetValue<bool>("RunMigrationsAtStartup");
if (app.Environment.IsDevelopment() || init || runMigrationsAtStartup)
{
    //app.RunDbMigrations<IdentityContext>();

    Log.Information("Seeding database...");
    Log.Information("Done seeding database. Exiting.");

    if (init)
    {
        return;
    }
}
//SeedData.EnsureSeedData(app);

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see http://aka.ms/aspnetcore-hsts.
app.UseHsts();
//}
//else
//{
app.UseOpenApi();
app.UseSwaggerUi();
//}

app.UseCors("AllowAllOrigins");
//app.UseSkipValidationMiddleware();
//app.UseCustomTokenValidation();
app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCookiePolicy(new CookiePolicyOptions()
{
    HttpOnly = HttpOnlyPolicy.None,
    Secure = CookieSecurePolicy.Always,
    MinimumSameSitePolicy = (SameSiteMode)(-1)
});

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();