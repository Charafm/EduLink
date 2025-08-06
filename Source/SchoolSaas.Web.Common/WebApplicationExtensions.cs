using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SchoolSaas.Web.Common.Middlewares;


namespace SchoolSaas.Web.Common
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseSchoolSaasWebApplication(this WebApplication app, bool swagger)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see http://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            if (app.Environment.IsDevelopment() || swagger)
            {
                app.UseStaticFiles(); 
                app.UseOpenApi(settings =>
                {
                    settings.Path = "/swagger/v1/swagger.json";
                });
                Console.WriteLine("Swagger middleware is being executed...");
                app.UseSwaggerUi(settings =>
                {
                    settings.Path = "/swagger";
                    settings.DocumentPath = "/swagger/v1/swagger.json";
                    settings.TransformToExternalPath = (internalUiRoute, request) =>
                    {
                        if (internalUiRoute.StartsWith("/") == true && internalUiRoute.StartsWith(request.PathBase) == false)
                        {
                            return request.PathBase + internalUiRoute;
                        }
                        return internalUiRoute;
                    };
                });
            }

            //app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            //app.UseForwardedHeaders(new ForwardedHeadersOptions
            //{
            //    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
            //});

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization(); //disable anonymous access for the entire application

            app.MapHealthChecks("/healthz");
            //app.UseSkipValidationPathsMiddleware();
            app.UseJwtQueryAccessToken();
            return app;
        }


        public static async Task ScheduleJob<T>(this WebApplication app, string cronExpression)
            where T : class, IJob
        {
            var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await schedulerFactory.GetScheduler();

            var job = JobBuilder.Create<T>()
                .WithIdentity($"job-{typeof(T).Name}", "EduLink")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"job-{typeof(T).Name}-trigger", "EduLink")
                .WithCronSchedule(cronExpression)
                .ForJob($"job-{typeof(T).Name}", "EduLink")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
