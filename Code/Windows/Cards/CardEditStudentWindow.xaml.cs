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
using WpfBDLab2.VM.Forms.Cards;

namespace WpfBDLab2.Windows.Cards
{
    /// <summary>
    /// Логика взаимодействия для CardEditStudentWindow.xaml
    /// </summary>
    public partial class CardEditStudentWindow : Window
    {
        public CardEditStudentWindow(int id, int idStudent, int idTeacher, string dateGiven, int code)
        {
            InitializeComponent();
            DataContext = new CardEditFormVM(id, idStudent, idTeacher, dateGiven, code);
        }
    }
}
