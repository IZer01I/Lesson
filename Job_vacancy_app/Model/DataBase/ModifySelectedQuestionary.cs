using System;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.Model.DataBase
{
    internal class ModifySelectedQuestionary
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
        public string FileExtension { get; set; }

        public BitmapSource BitmapImage { get; set; }

        private static ModifySelectedQuestionary CreateFromContext(GetSelectedQuestionary1_Result result)
        {
            var modify = new ModifySelectedQuestionary
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
                FileExtension = result.FileExtension
            };

            return modify;
        }

        public static implicit operator ModifySelectedQuestionary(GetSelectedQuestionary1_Result result) => CreateFromContext(result);
    }
}
