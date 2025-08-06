using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Configuration;
using SchoolSaas.Infrastructure.Common.Areas.Emails.Views;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _emailOptions;
        private readonly ITemplateToStringRenderer _templateToStringRenderer;

        public EmailSender(IOptions<EmailOptions> emailOptions, ITemplateToStringRenderer templateToStringRenderer)
        {
            _emailOptions = emailOptions.Value;
            _templateToStringRenderer = templateToStringRenderer;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailOptions.SenderName, _emailOptions.Sender));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                var builder = new BodyBuilder();

                builder.HtmlBody = htmlMessage;

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailOptions.MailServer, _emailOptions.MailPort, _emailOptions.UseSSL);
                    await client.AuthenticateAsync(_emailOptions.Sender, _emailOptions.Password);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendEmailAsync<TModel>(string fromAlias, string fromEmail, string email, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class
        {
            await SendEmailAsync(fromAlias, fromEmail, new List<string>() { email }, subject, template, model, emailAttachementFile, bcc);
        }

        public async Task SendEmailAsync<TModel>(string email, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class
        {
            await SendEmailAsync(_emailOptions.SenderName, _emailOptions.Sender, new List<string>() { email }, subject, template, model, emailAttachementFile, bcc);
        }

        public async Task SendEmailAsync<TModel>(List<string> emails, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class
        {
            await SendEmailAsync(_emailOptions.SenderName, _emailOptions.Sender, emails, subject, template, model, emailAttachementFile, bcc);
        }

        private async Task SendEmailAsync<TModel>(string fromAlias, string fromEmail, List<string> emails, string subject, string template, TModel model, EmailAttachementFile? emailAttachementFile = null, string? bcc = null) where TModel : class
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromAlias, fromEmail));
                message.To.AddRange(emails.Select(e => MailboxAddress.Parse(e)));
                message.Subject = subject;

                if (bcc != null)
                    message.Bcc.Add(InternetAddress.Parse(bcc));

                var builder = new BodyBuilder();


                builder.HtmlBody = await _templateToStringRenderer.RenderTemplateToStringAsync($"/Areas/Emails/Views/Templates/{template}.cshtml",
                            new TemplateViewModel<TModel>()
                            {
                                Data = model
                            });

                if (emailAttachementFile != null)
                    builder.Attachments.Add(emailAttachementFile.FileName, emailAttachementFile.Data);

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailOptions.MailServer, _emailOptions.MailPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailOptions.Sender, _emailOptions.Password);

                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}