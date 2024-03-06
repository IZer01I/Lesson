using Job_vacancy_app.Core;
using Job_vacancy_app.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для QuestionaryAddWindow.xaml
    /// </summary>
    public partial class QuestionaryAddWindow : Window
    {
        public QuestionaryAddWindow()
        {
            InitializeComponent();

            DataContext = new QuestionaryAddViewModel();
        }


        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }

        private void ExpTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VacancyValue.Content = UserSingleton.vacanciesList.Value;
        }
    }
}
