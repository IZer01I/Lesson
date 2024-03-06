using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Job_vacancy_app.ViewModel;
using System.Windows;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            DataContext = new AdminViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as AdminViewModel).GetQuestionListInfo();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow window = new AuthWindow();
            window.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is AdminWindow) item.Close();
            }
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (questionaryList.SelectedItem != null)
                {
                    UserSingleton.QuestionaryId = (questionaryList.SelectedItem as ModifyAllQuestionary).Id;

                    QuestionaryInfoWindow window = new QuestionaryInfoWindow();
                    window.Show();
                    Close();

                    questionaryList.SelectedItem = null;
                }
            }
            catch { }
        }

        private void ReloadBtn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AdminViewModel).GetQuestionListInfo();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.Show();
        }
    }
}
