using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HR_management.Model
{
    public class Persistence
    {
        public string FilePath { get; }
        public List<Human> Workers { get; private set; } = new List<Human>();

        public Persistence(string filePath)
        {
            FilePath = filePath;
        }

        public void Save()
        {
            Workers = SingletonModel.Instance.WorkersList.ToList<Human>();

            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Human>));
            Stream fStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, Workers);
            fStream.Close();
        }

        public void Load()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Human>));
            Stream fStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            Workers = (List<Human>)xmlFormat.Deserialize(fStream);
            fStream.Close();

            //SingletonModel.Instance.WorkersList = new ObservableCollection<Human>(Workers);

            int nWorkers = SingletonModel.Instance.WorkersList.Count;
            for (int i=0; i< nWorkers; i++)
            {
                SingletonModel.Instance.WorkersList.RemoveAt(0);
            }

            for(int i=0; i< Workers.Count; i++)
            {
                SingletonModel.Instance.WorkersList.Add(Workers[i]);
                SingletonModel.Instance.latestID = Workers[i].ID;
            }
        }
    }
}
