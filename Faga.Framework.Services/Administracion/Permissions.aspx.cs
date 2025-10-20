using System;
using System.Web.UI.WebControls;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Web.UI.Controls;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Services.Administracion.Webpages
{
  /// <summary>
  ///   Summary description for Applications.
  /// </summary>
  public partial class PermissionsPage
    : DataAbmTemplate<Permission, PermissionCollection>
  {
    private readonly string seleccionarURL = "/Administracion/Dialogs/CrearParametro.aspx";


    protected void Page_Load(object sender, EventArgs e)
    {
      AddOnLoadScriptLines(
        "page.addControl( new RequiredCombo('cboApplication', 'Aplicaciones') );");

      if (!IsPostBack)
      {
        if (Request["application"] != null)
        {
          ListControls.SelectByValue(cboApplication, Request["application"]);
          cboApplication.Enabled = false;
        }

        InitializeAbm(new PermissionProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave,
          lnkCancel, lnkSearch, txtDescription, txtId, grdDetail);

        AddValidationScript("RequiredBox", "txtDescription", "Descripción");

        ListControls.Fill(
          cboApplication,
          new ApplicationProvider(new SimpleTransaction(CurrentUser)).List(null),
          "Name", "Id", "[Seleccione]");
      }
    }

    private void PermissionsPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<Permission> e)
    {
      var perm = e.Element;
      txtDescription.Text = perm.Description;
      txtId.Text = perm.Id.ToString();
      ListControls.SelectByValue(cboApplication, perm.IdApplication);

      grdDetail.DataSource = new PermissionParamProvider(e.Transaction).List(perm);
      grdDetail.DataBind();
    }


    private void PermissionsPage_MasterItemBeforeSave(object sender, UaMasterItemEventArgs<Permission> e)
    {
      var perm = e.Element;
      perm.Description = txtDescription.Text;
      perm.IdApplication = Convert.ToInt32(cboApplication.SelectedItem.Value);
    }


    private void PermissionsPage_MasterItemSaved(object sender, UaMasterItemEventArgs<Permission> e)
    {
      var p = e.Element;
      var pps = grdDetail.DataSource as PermissionParamCollection;

      if (pps != null)
      {
        foreach (var pp in pps)
        {
          pp.IdApplication = p.IdApplication;
          pp.IdPermission = p.Id;
        }

        if (new PermissionParamProvider(e.Transaction).SetItems(pps) == null)
        {
          throw new Exception("Error al grabar los elementos.");
        }
      }
    }


    private void PermissionsPage_MasterItemDeleted(object sender, UaMasterItemEventArgs<Permission> e)
    {
      var p = e.Element;
      new PermissionParamProvider(e.Transaction).Remove(p);
      new GroupPermissionProvider(e.Transaction).Remove(
        p, new GroupProvider(e.Transaction).List(p));
    }


    private void lnkAttrAdd_Click(object sender, EventArgs e)
    {
      OpenDialog(seleccionarURL, 80, 280);
    }


    private void PermissionsPage_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      if (e.WasCalledFrom(seleccionarURL))
      {
        try
        {
          var key = e.ReturnValue["key"];

          if (key != null)
          {
            var lst = (PermissionParamCollection) grdDetail.DataSource;

            if (lst == null)
            {
              lst = new PermissionParamCollection();
            }

            var idp = int.MinValue;

            if (grdMaster.SelectedIndex >= 0)
            {
              idp = ((PermissionCollection) grdMaster.DataSource)[grdMaster.SelectedItem.DataSetIndex].Id;
            }

            var pp = new PermissionParam(idp, IdApplication, key);

            if (lst.Contains(pp))
            {
              throw new Exception("El elemento ya existe en la lista.");
            }

            lst.Add(pp);

            grdDetail.DataSource = lst;
            grdDetail.DataBind();
          }
        }
        catch (Exception ex)
        {
          ShowError("La selección no es válida. " + ex.Message);
        }
      }
    }


    private void PermissionsPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<Permission> e)
    {
      var p = e.Element;

      if (cboApplication.SelectedIndex > 0)
      {
        p.IdApplication = Convert.ToInt32(cboApplication.SelectedItem.Value);
      }

      if (txtDescription.Text != string.Empty)
      {
        p.Description = txtDescription.Text;
      }
    }


    private void grdDetail_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
      var lst = (PermissionParamCollection) grdDetail.DataSource;
      lst.RemoveAt(e.Item.DataSetIndex);
      grdDetail.DataSource = lst;
      grdDetail.DataBind();
    }


    private void cboApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboApplication.SelectedIndex > 0)
      {
        IdApplication = Convert.ToInt32(cboApplication.SelectedItem.Value);
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
      this.cboApplication.SelectedIndexChanged += new EventHandler(this.cboApplication_SelectedIndexChanged);
      this.grdDetail.DeleteCommand += new DataGridCommandEventHandler(this.grdDetail_DeleteCommand);
      this.lnkAttrAdd.Click += new EventHandler(this.lnkAttrAdd_Click);
      this.MasterItemBeforeSearch +=
        new EventHandler<UaMasterItemEventArgs<Permission>>(this.PermissionsPage_MasterItemBeforeSearch);
      this.MasterItemDeleted +=
        new EventHandler<UaMasterItemEventArgs<Permission>>(this.PermissionsPage_MasterItemDeleted);
      this.MasterItemSaved += new EventHandler<UaMasterItemEventArgs<Permission>>(this.PermissionsPage_MasterItemSaved);
      this.MasterItemLoaded +=
        new EventHandler<UaMasterItemEventArgs<Permission>>(this.PermissionsPage_MasterItemLoaded);
      this.MasterItemBeforeSave +=
        new EventHandler<UaMasterItemEventArgs<Permission>>(this.PermissionsPage_MasterItemBeforeSave);
      this.ReturnedFromDialog += new EventHandler<UaDialogReturnEventArgs>(this.PermissionsPage_ReturnedFromDialog);
      this.Load += new EventHandler(this.Page_Load);
    }

    #endregion
  }
}