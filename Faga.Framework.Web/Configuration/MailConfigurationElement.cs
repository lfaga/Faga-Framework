using System.Configuration;

namespace Faga.Framework.Web.Configuration
{
  public class MailConfigurationElement : ConfigurationElement
  {
    public MailConfigurationElement()
    {
    }


    public MailConfigurationElement(string smtpServer)
    {
      SmtpServer = smtpServer;
    }


    public MailConfigurationElement(string smtpServer, string from)
    {
      SmtpServer = smtpServer;
      From = from;
    }

    #region Properties

    [ConfigurationProperty("SmtpServer", IsRequired = true)]
    public string SmtpServer
    {
      get { return (string) this["SmtpServer"]; }
      set { this["SmtpServer"] = value; }
    }

    [ConfigurationProperty("From", IsRequired = false)]
    public string From
    {
      get { return (string) this["From"]; }
      set { this["From"] = value; }
    }

    #endregion
  }
}