
using System.ComponentModel;
using WPF.ShellManager;

namespace WPF.ViewModels
{
    public class TaskbarViewModel : INotifyPropertyChanged
    {
        public DateTimeManager _dateTime;

        public DateTimeManager DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        //public ShellManager ShellManager => new ShellManager();

        public TaskbarViewModel()
        {
            _dateTime = new DateTimeManager();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}