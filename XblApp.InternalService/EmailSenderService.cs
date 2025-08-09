using Microsoft.AspNetCore.Identity.UI.Services;

namespace XblApp.InternalService
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
