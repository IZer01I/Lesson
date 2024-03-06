using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.ViewModel
{
    internal class UserProfileViewModel : BaseViewModel
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _login;
        private string _password;
        private BitmapImage _photo;

        public string FirstName { get => _firstName; set => SetPropertyChanged(ref _firstName, value); }
        public string MiddleName { get => _middleName; set => SetPropertyChanged(ref _middleName, value); }
        public string LastName { get => _lastName; set => SetPropertyChanged(ref _lastName, value); }
        public string Login { get => _login; set => SetPropertyChanged(ref _login, value); }
        public string Password { get => _password; set => SetPropertyChanged(ref _password, value); }

        public BitmapImage Photo { get => _photo; set => SetPropertyChanged(ref _photo, value);}

        public UserProfileViewModel()
        {
            GetUserInfo();
        }

        private void GetUserInfo()
        {
            _firstName = UserSingleton.User.FirstName;
            _middleName = UserSingleton.User.MiddleName;
            _lastName = UserSingleton.User.LastName;
            _login = UserSingleton.User.Login;
            _password = UserSingleton.User.Password;

            if (UserSingleton.User.Photo == null) _photo = new BitmapImage(new Uri("pack://application:,,,/Assets/EmptyProfileImage.jpg"));

            else
            {
                _photo = (BitmapImage)new ImageSourceConverter().ConvertFrom(UserSingleton.User.Photo);
            }
        }
    }
}
