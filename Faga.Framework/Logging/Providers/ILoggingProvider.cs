using System;
using System.Runtime.Remoting.Messaging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Logging.Model.Collections;

namespace Faga.Framework.Logging.Providers
{
  /// <summary>
  ///   Summary description for ILoggingProvider.
  /// </summary>
  public interface ILoggingProvider
  {
    void Write(LogRecord record);

    void Write(Exception e, ExtendedPropertyDictionary extendedProperties);

    void Write(IMethodCallMessage methodMessage, LoggingMethod loggingMethod);
  }
}