namespace Faga.Framework.Spooler.Model
{
  public interface IPrintJob
  {
    IDocument Document { get; set; }
    void Print();
  }
}