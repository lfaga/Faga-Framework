using System.Configuration;
using Faga.Framework.Configuration;

namespace Faga.Framework.Web.Configuration
{
  public class WebUiConfigurationElement : ConfigurationElement
  {
    public WebUiConfigurationElement()
    {
      DefaultJavaScriptIncludes = new GenericConfigurationCollection<string>();
      MenuItemsProvider = new GenericConfigurationElement<string>();
    }

    #region Properties

    [ConfigurationProperty("DefaultJavaScriptIncludes", IsRequired = false)]
    [ConfigurationCollection(typeof (GenericConfigurationCollection<string>))]
    public GenericConfigurationCollection<string> DefaultJavaScriptIncludes
    {
      get { return (GenericConfigurationCollection<string>) this["DefaultJavaScriptIncludes"]; }
      set { this["DefaultJavaScriptIncludes"] = value; }
    }

    [ConfigurationProperty("MenuItemsProvider", IsRequired = false)]
    public GenericConfigurationElement<string> MenuItemsProvider
    {
      get { return (GenericConfigurationElement<string>) this["MenuItemsProvider"]; }
      set { this["MenuItemsProvider"] = value; }
    }

    [ConfigurationProperty("ThemeName", IsRequired = false)]
    public string ThemeName
    {
      get { return (string) this["ThemeName"]; }
      set { this["ThemeName"] = value; }
    }

    #endregion
  }
}