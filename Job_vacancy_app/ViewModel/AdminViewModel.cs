using Job_vacancy_app.Command;
using Job_vacancy_app.Core;
using Job_vacancy_app.Model.DataBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.ViewModel
{
    internal class AdminViewModel : BaseViewModel
    {
        Job_vacancyEntities db;

        private string _userFirstName;
        private string _userMiddleName;
        private string _userLastName;

        private ObservableCollection<ModifyAllQuestionary> _questionaryList;

        public string UserFirstName { get => _userFirstName; set => SetPropertyChanged(ref _userFirstName, value); }
        public string UserMiddleName { get => _userMiddleName; set => SetPropertyChanged(ref _userMiddleName, value); }
        public string UserLastName { get => _userLastName; set => SetPropertyChanged(ref _userLastName, value); }

        public ObservableCollection<ModifyAllQuestionary> QuestionaryList { get => _questionaryList; set => SetPropertyChanged(ref _questionaryList, value); }


        public AdminViewModel()
        {
            db = new Job_vacancyEntities();

            QuestionaryList = new ObservableCollection<ModifyAllQuestionary>();

            _userFirstName = UserSingleton.User.FirstName;
            _userMiddleName = UserSingleton.User.MiddleName;
            _userLastName = UserSingleton.User.LastName;
        }

        public void GetQuestionListInfo()
        {
            QuestionaryList.Clear();

            List<ModifyAllQuestionary> modifies = new List<ModifyAllQuestionary>();
            var questionaryList = db.Questionary.ToList();

            foreach (var item in questionaryList)
            {
                modifies.Add(item);
            }

            foreach (var item in modifies)
            {
                if (item.Photo != null)
                {
                    (item as ModifyAllQuestionary).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Photo);
                }
            }

            foreach (var item in modifies)
            {
                QuestionaryList.Add(item);
            }
        }
    }
}
