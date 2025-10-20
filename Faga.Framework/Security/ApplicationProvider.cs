using System;
using System.Reflection;
using System.Security;
using Faga.Framework.Data.Generics;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Logging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Security.Exceptions;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Security.Providers;

namespace Faga.Framework.Security
{
  [Serializable]
  public sealed class ApplicationProvider
    : BaseBLMasterData<Application, ApplicationCollection>
  {
    public ApplicationProvider(BaseTransaction trans)
      : base(trans, SecurityDAFactory.DAApplication())
    {
    }


    private new BaseDAApplication Provider
    {
      get { return (BaseDAApplication) base.Provider; }
    }


    public override Application New()
    {
      return new Application();
    }


    public override Application GetItem(Application filter)
    {
      return Provider.GetItem(filter.Id);
    }


    public override ApplicationCollection List(Application filter)
    {
      return Provider.List(filter);
    }


    public override Application SetItem(Application entity)
    {
      var rec = new LogRecord(
        LoggingEvents.ApplicationSetItem, Assembly.GetExecutingAssembly().FullName,
        "Application.SetItem", "General", "", Severity.Information);

      rec.ExtendedProperties.AddSerialized("Parameter Application", entity);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      return Provider.SetItem(entity);
    }


    public override ApplicationCollection SetItems(ApplicationCollection entities)
    {
      var n = new ApplicationCollection();

      foreach (var app in entities)
      {
        n.Add(SetItem(app));
      }

      return n;
    }


    public override ApplicationCollection SetItems(ApplicationCollection existingEntities,
      ApplicationCollection newEntities)
    {
      var newCol = new ApplicationCollection();

      foreach (var newEntity in newEntities)
      {
        if (existingEntities.Contains(newEntity))
        {
          existingEntities.Remove(newEntity);
        }
        else
        {
          newCol.Add(SetItem(newEntity));
        }
      }

      foreach (var existingEntity in existingEntities)
      {
        Remove(existingEntity);
      }

      return newCol;
    }


    public override void Remove(Application filter)
    {
      var rec = new LogRecord(
        LoggingEvents.ApplicationRemove, Assembly.GetExecutingAssembly().FullName,
        "Application.Remove", "General", "", Severity.Information);

      rec.ExtendedProperties.Add("Parameter Id", filter.Id);
      rec.ExtendedProperties.AddSerialized("Parameter Transaction", Transaction);

      LoggingProvider.Write(rec);

      Provider.Remove(filter.Id);
    }


    internal static void ValidateMismatch(int idApplication, params object[] objects)
    {
      foreach (var o in objects)
      {
        var t = o.GetType();
        var pi = t.GetProperty("IdApplication");
        if (pi == null)
        {
          throw new SecurityException("El objeto " + t.Name
                                      + " no contiene IdApplication");
        }
        var val = Convert.ToInt32(pi.GetValue(o, null));
        if ((val != int.MinValue)
            && (val != idApplication))
        {
          throw new ApplicationMismatchException();
        }
      }
    }
  }
}