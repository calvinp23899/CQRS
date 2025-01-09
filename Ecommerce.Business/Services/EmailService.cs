using Ecommerce.Core.DTOs.Settings;
using Ecommerce.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _emailSettings.Password = Environment.GetEnvironmentVariable("SECRETEMAILPASS", EnvironmentVariableTarget.Machine);
        }

        public async Task<bool> SendEmailAsync(List<string>? toEmails, string subject, string body, List<string>? ccEmails)
        {
            if (toEmails == null || !toEmails.Any())
                throw new ArgumentException("Missing objects for sending email.");

            var email = _emailSettings.Email;
            var password = _emailSettings.Password;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.Name, email));
            foreach (var toEmail in toEmails)
            {
                message.To.Add(new MailboxAddress("", toEmail));
            }

            if (ccEmails != null)
            {
                foreach (var ccEmail in ccEmails)
                {
                    message.Cc.Add(new MailboxAddress("", ccEmail));
                }
            }

            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(email, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to send email.", ex);
                }
            }
            return true;
        }
    }
}
