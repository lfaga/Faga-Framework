using System;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class GroupPermissionProvider : BaseDAGroupPermission
  {
    public override GroupPermission GetItem(int idGroup, int idPermission,
      int idApplication)
    {
      var gp = new GroupPermission
      {
        Group = new GroupProvider().GetItem(
          idGroup, idApplication),
        Permission = new PermissionProvider().GetItem(
          idPermission, idApplication)
      };

      return gp;
    }


    public override GroupPermissionCollection List(GroupPermission filter)
    {
      var gps = new GroupPermissionCollection
      {
        GetItem(filter.Group.Id, filter.Permission.Id,
          filter.Group.IdApplication)
      };

      return gps;
    }


    public override GroupPermission SetItem(GroupPermission gp)
    {
      return gp;
    }


    public override void Remove(int idGroup, int idPermission, int idApplication)
    {
      throw new NotImplementedException();
    }
  }
}