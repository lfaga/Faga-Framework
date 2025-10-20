using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.UserData.Providers
{
  /// <summary>
  ///   Summary description for IUserDataProvider.
  /// </summary>
  public interface IUserDataProvider
  {
    User FillUserData(User user);
  }
}