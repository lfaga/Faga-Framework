using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Faga.Framework.Data.Generics;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Logging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Security.Exceptions;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Security.Providers;
using Faga.Framework.Security.Transactions;

namespace Faga.Framework.Security
{
  [Serializable]
  public sealed class UserProvider
    : BaseBLMasterData<User, UserCollection>
  {
    public UserProvider(BaseTransaction transaction)
      : base(transaction, SecurityDAFactory.DAUser())
    {
    }


    private new BaseDAUser Provider
    {
      get { return (BaseDAUser) base.Provider; }
    }


    public int Validate(string userName)
    {
      return Provider.Validate(userName);
    }


    public void SetOnline(User user)
    {
      Provider.SetOnline(user.Id, Transaction.IdApplication, user.Online);
    }


    public DateTime GetOnline(User user)
    {
      try
      {
        return Provider.GetOnline(user.Id, Transaction.IdApplication);
      }
      catch (Exception)
      {
        return DateTime.MinValue;
      }
    }


    public bool IsInRole(User user, Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);

      return Provider.IsInRole(user.Id, @group.Id, Transaction.IdApplication);
    }


    public User Logon()
    {
      var uinf = Provider.GetItem(Validate(Transaction.Usuario.UserName));

      if (uinf != null)
      {
        uinf.Online = true;
        SetOnline(uinf);
      }

      return uinf;
    }


    public void Logoff()
    {
      var uinf = Provider.GetItem(Validate(Transaction.Usuario.UserName));

      if (uinf != null)
      {
        uinf.Online = false;
        SetOnline(uinf);
      }
    }


    public bool CheckPermission(User user, Permission permission)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, permission);
      var groups = new GroupProvider(Transaction).List(permission);

      foreach (var g in groups)
      {
        if (IsInRole(user, g))
        {
          return true;
        }
      }

      var rec = new LogRecord(
        LoggingEvents.UserCheckPermission, Assembly.GetExecutingAssembly().FullName,
        "User.CheckPermission", "General", "", Severity.Error);

      rec.ExtendedProperties.AddSerialized("Parameter User", user);
      rec.ExtendedProperties.AddSerialized("Parameter Permission", permission);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      throw new AccessDeniedException(
        user, string.Format(CultureInfo.InvariantCulture,
          "Permission '{0}' required.", permission.Description));
    }


    [CLSCompliant(false)]
    public bool CheckPermission(RequiresPermissionAttribute attribute)
    {
      if (attribute == null)
      {
        throw new ArgumentNullException("attribute");
      }

      var permission = new Permission();
      permission.Id = attribute.IdPermission;
      using (var st = new SecurityTransaction(attribute.CurrentUser))
      {
        using (var pp = new PermissionProvider(st))
        {
          permission = pp.GetItem(permission);
        }
      }
      return CheckPermission(attribute.CurrentUser, permission);
    }


    public bool CheckParameter(User user, Permission permission,
      KeyValuePair<string, string> parameter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, permission);

      var filter = new PermissionValue();
      filter.IdApplication = permission.IdApplication;
      filter.IdPermission = permission.Id;
      filter.Key = parameter.Key;
      filter.Value = parameter.Value;

      var vals =
        new PermissionValueProvider(Transaction).List(filter);

      foreach (var val in vals)
      {
        var gfilter = new Group();
        gfilter.Id = val.IdGroup;
        if (IsInRole(
          user, new GroupProvider(Transaction).GetItem(gfilter)))
        {
          return true;
        }
      }

      var rec = new LogRecord(
        LoggingEvents.UserCheckParameter, Assembly.GetExecutingAssembly().FullName,
        "User.CheckParameter", "General", "", Severity.Error);

      rec.ExtendedProperties.AddSerialized("Parameter User", user);
      rec.ExtendedProperties.AddSerialized("Parameter Permission", permission);
      rec.ExtendedProperties.AddSerialized("Parameter KeyValueItem", parameter);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return false;
    }


    public override User New()
    {
      return new User();
    }


    public override User GetItem(User filter)
    {
      return Provider.GetItem(filter.Id);
    }


    public override UserCollection List(User filter)
    {
      return Provider.List(filter);
    }


    public UserCollection List(Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);
      return Provider.List(null, @group.Id, Transaction.IdApplication);
    }


    public UserCollection List(User filter, Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);

      return Provider.List(filter, @group.Id, Transaction.IdApplication);
    }


    public UserCollection List(Permission permission)
    {
      var ret = new UserCollection();

      var groups = new GroupProvider(Transaction).List(permission);

      foreach (var group in groups)
      {
        var uing = List(group);

        foreach (var user in uing)
        {
          if (!ret.Contains(user))
          {
            ret.Add(user);
          }
        }
      }

      return ret;
    }


    public UserCollection List(PermissionValue value)
    {
      var ret = new UserCollection();
      var pfilter = new Permission();
      pfilter.Id = value.IdPermission;
      var permission = new PermissionProvider(Transaction).GetItem(pfilter);
      var groups = new GroupProvider(Transaction).List(permission);
      var kvi
        = new KeyValuePair<string, string>(value.Key, value.Value);

      foreach (var group in groups)
      {
        var uing = List(group);

        foreach (var user in uing)
        {
          if (!ret.Contains(user)
              && CheckParameter(user, permission, kvi))
          {
            ret.Add(user);
          }
        }
      }

      return ret;
    }


    public override User SetItem(User entity)
    {
      var rec = new LogRecord(
        LoggingEvents.UserSetItem, Assembly.GetExecutingAssembly().FullName,
        "User.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter User", entity);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(entity);
    }


    public override UserCollection SetItems(UserCollection entities)
    {
      var c = new UserCollection();

      foreach (var user in entities)
      {
        c.Add(SetItem(user));
      }

      return c;
    }


    public override UserCollection SetItems(UserCollection existingEntities, UserCollection newEntities)
    {
      var newCol = new UserCollection();

      foreach (var newEntity in newEntities)
      {
        if (existingEntities.Contains(newEntity))
        {
          existingEntities.Remove(newEntity);
        }

        newCol.Add(SetItem(newEntity));
      }

      foreach (var existingEntity in existingEntities)
      {
        Remove(existingEntity);
      }

      return newCol;
    }


    public override void Remove(User filter)
    {
      var rec = new LogRecord(
        LoggingEvents.UserRemove, Assembly.GetExecutingAssembly().FullName,
        "User.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Id", filter.Id);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(filter.Id);
    }
  }
}