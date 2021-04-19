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
using WpfBDLab2.VM.Forms.Faculties;

namespace WpfBDLab2.Windows.Faculties
{
    /// <summary>
    /// Логика взаимодействия для FacEditWindow.xaml
    /// </summary>
    public partial class FacEditWindow : Window
    {
        public FacEditWindow(int id, string name, string letter)
        {
            InitializeComponent();
            DataContext = new FacEditFormVM(id, name, letter);
        }
    }
}
