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
using WpfBDLab2.VM.Forms.Cities;

namespace WpfBDLab2.Windows.Cities
{
    /// <summary>
    /// Логика взаимодействия для CityAddWindow.xaml
    /// </summary>
    public partial class CityAddWindow : Window
    {
        public CityAddWindow(int id, string name)
        {
            InitializeComponent();
            DataContext = new CityAddFormVM(id, name);
        }
    }
}
