using System;
using System.Configuration;
using System.Web;
using Faga.Framework.Configuration;

namespace Faga.Framework.Web.Configuration
{
  public class WebApplicationConfiguration : ApplicationConfiguration
  {
    private WebApplicationConfiguration()
    {
    }


    public static WebUiConfigurationElement WebUiConfiguration
    {
      get { return Section.WebUiConfiguration; }
    }


    public static MailConfigurationElement MailConfiguration
    {
      get { return Section.MailConfiguration; }
    }

    public static string WelcomePage
    {
      get { return Section.WelcomePage; }
    }

    public static string ApplicationName
    {
      get
      {
        var ret = string.Empty;
        try
        {
          ret = Section.ApplicationName;
        }
        catch
        {
        }

        if ((ret != null)
            && (ret.Length == 0))
        {
          ret = AppDomain.CurrentDomain.FriendlyName;
        }

        return ret;
      }
    }

    private static WebApplicationConfigurationSection Section
    {
      get
      {
        var wacs =
          ConfigurationManager.GetSection("ApplicationConfiguration") as WebApplicationConfigurationSection;

        if (wacs == null)
        {
          throw new ConfigurationErrorsException("Faga.Framework configuration is missing.");
        }

        return wacs;
      }
    }


    public static string GetBaseUrl()
    {
      var s = HttpContext.Current.Request.ApplicationPath;

      if (s.EndsWith("/"))
      {
        s = s.Substring(0, s.Length - 1);
      }

      return s;
    }


    public static string GetRelativePath(string path)
    {
      var bu = GetBaseUrl();

      if (path.StartsWith(bu))
      {
        return path;
      }

      return string.Concat(bu, path.StartsWith("/") ? "" : "/", path);
    }
  }
}