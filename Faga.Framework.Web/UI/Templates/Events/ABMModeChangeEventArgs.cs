using System;

namespace Faga.Framework.Web.UI.Templates.Events
{
  public class AbmModeChangeEventArgs : EventArgs
  {
    private readonly DataAbmTemplateMode _mode;


    public AbmModeChangeEventArgs(DataAbmTemplateMode mode)
    {
      _mode = mode;
    }


    public DataAbmTemplateMode Mode
    {
      get { return _mode; }
    }
  }
}