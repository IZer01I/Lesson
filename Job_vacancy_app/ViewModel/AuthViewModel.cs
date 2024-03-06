using Job_vacancy_app.Core;
using Job_vacancy_app.View;
using System.Windows;

namespace Job_vacancy_app.ViewModel
{
    internal class AuthViewModel : BaseViewModel
    {
        private readonly DbManager dbManager;

        private string _login;
        private string _password;

        public string Login
        {
            get => _login; set => SetPropertyChanged(ref _login, value);
        }

        public string Password
        {
            get => _password; set => SetPropertyChanged(ref _password, value);
        }

        public AuthViewModel()
        {
            dbManager = new DbManager();
        }

        public async void AuthUser()
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            if (await dbManager.Authority(_login, _password))
            {
                var message = MessageBox.Show("Вход выполнен успешно!", "", MessageBoxButton.OK);
                if (message == MessageBoxResult.OK)
                {
                    if (UserSingleton.User.RoleId == 1)
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item is AuthWindow) item.Close();
                    }
                }
            }
            else
                MessageBox.Show("Не удалось найти пользователя!");
        }
    }
}
