using SchoolSaas.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Backoffice.Context;
using SchoolSaas.Infrastructure.Backoffice.Services;
using SchoolSaas.Infrastructure.Backoffice.SignalR;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices;
using SchoolSaas.Infrastructure.Backoffice.ConnectedServices;

namespace SchoolSaas.Infrastructure.Backoffice
{
    public static class BackofficeDependencyInjection
    {
        public static IServiceCollection AddBackofficeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext<BackofficeContext, IBackofficeContext>(configuration);
            services.AddContext<BackofficeReadOnlyContext, IBackofficeReadOnlyContext>(configuration);
            services.AddScoped<IBackofficeContext>(provider => provider.GetService<BackofficeContext>()!);
            services.AddTransient<IBackofficeService, BackofficeService>();
            services.AddTransient<IAcademicsService, AcademicsService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<ICourseManagementService, CourseManagementService>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
            services.AddTransient<IGradebookService, GradebookService>();
            services.AddTransient<IParentService, ParentService>();
            services.AddTransient<IResourceManagementService, ResourceManagementService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<ISchoolSupplyService, SchoolSupplyService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<IGradeLevelService, GradeLevelService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<ITransferPortalService, TransferPortalService>();
            services.AddTransient<IResourceViewerService, ResourceViewerService>();
            services.AddTransient<IScheduleViewerService, ScheduleViewerService>();
            services.AddTransient<IEnrollmentPortalService, EnrollmentPortalService>();
            services.AddTransient<ICourseViewerService, CourseViewerService>();
            services.AddTransient<IAttendanceViewerService, AttendanceViewerService>();
            services.AddTransient<IAcademicsViewerService, AcademicsViewerService>();
            services.AddTransient<IGradeViewerService, GradeViewerService>();
            services.AddTransient<IDocumentService, AzureBlobDocumentService>();
            services.AddTransient<ISchoolManagmentService, SchoolManagmentService>();
            services.AddTransient<IEdulinkConnectedService, EdulinkConnectedService>();
            services.AddSingleton<BackOfficeHub>();
            services.AddSingleton<FrontOfficeHub>();


            return services;
        }
    }
}