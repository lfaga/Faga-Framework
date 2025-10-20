using System;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class GroupProvider : BaseDAGroup
  {
    public override Group GetItem(int id, int idApplication)
    {
      return new Group(id, idApplication, "Anonymous", "Anonymous");
    }


    public override GroupCollection List(Group filter)
    {
      return List(filter, int.MinValue, int.MinValue);
    }


    public override GroupCollection List(int idUser, int idPermission, int idApplication)
    {
      var g = new Group
      {
        IdApplication = idApplication
      };
      return List(g, idUser, idPermission);
    }


    public GroupCollection List(Group filter, int idUser, int idPermission)
    {
      var cs = new GroupCollection
      {
        GetItem(filter.Id, filter.IdApplication)
      };
      return cs;
    }


    public override Group SetItem(Group group)
    {
      return @group;
    }


    public override void Remove(int id, int idApplication)
    {
      throw new NotImplementedException();
    }
  }
}