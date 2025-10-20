using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.Providers.MySql
{
  public class UserGroupProvider : BaseDAUserGroup
  {
    public override UserGroup GetItem(int idUser, int idGroup, int idApplication)
    {
      return new UserGroup(
        new UserProvider().GetItem(idUser),
        new GroupProvider().GetItem(idGroup, idApplication));
    }


    public override UserGroup SetItem(UserGroup userGroup)
    {
      return userGroup;
    }


    public override void Remove(int idUser, int idGroup, int idApplication)
    {
    }
  }
}