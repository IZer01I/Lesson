using Job_vacancy_app.ViewModel;
using System.Windows;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();

            DataContext = new RegViewModel();
        }

        private async void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LogPassPanel.Visibility == Visibility.Visible)
            {
                if (await(DataContext as RegViewModel).RegInfoChecker())
                {
                    LogPassPanel.Visibility = Visibility.Hidden;
                    UserInfoPanel.Visibility = Visibility.Visible;
                }
            }

            else if (UserInfoPanel.Visibility == Visibility.Visible)
            {
                NexTxt.Text = "Далее";
                UserInfoPanel.Visibility = Visibility.Hidden;
                MailPanel.Visibility = Visibility.Visible;
            }

            else if (MailPanel.Visibility == Visibility.Visible)
            {
                NexTxt.Text = "Создать";
                (DataContext as RegViewModel).RegUser();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(LogPassPanel.Visibility == Visibility.Visible)
            {
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is RegWindow) item.Close();
                }
            }

            if (MailPanel.Visibility == Visibility.Visible)
            {
                NexTxt.Text = "Далее";
                UserInfoPanel.Visibility = Visibility.Visible;
                MailPanel.Visibility = Visibility.Hidden;
            }

            else if (UserInfoPanel.Visibility == Visibility.Visible)
            {
                LogPassPanel.Visibility = Visibility.Visible;
                UserInfoPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
