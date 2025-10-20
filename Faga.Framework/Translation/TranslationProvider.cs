using System;
using System.Reflection;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Translation.Providers;

namespace Faga.Framework.Translation
{
  [Serializable]
  public sealed class TranslationProvider
  {
    private TranslationProvider()
    {
    }


    public static string Translate(string textId, BaseTransaction trans)
    {
      return TranslationProviderFactory.Translator().Translate(trans.Usuario.IdLanguage,
        textId, trans.Connection);
    }


    public static string Translate(string textId, Type objectType, BaseTransaction trans)
    {
      return TranslationProviderFactory.Translator().Translate(trans.Usuario.IdLanguage,
        textId, objectType,
        trans.Connection);
    }


    public static string Translate(Type objectType, BaseTransaction trans)
    {
      return TranslationProviderFactory.Translator().Translate(trans.Usuario.IdLanguage,
        objectType, trans.Connection);
    }


    public static string Translate(Type objectType, string propertyName,
      TextLength tl, BaseTransaction trans)
    {
      return Translate(objectType, objectType.GetProperty(propertyName),
        tl, trans);
    }


    public static string Translate(Type objectType, PropertyInfo property,
      TextLength tl, BaseTransaction trans)
    {
      return TranslationProviderFactory.Translator().Translate(trans.Usuario.IdLanguage,
        objectType, property,
        tl, trans.Connection);
    }
  }
}