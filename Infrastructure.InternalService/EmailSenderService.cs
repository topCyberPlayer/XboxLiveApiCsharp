using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.InternalService
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
