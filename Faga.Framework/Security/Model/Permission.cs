using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  [SuppressMessage("Microsoft.Naming", "CA1711",
    Justification = "It is actually a 'Permission'!")]
  public class Permission
  {
    public Permission()
    {
      Id = int.MinValue;
      IdApplication = int.MinValue;
      Description = string.Empty;
    }


    public Permission(int id, int idApplication, string description)
    {
      Id = id;
      IdApplication = idApplication;
      Description = description;
    }


    public int Id { get; set; }


    public int IdApplication { get; set; }

    public string Description { get; set; }


    public override string ToString()
    {
      return Description;
    }
  }
}