using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Faga.Framework.Spooler.Model;

namespace Faga.Framework.Spooler
{
  public class Spooler
  {
    private const int TimerInterval = 1000;
    private static readonly Spooler Singleton = new Spooler();
    private readonly Collection<IDocument> _erroneous;
    private readonly Queue<IPrintJob> _queue;
    private bool _enabled;
    private int _maxRetries = 2;
    private Timer _timer;

    public QueueFinished QueueFinished;

    private Spooler()
    {
      _queue = new Queue<IPrintJob>();
      _erroneous = new Collection<IDocument>();
    }

    public static Spooler Instance
    {
      get { return Singleton; }
    }

    public bool Enabled
    {
      get { return _enabled; }
      set
      {
        _enabled = value;

        if (_timer != null) _timer.Dispose();

        if (_enabled)
        {
          _timer = new Timer(timer_Elapsed, null, TimerInterval, Timeout.Infinite);
        }
      }
    }

    public int PendingCount
    {
      get { return _queue.Count; }
    }

    public int MaxRetries
    {
      get { return _maxRetries; }
      set { _maxRetries = value; }
    }

    private void timer_Elapsed(object sender)
    {
      if (_queue.Count <= 0) return;

      var job = _queue.Dequeue();
      try
      {
        job.Print();
      }
      catch (JobException)
      {
        job.Document.RetryCount++;

        if (job.Document.RetryCount == _maxRetries)
        {
          _erroneous.Add(job.Document);
        }
        else
        {
          _queue.Enqueue(job);
        }
      }

      if ((_queue.Count == 0) && (QueueFinished != null))
        QueueFinished(_erroneous);


      if (!_enabled) return;

      if (_timer != null)
        _timer.Dispose();

      _timer = new Timer(timer_Elapsed, null, TimerInterval, Timeout.Infinite);
    }

    public void Add(IPrintJob job)
    {
      _queue.Enqueue(job);
    }


    ~Spooler()
    {
      if (_timer != null)
        _timer.Dispose();
    }
  }
}