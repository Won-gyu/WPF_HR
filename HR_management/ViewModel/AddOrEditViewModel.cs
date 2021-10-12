using HR_management.Model;
using HR_management.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HR_management.ViewModel
{
    public class AddOrEditViewModel : INotifyPropertyChanged
    {
        public DelegateCommand AddOrEditHumanCommand { get; private set; }

        string AddOrEdit;
        public string FinishButtonContent { get; set; }
        public Human selectedWorker { get; set; }

        public string Name { get; set; }
        public string Age { get; set; }
        public string SelectedPosition { get; set; }
        public string Salary { get; set; }
        public string Telephone { get; set; }

        public float EvaluationPoint_responsibility { get; set; }
        public float EvaluationPoint_Diligence { get; set; }
        public float EvaluationPoint_Effort { get; set; }
        public float EvaluationPoint_Originality { get; set; }
        public float EvaluationPoint_positiveness { get; set; }
        public float EvaluationPoint_WorkUnderstanding { get; set; }

        Window addOrEditWindow;

        public ObservableCollection<string> Positions { get; private set; }
        public ObservableCollection<float> Rates { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Human> WorkersList
        {
            get { return SingletonModel.Instance.WorkersList; }
            set { SingletonModel.Instance.WorkersList = value; }
        }

        // 생성자
        public AddOrEditViewModel(Window addOrEditWindow, string AddOrEdit, Human selectedWorker) {
            AddOrEditHumanCommand = new DelegateCommand(AddOrEditHuman);
            this.addOrEditWindow = addOrEditWindow;

            this.AddOrEdit = AddOrEdit;

            this.selectedWorker = selectedWorker;

            Positions = new ObservableCollection<string>()
            {
                "부장",
                "과장",
                "대리",
                "주임",
                "사원"
            };

            Rates = new ObservableCollection<float>()
            {
                5.0f,
                4.5f,
                4.0f,
                3.5f,
                3.0f,
                2.5f,
                2.0f,
                1.5f,
                1.0f,
                0.5f,
                0.0f,
            };
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            switch (AddOrEdit)
            {
                case "Add":
                    addOrEditWindow.Title = "고용인 추가";
                    FinishButtonContent = "추가";
                    OnPropertyChanged("FinishButtonContent");
                    break;
                case "Edit":
                    addOrEditWindow.Title = "고용인 수정";
                    FinishButtonContent = "수정";
                    OnPropertyChanged("FinishButtonContent");

                    Name = selectedWorker.Name;
                    Age = selectedWorker.Age.ToString();
                    SelectedPosition = selectedWorker.Position;
                    Salary = selectedWorker.Salary.ToString();
                    Telephone = selectedWorker.Telephone;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Age");
                    OnPropertyChanged("SelectedPosition");
                    OnPropertyChanged("Salary");
                    OnPropertyChanged("Telephone");

                    EvaluationPoint_responsibility = selectedWorker.EvaluationPoint_responsibility;
                    EvaluationPoint_Diligence = selectedWorker.EvaluationPoint_Diligence;
                    EvaluationPoint_Effort = selectedWorker.EvaluationPoint_Effort;
                    EvaluationPoint_Originality = selectedWorker.EvaluationPoint_Originality;
                    EvaluationPoint_positiveness = selectedWorker.EvaluationPoint_positiveness;
                    EvaluationPoint_WorkUnderstanding = selectedWorker.EvaluationPoint_WorkUnderstanding;
                    OnPropertyChanged("EvaluationPoint_responsibility");
                    OnPropertyChanged("EvaluationPoint_Diligence");
                    OnPropertyChanged("EvaluationPoint_Effort");
                    OnPropertyChanged("EvaluationPoint_Originality");
                    OnPropertyChanged("EvaluationPoint_positiveness");
                    OnPropertyChanged("EvaluationPoint_WorkUnderstanding");

                    break;
            }

        }

        public void AddOrEditHuman(object message)
        {
            switch (AddOrEdit)
            {
                case "Add":
                    WorkersList.Add(new Human()
                    {
                        ID = SingletonModel.Instance.getNewID(),
                        Name = this.Name,
                        Age = int.Parse(this.Age),
                        Position = this.SelectedPosition,
                        Salary = int.Parse(this.Salary),
                        Telephone = this.Telephone,
                        EvaluationPoint_responsibility = this.EvaluationPoint_responsibility,
                        EvaluationPoint_Diligence = this.EvaluationPoint_Diligence,
                        EvaluationPoint_Effort = this.EvaluationPoint_Effort,
                        EvaluationPoint_Originality = this.EvaluationPoint_Originality,
                        EvaluationPoint_positiveness = this.EvaluationPoint_positiveness,
                        EvaluationPoint_WorkUnderstanding = this.EvaluationPoint_WorkUnderstanding
                    });
                    break;
                case "Edit":
                    selectedWorker.Name = this.Name;
                    selectedWorker.Age = int.Parse(this.Age);
                    selectedWorker.Position = this.SelectedPosition;
                    selectedWorker.Salary = int.Parse(this.Salary);
                    selectedWorker.Telephone = this.Telephone;
                    selectedWorker.EvaluationPoint_responsibility = this.EvaluationPoint_responsibility;
                    selectedWorker.EvaluationPoint_Diligence = this.EvaluationPoint_Diligence;
                    selectedWorker.EvaluationPoint_Effort = this.EvaluationPoint_Effort;
                    selectedWorker.EvaluationPoint_Originality = this.EvaluationPoint_Originality;
                    selectedWorker.EvaluationPoint_positiveness = this.EvaluationPoint_positiveness;
                    selectedWorker.EvaluationPoint_WorkUnderstanding = this.EvaluationPoint_WorkUnderstanding;
                    break;
            }

            addOrEditWindow.Close();
        }
    }
}
