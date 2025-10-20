using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAGroupPermission : BaseDAData
  {
    public abstract GroupPermission GetItem(int idGroup, int idPermission, int idApplication);

    public abstract GroupPermissionCollection List(GroupPermission filter);

    public abstract GroupPermission SetItem(GroupPermission groupParameter);

    public abstract void Remove(int idGroup, int idPermission, int idApplication);
  }
}