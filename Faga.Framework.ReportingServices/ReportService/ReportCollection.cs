using System.Collections;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for ReportCollection.
  /// </summary>
  public class ReportCollection : Hashtable
  {
    public Report this[string id]
    {
      get { return (Report) base[id]; }
      set { base[id] = value; }
    }

    public void Add(Report r)
    {
      base.Add(r.Id, r);
    }
  }
}