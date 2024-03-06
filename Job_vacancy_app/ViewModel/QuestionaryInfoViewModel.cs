using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.ViewModel
{
    internal class QuestionaryInfoViewModel : BaseViewModel
    {
        Job_vacancyEntities db;
        DbManager dbManager;
        FtpManager ftpManager;
        StreamReader sr;

        private string _vacancy;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _gender;
        private string _telephonNumber;
        private string _mail;
        private DateTime _dateOfBirth;
        private int _expirience;
        private string _info;
        private int _isTakenInt;
        private string _fileExtension;

        private string _selectedCity;
        private string _selectedNationality;
        private int _cityId;
        private int _nationalityId;
        private int _questionaryId = UserSingleton.QuestionaryId;

        private BitmapSource _usesImage;
        private BitmapImage _image;

        private Questionary newQuestionary;

        List<ModifySelectedQuestionary> list;

        public string Vacancy { get => _vacancy; set => SetPropertyChanged(ref _vacancy, value); }
        public string FirstName { get => _firstName; set => SetPropertyChanged(ref _firstName, value); }
        public string MiddleName { get => _middleName; set => SetPropertyChanged(ref _middleName, value); }
        public string LastName { get => _lastName; set => SetPropertyChanged(ref _lastName, value); }
        public string Gender { get => _gender; set => SetPropertyChanged(ref _gender, value); }
        public string TelephonNumber { get => _telephonNumber; set => SetPropertyChanged(ref _telephonNumber, value); }
        public string Mail { get => _mail; set => SetPropertyChanged(ref _mail, value); }
        public int IsTakenInt { get => _isTakenInt; set => SetPropertyChanged(ref _isTakenInt, value); }

        public string SelectedCity { get => _selectedCity; set => SetPropertyChanged(ref _selectedCity, value); }
        public string SelectedNationality { get => _selectedNationality; set => SetPropertyChanged(ref _selectedNationality, value); }

        public DateTime DateOfBirth { get => _dateOfBirth; set => SetPropertyChanged(ref _dateOfBirth, value); }
        public int Expirience { get => _expirience; set => SetPropertyChanged(ref _expirience, value); }
        public string Info { get => _info; set => SetPropertyChanged(ref _info, value); }

        public BitmapImage Image { get => _image; set => SetPropertyChanged(ref _image, value); }
        public BitmapSource UsesImage { get => _usesImage; set => SetPropertyChanged(ref _usesImage, value); }

        public QuestionaryInfoViewModel()
        {
            db = new Job_vacancyEntities();
            dbManager = new DbManager();
            ftpManager = new FtpManager();
            sr = new StreamReader("FileSavePath.txt");

            list = new List<ModifySelectedQuestionary>();

            newQuestionary = new Questionary();

            GetSelectedQuestionaryInfo();
        }

        private void GetSelectedQuestionaryInfo()
        {
            foreach (var item in db.GetSelectedQuestionary1(UserSingleton.QuestionaryId))
            {
                list.Add(item);
            }
            foreach (var item in list)
            {
                FirstName = item.FirstName;
                MiddleName = item.MiddleName;
                LastName = item.LastName;
                Gender = item.Gender;
                TelephonNumber = item.TelephonNumber;
                Mail = item.Email;
                DateOfBirth = item.DateOfBirth;
                Expirience = Convert.ToInt32(item.Experience);
                Info = item.Info;
                _fileExtension = item.FileExtension;

                _cityId = Convert.ToInt32(item.CityId);
                _nationalityId = Convert.ToInt32(item.NationalityId);

                foreach (var cityName in db.GetCityValue(Convert.ToInt32(item.CityId)))
                {
                    SelectedCity = cityName;
                }

                foreach (var NationalityName in db.GetNationalityValue(Convert.ToInt32(item.NationalityId)))
                {
                    SelectedNationality = NationalityName;
                }


                if (item.Photo != null)
                    UsesImage = (item as ModifySelectedQuestionary).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Photo);

                newQuestionary.Photo = item.Photo;
            }

            foreach (var item in db.GetVacancyByQuestionary(UserSingleton.QuestionaryId))
            {
                Vacancy = item.Value;
            }
        }

        public BitmapSource Get_usesImage()
        {
            return _usesImage;
        }

        public async void EditInfoInDB()
        {
            newQuestionary.Id = UserSingleton.QuestionaryId;
            newQuestionary.FirstName = FirstName;
            newQuestionary.MiddleName = MiddleName;
            newQuestionary.LastName = LastName;
            newQuestionary.Gender = Gender;
            newQuestionary.Email = Mail;
            newQuestionary.TelephonNumber = TelephonNumber;
            newQuestionary.CityId = _cityId;
            newQuestionary.NationalityId = _nationalityId;
            newQuestionary.DateOfBirth = DateOfBirth;
            newQuestionary.Experience = Expirience;
            newQuestionary.Info = Info;
            newQuestionary.IsTaken = IsTakenInt;
            newQuestionary.FileExtension = _fileExtension;

            await dbManager.EditQuestionary(newQuestionary);
        }

        public async void DownloadFile()
        {
            try
            {
                await ftpManager.DownloadFile(_questionaryId + "_" + FirstName + "_" + MiddleName + "_" + LastName + "." + _fileExtension, await sr.ReadLineAsync());
                sr.Close();

                MessageBox.Show("Файл загружен успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки файла! проверьте правильность пути сохранения!");
            }
        }
    }
}
