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
using WpfBDLab2.VM.Forms.SpecFac;

namespace WpfBDLab2.Windows.SpecFac
{
    /// <summary>
    /// Логика взаимодействия для SpecFacEditWindow.xaml
    /// </summary>
    public partial class SpecFacEditWindow : Window
    {
        public SpecFacEditWindow(int id, int idSpec, int idFac)
        {
            InitializeComponent();
            DataContext = new SpecFacEditFormVM(id, idSpec, idFac);
        }
    }
}
