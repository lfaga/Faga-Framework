using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class ApplicationConfiguration
  {
    protected ApplicationConfiguration()
    {
    }


    public static ProviderSettingsCollection Providers
    {
      get { return Section.Providers; }
    }


    public static ReportingConfigurationElement RepotingConfiguration
    {
      get { return Section.ReportingConfiguration; }
    }


    public static DataProviderSettingsHelper DataProvider
    {
      get { return new DataProviderSettingsHelper(Section.Providers["DataProvider"]); }
    }

    public static SecurityProviderSettingsHelper SecurityProvider
    {
      get { return new SecurityProviderSettingsHelper(Section.Providers["SecurityProvider"]); }
    }

    public static UserDataProviderSettingsHelper UserDataProvider
    {
      get { return new UserDataProviderSettingsHelper(Section.Providers["DataProvider"]); }
    }


    private static ApplicationConfigurationSection Section
    {
      get
      {
        return (ApplicationConfigurationSection)
          ConfigurationManager.GetSection("ApplicationConfiguration");
      }
    }
  }
}