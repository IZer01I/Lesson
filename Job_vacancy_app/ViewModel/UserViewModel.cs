using Job_vacancy_app.Command;
using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.ViewModel
{
    internal class UserViewModel : BaseViewModel
    {
        Job_vacancyEntities db;

        private string _userFirstName;
        private string _userMiddleName;
        private string _userLastName;

        private string _takenInfo;

        private ObservableCollection<ModifyQuestionaryList> _questionaryList;

        public string UserFirstName { get => _userFirstName; set => SetPropertyChanged(ref _userFirstName, value); }
        public string UserMiddleName { get => _userMiddleName; set => SetPropertyChanged(ref _userMiddleName, value); }
        public string UserLastName { get => _userLastName; set => SetPropertyChanged(ref _userLastName, value); }
        public string TakenInfo { get => _takenInfo; set => SetPropertyChanged(ref _takenInfo, value); }

        public ObservableCollection<ModifyQuestionaryList> QuestionaryList { get => _questionaryList; set => SetPropertyChanged(ref _questionaryList, value); }


        public ICommand GetQuestionList { get; private set; }

        public UserViewModel()
        {
            db = new Job_vacancyEntities();

            QuestionaryList = new ObservableCollection<ModifyQuestionaryList>();

            _userFirstName = UserSingleton.User.FirstName;
            _userMiddleName = UserSingleton.User.MiddleName;
            _userLastName = UserSingleton.User.LastName;

            GetQuestionList = new RelayCommand(GetQuestionListInfo);
        }

        private void GetQuestionListInfo(object obj)
        {
            List<ModifyQuestionaryList> modifies = new List<ModifyQuestionaryList>();
            var quesionaryList = db.GetQuestionary(UserSingleton.User.Id);

            QuestionaryList.Clear();

            foreach (var item in quesionaryList)
            {
                modifies.Add(item);
            }

            foreach (var item in modifies)
            {
                if (item.Photo != null)
                {
                    (item as ModifyQuestionaryList).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Photo);
                }
            }

            foreach (var item in modifies)
            {
                _questionaryList.Add(item);
            }
        }
    }
}
