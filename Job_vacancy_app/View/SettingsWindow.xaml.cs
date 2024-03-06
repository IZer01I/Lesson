using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        StreamReader sr;
        StreamWriter sw;

        public SettingsWindow()
        {
            InitializeComponent();
            sr = new StreamReader("FileSavePath.txt");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FolderPathTb.Text = sr.ReadLine();
            sr.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sw = new StreamWriter("FileSavePath.txt", false);
                sw.Write(FolderPathTb.Text.Replace("\\", "/") + "/");
                sw.Close();

                MessageBox.Show("Путь сохранен успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения, проверьте правильность пути!");
            }
        }
    }
}
