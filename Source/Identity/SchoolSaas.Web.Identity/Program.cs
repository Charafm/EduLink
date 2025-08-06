using SchoolSaas.Application.Identity;
using SchoolSaas.Infrastructure.Identity;
using SchoolSaas.Web.Common;
using SchoolSaas.Web.Identity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SchoolSaas.Web.Identity.Helpers;
using SchoolSaas.Infrastructure.Common;
var init = args.Contains("/init");
if (init)
{
    args = args.Except(new[] { "/init" }).ToArray();
}

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddSchoolWebApplication("identity", services =>
    {
        services.AddIdentityApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddIdentityServer(builder.Configuration);
        services.AddIdentityInfrastructure(builder.Configuration);
        services.ConfigureNonBreakingSameSiteCookies();


    });
    //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

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
    SeedData.EnsureSeedData(app);

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
    app.UseSkipValidationMiddleware();
    app.UseCustomTokenValidation();
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

    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // http://github.com/dotnet/runtime/issues/60600
{
    Console.WriteLine(ex);
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
