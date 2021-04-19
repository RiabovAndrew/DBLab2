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
using WpfBDLab2.VM.Forms.PublHouses;

namespace WpfBDLab2.Windows.PublHouses
{
    /// <summary>
    /// Логика взаимодействия для PublHouseAddWindow.xaml
    /// </summary>
    public partial class PublHouseAddWindow : Window
    {
        public PublHouseAddWindow(int id, string name)
        {
            InitializeComponent();
            DataContext = new PublHouseAddFormVM(id, name);
        }
    }
}
