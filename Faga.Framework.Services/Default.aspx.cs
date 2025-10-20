using System;
using System.Security;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Controls.WebControls;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.Services.Webpages
{
  /// <summary>
  ///   Summary description for Default.
  /// </summary>
  public partial class Default : PageTemplate
  {
    protected SimpleMenuBar smbMenu;


    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        var trans = new SimpleTransaction(CurrentUser);
        var p = new Permission();
        p.Id = 1;
        new UserProvider(trans).CheckPermission(
          CurrentUser, new PermissionProvider(trans).GetItem(p));
      }
      catch (SecurityException ex)
      {
        ExceptionHandling(ex);
      }

      if (!IsPostBack)
      {
        IncludeScript("/java/ui/controls/ajax/UserLabel.pjs");

        AddOnLoadScriptLines(
          "page.addControl( new UserLabel('lblUserName') );");
      }
    }

    #region Web Form Designer generated code

    protected override void OnInit(EventArgs e)
    {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
    }


    /// <summary>
    ///   Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.Load += new EventHandler(this.Page_Load);
    }

    #endregion
  }
}