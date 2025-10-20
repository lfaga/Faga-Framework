using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Faga.Framework.Configuration;
using Faga.Framework.Data.Generics;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Web.Configuration;
using Faga.Framework.Web.UI.Controls.WebControls;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Web.UI.Templates
{
  public enum DataAbmTemplateMode
  {
    ModeNone,
    ModeEdit,
    ModeNew
  }


  /// <summary>
  ///   Summary description for DataABMTemplate.
  /// </summary>
  public class DataAbmTemplate<TEntity, TEntityCollection>
    : PageTemplate
    where TEntityCollection : Collection<TEntity>
  {
    private LinkButton _buttonCancel;
    private LinkButton _buttonCopy;
    private LinkButton _buttonDelete;
    private LinkButton _buttonNew;
    private LinkButton _buttonSave;
    private LinkButton _buttonSearch;

    private StringCollection _detailControls;
    private int? _idApplication;
    private Guid _instanceGuid;
    private BaseBLMasterData<TEntity, TEntityCollection> _masterData;
    private CustomDataGrid _masterList;
    private StringCollection _validationScripts;

    public DataAbmTemplate()
    {
      _idApplication = int.MinValue;
      _instanceGuid = Guid.Empty;
    }

    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemBeforeSave;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemLoaded;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemCopied;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemSaved;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemBeforeDelete;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemDeleted;
    public event EventHandler<UaMasterItemEventArgs<TEntity>> MasterItemBeforeSearch;
    public event EventHandler<AbmModeChangeEventArgs> AbmModeChanged;

    #region Overrides

    protected override void OnLoad(EventArgs e)
    {
      IncludeScript("/java/ui/controls/RequiredBox.pjs");
      IncludeScript("/java/ui/controls/RequiredCombo.pjs");
      IncludeScript("/java/ui/controls/LinkButton.pjs");

      try
      {
        if (IsPostBack)
        {
          SetupEvents();
        }
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }

      base.OnLoad(e);

      AddSearchValidationScripts();
    }


    protected override void OnPreRender(EventArgs e)
    {
      ViewState["_detailControls"] = DetailControls;
      ViewState["_validationScripts"] = ValidationScripts;
      ViewState["_instanceGuid"] = InstanceGuid;

      if (Mode != DataAbmTemplateMode.ModeNone)
      {
        foreach (var s in ValidationScripts)
        {
          AddOnLoadScriptLines(s);
        }
      }

      base.OnPreRender(e);
    }


    protected override void OnUnload(EventArgs e)
    {
      if (_masterData != null)
      {
        _masterData.Dispose();
        _masterData = null;
      }
      base.OnUnload(e);
    }

    #endregion

    #region Event Handlers

    private void ButtonNew_Click(object sender, EventArgs e)
    {
      try
      {
        Mode = DataAbmTemplateMode.ModeNew;
        ClearDetails();
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }
    }


    private void ButtonCopy_Click(object sender, EventArgs e)
    {
      if (MasterList.SelectedIndex >= 0)
      {
        var col = MasterList.DataSource as TEntityCollection;
        if (col != null)
        {
          var obj = col[MasterList.SelectedItem.DataSetIndex];
          var newobj = BllObject.New();

          Mode = DataAbmTemplateMode.ModeEdit;
          ClearDetails();

          foreach (var property in obj.GetType().GetProperties())
          {
            property.SetValue(newobj, property.GetValue(obj, null), null);
          }

          using (var trans = new SimpleTransaction(CurrentUser, IdApplication))
          {
            if (MasterItemCopied != null)
            {
              MasterItemCopied(null, new UaMasterItemEventArgs<TEntity>(newobj, trans));
            }
          }
        }
      }
    }


    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      try
      {
        if (Mode != DataAbmTemplateMode.ModeNone)
        {
          Mode = DataAbmTemplateMode.ModeNone;
          ClearDetails();
          MasterListRebind();
        }
        else
        {
          Response.Redirect(WebApplicationConfiguration.WelcomePage);
        }
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }
    }


    private void ButtonDelete_Click(object sender, EventArgs e)
    {
      if (MasterList.SelectedIndex >= 0)
      {
        var col = MasterList.DataSource as TEntityCollection;
        if (col != null)
        {
          var obj = col[MasterList.SelectedIndex];

          var trans = new UndoableTransaction(
            CurrentUser, IdApplication);
          BllObject.ChangeTransaction(trans);

          try
          {
            var uea
              = new UaMasterItemEventArgs<TEntity>(obj, trans);
            if (MasterItemBeforeDelete != null)
            {
              MasterItemBeforeDelete(this, uea);
            }

            if (!uea.MustSkip)
            {
              BllObject.Remove(obj);
            }

            if (MasterItemDeleted != null)
            {
              MasterItemDeleted(this, new UaMasterItemEventArgs<TEntity>(obj, trans));
            }

            trans.Commit();

            Mode = DataAbmTemplateMode.ModeNone;
            ClearDetails();
            MasterListRebind();
          }
          catch (Exception ex)
          {
            trans.Rollback();
            ShowError("No se puede borrar. Existen elementos asociados a este registro.");
            ExceptionHandling(ex);
          }
        }
      }
    }


    private void ButtonSave_Click(object sender, EventArgs e)
    {
      TEntityCollection elements = null;

      if (MasterList.DataSource != null)
      {
        elements = MasterList.DataSource as TEntityCollection;
      }

      TEntity element;

      if ((elements != null) && (MasterList.SelectedIndex >= 0))
      {
        element = elements[MasterList.SelectedItem.DataSetIndex];
      }
      else
      {
        element = BllObject.New();
      }

      var trans = new UndoableTransaction(
        CurrentUser, IdApplication);
      BllObject.ChangeTransaction(trans);

      var uea
        = new UaMasterItemEventArgs<TEntity>(element, trans);
      if (MasterItemBeforeSave != null)
      {
        MasterItemBeforeSave(this, uea);
      }

      try
      {
        if (!uea.MustSkip)
        {
          element = BllObject.SetItem(element);
        }

        if (element == null)
        {
          throw new Exception("Grabación fallida.");
        }

        if (MasterItemSaved != null)
        {
          MasterItemSaved(this, new UaMasterItemEventArgs<TEntity>(element, trans));
        }

        trans.Commit();

        try
        {
          MasterListRebind();
          MasterList.SelectItem(element);

          ShowSuccess("Se grabó satisfactoriamente.");

          Mode = DataAbmTemplateMode.ModeEdit;
          ClearDetails();

          if (MasterItemLoaded != null)
          {
            MasterItemLoaded(null, new UaMasterItemEventArgs<TEntity>(
              element,
              new SimpleTransaction(CurrentUser, IdApplication)));
          }
        }
        catch (Exception ex)
        {
          ShowError(ex.Message);
        }
      }
      catch (Exception ex)
      {
        trans.Rollback();
        ShowError(ex.Message);
      }
    }


    private void ButtonSearch_Click(object sender, EventArgs e)
    {
      try
      {
        MasterListRebind();
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }
    }


    private void MasterList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
      var trans = new SimpleTransaction(
        CurrentUser, IdApplication);

      try
      {
        if (e.CommandName == "Select")
        {
          var col = MasterList.DataSource as TEntityCollection;
          if (col != null)
          {
            var obj = col[e.Item.DataSetIndex];

            Mode = DataAbmTemplateMode.ModeEdit;
            ClearDetails();

            if (MasterItemLoaded != null)
            {
              MasterItemLoaded(null, new UaMasterItemEventArgs<TEntity>(obj, trans));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }
    }

    #endregion

    #region Private Methods

    private void AddEditValidationScripts()
    {
      if (ButtonSave != null)
      {
        AddValidationScript("page.addControl( new LinkButton('"
                            + ButtonSave.ClientID + "') );");

        AddValidationScript(@"page.Controls.Items['"
                            + ButtonSave.ClientID +
                            @"'].onClick = function(source, eventargs)
				{
					if ( source.page.isValid() )
						return (
							source.page.ShowConfirm('Se almacenará el elemento.<br>¿Desea continuar?')
						);
					else
						page.StatusBar.flash();
				}");
      }

      if (ButtonDelete != null)
      {
        AddValidationScript("page.addControl( new LinkButton('"
                            + ButtonDelete.ClientID + "') );");

        AddValidationScript(@"
				page.Controls.Items['"
                            + ButtonDelete.ClientID +
                            @"'].onClick = function(source, eventargs)
				{
					return (
						source.page.ShowConfirm('Se eliminará el elemento.<br>¿Desea continuar?')
					);
				}");
      }
    }


    private void AddSearchValidationScripts()
    {
      if (ButtonSearch != null)
      {
        AddOnLoadScriptLines("page.addControl( new LinkButton('"
                             + ButtonSearch.ClientID + "') );");

        AddOnLoadScriptLines(@"
				page.Controls.Items['"
                             + ButtonSearch.ClientID +
                             @"'].onClick = function(source, eventargs)
				{
					var ret = source.page.isValid();
					if (! ret) page.StatusBar.flash();
					return (ret);
				}");
      }
    }


    private void SetupEvents()
    {
      ButtonNew.Click += ButtonNew_Click;
      if (ButtonCopy != null)
      {
        ButtonCopy.Click += ButtonCopy_Click;
      }
      ButtonCancel.Click += ButtonCancel_Click;
      ButtonDelete.Click += ButtonDelete_Click;
      ButtonSave.Click += ButtonSave_Click;
      ButtonSearch.Click += ButtonSearch_Click;
      MasterList.ItemCommand += MasterList_ItemCommand;
    }

    #endregion

    #region Protected Methods

    protected void ClearDetails()
    {
      MasterList.SelectedIndex = -1;

      foreach (var ctlname in DetailControls)
      {
        object ctl = FindControl(ctlname);

        if (ctl is TextBox)
        {
          ((TextBox) ctl).Text = string.Empty;
        }
        else if (ctl is ListControl)
        {
          ((ListControl) ctl).SelectedIndex = -1;
        }
        else if (ctl is BaseDataList)
        {
          var bdl = ctl as BaseDataList;
          bdl.DataSource = null;
          bdl.DataBind();
        }
        else if (ctl is CheckBox)
        {
          ((CheckBox) ctl).Checked = false;
        }
        else if (ctl is Label)
        {
          ((Label) ctl).Text = string.Empty;
        }
        else if (ctl is LinkButton)
        {
          ((LinkButton) ctl).Enabled = Mode != DataAbmTemplateMode.ModeNone;
        }
        else
        {
          throw new Exception(string.Format("ABMTemplate.ClearDetails, Agregar {0}.",
            ctl.GetType()));
        }
      }
    }


    protected void MasterListRebind()
    {
      var trans = new SimpleTransaction(
        CurrentUser, IdApplication);
      BllObject.ChangeTransaction(trans);

      var filter = BllObject.New();

      var uea
        = new UaMasterItemEventArgs<TEntity>(filter, trans);
      if (MasterItemBeforeSearch != null)
      {
        MasterItemBeforeSearch(null, uea);
      }

      ICollection col = null;

      if (!uea.MustSkip)
      {
        col = BllObject.List(filter);
      }

      if ((col != null) && (col.Count > 0))
      {
        MasterList.DataSource = col;
      }
      else
      {
        MasterList.DataSource = null;
      }

      MasterList.DataBind();
    }


    protected void InitializeAbm(
      BaseBLMasterData<TEntity, TEntityCollection> _object,
      CustomDataGrid _masterList, LinkButton _buttonNew, LinkButton _buttonCopy,
      LinkButton _buttonDelete, LinkButton _buttonSave, LinkButton _buttonCancel,
      LinkButton _buttonSearch, params Control[] _detailControls
      )
    {
      try
      {
        BllObject = _object;
        MasterList = _masterList;

        ButtonSearch = _buttonSearch;
        ButtonNew = _buttonNew;
        ButtonCopy = _buttonCopy;
        ButtonDelete = _buttonDelete;
        ButtonSave = _buttonSave;
        ButtonCancel = _buttonCancel;

        foreach (var control in _detailControls)
        {
          DetailControls.Add(control.ID);
        }

        SetupEvents();
        Mode = DataAbmTemplateMode.ModeNone;
        ClearDetails();
      }
      catch (Exception ex)
      {
        ExceptionHandling(ex);
      }
    }


    protected void AddValidationScript(string scp)
    {
      if (!ValidationScripts.Contains(scp))
      {
        ValidationScripts.Add(scp);
      }
    }


    protected void AddValidationScript(string validatorClassName,
      string controlName,
      params string[] parameters)
    {
      var sb = new StringBuilder();

      sb.AppendFormat("'{0}', ", controlName);

      foreach (var parameter in parameters)
      {
        sb.AppendFormat("'{0}', ", parameter);
      }

      var p = sb.ToString();
      if (p.Length > 3)
      {
        p = p.Substring(0, p.Length - 2);
      }

      AddValidationScript(
        string.Format(CultureInfo.InvariantCulture, "page.addControl(new {0}({1}) );",
          validatorClassName, p));
    }

    #endregion

    #region Private Fields

    private CustomDataGrid MasterList
    {
      get
      {
        if (_masterList == null)
        {
          if (ViewState["masterList_name"] != null)
          {
            _masterList = (CustomDataGrid) FindControl(
              ViewState["masterList_name"].ToString()
              );
          }
        }
        return _masterList;
      }
      set
      {
        _masterList = value;
        ViewState["masterList_name"] = _masterList.ID;
      }
    }

    private LinkButton ButtonSearch
    {
      get
      {
        if (_buttonSearch == null)
        {
          if (ViewState["buttonSearch_name"] != null)
          {
            _buttonSearch = (LinkButton) FindControl(
              ViewState["buttonSearch_name"].ToString()
              );
          }
        }
        return _buttonSearch;
      }
      set
      {
        _buttonSearch = value;
        ViewState["buttonSearch_name"] = _buttonSearch.ID;
      }
    }

    private LinkButton ButtonNew
    {
      get
      {
        if (_buttonNew == null)
        {
          if (ViewState["buttonNew_name"] != null)
          {
            _buttonNew = (LinkButton) FindControl(
              ViewState["buttonNew_name"].ToString()
              );
          }
        }
        return _buttonNew;
      }
      set
      {
        _buttonNew = value;
        ViewState["buttonNew_name"] = _buttonNew.ID;
      }
    }

    private LinkButton ButtonCopy
    {
      get
      {
        if (_buttonCopy == null)
        {
          if (ViewState["buttonCopy_name"] != null)
          {
            _buttonCopy = (LinkButton) FindControl(
              ViewState["buttonCopy_name"].ToString()
              );
          }
        }
        return _buttonCopy;
      }
      set
      {
        _buttonCopy = value;
        if (_buttonCopy != null)
        {
          ViewState["buttonCopy_name"] = _buttonCopy.ID;
        }
      }
    }

    private LinkButton ButtonDelete
    {
      get
      {
        if (_buttonDelete == null)
        {
          if (ViewState["buttonDelete_name"] != null)
          {
            _buttonDelete = (LinkButton) FindControl(
              ViewState["buttonDelete_name"].ToString()
              );
          }
        }
        return _buttonDelete;
      }
      set
      {
        _buttonDelete = value;
        ViewState["buttonDelete_name"] = _buttonDelete.ID;
      }
    }

    private LinkButton ButtonSave
    {
      get
      {
        if (_buttonSave == null)
        {
          if (ViewState["buttonSave_name"] != null)
          {
            _buttonSave = (LinkButton) FindControl(
              ViewState["buttonSave_name"].ToString()
              );
          }
        }
        return _buttonSave;
      }
      set
      {
        _buttonSave = value;
        ViewState["buttonSave_name"] = _buttonSave.ID;
      }
    }

    private LinkButton ButtonCancel
    {
      get
      {
        if (_buttonCancel == null)
        {
          if (ViewState["buttonCancel_name"] != null)
          {
            _buttonCancel = (LinkButton) FindControl(
              ViewState["buttonCancel_name"].ToString()
              );
          }
        }
        return _buttonCancel;
      }
      set
      {
        _buttonCancel = value;
        ViewState["buttonCancel_name"] = _buttonCancel.ID;
      }
    }


    private StringCollection DetailControls
    {
      get
      {
        if (_detailControls == null)
        {
          if (ViewState["_detailControls"] != null)
          {
            _detailControls = (StringCollection)
              ViewState["_detailControls"];
          }
          else
          {
            _detailControls = new StringCollection();
          }
        }
        return _detailControls;
      }
    }


    private BaseBLMasterData<TEntity, TEntityCollection> BllObject
    {
      get
      {
        if (_masterData == null)
        {
          if (Session[string.Format("{0}_bllObject", _instanceGuid)]
              != null)
          {
            _masterData = (BaseBLMasterData<TEntity, TEntityCollection>)
              Session[string.Format("{0}_bllObject", _instanceGuid)];
          }
        }
        return _masterData;
      }
      set
      {
        _masterData = value;
        Session[string.Format("{0}_bllObject", _instanceGuid)]
          = _masterData;
      }
    }


    private StringCollection ValidationScripts
    {
      get
      {
        if (_validationScripts == null)
        {
          if (ViewState["_validationScripts"] != null)
          {
            _validationScripts = (StringCollection)
              ViewState["_validationScripts"];
          }
          else
          {
            _validationScripts = new StringCollection();
          }
        }
        return _validationScripts;
      }
    }

    #endregion

    #region Protected Fields

    protected Guid InstanceGuid
    {
      get
      {
        if (_instanceGuid == Guid.Empty)
        {
          if (ViewState["_instanceGuid"] != null)
          {
            _instanceGuid = (Guid) ViewState["_instanceGuid"];
          }
          else
          {
            _instanceGuid = Guid.NewGuid();
          }
        }
        return _instanceGuid;
      }
    }


    protected DataAbmTemplateMode Mode
    {
      get { return (DataAbmTemplateMode) ViewState["_Mode"]; }
      set
      {
        ViewState["_Mode"] = value;
        if (value != DataAbmTemplateMode.ModeNone)
        {
          AddEditValidationScripts();

          ButtonDelete.Enabled = value == DataAbmTemplateMode.ModeEdit;
          if (ButtonCopy != null)
          {
            ButtonCopy.Enabled = value == DataAbmTemplateMode.ModeEdit;
          }

          ButtonSave.Enabled = true;
          ButtonSearch.Enabled = false;
          ButtonNew.Enabled = false;
        }
        else
        {
          ButtonDelete.Enabled = false;
          ButtonSave.Enabled = false;
          ButtonSearch.Enabled = true;
          ButtonNew.Enabled = true;
          if (ButtonCopy != null)
          {
            ButtonCopy.Enabled = false;
          }
        }

        if (AbmModeChanged != null)
        {
          AbmModeChanged(this, new AbmModeChangeEventArgs(value));
        }
      }
    }


    protected TEntity SelectedItem
    {
      get
      {
        if (MasterList.SelectedIndex >= 0)
        {
          var col = MasterList.DataSource as TEntityCollection;
          if (col != null)
          {
            return col[MasterList.SelectedItem.DataSetIndex];
          }
          return default(TEntity);
        }
        return default(TEntity);
      }
    }


    protected int IdApplication
    {
      get
      {
        try
        {
          if (_idApplication.HasValue)
          {
            _idApplication = Convert.ToInt32(ViewState["idApplication"]);
          }
        }
        catch (Exception)
        {
          _idApplication = ApplicationConfiguration.SecurityProvider.ApplicationId;
        }
        return _idApplication.Value;
      }
      set
      {
        _idApplication = value;
        ViewState["idApplication"] = _idApplication;
      }
    }

    #endregion
  }
}