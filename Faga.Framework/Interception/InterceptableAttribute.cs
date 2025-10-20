using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;

namespace Faga.Framework.Interception
{
  /// <summary>
  ///   Atributo que marca que la clase se interceptará para
  ///   posibilitar la ejecución de los aspectos.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class InterceptableAttribute : Attribute, IContextAttribute
  {
    #region IContextAttribute Members

    /// <summary>
    ///   Checkea el contexto
    /// </summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      if (ctx != null && ctx.GetProperty("ContextProps") == null)
      {
        return false;
      }
      return true;
    }


    /// <summary>
    ///   Agrega las propiedades del contexto
    /// </summary>
    /// <param name="msg">Mensaje</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public void GetPropertiesForNewContext(IConstructionCallMessage msg)
    {
      if (msg != null)
      {
        msg.ContextProperties.Add(new InterceptableContextProps());
      }
    }

    #endregion
  }
}