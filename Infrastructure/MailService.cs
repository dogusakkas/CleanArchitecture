using Application.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;

namespace Infrastructure
{
    public sealed class MailService : IMailService
    {
        private readonly string _smtpHost = "10.20.50.56";
        private readonly int _smtpPort = 25;
        //private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "dogusakkas@vestel.com.tr";
        private readonly string _senderPassword = "Dogus.45";

        public async Task SendMailAsync(SendMailDto dto)
        {
            using MailMessage mail = new()
            {
                From = new MailAddress(_senderEmail),
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            foreach (var email in dto.Emails)
            {
                mail.To.Add(email);
            }

            foreach (var attachment in dto.Attachments)
            {
                mail.Attachments.Add(attachment);
            }

            using SmtpClient smtp = new(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                EnableSsl = false
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
