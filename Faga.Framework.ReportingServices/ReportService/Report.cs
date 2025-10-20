namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for Report.
  /// </summary>
  public class Report : Resource
  {
    private readonly ResourceCollection _resources;


    public Report()
    {
      _resources = new ResourceCollection();
    }


    public ResourceCollection Resources
    {
      get { return _resources; }
    }
  }
}