using System;
using Faga.Framework.Spooler.Model;

namespace Faga.Framework.Spooler
{
  public class JobException : Exception
  {
    public JobException(IDocument doc)
    {
      Document = doc;
    }

    public IDocument Document { get; set; }
  }
}