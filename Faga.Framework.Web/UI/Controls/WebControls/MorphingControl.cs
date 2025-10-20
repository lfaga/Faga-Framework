using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:MorphingControl runat=server></{0}:MorphingControl>")]
  public class MorphingControl : ScriptableControl
  {
    private readonly WebControl _control;
    private Type _dataType;
    private object _value;

    public MorphingControl(Type dataType)
    {
      _dataType = dataType;

      if (dataType.Equals(typeof (bool)))
      {
        _control = new CheckBox();
      }
      else if (dataType.Equals(typeof (DateTime)))
      {
        _control = new DateSelector();
      }
      else
      {
        _control = new TextBox();
      }
    }

    public object Value
    {
      get
      {
        if (_dataType.Equals(typeof (bool)))
        {
          _value = ((CheckBox) _control).Checked;
        }
        else if (_dataType.Equals(typeof (DateTime)))
        {
          _value = ((DateSelector) _control).SelectedValue;
        }
        else if (_dataType.Equals(typeof (int)))
        {
          _value = ((TextBox) _control).Text;
        }
        else
        {
          _value = ((TextBox) _control).Text;
        }

        return _value;
      }
      set
      {
        _value = value;

        if (_dataType.Equals(typeof (bool)))
        {
          ((CheckBox) _control).Checked = (bool) _value;
        }
        else if (_dataType.Equals(typeof (DateTime)))
        {
          ((DateSelector) _control).SelectedValue = (DateTime) _value;
        }
        else if (_dataType.Equals(typeof (int)))
        {
          ((TextBox) _control).Text = _value.ToString();
        }
        else
        {
          ((TextBox) _control).Text = (string) _value;
        }
      }
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
          _dataType = (Type) myState[1];
        }
        if (myState[2] != null)
        {
          _value = Convert.ChangeType(myState[2], _dataType);
        }
      }
    }


    protected override object SaveViewState()
    {
      var baseState = base.SaveViewState();

      var allStates = new object[3];

      allStates[0] = baseState;
      allStates[1] = _dataType;
      allStates[2] = _value;

      return allStates;
    }


    protected override void CreateChildControls()
    {
      if (!ChildControlsCreated)
      {
        base.CreateChildControls();

        _control.Attributes["class"] = "box";

        Controls.Add(_control);

        ChildControlsCreated = true;
      }
    }

    #endregion
  }
}