using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Interfaces;
using Web.Application.DTOs.EmailDTO;

namespace Web.Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailDto _emailDto;

        public EmailService(IOptions<EmailDto> mailSettings)
        {
            _emailDto = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailDto.Email, _emailDto.DisplayName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_emailDto.Host)
            {
                Port = _emailDto.Port,
                Credentials = new NetworkCredential(_emailDto.Email, _emailDto.Password),
                EnableSsl = _emailDto.EnableSSL,
                UseDefaultCredentials = _emailDto.UseDefaultCredentials
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}
