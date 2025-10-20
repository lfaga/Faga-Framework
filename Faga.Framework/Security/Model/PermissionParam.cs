using System;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class PermissionParam
  {
    public PermissionParam()
    {
      IdPermission = int.MinValue;
      IdApplication = int.MinValue;
      Key = string.Empty;
    }


    public PermissionParam(int idPermission, int idApplication, string key)
    {
      IdPermission = idPermission;
      IdApplication = idApplication;
      Key = key;
    }


    public int IdPermission { get; set; }

    public int IdApplication { get; set; }

    public string Key { get; set; }
  }
}