using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Job_vacancy_app.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        private string _folderPath;

        public string FolderPath { get => _folderPath; set => SetPropertyChanged(ref _folderPath, value); }

        StreamReader sr;

        SettingsViewModel()
        {
            sr = new StreamReader("FileSavePath.txt");

            FolderPath = sr.ReadLine();
        }
    }
}
