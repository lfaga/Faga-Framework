using System;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class UserProvider : BaseDAUser
  {
    public override User GetItem(int id)
    {
      var u = new User
      {
        Id = id,
        UserName = "Anonymous",
        Email = "Anonymous@none.com",
        IdLanguage = 1
      };
      return u;
    }


    public override UserCollection List(User filter)
    {
      return new UserCollection {GetItem(1)};
    }


    public override UserCollection List(User filter, int idGroup, int idApplication)
    {
      return new UserCollection {GetItem(1)};
    }


    public override User SetItem(User user)
    {
      return user;
    }


    public override void Remove(int id)
    {
    }


    public override int Validate(string userName)
    {
      return 1;
    }


    public override void SetOnline(int idUser, int idApplication,
      bool online)
    {
    }


    public override DateTime GetOnline(int idUser, int idApplication)
    {
      return DateTime.Now;
    }


    public override bool IsInRole(int idUser, int idGroup, int idApplication)
    {
      return true;
    }
  }
}