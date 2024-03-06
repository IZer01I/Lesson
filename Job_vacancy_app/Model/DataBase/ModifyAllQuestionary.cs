using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.Model.DataBase
{
    internal class ModifyAllQuestionary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephonNumber { get; set; }
        public long CityId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int NationalityId { get; set; }
        public int Experience { get; set; }
        public string Info { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<int> IsTaken { get; set; }
        public string TakenInfo { get; set; }
        public BitmapSource BitmapImage { get; set; }

        private static ModifyAllQuestionary CreateFromContext(Questionary result)
        {
            var modify = new ModifyAllQuestionary
            {
                Id = result.Id,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                LastName = result.LastName,
                Email = result.Email,
                TelephonNumber = result.TelephonNumber,
                CityId = result.CityId,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender,
                NationalityId = result.NationalityId,
                Experience = result.Experience,
                Info = result.Info,
                Photo = result.Photo,
                TakenInfo = result.IsTaken.ToString()
            };

            if (result.IsTaken == null || result.IsTaken == 2)
                modify.TakenInfo = "На рассмотрении";
            if (result.IsTaken == 1)
                modify.TakenInfo = "Отказ";
            if (result.IsTaken == 0)
                modify.TakenInfo = "Принят";

            return modify;
        }

        public static implicit operator ModifyAllQuestionary(Questionary result) => CreateFromContext(result);
    }
}

