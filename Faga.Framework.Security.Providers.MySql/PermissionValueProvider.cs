using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class PermissionValueProvider : BaseDAPermissionValue
  {
    public override PermissionValue GetItem(int idPermission, int idApplication,
      string key, int idGroup, string value)
    {
      return new PermissionValue(idPermission, idApplication, key, idGroup, value);
    }


    public override PermissionValueCollection List(PermissionValue filter)
    {
      return new PermissionValueCollection
      {
        filter
      };
    }


    public override PermissionValue SetItem(PermissionValue parameter)
    {
      return parameter;
    }


    public override void Remove(int idPermission, int idApplication,
      string key, int idGroup, string value)
    {
    }
  }
}