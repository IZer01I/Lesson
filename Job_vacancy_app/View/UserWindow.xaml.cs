using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Job_vacancy_app.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();

            DataContext = new UserViewModel();
        }

        private void ExitBnt_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is UserWindow) item.Close();
            }
        }

        private void CreateQuestionaryBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionaryAddWindow window = new QuestionaryAddWindow();
            window.Show();
        }

        private void userWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UserViewModel).GetQuestionList.Execute(sender);
        }

        private void questionaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (questionaryList.SelectedItem != null)
                {
                    UserSingleton.QuestionaryId = (questionaryList.SelectedItem as ModifyQuestionaryList).Id;

                    QuestionaryEditwindow window = new QuestionaryEditwindow();
                    window.ShowDialog();

                    questionaryList.SelectedItem = null;
                }
            }
            catch { }
        }
    }
}
