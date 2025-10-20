using System;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class UserGroup
  {
    public UserGroup()
    {
    }


    public UserGroup(User user, Group group)
    {
      User = user;
      Group = group;
    }


    public User User { get; set; }


    public Group Group { get; set; }
  }
}