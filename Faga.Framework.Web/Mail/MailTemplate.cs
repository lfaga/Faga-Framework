using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.Mail
{
  public class MailTemplate
  {
    internal object body;


    public MailTemplate()
    {
      From = WebApplicationConfiguration.MailConfiguration.From;
      To = new UserCollection();
      Subject = null;
      IsHtml = true;
      BodyEncoding = Encoding.Default;
      BodyVariables = new NameValueCollection();

      body = string.Empty;

      if (HttpContext.Current != null)
      {
        BodyVariables.Add("Application.Link",
          string.Concat("http://",
            HttpContext.Current.Request.Url.Authority));
      }
    }


    public virtual string RenderBody()
    {
      return PrepareBody(Body);
    }


    protected string PrepareBody(string bodytext)
    {
      var s = bodytext;

      if (BodyVariables != null)
      {
        foreach (string k in BodyVariables.Keys)
        {
          s = s.Replace("{{" + k + "}}", BodyVariables[k]);
        }
      }

      return s;
    }

    #region Public Fields

    public string From { get; set; }


    public UserCollection To { get; set; }


    public string StringTo
    {
      get
      {
        var s = "";

        foreach (var u in To)
        {
          s += u.Email + ";";
        }

        return s;
      }
    }


    public string Subject { get; set; }


    public bool IsHtml { get; set; }


    public Encoding BodyEncoding { get; set; }


    [CLSCompliant(false)]
    public virtual string Body
    {
      get { return (string) body; }
      set { body = value; }
    }


    public NameValueCollection BodyVariables { get; set; }

    #endregion
  }
}