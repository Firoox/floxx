using Floxx.Core.Configurations;
using Floxx.Core.DTO.Mail;
using Floxx.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace Floxx.Infrastructure.Services
{
    public class SMTPMailService : IMailService
    {
        public MailConfiguration configuration { get; }
        public ILogger<SMTPMailService> logger { get; }

        public SMTPMailService(IOptions<MailConfiguration> config, ILogger<SMTPMailService> logger)
        {
            configuration = config.Value;
            this.logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                MimeMessage email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? configuration.From);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;

                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Connect(configuration.Host, configuration.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(configuration.UserName, configuration.Password);

                    await smtp.SendAsync(email);

                    smtp.Disconnect(true);
                }
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }
        }
    }
}
