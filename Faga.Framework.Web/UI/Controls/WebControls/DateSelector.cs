using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:DateSelector runat=server></{0}:DateSelector>")]
  public class DateSelector : ScriptableControl
  {
    private readonly TextBox _txtDate;
    private DateTime? _selectedValue;

    public DateSelector()
    {
      _txtDate = new TextBox();
    }

    #region Overrides

    protected override void OnInit(EventArgs e)
    {
      if (Page.Request[UniqueID + "_txtDate"] != null)
      {
        try
        {
          SelectedValue = DateTime.Parse(Page.Request[UniqueID + "_txtDate"],
            new CultureInfo("es-ar"));
        }
        catch
        {
          SelectedValue = null;
        }
      }

      if ((SelectedValue == null)
          && !AllowBlank)
      {
        _selectedValue = DateTime.Now;
      }

      base.OnInit(e);
    }


    protected override void OnLoad(EventArgs e)
    {
      IncludeScript("/java/ui/controls/jscalendar/calendar.pjs");
      IncludeScript("/java/ui/controls/jscalendar/calendar-lang.pjs");
      IncludeScript("/java/ui/controls/jscalendar/calendar-setup.pjs");
      IncludeScript("/java/ui/controls/DateSelector.pjs");

      AddOnLoadScriptLines("page.addControl( new DateSelector('"
                           + UniqueID + "', " + AllowBlank.ToString().ToLower() + ") );");

      AddOnAfterLoadScriptLines(@"
				Calendar.setup( {
						inputField  : '" + UniqueID + "_txtDate" +
                                @"',
						ifFormat    : '%d/%m/%Y',
						button      : '" + UniqueID +
                                "_lnkCalendar" +
                                @"',
						electric		: false,
						step				: 1
					} );");

      base.OnLoad(e);
    }


    protected override void LoadViewState(object savedState)
    {
      if (savedState != null)
      {
        var myState = (object[]) savedState;

        if (myState[0] != null)
        {
          base.LoadViewState(myState[0]);
        }
        if (myState[1] != null)
        {
          AllowBlank = (bool) myState[1];
        }
      }
    }


    protected override object SaveViewState()
    {
      var baseState = base.SaveViewState();

      var allStates = new object[2];

      allStates[0] = baseState;
      allStates[1] = AllowBlank;

      return allStates;
    }


    protected override void CreateChildControls()
    {
      if (!ChildControlsCreated)
      {
        base.CreateChildControls();

        var tbl = new HtmlTable();
        tbl.Border = 0;
        tbl.CellPadding = 0;
        tbl.CellSpacing = 0;

        var tr = new HtmlTableRow();
        HtmlTableCell td;

        td = new HtmlTableCell();
        td.Attributes["class"] = "nopadding";

        _txtDate.ID = UniqueID + "_txtDate";
        _txtDate.Style["width"] = "72px";
        _txtDate.Style["height"] = "20px";
        _txtDate.Style["text-align"] = "center";

        _txtDate.ReadOnly = true;
        _txtDate.Style["cursor"] = "default";

        _txtDate.CssClass = "box";
        _txtDate.Attributes["unselectable"] = "on";
        td.Controls.Add(_txtDate);
        tr.Cells.Add(td);

        td = new HtmlTableCell();
        td.Attributes["class"] = "nopadding";
        var lnkCalendar = new HtmlAnchor();
        lnkCalendar.ID = UniqueID + "_lnkCalendar";
        lnkCalendar.Attributes["class"] = "button";
        lnkCalendar.Attributes["unselectable"] = "on";
        lnkCalendar.Style["width"] = "18px";
        lnkCalendar.Style["height"] = "20px";
        lnkCalendar.Style["text-align"] = "center";
        lnkCalendar.InnerHtml = string.Format("<img src='{0}' border=0>",
          WebApplicationConfiguration.GetRelativePath(
            "/images/icons/icon_small_calendar.gif"));
        lnkCalendar.HRef = "#";
        td.Controls.Add(lnkCalendar);
        tr.Cells.Add(td);


        if (AllowBlank)
        {
          td = new HtmlTableCell();
          td.Attributes["class"] = "nopadding";
          var lnkBlank = new HtmlAnchor();
          lnkBlank.ID = UniqueID + "_lnkBlank";
          lnkBlank.Attributes["class"] = "button";
          lnkBlank.Attributes["unselectable"] = "on";
          lnkBlank.Style["width"] = "18px";
          lnkBlank.Style["height"] = "20px";
          lnkBlank.InnerHtml = string.Format("<img src='{0}' border=0>",
            WebApplicationConfiguration.GetRelativePath(
              "/images/icons/icon_small_new.gif"));
          lnkBlank.HRef = "#";
          td.Controls.Add(lnkBlank);
          tr.Cells.Add(td);
        }

        tbl.Rows.Add(tr);

        Controls.Add(tbl);

        var css = new HtmlGenericControl("LINK");
        css.Attributes.Add("rel", "stylesheet");
        css.Attributes.Add("type", "text/css");
        css.Attributes.Add("href", WebApplicationConfiguration.GetRelativePath(
          "/java/ui/controls/jscalendar/theme.css"));

        Controls.Add(css);
      }
    }

    #endregion

    #region Public Fields

    public DateTime? SelectedValue
    {
      get { return _selectedValue; }
      set
      {
        EnsureChildControls();
        _selectedValue = value;

        if (_selectedValue.HasValue)
        {
          _txtDate.Text = _selectedValue.Value.ToString("dd/MM/yyyy");
        }
        else
        {
          _txtDate.Text = string.Empty;
        }
      }
    }


    public bool AllowBlank { get; set; }

    #endregion
  }
}