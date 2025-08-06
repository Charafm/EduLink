using Newtonsoft.Json;
using SchoolSaas.Application.Frontoffice;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Infrastructure.Identity;
using SchoolSaas.Web.Common;





var init = args.Contains("/init");
if (init)
{
    args = args.Except(new[] { "/init" }).ToArray();
}


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Use your common extension method to set up the web application:
var converters = new List<JsonConverter>();
// (Insert your converter registration code here, as needed)

builder.AddSchoolWebApplication("frontoffice", services =>
{
    builder.Services.AddFrontofficeApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddAuthentication(builder.Configuration);
    builder.Services.AddIdentityInfrastructure(builder.Configuration);
    // builder.Services.AddFrontofficeInfrastructure(builder.Configuration);

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
    // Optionally, add additional HTTP client registrations.
}, converters);
var app = builder.Build();

// Run migrations/seed if needed.
var runMigrationsAtStartup = app.Configuration.GetValue<bool>("RunMigrationsAtStartup");
if (app.Environment.IsDevelopment() || init || runMigrationsAtStartup)
{
    // Optionally, run migrations and seed data here.
    if (init)
    {
        return;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseOpenApi();
    app.UseSwaggerUi();
}

// Configure middleware pipeline using your common extension method.
app.UseSchoolSaasWebApplication(true);



// Optionally, map SignalR hubs if needed.
// app.MapHub<BackOfficeHub>("NotifyBackOfficeHub");
// app.MapHub<FrontOfficeHub>("NotifyFrontOfficeHub");

app.Run();