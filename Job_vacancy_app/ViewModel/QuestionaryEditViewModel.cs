using Job_vacancy_app.Command;
using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using Job_vacancy_app.View;

namespace Job_vacancy_app.ViewModel
{
    internal class QuestionaryEditViewModel : BaseViewModel
    {
        Job_vacancyEntities db;
        DbManager dbManager;
        private ObservableCollection<City> _citysList;
        private ObservableCollection<Nationality> _natianalityList;

        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _gender;
        private string _telephonNumber;
        private string _mail;
        private City _city;
        private Nationality _nationality;
        private DateTime _dateOfBirth;
        private int _expirience;
        private string _info;
        private string _vacancy;

        private OpenFileDialog dlg;

        private BitmapImage _image;
        private BitmapSource _usesImage;

        private Questionary newQuestionary;

        public string FirstName { get => _firstName; set => SetPropertyChanged(ref _firstName, value); }
        public string MiddleName { get => _middleName; set => SetPropertyChanged(ref _middleName, value); }
        public string LastName { get => _lastName; set => SetPropertyChanged(ref _lastName, value); }
        public string Gender { get => _gender; set => SetPropertyChanged(ref _gender, value); }
        public string TelephonNumber { get => _telephonNumber; set => SetPropertyChanged(ref _telephonNumber, value); }
        public string Mail { get => _mail; set => SetPropertyChanged(ref _mail, value); }

        public City City { get => _city; set => SetPropertyChanged(ref _city, value); }
        public Nationality Nationality { get => _nationality; set => SetPropertyChanged(ref _nationality, value); }
        public DateTime DateOfBirth { get => _dateOfBirth; set => SetPropertyChanged(ref _dateOfBirth, value); }
        public int Expirience { get => _expirience; set => SetPropertyChanged(ref _expirience, value); }
        public string Info { get => _info; set => SetPropertyChanged(ref _info, value); }
        public string Vacancy { get => _vacancy; set => SetPropertyChanged(ref _vacancy, value); }

        public BitmapImage Image { get => _image; set => SetPropertyChanged(ref _image, value); }
        public BitmapSource UsesImage { get => _usesImage; set => SetPropertyChanged(ref _usesImage, value); }
        public ObservableCollection<City> CitysList { get => _citysList; set => SetPropertyChanged(ref _citysList, value); }
        public ObservableCollection<Nationality> NatianalityList { get => _natianalityList; set => SetPropertyChanged(ref _natianalityList, value); }

        public ICommand EditUserImage { get; private set; }
        public ICommand EditInfo { get; private set; }
        public ICommand DeleteInfo { get; private set; }

        public QuestionaryEditViewModel()
        {

            db = new Job_vacancyEntities();
            dbManager = new DbManager();

            newQuestionary = new Questionary();

            CitysList = new ObservableCollection<City>();
            NatianalityList = new ObservableCollection<Nationality>();

            dlg = new OpenFileDialog();

            GetCitysList();
            GetNatianalityList();
            GetSelectedQuestionaryInfo();

            EditUserImage = new RelayCommand(EditImage);
            EditInfo = new RelayCommand(EditInfoInDB);
            DeleteInfo = new RelayCommand(DeleteInfoFromDB);
        }

        void GetCitysList()
        {
            using (Job_vacancyEntities db = new Job_vacancyEntities())
            {
                foreach (var item in db.City)
                {
                    CitysList.Add(item);
                }
            }
        }

        void GetNatianalityList()
        {
            using (Job_vacancyEntities db = new Job_vacancyEntities())
            {
                foreach (var item in db.Nationality)
                {
                    NatianalityList.Add(item);
                }
            }
        }

        private void EditImage(object obj)
        {
            dlg.FileName = "";
            dlg.DefaultExt = ".jpeg, .jpg, .png";

            bool? res = dlg.ShowDialog();

            if (res == true)
                Image = new BitmapImage(new Uri(dlg.FileName));

        }

        public void GetSelectedQuestionaryInfo()
        { 
            List<ModifySelectedQuestionary> list = new List<ModifySelectedQuestionary>();

            foreach (var item in db.GetSelectedQuestionary1(UserSingleton.QuestionaryId))
            {
                list.Add(item);
            }
            foreach(var item in list)
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

                if (item.Photo != null)
                {
                    UsesImage = (item as ModifySelectedQuestionary).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Photo);
                }

                newQuestionary.IsTaken = item.IsTaken;
            }

            foreach (var item in db.GetVacancyByQuestionary(UserSingleton.QuestionaryId))
            {
                Vacancy = item.Value;
            }
        }

        public async void EditInfoInDB(object obj)
        {
            try
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                byte[] byteImage;

                if (Image == null)
                    Image = new BitmapImage(new Uri("pack://application:,,,/Assets/EmptyProfileImage.jpg"));

                encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(Image.UriSource)));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    encoder.Save(memoryStream);

                    byteImage = memoryStream.ToArray();
                }
                newQuestionary.Id = UserSingleton.QuestionaryId;
                newQuestionary.FirstName = _firstName;
                newQuestionary.MiddleName = _middleName;
                newQuestionary.LastName = _lastName;
                newQuestionary.Gender = _gender;
                newQuestionary.Email = _mail;
                newQuestionary.TelephonNumber = _telephonNumber;
                newQuestionary.CityId = _city.Id;
                newQuestionary.NationalityId = _nationality.Id;
                newQuestionary.DateOfBirth = _dateOfBirth;
                newQuestionary.Experience = _expirience;
                newQuestionary.Info = _info;
                newQuestionary.Photo = byteImage;
                newQuestionary.FileExtension = UserSingleton.Questionary.FileExtension;

                if (await dbManager.EditQuestionary(newQuestionary))
                {
                   var message = MessageBox.Show("Aнкета изменена успешно!", "", MessageBoxButton.OK);
                    if (message == MessageBoxResult.OK)
                    {
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is QuestionaryAddWindow) item.Close();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не все обязательные поля заполнены!");
            }
        }

        //ИСПРАВИТЬ УДАЛЕНИЕ
        private async void DeleteInfoFromDB(object obj)
        {
            newQuestionary.Id = UserSingleton.QuestionaryId;

            var message = MessageBox.Show("Вы действительно хотите удалить эту анкету?", "", MessageBoxButton.YesNo);

            if (message == MessageBoxResult.Yes)
            {
                if (await dbManager.DeleteQuestionary(newQuestionary))
                {
                    var message_2 = MessageBox.Show("Анкета удалена успешно!", "", MessageBoxButton.OK);
                    if (message_2 == MessageBoxResult.OK)
                    {
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is QuestionaryEditwindow) item.Close();
                        }
                    }
                }
            }
        }
    }
}
