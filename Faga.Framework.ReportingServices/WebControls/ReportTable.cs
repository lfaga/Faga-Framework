using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Faga.Framework.ReportingServices.ReportService;
using Faga.Framework.ReportingServices.Templates;
using Faga.Framework.Web.UI.Controls;

namespace Faga.Framework.ReportingServices.WebControls
{
  /// <summary>
  ///   Summary description for ReportTable.
  /// </summary>
  //[DefaultProperty("Text")]
  [ToolboxData("<{0}:ReportTable runat=server></{0}:ReportTable>")]
  public class ReportTable : ScriptableControl
  {
    private readonly ParameterValueCollection _parameters;
    private Report _report;


    public ReportTable()
    {
      _parameters = new ParameterValueCollection();
    }


    [DefaultValue("")]
    public string ReportPath { get; set; }

    [Editor(typeof (CollectionEditor), typeof (UITypeEditor))]
    public ParameterValueCollection Parameters
    {
      get { return _parameters; }
    }


    protected override void OnInit(EventArgs e)
    {
      if (string.Compare(CssClass, string.Empty, StringComparison.Ordinal) == 0)
      {
        CssClass = "_report";
      }

      base.OnInit(e);
    }


    protected override void OnLoad(EventArgs e)
    {
      if ((Site == null) || !Site.DesignMode)
      {
        var rsh = new ReportingServiceHelper();

        var ds = new HtmlDeviceSettings
        {
          HtmlFragment = "true",
          StyleStream = "true",
          StreamRoot = string.Format("{0}images/", Page.Request.ApplicationPath)
        };


        _report = rsh.RenderReport(ReportPath, RenderFormat.Format.Htmlowc,
          _parameters, ds);

        var sb = new StringBuilder();

        sb.Append("try { ");
        sb.AppendFormat("LoadOWCData(\"4\", \"{0}?Resource=M_4&Report={1}\");",
          Page.Request.Url, _report.Id);
        sb.Append(" } catch (e) {}");

        AddOnLoadScriptLines(sb.ToString());
      }

      base.OnLoad(e);
    }


    /// <summary>
    ///   Render this control to the output parameter specified.
    /// </summary>
    /// <param name="output"> The HTML writer to write out to </param>
    protected override void Render(HtmlTextWriter output)
    {
      var div = new HtmlGenericControl("DIV");
      div.Attributes["class"] = CssClass;

      if (!Width.IsEmpty)
      {
        div.Style["Width"] = Width.ToString(CultureInfo.InvariantCulture);
        div.Style["overflow-x"] = "auto";
      }

      if (!Height.IsEmpty)
      {
        div.Style["Height"] = Height.ToString(CultureInfo.InvariantCulture);
        div.Style["overflow-y"] = "auto";
      }

      if (Page is ReportPage)
      {
        if ((Site == null) || !Site.DesignMode)
        {
          foreach (Resource resource in _report.Resources.Values)
          {
            if (string.Compare(resource.MimeType, "text/css", StringComparison.Ordinal) == 0)
            {
              var css = new HtmlGenericControl("STYLE");
              css.Attributes.Add("type", "text/css");
              css.InnerHtml = resource.ToString();
              css.RenderControl(output);
            }
          }
          div.InnerHtml = _report.ToString();
        }
        else
        {
          div.Style["border"] = "1px solid black";
          div.InnerHtml = string.Format("[ReportTabe '{0}']<br>Report: '{1}'",
            ID, ReportPath);
        }
      }
      else
      {
        div.Style["border"] = "1px solid black";
        div.InnerHtml = "Este control sólo funciona en una página que herede de ReportPage";
      }

      div.RenderControl(output);
    }
  }
}