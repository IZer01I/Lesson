using Job_vacancy_app.Model;
using Job_vacancy_app.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_vacancy_app.Core
{
    public static class UserSingleton
    {
        public static User User {  get; set; }
        public static Questionary Questionary {  get; set; }
        public static int QuestionaryId {  get; set; }
        public static int DepartmentId { get; set; }
        public static ModifyVacanciesList vacanciesList { get; set; }
    }
}
