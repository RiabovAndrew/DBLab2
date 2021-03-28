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
using WpfBDLab2.VM.Forms.Teachers;

namespace WpfBDLab2.Windows.Teachers
{
    /// <summary>
    /// Логика взаимодействия для TeacherAddWindow.xaml
    /// </summary>
    public partial class TeacherAddWindow : Window
    {
        public TeacherAddWindow(int id, string name, int idFac)
        {
            InitializeComponent();
            DataContext = new TeacherAddFormVM(id, name, idFac);
        }
    }
}
