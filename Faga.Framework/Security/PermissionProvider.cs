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
  public sealed class PermissionProvider
    : BaseBLMasterData<Permission, PermissionCollection>
  {
    public PermissionProvider(BaseTransaction transaction)
      : base(transaction, SecurityDAFactory.DAPermission())
    {
    }


    private new BaseDAPermission Provider
    {
      get { return (BaseDAPermission) base.Provider; }
    }


    public override Permission New()
    {
      return new Permission();
    }


    public override Permission GetItem(Permission filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, filter);

      return Provider.GetItem(filter.Id, Transaction.IdApplication);
    }


    public override PermissionCollection List(Permission filter)
    {
      return Provider.List(filter);
    }


    public PermissionCollection List(Group group)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group);

      return Provider.List(@group.Id, Transaction.IdApplication);
    }


    public override Permission SetItem(Permission entity)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, entity);

      var rec = new LogRecord(
        LoggingEvents.PermissionSetItem, Assembly.GetExecutingAssembly().FullName,
        "Permission.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Permission", entity);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(entity);
    }


    public override PermissionCollection SetItems(PermissionCollection entities)
    {
      var c = new PermissionCollection();

      foreach (var perm in entities)
      {
        c.Add(SetItem(perm));
      }

      return c;
    }


    public override PermissionCollection SetItems(PermissionCollection existingEntities,
      PermissionCollection newEntities)
    {
      var newCol = new PermissionCollection();

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


    public override void Remove(Permission filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, filter);

      var rec = new LogRecord(
        LoggingEvents.PermissionRemove, Assembly.GetExecutingAssembly().FullName,
        "Permission.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.Add("Parameter Id", filter.Id);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(filter.Id, Transaction.IdApplication);
    }
  }
}