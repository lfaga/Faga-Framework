using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class ReportingConfigurationElement : ConfigurationElement
  {
    #region Properties

    [ConfigurationProperty("ServiceUrl", IsRequired = true,
      DefaultValue = "http://localhost/ReportServer/ReportService.asmx")]
    public string ServiceUrl
    {
      get { return (string) this["ServiceUrl"]; }
      set { this["ServiceUrl"] = value; }
    }

    #endregion
  }
}