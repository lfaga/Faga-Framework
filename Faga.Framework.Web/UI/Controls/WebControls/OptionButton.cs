using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:OptionButton runat=server></{0}:OptionButton>")]
  public class OptionButton : WebControl
  {
    private string _groupname;
    private string _text;
    private string _value;
    protected HtmlGenericControl Lbl;
    protected HtmlInputRadioButton Rad;


    public OptionButton()
    {
      Rad = new HtmlInputRadioButton();
      Lbl = new HtmlGenericControl("LABEL");

      Controls.Add(Rad);
      Controls.Add(Lbl);
    }

    #region Private Methods

    private void SetupScreen()
    {
      Rad.ID = Groupname + "_" + Value;
      Rad.Name = Groupname;
      Rad.Value = Value;
      Rad.Checked = Checked;
      if (!Enabled)
      {
        Rad.Attributes["disabled"] = "disabled";
      }

      Lbl.InnerHtml = Text;
      Lbl.Attributes["For"] = Rad.ID;
      Lbl.Style["font-size"] = "11px";
      Lbl.Style["padding-left"] = "2px";
      Lbl.Style["height"] = "15px";

      if (Hide)
      {
        Rad.Style["visibility"] = "hidden";
        Rad.Style["display"] = "none";
        Lbl.Style["visibility"] = "hidden";
        Lbl.Style["display"] = "none";
      }
    }

    #endregion

    #region Overrides

    protected override void OnLoad(EventArgs e)
    {
      if (Page.Request[Groupname] != null)
      {
        Checked = Page.Request[Groupname] == Value;
      }
      base.OnLoad(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
      SetupScreen();
      base.OnPreRender(e);
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      Rad.RenderControl(htm);
      Lbl.RenderControl(htm);

      output.Write(str.ToString());
    }

    #endregion

    #region Public Properties

    public string Groupname
    {
      get
      {
        if (_groupname == null)
        {
          if (ViewState[ClientID + "_groupname"] != null)
          {
            _groupname = ViewState[ClientID + "_groupname"].ToString();
          }
        }
        return _groupname;
      }
      set
      {
        _groupname = value;
        ViewState[ClientID + "_groupname"] = _groupname;
      }
    }


    public string Value
    {
      get
      {
        if (_value == null)
        {
          if (ViewState[ClientID + "_value"] != null)
          {
            _value = ViewState[ClientID + "_value"].ToString();
          }
        }
        return _value;
      }
      set
      {
        _value = value;
        ViewState[ClientID + "_value"] = _value;
      }
    }


    public string Text
    {
      get
      {
        if (_text == null)
        {
          if (ViewState[ClientID + "_text"] != null)
          {
            _text = ViewState[ClientID + "_text"].ToString();
          }
        }
        return _text;
      }
      set
      {
        _text = value;
        ViewState[ClientID + "_text"] = _text;
      }
    }


    public bool Checked
    {
      get
      {
        if (ViewState[ClientID + "_checked"] != null)
        {
          return ViewState[ClientID + "_checked"].ToString() == bool.TrueString;
        }
        return false;
      }
      set { ViewState[ClientID + "_checked"] = value.ToString(); }
    }


    public string IdForScripting
    {
      get { return Groupname + "_" + Value; }
    }


    public bool Hide
    {
      get
      {
        if (ViewState[ClientID + "_hide"] != null)
        {
          return Convert.ToBoolean(ViewState[ClientID + "_hide"]);
        }

        return false;
      }
      set { ViewState[ClientID + "_hide"] = value.ToString(); }
    }

    #endregion
  }
}