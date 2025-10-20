using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class ProviderSettingsHelper
  {
    private readonly ProviderSettings provider;


    public ProviderSettingsHelper(ProviderSettings ps)
    {
      provider = ps;
    }


    public string Name
    {
      get { return provider.Name; }
    }

    public string Type
    {
      get { return provider.Type; }
    }

    protected ProviderSettings Provider
    {
      get { return provider; }
    }
  }
}