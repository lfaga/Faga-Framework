using System.Collections;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for ResourceCollection.
  /// </summary>
  public class ResourceCollection : Hashtable
  {
    public Resource this[string id]
    {
      get { return (Resource) base[id]; }
      set { base[id] = value; }
    }

    public void Add(Resource r)
    {
      base.Add(r.Id, r);
    }
  }
}