using HR_management.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HR_management.ViewModel
{
    public struct Pos
    {
        public int x { get; set; }
        public int y { get; set; }


        public Pos(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
    //저장 열기 특이사항 직급별 평균연봉
    public class HumanAbilityGraphViewModel : INotifyPropertyChanged
    {
        // 각 능력치 그래프의 포인트 포지션
        public Pos ProfileGraphPos_responsibility { get; set; }
        public Pos ProfileGraphPos_Diligence { get; set; }
        public Pos ProfileGraphPos_Effort { get; set; }
        public Pos ProfileGraphPos_Originality { get; set; }
        public Pos ProfileGraphPos_positiveness { get; set; }
        public Pos ProfileGraphPos_WorkUnderstanding { get; set; }

        // 능력치 최대일때의 그래프 포인트의 포지션
        private Pos ProfileGraphPos_responsibility_Max { get; set; }
        private Pos ProfileGraphPos_Diligence_Max { get; set; }
        private Pos ProfileGraphPos_Effort_Max { get; set; }
        private Pos ProfileGraphPos_Originality_Max { get; set; }
        private Pos ProfileGraphPos_positiveness_Max { get; set; }
        private Pos ProfileGraphPos_WorkUnderstanding_Max { get; set; }

        public Human SelectedWorker { get; set; }

        public HumanAbilityGraphViewModel()
        {
            ProfileGraphPos_responsibility_Max = new Pos(0,40);
            ProfileGraphPos_Diligence_Max = new Pos(75, 0);
            ProfileGraphPos_Diligence_Max = new Pos(150, 40);
            ProfileGraphPos_Originality_Max = new Pos(150, 110);
            ProfileGraphPos_Originality_Max = new Pos(75, 150);
            ProfileGraphPos_Originality_Max = new Pos(0, 110);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void setProfileGraph(Human Worker)
        {
            calGraph(Worker);
            SelectedWorker = Worker;
            OnPropertyChanged("ProfileGraphPos_responsibility");
            OnPropertyChanged("ProfileGraphPos_Diligence");
            OnPropertyChanged("ProfileGraphPos_Effort");
            OnPropertyChanged("ProfileGraphPos_Originality");
            OnPropertyChanged("ProfileGraphPos_positiveness");
            OnPropertyChanged("ProfileGraphPos_WorkUnderstanding");
            OnPropertyChanged("SelectedWorker");
        }

        private void calGraph(Human worker)
        {
            //좌위
            ProfileGraphPos_responsibility = new Pos(
                75-(int)(75*worker.EvaluationPoint_responsibility/5.0f),
                75-(int)(35 * worker.EvaluationPoint_responsibility / 5.0f)
                );
            //위
            ProfileGraphPos_Diligence = new Pos(
                75,
                75 - (int)(75 * worker.EvaluationPoint_Diligence / 5.0f)
                );
            //우위
            ProfileGraphPos_Effort = new Pos(
                75 + (int)(75 * worker.EvaluationPoint_Effort / 5.0f),
                75 - (int)(35 * worker.EvaluationPoint_Effort / 5.0f)
                );
            //우하
            ProfileGraphPos_Originality = new Pos(
                75 + (int)(75 * worker.EvaluationPoint_Originality / 5.0f),
                75 + (int)(35 * worker.EvaluationPoint_Originality / 5.0f)
                );
            //하
            ProfileGraphPos_positiveness = new Pos(
                75,
                75 + (int)(75 * worker.EvaluationPoint_positiveness/ 5.0f)
                );
            //좌하
            ProfileGraphPos_WorkUnderstanding = new Pos(
                75 - (int)(75 * worker.EvaluationPoint_WorkUnderstanding / 5.0f),
                75 + (int)(35 * worker.EvaluationPoint_WorkUnderstanding / 5.0f)
                );
        }
    }
}
