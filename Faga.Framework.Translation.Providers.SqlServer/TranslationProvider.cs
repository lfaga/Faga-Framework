using System;
using System.Data.SqlClient;
using System.Reflection;
using Faga.Framework.Data;

namespace Faga.Framework.Translation.Providers.SqlServer
{
  /// <summary>
  ///   Summary description for TranslationProvider.
  /// </summary>
  public class TranslationProvider : ITranslationProvider
  {
    #region ITranslationProvider Members

    public string Translate(int languageCode, string textId, IConnection cn)
    {
      try
      {
        var pars = new ParameterCollection
        {
          new SqlParameter("Message_Code", textId),
          new SqlParameter("Language", languageCode)
        };

        var ret = Convert.ToString(cn.ExecuteScalar("spTranslateMessage", pars));

        if (IsNull.This(ret))
        {
          throw new Exception();
        }

        return ret;
      }
      catch (Exception)
      {
        return "L:" + languageCode + "; ID:" + textId;
      }
    }


    public string Translate(int languageCode, string textId, Type objectType,
      IConnection cn)
    {
      try
      {
        var pars = new ParameterCollection
        {
          new SqlParameter("Label_Code", textId),
          new SqlParameter("Label_Type", objectType.Name),
          new SqlParameter("Language", languageCode)
        };

        var ret = Convert.ToString(cn.ExecuteScalar("spTranslateLabel", pars));

        if (IsNull.This(ret))
        {
          throw new Exception();
        }

        return ret;
      }
      catch (Exception)
      {
        return "L:" + languageCode + "; ID:" + textId + "; Type:" + objectType.Name;
      }
    }


    public string Translate(int languageCode, Type objectType, IConnection cn)
    {
      try
      {
        var pars = new ParameterCollection
        {
          new SqlParameter("Object_Code", objectType.Name),
          new SqlParameter("Language", languageCode)
        };

        var ret = Convert.ToString(cn.ExecuteScalar("spTranslateEntity", pars));

        if (IsNull.This(ret))
        {
          throw new Exception();
        }

        return ret;
      }
      catch (Exception)
      {
        return "L:" + languageCode + ";  Type:" + objectType.Name;
      }
    }


    public string Translate(int languageCode, Type objectType, PropertyInfo property,
      TextLength length, IConnection cn)
    {
      try
      {
        var pars = new ParameterCollection
        {
          new SqlParameter("Object_Code", objectType.Name),
          new SqlParameter("Property_Code", property.Name),
          new SqlParameter("Language", languageCode),
          new SqlParameter("LongDescription", Convert.ToInt32(length))
        };

        var ret = Convert.ToString(cn.ExecuteScalar("spTranslateProperty", pars));

        if (IsNull.This(ret))
        {
          throw new Exception();
        }

        return ret;
      }
      catch (Exception)
      {
        return "L:" + languageCode + ";  Type:" + objectType.Name + "; PROP:" + property.Name;
      }
    }

    #endregion
  }
}