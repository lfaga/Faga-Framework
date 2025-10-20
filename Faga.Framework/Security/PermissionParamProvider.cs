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
  public sealed class PermissionParamProvider : BaseBLData
  {
    public PermissionParamProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAPermissionParam())
    {
    }


    private new BaseDAPermissionParam Provider
    {
      get { return (BaseDAPermissionParam) base.Provider; }
    }


    public PermissionParam GetItem(Permission perm, string key)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, perm);

      return Provider.GetItem(perm.Id, Transaction.IdApplication, key);
    }


    public PermissionParamCollection List(Permission filter)
    {
      var f = new PermissionParam();
      f.IdPermission = filter.Id;
      f.IdApplication = filter.IdApplication;

      return List(f);
    }


    public PermissionParamCollection List(PermissionParam filter)
    {
      return Provider.List(filter);
    }


    public PermissionParam SetItem(PermissionParam parameter)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, parameter);

      var rec = new LogRecord(
        LoggingEvents.PermissionParamSetItem, Assembly.GetExecutingAssembly().FullName,
        "PermissionParam.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter PermissionParam", parameter);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(parameter);
    }


    public PermissionParamCollection SetItems(PermissionParamCollection perparams)
    {
      var c = new PermissionParamCollection();

      if (perparams.Count > 0)
      {
        var pfilter = new Permission();
        pfilter.Id = perparams[0].IdPermission;
        Remove(new PermissionProvider(Transaction).GetItem(pfilter));

        foreach (var parameter in perparams)
        {
          c.Add(SetItem(parameter));
        }
      }
      return c;
    }


    public void Remove(Permission perm)
    {
      var ps = List(perm);
      foreach (var p in ps)
      {
        Remove(perm, p.Key);
      }
    }


    public void Remove(Permission perm, string key)
    {
      ApplicationProvider.ValidateMismatch(Transaction.IdApplication, perm);

      var rec = new LogRecord(
        LoggingEvents.PermissionParamRemove, Assembly.GetExecutingAssembly().FullName,
        "PermissionParam.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Permission", perm);
      rec.ExtendedProperties.Add("Parameter Key", key);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(perm.Id, Transaction.IdApplication, key);
    }
  }
}