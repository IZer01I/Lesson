using Job_vacancy_app.Model.DataBase;
using System;
using System.Windows.Media.Imaging;

namespace Job_vacancy_app.Model
{
    public class ModifyVacanciesList
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public int NumberOfSeats { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public BitmapSource BitmapImage { get; set; }

        private static ModifyVacanciesList CreateFromContext(GetVacanciesByDepartment_Result result)
        {
            var modify = new ModifyVacanciesList
            {
                Id = result.Id,
                Value = result.Value,
                DepartmentId = result.DepartmentId,
                NumberOfSeats = result.NumberOfSeats,
                Description = result.Description,
                Image = result.Image,
            };
            return modify;
        }

        public static implicit operator ModifyVacanciesList(GetVacanciesByDepartment_Result result) => CreateFromContext(result);
    }
}
