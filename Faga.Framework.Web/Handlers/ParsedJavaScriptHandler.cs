using System.IO;
using System.Web;
using Faga.Framework.Configuration;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.Handlers
{
  public class ParsedJavaScriptHandler : IHttpHandler
  {
    #region IHttpHandler Members

    public bool IsReusable
    {
      get { return false; }
    }


    public void ProcessRequest(HttpContext context)
    {
      var request = context.Request;
      var response = context.Response;

      var path = request.PhysicalPath;

      if (File.Exists(path))
      {
        string scp;

        using (var sr = new StreamReader(path))
        {
          scp = sr.ReadToEnd();
          sr.Close();
        }

        scp = scp.Replace("{SERVER}", request.Url.Authority);

        scp = scp.Replace("{Faga.Framework.Services.Security}",
          ApplicationConfiguration.SecurityProvider.SecurityServicesUrl);

        scp = scp.Replace("{ApplicationId}",
          ApplicationConfiguration.SecurityProvider.ApplicationId.ToString());

        scp = scp.Replace("{ApplicationName}",
          WebApplicationConfiguration.ApplicationName);

        var filename = string.Concat(Path.GetFileNameWithoutExtension(path), ".js");

        response.ContentType = "application/x-javascript";

        response.AddHeader("Content-Disposition",
          string.Format("inline; filename={0};", filename));

        response.Write(scp);
      }
    }

    #endregion
  }
}