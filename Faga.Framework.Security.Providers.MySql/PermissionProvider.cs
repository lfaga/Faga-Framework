using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class PermissionProvider : BaseDAPermission
  {
    public override Permission GetItem(int id, int idApplication)
    {
      return new Permission(id, idApplication, "Generic");
    }


    public override PermissionCollection List(Permission filter)
    {
      return List(filter, int.MinValue);
    }


    public override PermissionCollection List(int idGroup, int idApplication)
    {
      var p = new Permission
      {
        IdApplication = idApplication
      };

      return List(p, idGroup);
    }


    private PermissionCollection List(Permission filter, int idGroup)
    {
      return new PermissionCollection
      {
        GetItem(filter.Id, filter.IdApplication)
      };
    }


    public override Permission SetItem(Permission permission)
    {
      return permission;
    }


    public override void Remove(int id, int idApplication)
    {
    }
  }
}