using System;
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
  [Serializable]
  public sealed class GroupPermissionProvider
    : BaseBLMasterData<GroupPermission, GroupPermissionCollection>
  {
    public GroupPermissionProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAGroupPermission())
    {
    }


    private new BaseDAGroupPermission Provider
    {
      get { return (BaseDAGroupPermission) base.Provider; }
    }


    public override GroupPermission New()
    {
      return new GroupPermission();
    }


    public override GroupPermission GetItem(GroupPermission filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication,
        filter.Group, filter.Permission);

      return Provider.GetItem(
        filter.Group.Id, filter.Permission.Id, Transaction.IdApplication);
    }


    public override GroupPermissionCollection List(GroupPermission filter)
    {
      return Provider.List(filter);
    }


    public override GroupPermission SetItem(GroupPermission entity)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, entity.Group,
        entity.Permission);

      var rec = new LogRecord(
        LoggingEvents.GroupPermissionSetItem, Assembly.GetExecutingAssembly().FullName,
        "GroupPermission.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter GroupPermission", entity);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(entity);
    }


    public override GroupPermissionCollection SetItems(GroupPermissionCollection entities)
    {
      var gs = new GroupPermissionCollection();

      foreach (var permission in entities)
      {
        gs.Add(SetItem(permission));
      }

      return gs;
    }


    public override GroupPermissionCollection SetItems(GroupPermissionCollection existingEntities,
      GroupPermissionCollection newEntities)
    {
      var newCol = new GroupPermissionCollection();

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


    public override void Remove(GroupPermission filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, filter.Group,
        filter.Permission);

      var rec = new LogRecord(
        LoggingEvents.GroupPermissionRemove, Assembly.GetExecutingAssembly().FullName,
        "GroupPermission.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.Add("Parameter Group", filter.Group);
      rec.ExtendedProperties.Add("Parameter Permission", filter.Permission);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(filter.Group.Id,
        filter.Permission.Id,
        Transaction.IdApplication);
    }


    public void Remove(Group grp, PermissionCollection perms)
    {
      foreach (var p in perms)
      {
        Remove(new GroupPermission(grp, p));
      }
    }


    public void Remove(Permission prm, GroupCollection grps)
    {
      foreach (var g in grps)
      {
        Remove(new GroupPermission(g, prm));
      }
    }
  }
}