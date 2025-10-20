using System;
using System.Collections.Generic;

namespace Faga.Framework.Web.UI.Templates.Events
{
  public class UaDialogReturnEventArgs : EventArgs
  {
    private readonly string _dialogUrl;
    private readonly Dictionary<string, string> _returnValues;


    public UaDialogReturnEventArgs(string dialogUrl,
      Dictionary<string, string> returnValues)
    {
      _dialogUrl = dialogUrl;
      _returnValues = returnValues;
    }


    public Dictionary<string, string> ReturnValue
    {
      get { return _returnValues; }
    }

    public string DialogUrl
    {
      get { return _dialogUrl; }
    }


    public bool WasCalledFrom(string url)
    {
      return _dialogUrl == url;
    }
  }
}