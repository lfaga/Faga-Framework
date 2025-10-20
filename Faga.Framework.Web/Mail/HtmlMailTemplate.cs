using System.IO;
using System.Web.UI;

namespace Faga.Framework.Web.Mail
{
  public class HtmlMailTemplate : MailTemplate
  {
    public HtmlMailTemplate()
    {
      body = new Page();
    }


    public new Page Body
    {
      get { return (Page) body; }
      set { body = value; }
    }

    public override string RenderBody()
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      Body.RenderControl(htm);
      var ret = PrepareBody(str.ToString());
      htm.Close();
      str.Close();

      return ret;
    }
  }
}