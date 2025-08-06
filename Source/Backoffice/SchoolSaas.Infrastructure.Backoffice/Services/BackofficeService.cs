using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Backoffice.SignalR;


namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class BackofficeService : IBackofficeService 
    {
       
        private readonly BackOfficeHub _BackOfficeHub;
        private readonly FrontOfficeHub _FrontOfficeHub;
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPdfGenerationService _pdfGenerationService;
        private readonly IEmailSender _emailSender;
        private readonly IIdentityService _identityService;
        private readonly IIdentityConnectedService _identityConnectedService;
  


        public BackofficeService(IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            ICurrentUserService currentUserService,
            IPdfGenerationService pdfGenerationService,
            IEmailSender emailSender,
            IIdentityService identityService,
            FrontOfficeHub FrontOfficeHub,
            BackOfficeHub BackOfficeHub,
            IIdentityConnectedService identityConnectedService)
            
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _currentUserService = currentUserService;
            _pdfGenerationService = pdfGenerationService;
            _emailSender = emailSender;
            _identityService = identityService;
            _BackOfficeHub = BackOfficeHub;
            _identityConnectedService = identityConnectedService;
            _FrontOfficeHub = FrontOfficeHub;
        }
  
     
    }
}

