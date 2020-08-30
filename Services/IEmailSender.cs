using System.Threading.Tasks;

namespace web_identity_csharp_base.Services
{
    public interface IEmailSender
    {
         Task SendEmailAsync (string fromAddress, string toAddress, string subject, string message);
    }
}