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
  public sealed class PermissionValueProvider : BaseBLData
  {
    public PermissionValueProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAPermissionValue())
    {
    }


    private new BaseDAPermissionValue Provider
    {
      get { return (BaseDAPermissionValue) base.Provider; }
    }


    public PermissionValue GetItem(Permission perm, Group group, string key,
      string value)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group, perm);

      return Provider.GetItem(perm.Id, Transaction.IdApplication, key,
        @group.Id, value);
    }


    public PermissionValueCollection List(PermissionValue filter)
    {
      return Provider.List(filter);
    }


    public PermissionValue SetItem(PermissionValue pervalue)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, pervalue);

      var rec = new LogRecord(
        LoggingEvents.PermissionValueSetItem, Assembly.GetExecutingAssembly().FullName,
        "PermissionValue.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter PermissionValue", pervalue);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(pervalue);
    }


    public PermissionValueCollection SetItems(PermissionValueCollection values)
    {
      var c = new PermissionValueCollection();

      foreach (var value in values)
      {
        c.Add(SetItem(value));
      }

      return c;
    }


    public void Remove(Permission perm, Group group)
    {
      Remove(perm, group, null, null);
    }


    public void Remove(Permission perm, Group group, string key)
    {
      Remove(perm, group, key, null);
    }


    public void Remove(Permission perm, Group group, string key, string value)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, group, perm);

      var rec = new LogRecord(
        LoggingEvents.PermissionValueRemove, Assembly.GetExecutingAssembly().FullName,
        "PermissionValue.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Permission", perm);
      rec.ExtendedProperties.AddSerialized("Parameter Group", group);
      rec.ExtendedProperties.Add("Parameter Key", key);
      rec.ExtendedProperties.Add("Parameter Value", value);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(perm.Id, Transaction.IdApplication, key, group.Id, value);
    }
  }
}