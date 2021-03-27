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
using WpfBDLab2.VM.Forms.Students;

namespace WpfBDLab2.Windows.Students
{
    /// <summary>
    /// Логика взаимодействия для StudentAddWindow.xaml
    /// </summary>
    public partial class StudentAddWindow : Window
    {
        public StudentAddWindow(int id, string name, int idSpecFac, int year)
        {
            InitializeComponent();
            DataContext = new StudentAddForm(id, name, idSpecFac, year);
        }
    }
}
