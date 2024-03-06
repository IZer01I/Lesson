using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Job_vacancy_app.ViewModel
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected virtual void SetPropertyChanged<T>([Required] ref T dist, T value, [CallerMemberName] string prop = null)
        {
            dist = value;
            OnPropertyChanged(prop);
        }
    }
}
