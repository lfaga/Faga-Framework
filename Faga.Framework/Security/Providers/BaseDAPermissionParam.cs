using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAPermissionParam : BaseDAData
  {
    public abstract PermissionParam GetItem(int idPermission, int idApplication, string key);

    public abstract PermissionParamCollection List(PermissionParam filter);

    public abstract PermissionParam SetItem(PermissionParam parameter);

    public abstract void Remove(int idPermission, int idApplication, string key);
  }
}