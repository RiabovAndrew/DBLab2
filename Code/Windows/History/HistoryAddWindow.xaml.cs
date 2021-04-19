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
using WpfBDLab2.VM.Forms.History;

namespace WpfBDLab2.Windows.History
{
    /// <summary>
    /// Логика взаимодействия для HistoryAddWindow.xaml
    /// </summary>
    public partial class HistoryAddWindow : Window
    {
        public HistoryAddWindow(int id, int idCard, int idBook, string dateGiven, string dateGivenUntill, string worker)
        {
            InitializeComponent();
            DataContext = new HistoryAddFormVM(id, idCard, idBook, dateGiven, dateGivenUntill, worker);
        }
    }
}
