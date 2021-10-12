using HR_management.Model;
using HR_management.ViewModel.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;

namespace HR_management.ViewModel
{
    
    public struct Summary
    {
        public float AverageSalary
        {
            get
            {
                if (SingletonModel.Instance.WorkersList.Count == 0)
                    return 0.0f;

                float sumEvaluationPoint = 0.0f;
                for (int i = 0; i < SingletonModel.Instance.WorkersList.Count; i++)
                {
                    sumEvaluationPoint += SingletonModel.Instance.WorkersList[i].Salary;
                }

                return sumEvaluationPoint / SingletonModel.Instance.WorkersList.Count;
            }
        }
        public float AverageAge
        {
            get
            {
                if (SingletonModel.Instance.WorkersList.Count == 0)
                    return 0.0f;

                float sumEvaluationPoint = 0.0f;
                for (int i = 0; i < SingletonModel.Instance.WorkersList.Count; i++)
                {
                    sumEvaluationPoint += SingletonModel.Instance.WorkersList[i].Age;
                }

                return sumEvaluationPoint / SingletonModel.Instance.WorkersList.Count;
            }
        }
        public float AverageEvaluationPoint
        {
            get
            {
                if (SingletonModel.Instance.WorkersList.Count == 0)
                    return 0.0f;

                float sumEvaluationPoint = 0.0f;
                for (int i = 0; i < SingletonModel.Instance.WorkersList.Count; i++)
                {
                    sumEvaluationPoint += SingletonModel.Instance.WorkersList[i].EvaluationPoint;
                }

                return sumEvaluationPoint/ SingletonModel.Instance.WorkersList.Count;
            }
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public DelegateCommand NewCommand { get; private set; }
        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SaveAsCommand { get; private set; }
        public DelegateCommand AboutCommand { get; private set; }

        public DelegateCommand OpenAddOrEditWindowCommand { get; private set; }
        public DelegateCommand RemoveSelectedWorkerCommand { get; private set; }

        public string Profile_Name { get; set; }
        public string Profile_Age { get; set; }
        public string Profile_Position { get; set; }
        public string Profile_Salary { get; set; }
        public string Profile_Telephone { get; set; }

        public HumanAbilityGraphViewModel humanAbilityGraphViewModel { get; set; }

        private Human selectedWorker;
        private Persistence FileHelper;
        public Summary summary { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Human SelectorWorker
        {
            get { return selectedWorker; }
            set
            {
                selectedWorker = value;
                setSelectedProfile(selectedWorker);
            }
        }

        public ObservableCollection<Human> WorkersList
        {
            get { return SingletonModel.Instance.WorkersList; }
            set { SingletonModel.Instance.WorkersList = value; }
        }


        public MainViewModel()
        {
            NewCommand = new DelegateCommand(New);
            OpenCommand = new DelegateCommand(Open);
            SaveCommand = new DelegateCommand(Save);
            SaveAsCommand = new DelegateCommand(SaveAs);
            AboutCommand = new DelegateCommand(About);

            OpenAddOrEditWindowCommand = new DelegateCommand(OpenAddOrEditWindow);
            RemoveSelectedWorkerCommand = new DelegateCommand(RemoveSelectedWorker);

            humanAbilityGraphViewModel = new HumanAbilityGraphViewModel();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {/*
            WorkersList.Add(new Human()
            {
                ID = SingletonModel.Instance.getNewID(),
                Name = "김유신",
                Position = "과장",
                Salary = 5000,
                Age = 40,
                Telephone = "010-0000-0000",
                EvaluationPoint_responsibility = 5.0f,
                EvaluationPoint_Diligence = 4.0f,
                EvaluationPoint_Effort = 5.0f,
                EvaluationPoint_Originality = 4.5f,
                EvaluationPoint_positiveness = 4.5f,
                EvaluationPoint_WorkUnderstanding = 5.0f
            });
            WorkersList.Add(new Human()
            {
                ID = SingletonModel.Instance.getNewID(),
                Name = "김철수",
                Position = "대리",
                Salary = 4000,
                Age = 35,
                Telephone = "010-0000-0001",
                EvaluationPoint_responsibility = 5.0f,
                EvaluationPoint_Diligence = 4.0f,
                EvaluationPoint_Effort = 5.0f,
                EvaluationPoint_Originality = 2.0f,
                EvaluationPoint_positiveness = 3.5f,
                EvaluationPoint_WorkUnderstanding = 4.0f
            });
            WorkersList.Add(new Human()
            {
                ID = SingletonModel.Instance.getNewID(),
                Name = "신짱구",
                Position = "주임",
                Salary = 3000,
                Age = 30,
                Telephone = "010-0000-0002",
                EvaluationPoint_responsibility = 4.0f,
                EvaluationPoint_Diligence = 4.5f,
                EvaluationPoint_Effort = 3.0f,
                EvaluationPoint_Originality = 4.5f,
                EvaluationPoint_positiveness = 3.0f,
                EvaluationPoint_WorkUnderstanding = 2.0f
            });*/
        }

        public void OpenAddOrEditWindow(object message)
        {
            //MessageBox.Show();
            if(message.ToString() == "Edit" && selectedWorker == null)
            {
                MessageBox.Show("선택된 고용인이 없습니다.");
                return;
            }
            var addOrEditWindow = new AddOrEditWindow(message.ToString(), selectedWorker);
            addOrEditWindow.Closed += addOrEditWindowClosed;
            addOrEditWindow.ShowDialog();
            addOrEditWindow.Closed -= addOrEditWindowClosed;
        }

        public void New(object message)
        {
            int nWorkers = WorkersList.Count;
            for (int i=0; i< nWorkers; i++)
            {
                WorkersList.RemoveAt(0);
            }
            SingletonModel.Instance.latestID = 0;
            updateSummary();
        }
        
        public void Open(object message)
        {
            var ofd = new OpenFileDialog
            {
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };
            if (ofd.ShowDialog() == true)
            {
                FileHelper = new Persistence(ofd.FileName);
                FileHelper.Load();
                updateSummary();
            }
        }

        public void Save(object message)
        {
            if(FileHelper == null)
            {
                SaveAs(message);
            }
            else
            {
                FileHelper.Save();
            }
        }

        public void SaveAs(object message)
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                FileHelper = new Persistence(sfd.FileName);
                FileHelper.Save();
            }
        }

        public void About(object message)
        {
            MessageBox.Show("1.0 버전입니다.\n hwg36@naver.com");
        }

        // 추가 또는 수정 후 속성 업데이트
        private void addOrEditWindowClosed(object sender, EventArgs e)
        {
            if(selectedWorker != null)
            {
                selectedWorker.propertyChanged();
                setSelectedProfile(selectedWorker);
            }
            updateSummary();
        }

        public void RemoveSelectedWorker(object message)
        {
            if(selectedWorker == null)
            {
                MessageBox.Show("선택된 고용인이 없습니다.");

                return;
            }

            string question = selectedWorker.Name + "님에 대한 정보를 정말 삭제하시겠습니까?";
            if(MessageBox.Show(question,"고용인 삭제", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                WorkersList.Remove(selectedWorker);
                updateSummary();
            }
        }

        void setSelectedProfile(Human worker)
        {
            Profile_Name = worker.Name;
            OnPropertyChanged("Profile_Name");

            Profile_Age = worker.Age.ToString();
            OnPropertyChanged("Profile_Age");

            Profile_Position = worker.Position;
            OnPropertyChanged("Profile_Position");

            Profile_Salary = worker.Salary.ToString();
            OnPropertyChanged("Profile_Salary");

            Profile_Telephone = worker.Telephone;
            OnPropertyChanged("Profile_Telephone");

            humanAbilityGraphViewModel.setProfileGraph(worker);
        }

        void updateSummary()
        {
            OnPropertyChanged("summary");
        }
    }
}
