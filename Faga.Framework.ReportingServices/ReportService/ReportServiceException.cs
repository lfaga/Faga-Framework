using System;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for ReportServiceException.
  /// </summary>
  public class ReportServiceException : Exception
  {
    public ReportServiceException(string message)
      : base(message)
    {
    }


    public ReportServiceException(Exception innerException)
      : base(innerException.Message, innerException)
    {
    }
  }
}