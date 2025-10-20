using System.Collections.ObjectModel;
using Faga.Framework.Spooler.Model;

namespace Faga.Framework.Spooler
{
  public delegate void QueueFinished(Collection<IDocument> pendingJobs);
}