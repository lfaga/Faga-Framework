using Faga.Framework.Security.Model;
using Faga.Framework.Security.UserData.Providers;

namespace Faga.Framework.Security.UserData
{
  /// <summary>
  ///   Summary description for UserDataProvider.
  /// </summary>
  public sealed class UserDataProvider
  {
    private UserDataProvider()
    {
    }


    public static bool IsUserDataProviderPresent()
    {
      return UserDataProviderFactory.UserData() != null;
    }


    public static User FillUserData(string username)
    {
      return FillUserData(new User(username));
    }


    public static User FillUserData(User user)
    {
      return UserDataProviderFactory.UserData().FillUserData(user);
    }
  }
}