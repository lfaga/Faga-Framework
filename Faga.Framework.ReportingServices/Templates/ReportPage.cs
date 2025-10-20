using System;
using Faga.Framework.ReportingServices.ReportService;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.ReportingServices.Templates
{
  /// <summary>
  ///   Summary description for ReportPage.
  /// </summary>
  public class ReportPage : PageTemplate
  {
    protected override void OnLoad(EventArgs e)
    {
      if (Request["Report"] != null)
      {
        var rep = Request["Report"];
        Resource r = null;
        var report = ReportingServiceHelper.ReportCache[rep];

        if (Request["Resource"] != null)
        {
          var res = Request["Resource"];

          if ((report != null) && report.Resources.ContainsKey(res))
          {
            r = report.Resources[res];
          }
        }
        else
        {
          r = report;
        }

        if (r != null)
        {
          Response.ContentType = r.MimeType;
          Response.Clear();
          Response.BinaryWrite(r.Data);
          Response.End();
        }
      }

      base.OnLoad(e);
    }
  }
}