using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Faga.Framework.Web.UI.Controls.WebControls
{

  #region Item Template

  internal class CustomComboItemTemplate : ITemplate
  {
    private readonly string _ctlName;
    private readonly string _textField;
    private readonly string _valueField;


    public CustomComboItemTemplate(string ctlName, string textField, string valueField)
    {
      _textField = textField;
      _valueField = valueField;
      _ctlName = ctlName;
    }

    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
      var lnk = new HtmlAnchor();

      lnk.DataBinding += lnk_DataBinding;

      container.Controls.Add(lnk);
    }

    #endregion

    private void lnk_DataBinding(object sender, EventArgs e)
    {
      var lnk = (HtmlAnchor) sender;
      var container = (DataListItem) lnk.NamingContainer;
      lnk.ID = "item";
      lnk.InnerHtml = DataBinder.Eval(container.DataItem, _textField).ToString();
      lnk.HRef = "#";
      lnk.Attributes["onmousedown"] = _ctlName
                                      + "_text.controllerObject.selectItem('"
                                      + DataBinder.Eval(container.DataItem, _valueField)
                                      + "'); return (false);";

      lnk.Style["width"] = "100%";
      lnk.Style["text-decoration"] = "none";
    }
  }

  #endregion

  [ToolboxData("<{0}:CustomCombo runat=server></{0}:CustomCombo>")]
  public class CustomCombo<TKey, TElement>
    : ScriptableControl, INamingContainer
  {
    private string _cssClass;
    private Unit _popupWidth;

    protected HtmlGenericControl DivPopup;
    protected DataList DlItems;
    protected HtmlInputHidden HidValue;
    protected TextBox TxtText;


    public CustomCombo()
    {
      _popupWidth = Unit.Empty;

      TxtText = new TextBox();
      HidValue = new HtmlInputHidden();
      DivPopup = new HtmlGenericControl("DIV");
      DlItems = new DataList();

      _cssClass = "box";
      CssClassDisabled = "box_disabled";
    }

    #region Overrides

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
          DataSource = (Dictionary<TKey, TElement>) myState[1];
        }
        if (myState[2] != null)
        {
          TextField = (string) myState[2];
        }
        if (myState[3] != null)
        {
          ValueField = (string) myState[3];
        }
        if (myState[4] != null)
        {
          Description = (string) myState[4];
        }
        if (myState[5] != null)
        {
          _cssClass = (string) myState[5];
        }
        if (myState[6] != null)
        {
          CssClassDisabled = (string) myState[6];
        }
      }
    }


    protected override object SaveViewState()
    {
      var baseState = base.SaveViewState();

      var allStates = new object[7];

      allStates[0] = baseState;
      allStates[1] = DataSource;
      allStates[2] = TextField;
      allStates[3] = ValueField;
      allStates[4] = Description;
      allStates[5] = _cssClass;
      allStates[6] = CssClassDisabled;

      return allStates;
    }


    protected override void OnLoad(EventArgs e)
    {
      IncludeScript("/java/ui/controls/CustomCombo.pjs");

      AddOnLoadScriptLines(string.Format(
        CultureInfo.InvariantCulture,
        "page.addControl( new CustomCombo('{0}', '{1}') );",
        ClientID, Description));

      AddOnAfterLoadScriptLines(string.Format(
        CultureInfo.InvariantCulture,
        "page.Controls.Items['{0}'].Texts = new Array({1});",
        ClientID, GetTexts()));

      AddOnAfterLoadScriptLines(string.Format(
        CultureInfo.InvariantCulture,
        "page.Controls.Items['{0}'].Values = new Array({1});",
        ClientID, GetKeys()));

      if (!Enabled)
      {
        AddOnAfterLoadScriptLines(string.Format(
          CultureInfo.InvariantCulture,
          "page.Controls.Items['{0}'].Enabled = false;",
          ClientID));
      }

      if (Page.IsPostBack)
      {
        DataBind();
      }

      base.OnLoad(e);
    }


    protected override void CreateChildControls()
    {
      if (!ChildControlsCreated)
      {
        base.CreateChildControls();

        HidValue.ID = "value";
        Controls.Add(HidValue);

        if (Width.IsEmpty)
        {
          Width = Unit.Pixel(100);
        }

        if (_popupWidth.IsEmpty)
        {
          _popupWidth = Width;
        }

        TxtText.ID = "text";
        TxtText.Width = Width;

        if (Enabled)
        {
          TxtText.CssClass = _cssClass;
        }
        else
        {
          TxtText.CssClass = CssClassDisabled;
          TxtText.ReadOnly = true;
          TxtText.Attributes["unselectable"] = "on";
        }

        Controls.Add(TxtText);

        DivPopup.ID = "divPopup";
        DivPopup.Attributes["class"] = "button";
        DivPopup.Style["padding"] = "0px";
        DivPopup.Style["width"] = _popupWidth.ToString();
        DivPopup.Style["height"] = "200px";
        DivPopup.Style["position"] = "absolute";
        DivPopup.Style["overflow"] = "scroll";
        DivPopup.Style["overflow-x"] = "auto";
        DivPopup.Style["overflow-y"] = "scroll";
        DivPopup.Style["visibility"] = "hidden";
        DivPopup.Style["display"] = "none";

        DlItems.ID = "dlItems";
        DlItems.CellPadding = 0;
        DlItems.CellSpacing = 0;
        DlItems.BorderWidth = Unit.Pixel(0);
        DlItems.Width = Unit.Percentage(100);
        DlItems.Style["border"] = "0 none !important";
        DlItems.CssClass = "grid";
        DlItems.ItemStyle.CssClass = "colimpar";
        DlItems.AlternatingItemStyle.CssClass = "colimpar2";

        DivPopup.Controls.Add(DlItems);

        Controls.Add(DivPopup);

        Width = Unit.Empty;

        ChildControlsCreated = true;
      }
    }


    public override void DataBind()
    {
      if (DataSource != null)
      {
        EnsureChildControls();

        if (DataSource.Count < 10)
        {
          DivPopup.Style["height"] = DataSource.Count*20 + 2 + "px";
        }

        DlItems.ItemTemplate =
          new CustomComboItemTemplate(ClientID, TextField, ValueField);

        DlItems.DataSource = DataSource.Values;

        DlItems.DataBind();
      }
    }

    #endregion

    #region Public Methods

    public void FillControl(Dictionary<TKey, TElement> objects,
      string textField, string valueField)
    {
      TextField = textField;
      ValueField = valueField;

      DataSource = objects;
      DataBind();
    }


    public void FillControl(Collection<TElement> objects, string keyfieldname,
      string textField, string valueField)
    {
      var dic = new Dictionary<TKey, TElement>();
      var pi = typeof (TElement).GetProperty(keyfieldname);

      foreach (var item in objects)
      {
        dic.Add((TKey) pi.GetValue(item, null), item);
      }

      FillControl(dic, textField, valueField);
    }

    #endregion

    #region Public Properties

    public Dictionary<TKey, TElement> DataSource { get; set; }


    public string TextField { get; set; }


    public string ValueField { get; set; }


    public string Description { get; set; }


    public TElement SelectedItem
    {
      get
      {
        string val = null;

        if (!string.IsNullOrEmpty(HidValue.Value))
        {
          val = HidValue.Value;
        }
        else if (HttpContext.Current.Request[HidValue.UniqueID] != null)
        {
          val = HttpContext.Current.Request[HidValue.UniqueID];
        }

        if (val != null)
        {
          var key = (TKey) Convert.ChangeType(val, typeof (TKey));
          if ((key != null)
              && DataSource.ContainsKey(key))
          {
            return DataSource[key];
          }
        }
        return default(TElement);
      }
      set
      {
        if (value != null)
        {
          var pt = typeof (TElement).GetProperty(TextField);
          var pv = typeof (TElement).GetProperty(ValueField);

          TxtText.Text = Convert.ToString(pt.GetValue(value, null));
          HidValue.Value = Convert.ToString(pv.GetValue(value, null));
        }
        else
        {
          TxtText.Text = HidValue.Value = string.Empty;
        }
      }
    }

    public override string CssClass
    {
      get { return _cssClass; }
      set { _cssClass = value; }
    }

    public string CssClassDisabled { get; set; }

    #endregion

    #region Private Methods

    private string GetTexts()
    {
      var texts = new StringCollection();

      foreach (var o in DataSource.Values)
      {
        var pi = typeof (TElement).GetProperty(TextField);
        texts.Add((string) pi.GetValue(o, null));
      }
      return GetArray(texts);
    }


    private string GetKeys()
    {
      return GetArray(DataSource.Keys);
    }


    private static string GetArray(ICollection items)
    {
      var sb = new StringBuilder();
      foreach (var o in items)
      {
        var s = o.ToString();
        var ns = s.Replace(@"'", @"\'");
        ns = ns.Replace(@"\", @"\\");

        sb.AppendFormat("'{0}',", ns);
      }

      var ret = sb.ToString();

      if (ret.Length > 0)
      {
        return ret.Substring(0, ret.Length - 1);
      }
      return string.Empty;
    }

    #endregion
  }
}