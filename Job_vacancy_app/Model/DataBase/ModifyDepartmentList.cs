using System.Windows.Media.Imaging;

namespace Job_vacancy_app.Model.DataBase
{
    internal class ModifyDepartmentList
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public byte[] Image { get; set; }
        public BitmapSource BitmapImage { get; set; }

        private static ModifyDepartmentList CreateFromContext(Department result)
        {
            var modify = new ModifyDepartmentList
            {
                Id = result.Id,
                Value = result.Value,
                Image = result.Image,
            };
            return modify;
        }

        public static implicit operator ModifyDepartmentList(Department result) => CreateFromContext(result);
    }
}
