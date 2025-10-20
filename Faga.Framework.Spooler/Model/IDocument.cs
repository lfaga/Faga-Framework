using System;

namespace Faga.Framework.Spooler.Model
{
  public interface IDocument
  {
    Guid DocumentId { get; set; }
    int RetryCount { get; set; }
  }
}