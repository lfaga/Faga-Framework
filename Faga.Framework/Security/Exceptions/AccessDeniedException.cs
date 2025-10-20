using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.Exceptions
{
  [Serializable]
  public class AccessDeniedException : SecurityException
  {
    public AccessDeniedException()
    {
    }


    public AccessDeniedException(User user, string additionalinfo)
      : base(string.Format(
        CultureInfo.InvariantCulture,
        "El user {0} no tiene permisos suficientes para acceder a esta función.\n{1}",
        user.UserName, additionalinfo))
    {
    }


    protected AccessDeniedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}