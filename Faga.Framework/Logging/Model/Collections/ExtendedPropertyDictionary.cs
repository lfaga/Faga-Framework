using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Faga.Framework.Logging.Model.Collections
{
  /// <summary>
  ///   Summary description for ExtendedPropertyDictionary.
  /// </summary>
  [Serializable]
  public class ExtendedPropertyDictionary : Dictionary<string, object>
  {
    public ExtendedPropertyDictionary()
    {
    }


    protected ExtendedPropertyDictionary(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }


    public void AddSerialized(string key, object value)
    {
      using (var sw = new StringWriter(CultureInfo.InvariantCulture))
      {
        try
        {
          var xs = new XmlSerializer(value.GetType());
          xs.Serialize(sw, value);

          Add(key, sw.ToString());
        }
        catch
        {
          Add(key, "Warning: Object can't be serialized.");
        }
      }
    }
  }
}