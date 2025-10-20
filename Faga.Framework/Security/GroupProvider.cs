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
  public sealed class GroupProvider
    : BaseBLMasterData<Group, GroupCollection>
  {
    public GroupProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAGroup())
    {
    }


    private new BaseDAGroup Provider
    {
      get { return (BaseDAGroup) base.Provider; }
    }


    public override Group New()
    {
      return new Group();
    }


    public override Group GetItem(Group filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, filter);

      return Provider.GetItem(filter.Id, Transaction.IdApplication);
    }


    public override GroupCollection List(Group filter)
    {
      return Provider.List(filter);
    }


    public GroupCollection List(Permission permission)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, permission);

      return Provider.List(int.MinValue, permission.Id, Transaction.IdApplication);
    }


    public GroupCollection List(User user)
    {
      return Provider.List(user.Id, int.MinValue, Transaction.IdApplication);
    }


    public override Group SetItem(Group entity)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, entity);

      var rec = new LogRecord(
        LoggingEvents.GroupSetItem, Assembly.GetExecutingAssembly().FullName,
        "Group.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Group", entity);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(entity);
    }


    public override GroupCollection SetItems(GroupCollection entities)
    {
      var g = new GroupCollection();

      foreach (var group in entities)
      {
        g.Add(SetItem(group));
      }

      return g;
    }


    public override GroupCollection SetItems(GroupCollection existingEntities, GroupCollection newEntities)
    {
      var newCol = new GroupCollection();

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


    public override void Remove(Group filter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, filter);

      var rec = new LogRecord(
        LoggingEvents.GroupRemove, Assembly.GetExecutingAssembly().FullName,
        "Group.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.Add("Parameter Id", filter.Id);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(filter.Id, Transaction.IdApplication);
    }
  }
}