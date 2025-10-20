using System;
using System.Runtime.Remoting.Messaging;
using Faga.Framework.Logging.Model;
using Faga.Framework.Logging.Model.Collections;
using Faga.Framework.Translation.Providers;

namespace Faga.Framework.Logging
{
  /// <summary>
  ///   Summary description for LoggingProvider.
  /// </summary>
  [Serializable]
  public static class LoggingProvider
  {
    public static void Write(LogRecord record)
    {
      LoggingProviderFactory.Logging().Write(record);
    }


    public static void Write(Exception e)
    {
      Write(e, null);
    }


    public static void Write(Exception e, ExtendedPropertyDictionary extendedProperties)
    {
      LoggingProviderFactory.Logging().Write(e, extendedProperties);
    }


    public static void Write(IMethodCallMessage methodMessage, LoggingMethod loggingMethod)
    {
      LoggingProviderFactory.Logging().Write(methodMessage, loggingMethod);
    }
  }
}