using System.Configuration;
using Faga.Framework.Configuration;

namespace Faga.Framework.Web.Configuration
{
  public class WebApplicationConfigurationSection
    : ApplicationConfigurationSection
  {
    private MailConfigurationElement _mailConfiguration;
    private WebUiConfigurationElement _webUiConfiguration;


    public WebApplicationConfigurationSection()
    {
      _mailConfiguration = new MailConfigurationElement();
      _webUiConfiguration = new WebUiConfigurationElement();
    }

    protected override bool OnDeserializeUnrecognizedAttribute(
      string name, string value)
    {
      return true;
    }

    #region Configuration Properties

    [ConfigurationProperty("MailConfiguration", IsRequired = false)]
    public MailConfigurationElement MailConfiguration
    {
      get
      {
        try
        {
          var s =
            (MailConfigurationElement) this["MailConfiguration"];
          _mailConfiguration = s;
        }
        catch
        {
        }
        return _mailConfiguration;
      }
    }

    [ConfigurationProperty("WebUIConfiguration", IsRequired = false)]
    public WebUiConfigurationElement WebUiConfiguration
    {
      get
      {
        try
        {
          var s =
            (WebUiConfigurationElement) this["WebUIConfiguration"];
          _webUiConfiguration = s;
        }
        catch
        {
        }
        return _webUiConfiguration;
      }
    }

    [ConfigurationProperty("WelcomePage", IsRequired = false)]
    public string WelcomePage
    {
      get { return (string) this["WelcomePage"]; }
      set { this["WelcomePage"] = value; }
    }

    [ConfigurationProperty("ApplicationName", IsRequired = false)]
    public string ApplicationName
    {
      get { return (string) this["ApplicationName"]; }
      set { this["ApplicationName"] = value; }
    }

    #endregion
  }
}