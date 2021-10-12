using HR_management.Model;
using HR_management.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HR_management
{
    /// <summary>
    /// AddOrEditWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddOrEditWindow : Window
    {
        public AddOrEditWindow(string AddOrEdit, Human selectedWorker)
        {
            InitializeComponent();

            DataContext = new AddOrEditViewModel(this, AddOrEdit, selectedWorker);
            Loaded += (DataContext as AddOrEditViewModel).OnLoad;
        }
    }
}
