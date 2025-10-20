using System;
using System.Collections.ObjectModel;

namespace Faga.Framework.Security.Model.Collections
{
  [Serializable]
  public class UserCollection : Collection<User>
  {
    public new bool Contains(User user)
    {
      foreach (var u in this)
      {
        if (user.UserName.CompareTo(u.UserName) == 0)
        {
          return true;
        }
      }

      return false;
    }
  }
}