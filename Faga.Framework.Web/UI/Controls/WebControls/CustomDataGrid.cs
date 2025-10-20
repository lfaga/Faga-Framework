using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Faga.Framework.Collections;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:CustomDataGrid runat=server></{0}:CustomDataGrid>")]
  public class CustomDataGrid : DataGrid
  {
    private object _originalData;
    private string _sort = string.Empty;
    private string _sortdir = string.Empty;
    private string _title = string.Empty;


    public CustomDataGrid()
    {
      PageIndexChanged += SCRDataGrid_PageIndexChanged;
      SortCommand += SCRDataGrid_SortCommand;
      ItemCreated += SCRDataGrid_ItemCreated;
    }

    public void SelectItem(object dataItem)
    {
      foreach (DataGridItem item in Items)
      {
        var obj = ((IList) _originalData)[item.DataSetIndex];

        if (dataItem.Equals(obj))
        {
          SelectedIndex = item.ItemIndex;
          break;
        }
      }
    }


    public void SelectItem(object dataItem, string comparefield)
    {
      foreach (DataGridItem item in Items)
      {
        var obj = ((IList) _originalData)[item.DataSetIndex];

        var pi1 = obj.GetType().GetProperty(comparefield);
        var pi2 = dataItem.GetType().GetProperty(comparefield);

        var val1 = pi1.GetValue(obj, null);
        var val2 = pi2.GetValue(dataItem, null);

        if (val1.Equals(val2))
        {
          SelectedIndex = item.ItemIndex;
          break;
        }
      }
    }

    #region Overrides

    public override void DataBind()
    {
      DataBind(false);
    }


    protected override void DataBind(bool raiseOnDataBinding)
    {
      ReBind();
    }


    protected override void LoadViewState(object savedState)
    {
      base.LoadViewState(savedState);

      _originalData = Context.Session[GetUniqueId("OriginalData")];

      _title = Convert.ToString(ViewState[GetUniqueId("GridTitle")]);
      if (_title == null)
      {
        _title = string.Empty;
      }

      _sort = Convert.ToString(ViewState[GetUniqueId("Sort")]);
      if (_sort == null)
      {
        _sort = string.Empty;
      }

      _sortdir = Convert.ToString(ViewState[GetUniqueId("SortDir")]);
      if (_sortdir == null)
      {
        _sortdir = string.Empty;
      }
    }


    protected override object SaveViewState()
    {
      if (_originalData != null)
      {
        Context.Session[GetUniqueId("OriginalData")] = _originalData;
      }
      else
      {
        Context.Session.Remove(GetUniqueId("OriginalData"));
      }

      ViewState[GetUniqueId("GridTitle")] = _title;
      ViewState[GetUniqueId("Sort")] = _sort;
      ViewState[GetUniqueId("SortDir")] = _sortdir;

      return base.SaveViewState();
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      SetupPreRender();

      var tbl = new HtmlTable();

      if (Title.Length > 0)
      {
        tbl.Width = Width.ToString();
        tbl.Height = Height.ToString();

        Width = Unit.Parse("100%");
        Height = Unit.Parse("100%");
        Style["border"] = "0px none !important";
      }

      base.Render(htm);

      var thissrc = str.ToString();

      str.Close();
      htm.Close();

      if (Title.Length > 0)
      {
        str = new StringWriter();
        htm = new HtmlTextWriter(str);

        tbl.Attributes["class"] = CssClass;

        var tr = new HtmlTableRow();
        var td = new HtmlTableCell();
        td.InnerText = Title;
        td.Attributes["class"] = "caption";
        td.Style["font-weight"] = "bold";
        td.Style["text-align"] = "center";
        tr.Cells.Add(td);
        tbl.Rows.Add(tr);

        tr = new HtmlTableRow();
        td = new HtmlTableCell();
        td.Attributes["class"] = "nopadding";
        td.Controls.Add(new LiteralControl(thissrc));
        tr.Cells.Add(td);
        tbl.Rows.Add(tr);

        tbl.RenderControl(htm);

        output.Write(str.ToString());
      }
      else
      {
        output.Write(thissrc);
      }
    }

    #endregion

    #region Private Functions

    private string GetUniqueId(string suffix)
    {
      return string.Format("{0}_{1}_{2}", HttpContext.Current.Request.FilePath,
        UniqueID, suffix);
    }


    private void ReBind()
    {
      DataView dv = null;

      if (_originalData != null)
      {
        if (_originalData is DataTable)
        {
          var dt = (DataTable) _originalData;
          dv = new DataView(dt);
        }
        else if (_originalData is DataView)
        {
          dv = (DataView) _originalData;
        }
        else if (_originalData is IList)
        {
          dv = new DataView(CollectionConvert.ListToTable((IList) _originalData));
        }
        else if (_originalData is ICollection)
        {
          dv = new DataView(CollectionConvert.CollectionToTable((ICollection) _originalData));
        }

        if (dv != null)
        {
          var s = Sort;

          if (string.Compare(s, string.Empty, StringComparison.Ordinal) != 0)
          {
            if (string.Compare(SortDir, string.Empty, StringComparison.Ordinal) != 0)
            {
              s = string.Concat(s, " ", SortDir);
            }

            dv.Sort = s;
          }

          if (dv.Table != null)
          {
            base.DataSource = dv;
          }
          else
          {
            base.DataSource = null;
          }

          RewindBind();
        }
        else
        {
          base.DataSource = _originalData;
          base.DataBind();
        }
      }
      else
      {
        base.DataSource = null;
        base.DataBind();
      }
    }


    private void RewindBind()
    {
      try
      {
        base.DataBind();
      }
      catch
      {
        if (CurrentPageIndex > 0)
        {
          CurrentPageIndex--;
          RewindBind();
        }
      }
    }


    private void SetupPreRender()
    {
      UseAccessibleHeader = true;
      CssClass = "grid";
      HeaderStyle.CssClass = "header";
      PagerStyle.CssClass = "footer";
      SelectedItemStyle.CssClass = "selected";
    }


    private void SetCellClass(TableCellCollection cells, bool alternate)
    {
      var enu = cells.GetEnumerator();
      var impar = true;
      while (enu.MoveNext())
      {
        var tc = (TableCell) enu.Current;
        if (impar)
        {
          if (!alternate)
          {
            tc.Attributes["class"] = "colimpar";
          }
          else
          {
            tc.Attributes["class"] = "colimpar2";
          }
        }
        else if (!alternate)
        {
          tc.Attributes["class"] = "colpar";
        }
        else
        {
          tc.Attributes["class"] = "colpar2";
        }
        impar = !impar;
      }
    }

    #endregion

    #region Events

    private void SCRDataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
      CurrentPageIndex = e.NewPageIndex;
      ReBind();
    }


    private void SCRDataGrid_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
      if ((string.Compare(Sort, string.Empty, StringComparison.Ordinal) == 0) ||
          (string.Compare(Sort, e.SortExpression, StringComparison.Ordinal) != 0))
      {
        Sort = e.SortExpression;
        SortDir = "ASC";
      }
      else
      {
        if ((string.Compare(SortDir, string.Empty, StringComparison.Ordinal) == 0) ||
            (string.Compare(SortDir, "ASC", StringComparison.Ordinal) != 0))
        {
          SortDir = "ASC";
        }
        else
        {
          SortDir = "DESC";
        }
      }
      ReBind();
    }


    private void SCRDataGrid_ItemCreated(object sender, DataGridItemEventArgs e)
    {
      if (e.Item.ItemType == ListItemType.Pager)
      {
        var gc = new HtmlGenericControl("DIV");
        gc.InnerText = CurrentPageIndex + 1 + "/" + PageCount;
        gc.Style.Add("FLOAT", "RIGHT");
        e.Item.Cells[0].Controls.AddAt(0, gc);
      }
      else if ((SelectedIndex != e.Item.ItemIndex)
               && (e.Item.ItemType == ListItemType.Item))
      {
        SetCellClass(e.Item.Cells, false);
      }
      else if ((SelectedIndex != e.Item.ItemIndex)
               && (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SetCellClass(e.Item.Cells, true);
      }
    }

    #endregion

    #region Properties

    public override object DataSource
    {
      get { return _originalData; }
      set { _originalData = value; }
    }


    [Bindable(false)]
    [Category("Appearance")]
    [DefaultValue("")]
    public string Title
    {
      get { return _title; }
      set { _title = value; }
    }


    protected string Sort
    {
      get { return _sort; }
      set { _sort = value; }
    }


    protected string SortDir
    {
      get { return _sortdir; }
      set { _sortdir = value; }
    }

    #endregion
  }
}