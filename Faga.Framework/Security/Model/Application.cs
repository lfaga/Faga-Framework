using System;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class Application
  {
    public Application()
    {
      Id = int.MinValue;
    }


    public Application(int id, string name)
    {
      Id = id;
      Name = name;
    }


    public int Id { get; set; }

    public string Name { get; set; }
  }
}