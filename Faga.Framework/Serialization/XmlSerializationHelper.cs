using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Faga.Framework.Serialization
{
  /// <summary>
  ///   Summary description for XmlSerializationHelper.
  /// </summary>
  public sealed class XmlSerializationHelper
  {
    private XmlSerializationHelper()
    {
    }


    public static IXPathNavigable Serialize(object data)
    {
      if (data == null)
      {
        throw new ArgumentNullException("data");
      }

      var doc = new XmlDocument();

      using (var sw = new StringWriter(CultureInfo.InvariantCulture))
      {
        var ser = new XmlSerializer(data.GetType());
        ser.Serialize(sw, data);
        doc.LoadXml(sw.ToString());
      }
      return doc;
    }


    public static IXPathNavigable Serialize(StringDictionary data, string tableName)
    {
      if (data == null)
      {
        throw new ArgumentNullException("data");
      }

      if (tableName == null)
      {
        throw new ArgumentNullException("tableName");
      }

      using (var sw = new StringWriter(CultureInfo.InvariantCulture))
      {
        var xw = new XmlTextWriter(sw);

        xw.WriteStartElement(tableName);

        foreach (string s in data.Keys)
        {
          xw.WriteElementString(s, data[s]);
        }

        xw.WriteEndElement();
        xw.Close();

        var doc = new XmlDocument();
        doc.LoadXml(sw.ToString());

        return doc;
      }
    }
  }
}