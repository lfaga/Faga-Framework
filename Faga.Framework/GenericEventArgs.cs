using System;

namespace Faga.Framework
{
  public class GenericEventArgs<TElement> : EventArgs
  {
    public GenericEventArgs(TElement element)
    {
      Element = element;
    }


    public TElement Element { get; set; }
  }
}