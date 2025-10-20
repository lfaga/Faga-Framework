using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAPermission : BaseDAData
  {
    public abstract Permission GetItem(int id, int idApplication);

    public abstract PermissionCollection List(Permission filter);

    public abstract PermissionCollection List(int idGroup, int idApplication);

    public abstract Permission SetItem(Permission permission);

    public abstract void Remove(int id, int idApplication);
  }
}