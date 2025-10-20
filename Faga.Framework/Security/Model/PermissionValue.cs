using System;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class PermissionValue : PermissionParam
  {
    public PermissionValue()
    {
      IdGroup = int.MinValue;
      Value = string.Empty;
    }


    public PermissionValue(int idPermission, int idApplication, string key,
      int idGroup, string value)
      : base(idPermission, idApplication, key)
    {
      IdGroup = idGroup;
      Value = value;
    }


    public string Value { get; set; }

    public int IdGroup { get; set; }
  }
}