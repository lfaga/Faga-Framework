using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Faga.Framework.Web.UI.Templates
{
  /// <summary>
  ///   Summary description for DialogTemplate.
  /// </summary>
  public class DialogTemplate : BasePageTemplate
  {
    #region Overrides

    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      SetupPage();
      base.Render(htm);

      output.Write(str.ToString());
    }

    #endregion

    #region Private Methods

    private void SetupPage()
    {
      ClientScript.RegisterOnSubmitStatement(GetType(), Guid.NewGuid().ToString(),
        "return false;");
      Header.Controls.Add(GetScript());
    }


    private HtmlGenericControl GetScript()
    {
      var scp = new HtmlGenericControl("SCRIPT");
      scp.Attributes["language"] = "javascript";
      scp.Attributes["type"] = "text/javascript";
      scp.InnerHtml = "\n<!--"
                      + "\n\t	var controllerObject = window.dialogArguments;"
                      + "\n\t	controllerObject.setWindowHandle(window);"
                      + "\n//-->\n";
      return scp;
    }

    #endregion
  }
}