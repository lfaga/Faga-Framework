using System;

namespace Faga.Framework.ReportingServices.ReportService
{
  /// <summary>
  ///   Summary description for RenderFormat.
  /// </summary>
  public class RenderFormat
  {
    #region Format enum

    public enum Format
    {
      Xml = 1,
      Csv = 2,
      Image = 3,
      Pdf = 4,
      Html40 = 5,
      Html32 = 6,
      Mhtml = 7,
      Excel = 8,
      Htmlowc = 0
    }

    #endregion

    private RenderFormat()
    {
    }


    public static string ToString(Format f)
    {
      if (f == Format.Xml)
      {
        return "Xml";
      }
      if (f == Format.Csv)
      {
        return "Csv";
      }
      if (f == Format.Image)
      {
        return "Image";
      }
      if (f == Format.Pdf)
      {
        return "Pdf";
      }
      if (f == Format.Html40)
      {
        return "HTML4.0";
      }
      if (f == Format.Html32)
      {
        return "HTML3.2";
      }
      if (f == Format.Mhtml)
      {
        return "Mhtml";
      }
      if (f == Format.Excel)
      {
        return "Excel";
      }
      if (f == Format.Htmlowc)
      {
        return "Htmlowc";
      }

      throw new ArgumentException("Format Unknown");
    }
  }
}