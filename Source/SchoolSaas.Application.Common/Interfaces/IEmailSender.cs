using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IEmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        Task SendEmailAsync<TModel>(string email, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class;
        Task SendEmailAsync<TModel>(string fromAlias, string fromEmail, string email, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class;
        Task SendEmailAsync<TModel>(List<string> emails, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class;
    }
}