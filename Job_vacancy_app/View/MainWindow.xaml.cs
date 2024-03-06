using Job_vacancy_app.Core;
using Job_vacancy_app.Model;
using Job_vacancy_app.Model.DataBase;
using Job_vacancy_app.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).GetDepartmentsListInfo();
        }

        private void DepartmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (DepartmentListView.SelectedItem != null)
                {
                    UserSingleton.DepartmentId = (DepartmentListView.SelectedItem as ModifyDepartmentList).Id;
                    DepartmentListView.SelectedItem = null;

                    (DataContext as MainViewModel).GetVacanciesListInfo();
                }
            }
            catch { }

            DepartmentListView.Visibility = Visibility.Hidden;
            VacannciesListView.Visibility = Visibility.Visible;

            BackBtn.Visibility = Visibility.Visible;
        }

        private void VacannciesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VacannciesListView.SelectedItem != null)
            {
                UserSingleton.vacanciesList = VacannciesListView.SelectedItem as ModifyVacanciesList;
                DepartmentListView.SelectedItem = null;

                QuestionaryAddWindow questionaryAddWindow = new QuestionaryAddWindow();
                questionaryAddWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            BackBtn.Visibility = Visibility.Hidden;

            DepartmentListView.Visibility = Visibility.Visible;
            VacannciesListView.Visibility = Visibility.Hidden;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (!(item is AuthWindow)) item.Close();
            }
        }
    }
}
