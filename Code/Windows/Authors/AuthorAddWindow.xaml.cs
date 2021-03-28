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
using WpfBDLab2.VM.Forms.Authors;

namespace WpfBDLab2.Windows.Authors
{
    /// <summary>
    /// Логика взаимодействия для AuthorAddWindow.xaml
    /// </summary>
    public partial class AuthorAddWindow : Window
    {
        public AuthorAddWindow(int id, string name, int year)
        {
            InitializeComponent();
            DataContext = new AuthorAddFormVM(id, name, year);
        }
    }
}
