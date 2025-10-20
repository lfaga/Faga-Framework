using System;
using Faga.Framework.Logging.Model;

namespace Faga.Framework.Logging
{
  /// <summary>
  ///   Summary description for LogableAttribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class LogableAttribute : Attribute
  {
    private readonly LoggingMethod _method;


    public LogableAttribute(LoggingMethod method)
    {
      _method = method;
    }


    public LoggingMethod Method
    {
      get { return _method; }
    }
  }
}