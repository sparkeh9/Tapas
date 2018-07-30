namespace Tapas.Authentication.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    public class ConsoleLoggingEmailSender: IEmailSender // this line is written by me
    {
        private readonly ILogger<IEmailSender> logger;
        public ConsoleLoggingEmailSender( ILogger<IEmailSender> logger )
        {
            this.logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Task.Delay( 0 );
            logger.LogInformation( $"Sending Email to {email}" );
            logger.LogInformation( $"Subject: {subject}" );
            logger.LogInformation( $"Message: {message}" );
        }
    }
}