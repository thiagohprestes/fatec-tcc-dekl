using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace DEKL.CP.Infra.CrossCutting.Identity.Configuration
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var text = HttpUtility.HtmlEncode(message.Body);

            var msg = new MailMessage
            {
                From = new MailAddress("deklcpadm@gmail.com", "DEKL - Contas a Pagar - Administrador"),
                Subject = message.Subject,
             };

            msg.To.Add(new MailAddress(message.Destination));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

            var smtpClient = new SmtpClient();

            smtpClient.Send(msg);

            return Task.FromResult(0);
        }
    }
}