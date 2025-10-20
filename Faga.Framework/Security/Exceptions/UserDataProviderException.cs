using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace Faga.Framework.Security.Exceptions
{
  [Serializable]
  public class UserDataProviderException : SecurityException
  {
    public UserDataProviderException(Exception innerException)
      : base(string.Format(CultureInfo.InvariantCulture,
        "User data provider is unavaiable or has raised an error: {0}",
        innerException.Message))
    {
    }


    protected UserDataProviderException(SerializationInfo info,
      StreamingContext context)
      : base(info, context)
    {
    }
  }
}