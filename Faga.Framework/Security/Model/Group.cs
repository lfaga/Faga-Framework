using System;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class Group
  {
    public Group()
    {
      Id = int.MinValue;
      IdApplication = int.MinValue;
      Name = string.Empty;
      Description = string.Empty;
    }


    public Group(int id, int idApplication, string name, string description)
    {
      Id = id;
      IdApplication = idApplication;
      Name = name;
      Description = description;
    }


    public int Id { get; set; }

    public int IdApplication { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }


    public override string ToString()
    {
      return Name;
    }
  }
}