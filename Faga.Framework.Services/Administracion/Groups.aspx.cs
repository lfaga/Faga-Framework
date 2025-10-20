using System;
using System.Web.UI.WebControls;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Web.UI.Controls;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Services.Administracion
{
  /// <summary>
  ///   Summary description for Groups.
  /// </summary>
  public partial class GroupsPage
    : DataAbmTemplate<Group, GroupCollection>
  {
    private readonly string seleccionarURL = "/Administracion/Dialogs/SeleccionarPermiso.aspx?application={0}";


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

        InitializeAbm(new GroupProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave, lnkCancel,
          lnkSearch, txtName, txtDescription, grdDetail);

        AddValidationScript("RequiredBox", "txtName", "Nombre de Grupo");

        ListControls.Fill(
          cboApplication, new ApplicationProvider(new SimpleTransaction(CurrentUser)).List(null),
          "Name", "Id", "[Seleccione]");
      }
    }

    private void GroupsPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<Group> e)
    {
      var grp = e.Element;
      txtName.Text = grp.Name;
      txtDescription.Text = grp.Description;
      ListControls.SelectByValue(cboApplication, grp.IdApplication);

      grdDetail.DataSource = new PermissionProvider(e.Transaction).List(grp);
      grdDetail.DataBind();
    }


    private void GroupsPage_MasterItemBeforeSave(object sender, UaMasterItemEventArgs<Group> e)
    {
      var grp = e.Element;
      grp.Name = txtName.Text;
      grp.Description = txtDescription.Text;
      grp.IdApplication = Convert.ToInt32(cboApplication.SelectedItem.Value);
    }


    private void GroupsPage_MasterItemSaved(object sender, UaMasterItemEventArgs<Group> e)
    {
      var gsps = new GroupPermissionCollection();
      var g = e.Element;

      if (grdDetail.DataSource != null)
      {
        foreach (var p in (PermissionCollection) grdDetail.DataSource)
        {
          gsps.Add(new GroupPermission(g, p));
        }

        var gpv = new GroupPermissionProvider(e.Transaction);
        gpv.Remove(g, new PermissionProvider(e.Transaction).List(g));

        if (gpv.SetItems(gsps) == null)
        {
          throw new Exception("Error al grabar los elementos.");
        }
      }
    }


    private void GroupsPage_MasterItemDeleted(object sender, UaMasterItemEventArgs<Group> e)
    {
      var g = e.Element;

      new GroupPermissionProvider(e.Transaction).Remove(
        g, new PermissionProvider(e.Transaction).List(g));
    }


    private void lnkAttrAdd_Click(object sender, EventArgs e)
    {
      OpenDialog(string.Format(seleccionarURL, IdApplication), 100, 280);
    }


    private void GroupsPage_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      if (e.WasCalledFrom(string.Format(seleccionarURL, IdApplication)))
      {
        try
        {
          int? id = Convert.ToInt32(e.ReturnValue["permission"]);

          if (id.HasValue)
          {
            var lst = (PermissionCollection) grdDetail.DataSource;

            if (lst == null)
            {
              lst = new PermissionCollection();
            }

            var trans = new SimpleTransaction(CurrentUser);
            trans.IdApplication = IdApplication;
            var pfilter = new Permission();
            pfilter.Id = id.Value;
            var p = new PermissionProvider(trans).GetItem(pfilter);

            if (lst.Contains(p))
            {
              throw new Exception("El elemento ya existe en la lista.");
            }

            lst.Add(p);

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


    private void grdDetail_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
      var lst = grdDetail.DataSource as PermissionCollection;
      if (lst != null)
      {
        lst.RemoveAt(e.Item.DataSetIndex);
        grdDetail.DataSource = lst;
        grdDetail.DataBind();
      }
    }


    private void GroupsPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<Group> e)
    {
      var grp = e.Element;

      if (txtName.Text != string.Empty)
      {
        grp.Name = txtName.Text;
      }

      if (txtDescription.Text != string.Empty)
      {
        grp.Description = txtDescription.Text;
      }

      grp.IdApplication = Convert.ToInt32(cboApplication.SelectedItem.Value);
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
      this.lnkAttrAdd.Click += new EventHandler(this.lnkAttrAdd_Click);
      this.grdDetail.DeleteCommand += new DataGridCommandEventHandler(this.grdDetail_DeleteCommand);
      this.MasterItemBeforeSearch +=
        new EventHandler<UaMasterItemEventArgs<Group>>(this.GroupsPage_MasterItemBeforeSearch);
      this.MasterItemDeleted += new EventHandler<UaMasterItemEventArgs<Group>>(this.GroupsPage_MasterItemDeleted);
      this.MasterItemSaved += new EventHandler<UaMasterItemEventArgs<Group>>(this.GroupsPage_MasterItemSaved);
      this.MasterItemLoaded += new EventHandler<UaMasterItemEventArgs<Group>>(this.GroupsPage_MasterItemLoaded);
      this.MasterItemBeforeSave += new EventHandler<UaMasterItemEventArgs<Group>>(this.GroupsPage_MasterItemBeforeSave);
      this.ReturnedFromDialog += new EventHandler<UaDialogReturnEventArgs>(this.GroupsPage_ReturnedFromDialog);
      this.Load += new EventHandler(this.Page_Load);
    }

    #endregion
  }
}