using System;
using System.Globalization;
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
  public partial class ParamsPage
    : DataAbmTemplate<GroupPermission, GroupPermissionCollection>
  {
    private readonly string seleccionarURL =
      "/Administracion/Dialogs/SetearParametro.aspx?application={0:N0}&permission={1:N0}";


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

        InitializeAbm(new GroupPermissionProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave, lnkCancel, lnkSearch,
          cboGroup, cboPermission, grdDetail, lnkAttrAdd);

        var trans = new SimpleTransaction(CurrentUser);

        ListControls.Fill(cboApplication, new ApplicationProvider(trans).List(null),
          "Name", "Id", "[Seleccione]");
      }
    }

    private void ParamsPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<GroupPermission> e)
    {
      var gp = e.Element;

      ListControls.SelectByValue(cboGroup, gp.Group.Id);
      ListControls.SelectByValue(cboPermission, gp.Permission.Id);
      ListControls.SelectByValue(cboApplication, gp.Group.IdApplication);

      var pv = new PermissionValue();
      pv.IdGroup = gp.Group.Id;
      pv.IdPermission = gp.Permission.Id;
      pv.IdApplication = gp.Group.IdApplication;

      grdDetail.DataSource = new PermissionValueProvider(e.Transaction).List(pv);
      grdDetail.DataBind();
    }


    private void ParamsPage_MasterItemSaved(object sender, UaMasterItemEventArgs<GroupPermission> e)
    {
      var gp = SelectedItem;
      var pvs = (PermissionValueCollection) grdDetail.DataSource;

      foreach (var pv in pvs)
      {
        pv.IdApplication = gp.Group.IdApplication;
        pv.IdGroup = gp.Group.Id;
        pv.IdPermission = gp.Permission.Id;
      }

      var pvp = new PermissionValueProvider(e.Transaction);
      pvp.Remove(gp.Permission, gp.Group);
      if (pvp.SetItems(pvs).Count < 1)
      {
        throw new Exception("No se grabó ningún parámetro.");
      }
    }


    private void cboApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        IdApplication = int.Parse(cboApplication.SelectedItem.Value);
        grdMaster.Visible = true;

        var trans = new SimpleTransaction(CurrentUser);

        var g = new Group();
        g.IdApplication = IdApplication;
        ListControls.Fill(cboGroup, new GroupProvider(trans).List(g),
          "Name", "Id", "[Seleccione]");

        var p = new Permission();
        p.IdApplication = IdApplication;
        ListControls.Fill(cboPermission, new PermissionProvider(trans).List(p),
          "Description", "Id", "[Seleccione]");
      }
      catch (Exception)
      {
        IdApplication = 0;
        grdMaster.Visible = false;
      }
    }


    private void lnkAttrAdd_Click(object sender, EventArgs e)
    {
      var gp = SelectedItem;

      OpenDialog(string.Format(
        seleccionarURL, gp.Group.IdApplication, gp.Permission.Id), 140, 280);
    }


    private void ParamsPage_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      var gp = SelectedItem;

      if (e.WasCalledFrom(string.Format(
        seleccionarURL, gp.Group.IdApplication, gp.Permission.Id)))
      {
        try
        {
          var skey = e.ReturnValue["key"];
          var svalue = e.ReturnValue["value"];

          if ((skey != null) && (svalue != null))
          {
            var lst = grdDetail.DataSource as PermissionValueCollection;

            if (lst == null)
            {
              lst = new PermissionValueCollection();
            }

            var pv = new PermissionValue();

            pv.IdApplication = gp.Group.IdApplication;
            pv.IdGroup = gp.Group.Id;
            pv.IdPermission = gp.Permission.Id;
            pv.Key = skey;
            pv.Value = svalue;

            if (lst.Contains(pv))
            {
              throw new Exception("El elemento ya existe en la lista.");
            }

            lst.Add(pv);

            grdDetail.DataSource = lst;
            grdDetail.DataBind();
          }
          else
          {
            ShowError("La selección no es válida.");
          }
        }
        catch (Exception ex)
        {
          ShowError("La selección no es válida. " + ex.Message);
        }
      }
    }


    private void grdDetail_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
      var lst = grdDetail.DataSource as PermissionValueCollection;
      if (lst != null)
      {
        lst.RemoveAt(e.Item.DataSetIndex);
        grdDetail.DataSource = lst;
        grdDetail.DataBind();
      }
    }


    private void ParamsPage_ABMModeChanged(object sender, AbmModeChangeEventArgs e)
    {
      if (e.Mode != DataAbmTemplateMode.ModeNone)
      {
        cboGroup.Enabled = false;
        cboPermission.Enabled = false;
        cboGroup.CssClass = "box_disabled";
        cboPermission.CssClass = "box_disabled";
      }
      else
      {
        cboGroup.Enabled = true;
        cboPermission.Enabled = true;
        cboGroup.CssClass = "box";
        cboPermission.CssClass = "box";
      }
    }


    private void ParamsPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<GroupPermission> e)
    {
      var gp = e.Element;
      Group g;
      Permission p;

      if (cboGroup.SelectedIndex > 0)
      {
        var gfilter = new Group();
        gfilter.Id = Convert.ToInt32(cboGroup.SelectedItem.Value,
          CultureInfo.InvariantCulture);
        g = new GroupProvider(e.Transaction).GetItem(gfilter);
      }
      else
      {
        g = new Group();
        g.IdApplication = IdApplication;
      }

      if (cboPermission.SelectedIndex > 0)
      {
        var pfilter = new Permission();
        pfilter.Id = Convert.ToInt32(cboPermission.SelectedItem.Value,
          CultureInfo.InvariantCulture);
        p = new PermissionProvider(e.Transaction).GetItem(pfilter);
      }
      else
      {
        p = new Permission();
        p.IdApplication = IdApplication;
      }

      gp.Group = g;
      gp.Permission = p;
    }


    private void ParamsPage_MasterItemBeforeSave(object sender, UaMasterItemEventArgs<GroupPermission> e)
    {
      e.SkipOperation();
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
        new EventHandler<UaMasterItemEventArgs<GroupPermission>>(this.ParamsPage_MasterItemBeforeSearch);
      this.MasterItemSaved += new EventHandler<UaMasterItemEventArgs<GroupPermission>>(this.ParamsPage_MasterItemSaved);
      this.MasterItemLoaded +=
        new EventHandler<UaMasterItemEventArgs<GroupPermission>>(this.ParamsPage_MasterItemLoaded);
      this.AbmModeChanged += new EventHandler<AbmModeChangeEventArgs>(this.ParamsPage_ABMModeChanged);
      this.ReturnedFromDialog += new EventHandler<UaDialogReturnEventArgs>(this.ParamsPage_ReturnedFromDialog);
      this.Load += new EventHandler(this.Page_Load);
      this.MasterItemBeforeSave +=
        new EventHandler<UaMasterItemEventArgs<GroupPermission>>(ParamsPage_MasterItemBeforeSave);
    }

    #endregion
  }
}