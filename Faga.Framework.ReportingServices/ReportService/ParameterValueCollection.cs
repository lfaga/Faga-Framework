using System.Collections;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for ParameterValueCollection.
  /// </summary>
  public class ParameterValueCollection : ArrayList
  {
    public new ParameterValue this[int index]
    {
      get { return (ParameterValue) base[index]; }
      set { base[index] = value; }
    }

    public int Add(ParameterValue parameter)
    {
      return base.Add(parameter);
    }
  }
}