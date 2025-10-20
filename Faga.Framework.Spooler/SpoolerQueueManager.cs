using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Faga.Framework.Spooler.Model;

namespace Faga.Framework.Spooler
{
  public class SpoolerQueueManager<TDocument, TPrintJob>
    where TDocument : class, IDocument
    where TPrintJob : IPrintJob, new()
  {
    private readonly string _fileName;

    public SpoolerQueueManager()
    {
      _fileName = "pending.xml";
    }

    public SpoolerQueueManager(string fileName)
    {
      _fileName = fileName;
    }

    public void RecoverPersistedQueue()
    {
      if (!File.Exists(_fileName)) return;

      try
      {
        using (var sr = new StreamReader(_fileName))
        {
          var ser = new XmlSerializer(typeof (TDocument[]));
          var objects = (TDocument[]) ser.Deserialize(sr);

          foreach (var doc in objects)
          {
            doc.RetryCount = 0;
            Spooler.Instance.Add(new TPrintJob {Document = doc});
          }
        }
      }
      catch (InvalidOperationException)
      {
      }

      File.Delete(_fileName);
    }

    public void PersistQueue(Collection<IDocument> documents)
    {
      if (documents.Count <= 0) return;

      using (var sw = new StreamWriter(_fileName))
      {
        var docs = new TDocument[documents.Count];

        for (var idx = 0; idx < documents.Count; idx++)
        {
          docs[idx] = (TDocument) documents[idx];
        }

        var ser = new XmlSerializer(typeof (TDocument[]));

        ser.Serialize(sw, docs);
      }
    }
  }
}