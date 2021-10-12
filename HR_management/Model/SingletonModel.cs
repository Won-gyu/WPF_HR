using HR_management.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_management.Model
{
    public class SingletonModel
    {
        private static SingletonModel instance;
        private SingletonModel() { }

        public int latestID = 0;

        private ObservableCollection<Human> _workersList = new ObservableCollection<Human>();
        public ObservableCollection<Human> WorkersList
        {
            get { return _workersList; }
            set { _workersList = value; }
        }
        
        public static SingletonModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonModel();
                }
                return instance;
            }
        }

        public int getNewID()
        {
            latestID++;
            return latestID;
        }
    }
}
