using System.Reflection;
using Faga.Framework.Configuration;

namespace Faga.Framework.Translation.Providers
{
  public static class TranslationProviderFactory
  {
    public static ITranslationProvider Translator()
    {
      var path = ApplicationConfiguration.Providers["TranslationProvider"].Type;

      return (ITranslationProvider) Assembly.Load(path)
        .CreateInstance(path + ".TranslationProvider");
    }
  }
}