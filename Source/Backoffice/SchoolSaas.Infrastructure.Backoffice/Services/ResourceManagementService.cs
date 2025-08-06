using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class ResourceManagementService : IResourceManagementService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public ResourceManagementService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

       

       
    }
}
