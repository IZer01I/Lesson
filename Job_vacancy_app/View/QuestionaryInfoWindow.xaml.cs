using Job_vacancy_app.Core;
using Job_vacancy_app.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для QuestionaryInfoWindow.xaml
    /// </summary>
    public partial class QuestionaryInfoWindow : Window
    {
        AdminWindow window = new AdminWindow();
        public QuestionaryInfoWindow()
        {
            InitializeComponent();

            DataContext = new QuestionaryInfoViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { 
            (DataContext as QuestionaryInfoViewModel).EditInfoInDB();

            window.Show();
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as QuestionaryInfoViewModel).DownloadFile();
        }
    }
}
