using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBDLab2.DataBase.Tables;
using WpfBDLab2.VM.Forms;
using WpfBDLab2.Windows;
using WpfBDLab2.Windows.Authors;
using WpfBDLab2.Windows.Books;
using WpfBDLab2.Windows.Cards;
using WpfBDLab2.Windows.Faculties;
using WpfBDLab2.Windows.History;
using WpfBDLab2.Windows.PublHouses;
using WpfBDLab2.Windows.SpecFac;
using WpfBDLab2.Windows.Specs;
using WpfBDLab2.Windows.Students;
using WpfBDLab2.Windows.Teachers;

namespace WpfBDLab2.VM.MainFormVM
{
    class MainFormVM : BaseVM.BaseVM {
        private ICommand _openCitiesTableCommand;
        private ICommand _openAuthorsTableCommand;
        private ICommand _openPublHouseTableCommand;
        private ICommand _openBooksTableCommand;
        private ICommand _openFacTableCommand;
        private ICommand _openSpecTableCommand;
        private ICommand _openSpecFacTableCommand;
        private ICommand _openTeachersTableCommand;
        private ICommand _openStudentsTableCommand;
        private ICommand _openCardsTableCommand;
        private ICommand _openHistoryTableCommand;

        public ICommand OpenCitiesTableCommand =>
            _openCitiesTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new CitiesWindow().Show();
            });

        public ICommand OpenAuthorsTableCommand =>
            _openAuthorsTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new AuthorWindow().Show();
            });

        public ICommand OpenPublHouseTableCommand =>
            _openPublHouseTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new PublHousesWindow().Show();
            });

        public ICommand OpenBooksTableCommand =>
            _openBooksTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new BooksWindow().Show();
            });

        public ICommand OpenFacTableCommand =>
            _openFacTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new FacsWindow().Show();
            });

        public ICommand OpenSpecTableCommand =>
            _openSpecTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new SpecsWindow().Show();
            });

        public ICommand OpenSpecFacTableCommand =>
            _openSpecFacTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new SpecFacWindow().Show();
            });

        public ICommand OpenTeachersTableCommand =>
            _openTeachersTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new TeachersWindow().Show();
            });

        public ICommand OpenStudentsTableCommand =>
            _openStudentsTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new StudentsWindow().Show();
            });

        public ICommand OpenCardsTableCommand =>
            _openCardsTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new CardsWindow().Show();
            });

        public ICommand OpenHistoryTableCommand =>
            _openHistoryTableCommand ??= new RelayCommand.RelayCommand((o) => {
                new HistoryWindow().Show();
            });
    }
}
