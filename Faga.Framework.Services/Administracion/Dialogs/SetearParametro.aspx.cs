using System;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Controls;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.Services.Administracion.Dialogs
{
  /// <summary>
  ///   Summary description for SetearParametro.
  /// </summary>
  public partial class SetearParametro : DialogTemplate
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        var idap = Convert.ToInt32(Request["application"]);
        var idper = Convert.ToInt32(Request["permission"]);

        var trans = new SimpleTransaction(CurrentUser);
        trans.IdApplication = idap;

        var pp = new PermissionParam();
        pp.IdApplication = idap;
        pp.IdPermission = idper;

        ListControls.Fill(cboKey, new PermissionParamProvider(trans).List(pp),
          "Key", "Key", "[Parámetro]");
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