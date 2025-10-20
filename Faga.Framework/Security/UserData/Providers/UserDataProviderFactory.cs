using System.Reflection;
using Faga.Framework.Configuration;

namespace Faga.Framework.Security.UserData.Providers
{
  public sealed class UserDataProviderFactory
  {
    private UserDataProviderFactory()
    {
    }


    public static IUserDataProvider UserData()
    {
      var path
        = ApplicationConfiguration.UserDataProvider.Type;

      if (path != null)
      {
        return (IUserDataProvider) Assembly.Load(path)
          .CreateInstance(path + ".UserDataProvider");
      }
      return null;
    }
  }
}