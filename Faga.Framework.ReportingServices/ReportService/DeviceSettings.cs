using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for DeviceSettings.
  /// </summary>
  public abstract class DeviceSettings
  {
    public override string ToString()
    {
      var ser = new XmlSerializer(GetType());
      var sw = new StringWriter();
      ser.Serialize(sw, this);
      var doc = new XmlDocument();
      doc.LoadXml(sw.ToString());
      sw.Close();

      var sb = new StringBuilder();
      if (doc.DocumentElement != null) sb.AppendFormat("<DeviceInfo>{0}</DeviceInfo>", doc.DocumentElement.InnerXml);

      return sb.ToString();
    }
  }
}