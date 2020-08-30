using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace web_identity_csharp_base.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IOptions<SmtpOptions> _options;

        public SmtpEmailSender(IOptions<SmtpOptions> options){
            _options = options;        
        }


        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);
            using (var client = new SmtpClient(_options.Value.Host, _options.Value.Port){
                Credentials = new NetworkCredential(_options.Value.Username, _options.Value.Password)
            })
            {
                await client.SendMailAsync(mailMessage);
            }

        }
    }
}