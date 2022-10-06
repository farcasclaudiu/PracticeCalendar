using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Interfaces;

namespace PracticeCalendar.Infrastructure.Notification
{
    public class FileEmailSender : IEmailSender
    {
        private readonly ILogger<FileEmailSender> logger;

        public FileEmailSender(ILogger<FileEmailSender> logger)
        {
            this.logger = logger;
        }
        public Task SendEmailAsync(string to, string from, string subject, string body)
        {
            logger.LogDebug($"Sending email from {from}, to {to}, subject {subject}, body {body}");
            //TODO save email content to a file
            return Task.CompletedTask;
        }
    }
}
