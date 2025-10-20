using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Logging.Model.Collections;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Faga.Framework.Logging.Providers.EntLib
{
  /// <summary>
  ///   Summary description for ILoggingProvider.
  /// </summary>
  public class LoggingProvider : ILoggingProvider
  {
    #region ILoggingProvider Members

    public void Write(LogRecord record)
    {
      var log = new LogEntry
      {
        EventId = record.EventId,
        AppDomainName = record.AppName
      };


      switch (record.Severity)
      {
        case Severity.Information:
          log.Severity = TraceEventType.Information;
          break;
        case Severity.Error:
          log.Severity = TraceEventType.Error;
          break;
        case Severity.Warning:
          log.Severity = TraceEventType.Warning;
          break;
        default:
          log.Severity = TraceEventType.Information;
          break;
      }

      log.TimeStamp = record.TimeStamp;
      log.Title = string.Format("Module: {0} - Method: {1}", record.ModuleName, record.MethodName);
      log.Message = record.Message;
      log.Categories.Add(record.Category);
      log.Priority = 1;
      log.ExtendedProperties = record.ExtendedProperties;

      Logger.Write(log);
    }


    public void Write(Exception e, ExtendedPropertyDictionary extendedProperties)
    {
      var rec = new LogRecord
      {
        Message = e.Message,
        ExtendedProperties = extendedProperties
      };


      Write(rec);
    }


    public void Write(IMethodCallMessage methodMessage, LoggingMethod loggingMethod)
    {
      var extendedProperties = new ExtendedPropertyDictionary();
      if (methodMessage.MethodBase.DeclaringType != null)
      {
        var modulo = methodMessage.MethodBase.DeclaringType.Module.ScopeName;
        var entidad = methodMessage.MethodBase.DeclaringType.Name;
        var funcion = methodMessage.MethodBase.Name;

        if (loggingMethod == LoggingMethod.ActionWithParameters)
        {
          for (var i = 0; i < methodMessage.InArgCount; i++)
          {
            var arg = methodMessage.GetInArg(i);
            var sargname = string.Format("{0} ({1})",
              methodMessage.GetInArgName(i),
              arg.GetType().Name);

            if (arg.GetType().IsValueType)
            {
              extendedProperties.Add(sargname, arg.ToString());
            }
            else
            {
              extendedProperties.AddSerialized(sargname, arg);
            }
          }
        }

        var rec = new LogRecord
        {
          Message = string.Format("{0}: Method {1} of {2} called at {3:dd/MM/yyyy}",
            modulo, funcion, entidad, DateTime.Now),
          ExtendedProperties = extendedProperties
        };

        Write(rec);
      }
    }

    #endregion
  }
}