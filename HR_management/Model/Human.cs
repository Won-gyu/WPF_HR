using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_management.Model
{
    [Serializable]
    public class Human : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        int     id;
        int     salary;
        int     age;
        // 책임감
        float     evaluationPoint_responsibility;
        // 근면성
        float evaluationPoint_Diligence;
        // 노력도
        float evaluationPoint_Effort;
        // 독창성
        float evaluationPoint_Originality;
        // 적극성
        float evaluationPoint_positiveness;
        // 업무 이해도
        float evaluationPoint_WorkUnderstanding;
        string  name;
        string  telephone;
        string  position;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public string Position
        {
            get { return position; }
            set { position = value; }
        }
        public float EvaluationPoint
        {
            get { return EvaluationPoint_responsibility+ EvaluationPoint_Diligence+ EvaluationPoint_Effort+ EvaluationPoint_Originality+ EvaluationPoint_positiveness+ EvaluationPoint_WorkUnderstanding; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public float EvaluationPoint_responsibility
        {
            get { return evaluationPoint_responsibility; }
            set { evaluationPoint_responsibility = value; }
        }
        public float EvaluationPoint_Diligence
        {
            get { return evaluationPoint_Diligence; }
            set { evaluationPoint_Diligence = value; }
        }
        public float EvaluationPoint_Effort
        {
            get { return evaluationPoint_Effort; }
            set { evaluationPoint_Effort = value; }
        }
        public float EvaluationPoint_Originality
        {
            get { return evaluationPoint_Originality; }
            set { evaluationPoint_Originality = value; }
        }
        public float EvaluationPoint_positiveness
        {
            get { return evaluationPoint_positiveness; }
            set { evaluationPoint_positiveness = value; }
        }
        public float EvaluationPoint_WorkUnderstanding
        {
            get { return evaluationPoint_WorkUnderstanding; }
            set { evaluationPoint_WorkUnderstanding = value; }
        }

        public void propertyChanged()
        {
            OnPropertyChanged("Name");
            OnPropertyChanged("Telephone");
            OnPropertyChanged("Salary");
            OnPropertyChanged("Position");
            OnPropertyChanged("EvaluationPoint");
            OnPropertyChanged("Age");
            OnPropertyChanged("EvaluationPoint_responsibility");
            OnPropertyChanged("EvaluationPoint_Diligence");
            OnPropertyChanged("EvaluationPoint_Originality");
            OnPropertyChanged("EvaluationPoint_Effort");
            OnPropertyChanged("EvaluationPoint_positiveness");
            OnPropertyChanged("EvaluationPoint_WorkUnderstanding");
        }
    }


}
