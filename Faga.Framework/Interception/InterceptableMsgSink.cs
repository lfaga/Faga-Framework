using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using Faga.Framework.Logging;
using Faga.Framework.Security;
using Faga.Framework.Security.Transactions;

namespace Faga.Framework.Interception
{
  /// <summary>
  ///   Define el Message Sink
  /// </summary>
  public class InterceptableMsgSink : IMessageSink
  {
    private readonly IMessageSink _next;


    /// <summary>
    ///   Se setea el próximo message sink
    /// </summary>
    /// <param name="ims">Mensaje</param>
    public InterceptableMsgSink(IMessageSink ims)
    {
      _next = ims;
    }

    #region IMessageSink Members

    /// <summary>
    ///   Se procesa el mensaje
    /// </summary>
    /// <param name="msg">Mensaje</param>
    /// <returns>Mensaje procesado</returns>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public IMessage SyncProcessMessage(IMessage msg)
    {
      var methodMessage = (IMethodCallMessage) msg;

      //Logable attribute
      var atLog = methodMessage.MethodBase.GetCustomAttributes(
        typeof (LogableAttribute), false);
      if (atLog.Length > 0)
      {
        LoggingProvider.Write(methodMessage, ((LogableAttribute) atLog[0]).Method);
      }

      //RequiresPermission attribute
      var atRperm = methodMessage.MethodBase.GetCustomAttributes(
        typeof (RequiresPermissionAttribute), false);
      if (atRperm.Length > 0)
      {
        var rpa = atRperm[0] as RequiresPermissionAttribute;
        if (rpa != null)
        {
          using (var st = new SecurityTransaction(rpa.CurrentUser))
          {
            using (var up = new UserProvider(st))
            {
              up.CheckPermission(rpa);
            }
          }
        }
      }

      return _next.SyncProcessMessage(msg);
    }


    /// <summary>
    ///   Procesamiento asincrónico
    /// </summary>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
    public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
    {
      return _next.AsyncProcessMessage(msg, replySink);
    }


    /// <summary>
    ///   Proximo sink
    /// </summary>
    public IMessageSink NextSink
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      get
      {
        return _next;
      }
    }

    #endregion
  }
}