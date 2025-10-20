using System.Net.Mail;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.Mail
{
  public class MailSender
  {
    protected MailSender()
    {
    }


    public static void Send(MailTemplate mail)
    {
      var msg = new MailMessage();

      msg.From = new MailAddress(mail.From);
      msg.To.Add(mail.StringTo);
      msg.Subject = mail.Subject;
      msg.IsBodyHtml = mail.IsHtml;
      msg.BodyEncoding = mail.BodyEncoding;
      msg.Body = mail.RenderBody();

      var smtp = new SmtpClient(
        WebApplicationConfiguration.MailConfiguration.SmtpServer);
      smtp.Send(msg);
    }
  }
}