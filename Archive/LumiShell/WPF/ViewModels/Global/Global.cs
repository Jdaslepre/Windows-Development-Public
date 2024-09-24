using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WPF.ShellManager;

namespace WPF.ViewModels.Global
{
    public class Global : INotifyPropertyChanged
    {


        private DateTimeManager _timeManager;

        public DateTimeManager TimeManager
        {
            get { return _timeManager; }
            set
            {
                _timeManager = value;
                OnPropertyChanged(nameof(TimeManager));
            }
        }


        public Global()
        {
            _timeManager = new DateTimeManager();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
