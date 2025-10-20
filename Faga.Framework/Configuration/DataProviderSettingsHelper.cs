using System;
using System.Configuration;
using System.Globalization;

namespace Faga.Framework.Configuration
{
  public class DataProviderSettingsHelper : ProviderSettingsHelper
  {
    public DataProviderSettingsHelper(ProviderSettings ps)
      : base(ps)
    {
    }


    public string DefaultConnectionString
    {
      get { return Provider.Parameters["DefaultConnectionString"]; }
    }

    public int CommandTimeout
    {
      get
      {
        if (Provider.Parameters["CommandTimeout"] != null)
        {
          return Convert.ToInt32(
            Provider.Parameters["CommandTimeout"],
            CultureInfo.InvariantCulture);
        }
        return 60;
      }
    }
  }
}