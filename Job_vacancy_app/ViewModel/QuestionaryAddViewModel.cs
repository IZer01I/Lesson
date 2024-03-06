using Job_vacancy_app.Command;
using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using Job_vacancy_app.View;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.ViewModel
{
    internal class QuestionaryAddViewModel : BaseViewModel
    {
        DbManager dbManager;
        FtpManager ftpManager;

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

        private OpenFileDialog dlg;

        private BitmapImage _image;
        private BitmapImage _fileImage = new BitmapImage(new Uri("pack://application:,,,/Assets/EmptyFileImage.png"));
        private string _filePath = "empty";
        private string[] fileExtension = {" "};

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

        public BitmapImage Image { get => _image; set => SetPropertyChanged(ref _image, value); }
        public BitmapImage FileImage { get => _fileImage; set => SetPropertyChanged(ref _fileImage, value); }
        public string FilePath { get => _filePath; set => SetPropertyChanged(ref _filePath, value); }

        public ObservableCollection<City> CitysList { get => _citysList; set => SetPropertyChanged(ref _citysList, value); }
        public ObservableCollection<Nationality> NatianalityList { get => _natianalityList; set => SetPropertyChanged(ref _natianalityList, value); }

        public ICommand AddUserImage { get; private set; }
        public ICommand AddUserFile { get; private set; }
        public ICommand DeleteUserFile { get; private set; }
        public ICommand AddInfo { get; private set; }

        public QuestionaryAddViewModel()
        {
            ftpManager = new FtpManager();
            dbManager = new DbManager();

            newQuestionary = new Questionary();

            CitysList = new ObservableCollection<City>();
            NatianalityList = new ObservableCollection<Nationality>();

            dlg = new OpenFileDialog();

            GetCitysList();
            GetNatianalityList();

            AddUserImage = new RelayCommand(AddImage);
            AddUserFile = new RelayCommand(AddFile);
            DeleteUserFile = new RelayCommand(DeleteFile);
            AddInfo = new RelayCommand(AddInfoInDB);
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

        private void AddImage(object obj)
        {
            dlg.FileName = "";
            dlg.DefaultExt = ".jpeg, .jpg, .png";

            bool? res = dlg.ShowDialog();

            if (res == true)
                Image = new BitmapImage(new Uri(dlg.FileName));

        }

        private void AddFile(object obj)
        {
            dlg.FileName = "";
            dlg.DefaultExt = "";

            bool? res = dlg.ShowDialog();

            if (res == true)
            {
                string filePath = dlg.FileName;

                FilePath = filePath.Replace("\\", "/");

                string[] wordsMass = FilePath.Split('/');
                fileExtension = wordsMass.Last().Split('.');

                if (fileExtension.Last() == "rar")
                    FileImage = new BitmapImage(new Uri("pack://application:,,,/Assets/FileImage.png"));

                else if (fileExtension.Last() == "docx")
                    FileImage = new BitmapImage(new Uri("pack://application:,,,/Assets/FileImage.png"));

                else
                {
                    MessageBox.Show("Неверный формат загруженного файла! Разрешаются форматы .rar и .docx!");
                    dlg.FileName = "";
                    FilePath = "empty";

                    FileImage = new BitmapImage(new Uri("pack://application:,,,/Assets/EmptyFileImage.png"));
                }
            }
        }

        private void DeleteFile(object obj)
        {
            dlg.FileName = "";
            FilePath = "empty";

            FileImage = new BitmapImage(new Uri("pack://application:,,,/Assets/EmptyFileImage.png"));
    }

    public async void AddInfoInDB(object obj)
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
            newQuestionary.FileExtension = fileExtension.Last();

            if (await dbManager.RegQuestionary(newQuestionary))
            {
                if(FilePath != "empty")
                    await ftpManager.UploadFile(newQuestionary.Id + "_" + FirstName + "_" + MiddleName + "_" + LastName + "." + fileExtension.Last(), FilePath);

                var message = MessageBox.Show("Ваша анкета зарегистрирована!", "", MessageBoxButton.OK);

                if (message == MessageBoxResult.OK)
                {
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item is QuestionaryAddWindow) item.Close();
                    }
                }
            }
        }
    }
}
