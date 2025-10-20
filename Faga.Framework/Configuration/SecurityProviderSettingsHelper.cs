using System;
using System.Configuration;
using System.Globalization;

namespace Faga.Framework.Configuration
{
  public class SecurityProviderSettingsHelper : ProviderSettingsHelper
  {
    public SecurityProviderSettingsHelper(ProviderSettings ps)
      : base(ps)
    {
    }


    public int ApplicationId
    {
      get
      {
        return Convert.ToInt32(
          Provider.Parameters["ApplicationId"], CultureInfo.InvariantCulture);
      }
    }

    public string ConnectionString
    {
      get { return Provider.Parameters["ConnectionString"]; }
    }

    public string SecurityServicesUrl
    {
      get { return Provider.Parameters["SecurityServicesUrl"]; }
    }
  }
}