using Job_vacancy_app.Model.DataBase;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Windows;

namespace Job_vacancy_app.Core
{
    internal class DbManager
    {
        Job_vacancyEntities db = new Job_vacancyEntities();

        public async Task<bool> Authority(string login, string password)
        {
            try
            {
                var userList = await db.User.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

                if (userList != null)
                {
                    UserSingleton.User = userList;
                    return true;
                }

                else { return false; }
            }
            catch
            { return false; }
        }

        public async Task<bool> CheckUser(string login)
        {
            try
            {
                var userList = await db.User.FirstOrDefaultAsync(x => x.Login == login);

                if (userList != null) return true;

                else { return false; }

            }
            catch { MessageBox.Show("Не удалось проверить данные!"); return false; }
        }

        public async Task<bool> Registry(User users)
        {
            try
            {
                UserSingleton.User = users;

                db.User.Add(users);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            { return false; }
        }

        public async Task<bool> RegQuestionary(Questionary questionary)
        {
            try
            {
                db.Questionary.Add(questionary);

                await db.SaveChangesAsync();

                UserSingleton.Questionary = questionary;

                db.AddUserHasQuestionary(UserSingleton.User.Id, UserSingleton.Questionary.Id);
                db.AddVacancyHasQuestionary(UserSingleton.Questionary.Id, UserSingleton.vacanciesList.Id);

                await db.SaveChangesAsync();
                return true;
            }
            catch { MessageBox.Show("Ошибка загрузки анкеты!"); return false; }
        }

        public async Task<bool> EditQuestionary(Questionary questionary)
        {
            try
            {
                db.Questionary.AddOrUpdate(questionary);
                await db.SaveChangesAsync();
                return true;
            }
            catch { MessageBox.Show("Ошибка выполнения операции!"); return false; }
        }

        public async Task<bool> DeleteQuestionary(Questionary questionary)
        {
            try
            {
                int vacancyId = 0;

                foreach (var item in db.GetVacancyByQuestionary(UserSingleton.QuestionaryId))
                {
                    vacancyId = item.Id;
                }

                db.Questionary.Attach(questionary);

                db.DeleteQuest(UserSingleton.User.Id, vacancyId, questionary.Id);

                db.Questionary.Remove(questionary);
                await db.SaveChangesAsync();

                return true;
            }
            catch { MessageBox.Show("Ошибка удаления анкеты!"); return false; }
        }
    }
}
