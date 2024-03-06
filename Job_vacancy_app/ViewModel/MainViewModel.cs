using Job_vacancy_app.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Job_vacancy_app.Model;
using Job_vacancy_app.Core;
using Job_vacancy_app.View;

namespace Job_vacancy_app.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        Job_vacancyEntities db;

        private ObservableCollection<ModifyDepartmentList> _departmentsList;
        private ObservableCollection<ModifyVacanciesList> _vacanciesList;

        public ObservableCollection<ModifyDepartmentList> DepartmentsList { get => _departmentsList; set => SetPropertyChanged(ref _departmentsList, value); }
        public ObservableCollection<ModifyVacanciesList> VacanciesList { get => _vacanciesList; set => SetPropertyChanged(ref _vacanciesList, value); }

        public MainViewModel()
        {
            db = new Job_vacancyEntities();

            DepartmentsList = new ObservableCollection<ModifyDepartmentList>();
            VacanciesList = new ObservableCollection<ModifyVacanciesList>();
        }

        public void GetDepartmentsListInfo()
        {
            DepartmentsList.Clear();

            List<ModifyDepartmentList> modifies = new List<ModifyDepartmentList>();

            var departmentsList = db.Department.ToList();

            foreach (var item in departmentsList)
            {
                modifies.Add(item);
            }

            foreach (var item in modifies)
            {
                if (item.Image != null)
                {
                    (item as ModifyDepartmentList).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Image);
                }
            }

            foreach (var item in modifies)
            {
                _departmentsList.Add(item);
            }
        }

        public void GetVacanciesListInfo()
        {
            try
            {
                VacanciesList.Clear();

                List<ModifyVacanciesList> modifies = new List<ModifyVacanciesList>();

                foreach (var item in db.GetVacanciesByDepartment(UserSingleton.DepartmentId))
                {
                    modifies.Add(item);
                }

                foreach (var item in modifies)
                {
                    if (item.Image != null)
                    {
                        (item as ModifyVacanciesList).BitmapImage = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Image);
                    }
                }

                foreach (var item in modifies)
                {
                    _vacanciesList.Add(item);
                }
            }
            catch { }
        }
    }
}