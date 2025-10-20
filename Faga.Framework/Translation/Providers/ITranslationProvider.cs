using System;
using System.Reflection;
using Faga.Framework.Data;

namespace Faga.Framework.Translation.Providers
{
  public interface ITranslationProvider
  {
    string Translate(int languageCode, string textId, IConnection cn);

    string Translate(int languageCode, string textId, Type objectType, IConnection cn);

    string Translate(int languageCode, Type objectType, IConnection cn);

    string Translate(int languageCode, Type objectType, PropertyInfo property, TextLength tl, IConnection cn);
  }
}