using System;
using Faga.Framework.Logging.Model.Collections;

namespace Faga.Framework.Logging.Model
{
  /// <summary>
  ///   Summary description for LogRecord.
  /// </summary>
  public class LogRecord
  {
    private ExtendedPropertyDictionary extendedProperties;


    public LogRecord()
    {
      AppName = AppDomain.CurrentDomain.FriendlyName;
      TimeStamp = DateTime.Now;
    }


    public LogRecord(int _eventId, string _moduleName, string _methodName,
      string _category, string _message, Severity _severity)
      : this()
    {
      ModuleName = _moduleName;
      MethodName = _methodName;
      Category = _category;
      EventId = _eventId;
      Message = _message;
      Severity = _severity;
    }

    #region Public Properties

    public string AppName { get; set; }

    public string ModuleName { get; set; }

    public string MethodName { get; set; }

    public string Category { get; set; }

    public int EventId { get; set; }

    public string Message { get; set; }

    public ExtendedPropertyDictionary ExtendedProperties
    {
      get
      {
        if (extendedProperties == null)
        {
          extendedProperties = new ExtendedPropertyDictionary();
        }
        return extendedProperties;
      }
      set { extendedProperties = value; }
    }

    public Severity Severity { get; set; }

    public DateTime TimeStamp { get; set; }

    #endregion
  }
}