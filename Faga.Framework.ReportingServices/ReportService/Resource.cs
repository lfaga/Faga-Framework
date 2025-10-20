using System.IO;

namespace Faga.Framework.ReportingServices.ReportService
{
  public class Resource
  {
    public string Id { get; set; }

    public byte[] Data { get; set; }

    public string MimeType { get; set; }

    public string Encoding { get; set; }


    public override string ToString()
    {
      using (var ms = new MemoryStream(Data))
      {
        using (var sr = new StreamReader(ms))
        {
          var s = sr.ReadToEnd();
          sr.Close();
          ms.Close();
          return s;
        }
      }
    }
  }
}