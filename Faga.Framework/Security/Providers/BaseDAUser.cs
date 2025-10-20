using System;
using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAUser : BaseDAData
  {
    public abstract User GetItem(int id);

    public abstract UserCollection List(User filter);

    public abstract UserCollection List(User filter, int idGroup, int idApplication);

    public abstract User SetItem(User user);

    public abstract void Remove(int id);

    public abstract int Validate(string userName);

    public abstract void SetOnline(int idUser, int idApplication, bool online);

    public abstract DateTime GetOnline(int idUser, int idApplication);

    public abstract bool IsInRole(int idUser, int idGroup, int idApplication);
  }
}