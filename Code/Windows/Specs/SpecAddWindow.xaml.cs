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
using WpfBDLab2.VM.Forms.Specs;

namespace WpfBDLab2.Windows.Specs
{
    /// <summary>
    /// Логика взаимодействия для SpecAddWindow.xaml
    /// </summary>
    public partial class SpecAddWindow : Window
    {
        public SpecAddWindow(int id, string name, string letter, int code)
        {
            InitializeComponent();
            DataContext = new SpecAddFormVM(id, name, letter, code);
        }
    }
}
