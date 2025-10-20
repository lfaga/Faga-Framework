using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class UserDataProviderSettingsHelper : ProviderSettingsHelper
  {
    public UserDataProviderSettingsHelper(ProviderSettings ps)
      : base(ps)
    {
    }


    public string ActiveDirectoryPath
    {
      get { return Provider.Parameters["ActiveDirectoryPath"]; }
    }

    public string ActiveDirectoryUser
    {
      get { return Provider.Parameters["ActiveDirectoryUser"]; }
    }

    public string ActiveDirectoryPassword
    {
      get { return Provider.Parameters["ActiveDirectoryPassword"]; }
    }
  }
}