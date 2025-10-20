using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  [SuppressMessage("Microsoft.Naming", "CA1711",
    Justification = "It is actually a 'GroupPermission'!")]
  public class GroupPermission
  {
    public GroupPermission()
    {
    }


    public GroupPermission(Group group, Permission permission)
    {
      Group = group;
      Permission = permission;
    }


    public Group Group { get; set; }

    public Permission Permission { get; set; }
  }
}