using System;
using System.Runtime.Serialization;
using System.Security;

namespace Faga.Framework.Security.Exceptions
{
  [Serializable]
  public class ApplicationMismatchException : SecurityException
  {
    public ApplicationMismatchException()
      : base("No se pueden validar objetos de distintas aplicaciones.")
    {
    }


    protected ApplicationMismatchException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}