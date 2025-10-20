using System;
using System.Text;
using System.Xml.Serialization;

namespace Faga.Framework.Security.Model
{
  [Serializable]
  [XmlType(Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class User
  {
    public User()
    {
      Id = int.MinValue;
      IdLanguage = int.MinValue;
    }


    public User(string userName)
      : this()
    {
      UserName = userName;
    }


    public User(int id, string userName, string nombre, string apellido,
      string telefono, string email, int idLanguage)
    {
      Id = id;
      UserName = userName;
      Nombre = nombre;
      Apellido = apellido;
      Telefono = telefono;
      Email = email;
      IdLanguage = idLanguage;
    }

    #region Public Properties

    public int Id { get; set; }

    public bool Online { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Telefono { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string ScreenName
    {
      get
      {
        if ((Nombre != null)
            && (Apellido != null))
        {
          var sb = new StringBuilder();
          sb.Append(Nombre);
          sb.Append(" ");
          sb.Append(Apellido);

          if (sb.ToString().Length > 1)
          {
            return sb.ToString();
          }
        }
        return UserName;
      }
    }

    public int IdLanguage { get; set; }

    #endregion
  }
}