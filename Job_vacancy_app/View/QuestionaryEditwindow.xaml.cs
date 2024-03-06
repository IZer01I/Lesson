using Job_vacancy_app.Core;
using Job_vacancy_app.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Job_vacancy_app.View
{
    /// <summary>
    /// Логика взаимодействия для QuestionaryEditwindow.xaml
    /// </summary>
    public partial class QuestionaryEditwindow : Window
    {
        public QuestionaryEditwindow()
        {
            InitializeComponent();

            DataContext = new QuestionaryEditViewModel();
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
    }
}
