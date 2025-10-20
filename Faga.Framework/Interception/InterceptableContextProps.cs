using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace Faga.Framework.Interception
{
  /// <summary>
  ///   Propiedades del contexto.
  /// </summary>
  public class InterceptableContextProps : IContextProperty, IContributeObjectSink
  {
    internal static string PropName
    {
      get { return "ContextProps"; }
    }

    #region IContributeObjectSink Members

    /// <summary>
    ///   Devuelve el Object sink
    /// </summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
    {
      return new InterceptableMsgSink(nextSink);
    }

    #endregion

    #region IContextProperty Members

    /// <summary>
    ///   Siempre devuelve true
    /// </summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public bool IsNewContextOK(Context newCtx)
    {
      return true;
    }


    /// <summary>
    ///   No implementado
    /// </summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public void Freeze(Context newContext)
    {
    }


    /// <summary>
    ///   Nombre de las ContextProps
    /// </summary>
    public string Name
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      get
      {
        return PropName;
      }
    }

    #endregion
  }
}