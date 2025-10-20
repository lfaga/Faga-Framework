using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class PermissionParamProvider : BaseDAPermissionParam
  {
    public override PermissionParam GetItem(int idPermission, int idApplication,
      string key)
    {
      return new PermissionParam(idPermission, idApplication, key);
    }


    public override PermissionParamCollection List(PermissionParam filter)
    {
      var cs = new PermissionParamCollection {filter};
      return cs;
    }


    public override PermissionParam SetItem(PermissionParam parameter)
    {
      return parameter;
    }


    public override void Remove(int idPermission, int idApplication, string key)
    {
    }
  }
}