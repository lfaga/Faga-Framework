using System.Reflection;
using Faga.Framework.Configuration;
using Faga.Framework.Logging.Providers;

namespace Faga.Framework.Translation.Providers
{
  public sealed class LoggingProviderFactory
  {
    private LoggingProviderFactory()
    {
    }


    public static ILoggingProvider Logging()
    {
      var path =
        ApplicationConfiguration.Providers["LoggingProvider"].Type;

      return (ILoggingProvider) Assembly.Load(path)
        .CreateInstance(path + ".LoggingProvider");
    }
  }
}