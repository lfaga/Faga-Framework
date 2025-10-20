using System.Reflection;
using Faga.Framework.Data.Generics;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Logging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Security.Providers;

namespace Faga.Framework.Security
{
  public sealed class UserGroupProvider : BaseBLData
  {
    public UserGroupProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAUserGroup())
    {
    }


    private new BaseDAUserGroup Provider
    {
      get { return (BaseDAUserGroup) base.Provider; }
    }


    public UserGroup GetItem(User user, Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);

      return Provider.GetItem(user.Id, @group.Id, Transaction.IdApplication);
    }


    public UserGroup SetItem(UserGroup userGroup)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, userGroup.Group);

      var rec = new LogRecord(
        LoggingEvents.UserGroupSetItem, Assembly.GetExecutingAssembly().FullName,
        "UserGroup.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter UserGroup", userGroup);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(userGroup);
    }


    public UserGroupCollection SetItems(UserGroupCollection usersgroups)
    {
      var c = new UserGroupCollection();

      foreach (var usersgroup in usersgroups)
      {
        c.Add(SetItem(usersgroup));
      }

      return c;
    }


    public void Remove(User user, Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);

      var rec = new LogRecord(
        LoggingEvents.UserGroupRemove, Assembly.GetExecutingAssembly().FullName,
        "UserGroup.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.Add("Parameter User", user);
      rec.ExtendedProperties.Add("Parameter Group", group);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(user.Id, group.Id, Transaction.IdApplication);
    }


    public void Remove(User usr, GroupCollection grps)
    {
      foreach (var g in grps)
      {
        Remove(usr, g);
      }
    }


    public void Remove(Group grp, UserCollection usrs)
    {
      foreach (var u in usrs)
      {
        Remove(u, grp);
      }
    }
  }
}