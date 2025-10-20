using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using Faga.Framework.Configuration;
using Faga.Framework.ReportingServices.ReportService;

namespace Faga.Framework.ReportingServices
{
  /// <summary>
  ///   Summary description for ReportingServiceHelper.
  /// </summary>
  public class ReportingServiceHelper
  {
    private static readonly ReportCollection reportCache = new ReportCollection();

    private readonly ReportingService _rservice;
    private readonly SessionHeader _sessionHeader;


    public ReportingServiceHelper()
      : this(null)
    {
    }


    public ReportingServiceHelper(string reportServerUrl)
    {
      var url = reportServerUrl;
      if (url == null)
      {
        url = ApplicationConfiguration.RepotingConfiguration.ServiceUrl;
      }
      if (url == null)
      {
        url = "http://localhost/ReportServer/ReportService.asmx";
      }

      _rservice = new ReportingService(url);
      _sessionHeader = new SessionHeader();
    }


    public ReportingService ReportingService
    {
      get { return _rservice; }
    }

    public static ReportCollection ReportCache
    {
      get { return reportCache; }
    }


    public DataTable RenderDataTable(string reportPath,
      ParameterValueCollection parameters)
    {
      var ms = new MemoryStream(
        RenderReport(reportPath, RenderFormat.Format.Xml, parameters, null).Data);
      var xt = new XmlTextReader(ms);
      var ds = new DataSet();
      ds.ReadXml(xt);
      return ds.Tables[ds.Tables.Count - 1];
    }


    public XmlDocument RenderXmlDocument(string reportPath,
      ParameterValueCollection parameters)
    {
      var ms = new MemoryStream(
        RenderReport(reportPath, RenderFormat.Format.Xml, parameters, null).Data);
      var xt = new XmlTextReader(ms);
      var doc = new XmlDocument();
      doc.Load(xt);
      return doc;
    }


    public string RenderString(string reportPath, RenderFormat.Format format,
      ParameterValueCollection parameters,
      DeviceSettings devSettings)
    {
      var rep = RenderReport(reportPath, format, parameters, devSettings);
      return rep.ToString();
    }


    public void RenderFile(Stream output, string reportPath, RenderFormat.Format format,
      ParameterValueCollection parameters, DeviceSettings devSettings)
    {
      var bin = RenderReport(reportPath, format, parameters, devSettings).Data;
      output.Write(bin, 0, bin.Length);
    }


    public void RenderFile(HttpResponse response, string reportPath,
      RenderFormat.Format format, ParameterValueCollection parameters,
      DeviceSettings devSettings)
    {
      var rep = RenderReport(reportPath, format, parameters, devSettings);
      var bin = rep.Data;

      response.ContentType = rep.MimeType;

      if (rep.Encoding != null)
      {
        var e = Encoding.Default;
        if (string.Compare(rep.Encoding, "Unicode (UTF-8)", StringComparison.Ordinal) == 0)
        {
          e = Encoding.Unicode;
        }
        response.ContentEncoding = e;
      }

      response.AddHeader("Content-Disposition", "inline;filename=Report.bin");

      response.Clear();
      response.BinaryWrite(bin);
      response.End();
    }


    public Report RenderReport(string reportPath, RenderFormat.Format format,
      ParameterValueCollection parameters,
      DeviceSettings devSettings)
    {
      var devInfo = string.Empty;
      if (devSettings != null)
      {
        devInfo = devSettings.ToString();
      }

      _rservice.SessionHeaderValue = _sessionHeader;
      ParameterValue[] prms = null;
      if ((parameters != null) && (parameters.Count > 0))
      {
        prms = (ParameterValue[]) parameters.ToArray();
      }

      try
      {
        var rep = new Report {Id = Guid.NewGuid().ToString()};
        string mimeType;
        string encoding;
        Warning[] warnings;
        ParameterValue[] reportHistoryParameters;
        string[] streamIDs;
        rep.Data = _rservice.Render(reportPath, RenderFormat.ToString(format), null,
          devInfo, prms, null, null,
          out encoding, out mimeType, out reportHistoryParameters,
          out warnings, out streamIDs);
        rep.MimeType = mimeType;
        rep.Encoding = encoding;

        _sessionHeader.SessionId = _rservice.SessionHeaderValue.SessionId;

        foreach (var s in streamIDs)
        {
          var r = new Resource
          {
            Id = s,
            Data = _rservice.RenderStream(reportPath, RenderFormat.ToString(format),
              s, null, devInfo, prms, out encoding,
              out mimeType),
            Encoding = encoding,
            MimeType = mimeType
          };
          rep.Resources.Add(r);
        }

        if (!ReportCache.ContainsKey(rep.Id))
        {
          ReportCache.Add(rep);
        }

        return rep;
      }
      catch (SoapException ex)
      {
        throw new ReportServiceException(ex);
      }
    }
  }
}