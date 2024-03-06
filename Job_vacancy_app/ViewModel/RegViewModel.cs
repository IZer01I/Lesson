using Job_vacancy_app.Command;
using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Job_vacancy_app.View;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Job_vacancy_app.ViewModel
{
    internal class RegViewModel : BaseViewModel
    {
        private readonly DbManager dbManager;

        private string _login;
        private string _password;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _email;
        private string _confirmCode;

        private int generatedCode;

        public string Login
        {
            get => _login; set => SetPropertyChanged(ref _login, value);
        }

        public string Password
        {
            get => _password; set => SetPropertyChanged(ref _password, value);
        }

        public string FirstName
        {
            get => _firstName; set => SetPropertyChanged(ref _firstName, value);
        }

        public string MiddleName
        {
            get => _middleName; set => SetPropertyChanged(ref _middleName, value);
        }

        public string LastName
        {
            get => _lastName; set => SetPropertyChanged(ref _lastName, value);
        }

        public string Mail
        {
            get => _email; set => SetPropertyChanged(ref _email, value);
        }

        public string ConfirmCode
        {
            get => _confirmCode; set => SetPropertyChanged(ref _confirmCode, value);
        }

        public ICommand SendCode { get; private set; }

        public RegViewModel()
        {
            dbManager = new DbManager();

            SendCode = new RelayCommand(sendCode);
        }

        public async Task<bool> RegInfoChecker()
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                MessageBox.Show("Не все поля заполнены!");
                return false;
            }
            else if (await dbManager.CheckUser(_login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return false;
            }

            else return true;
        }
        public async void RegUser()
        {
            User user = new User()
            {
                Login = _login,
                Password = _password,
                FirstName = _firstName,
                MiddleName = _middleName,
                LastName = _lastName,
                Email = _email,
                RoleId = 2
            };

            if (MailConfirm() && await dbManager.Registry(user))
            {
                var message = MessageBox.Show("Регистрация прошла успешно!", "", MessageBoxButton.OK);
                if (message == MessageBoxResult.OK)
                {
                    UserWindow window = new UserWindow();
                    window.Show();

                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item is RegWindow) item.Close();
                    }
                }
            }
        }

        private bool MailConfirm()
        {
            if (string.IsNullOrEmpty(_confirmCode))
            {
                MessageBox.Show("Код подтверждения не указан!");
                return false;
            }

            else if (_confirmCode != generatedCode.ToString())
            {
                MessageBox.Show("Не верный код подтверждения!");
                return false;
            }
            else return true;
        }

        private async void sendCode(object obj)
        {
            try
            {
                if(string.IsNullOrEmpty(_email))
                {
                    MessageBox.Show("Введите электронную почту!");
                    return;
                }

                Random random = new Random();
                generatedCode = random.Next(1000, 9999);

                SmtpClient _smtp = new SmtpClient("smtp.mail.ru", 25)
                {
                    Credentials = new NetworkCredential("jobvacancy2024@mail.ru", "E0z8M3yzQeXTrFPqFRiP"),
                    EnableSsl = true
                };
                MailMessage _mail = new MailMessage
                {
                    From = new MailAddress("jobvacancy2024@mail.ru", "")
                };

                _mail.To.Add(new MailAddress(_email));
                _mail.SubjectEncoding = Encoding.UTF8;
                _mail.BodyEncoding = Encoding.UTF8;
                _mail.Body = "Код подтверждения: " + generatedCode;

                await _smtp.SendMailAsync(_mail);

            }
            catch { MessageBox.Show("Ошибка при отправке кода подтверждения! Повторите попытку позже."); }
        }
    }
}
