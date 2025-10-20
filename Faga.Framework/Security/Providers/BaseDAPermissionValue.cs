using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAPermissionValue : BaseDAData
  {
    public abstract PermissionValue GetItem(int idPermission, int idApplication,
      string key, int idGroup, string value);

    public abstract PermissionValueCollection List(PermissionValue filter);

    public abstract PermissionValue SetItem(PermissionValue parameter);


    public abstract void Remove(int idPermission, int idApplication, string key,
      int idGroup, string value);
  }
}