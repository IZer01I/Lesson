using Job_vacancy_app.ViewModel;
using System.Windows;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();

            DataContext = new AuthViewModel();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e) => (DataContext as AuthViewModel).AuthUser();

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegWindow window = new RegWindow();
            window.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if(item is  AuthWindow) item.Close();
            }
        }
    }
}
