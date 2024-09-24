using CustomShellTesting.ShellUtilities;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CustomShellTesting.Shell.Taskbar
{
    public class ViewModel// : INotifyPropertyChanged
    {
        public DateTimeManager TimeManager => new DateTimeManager();
        public ShellManager ShellManager => new ShellManager();
        //private string _property;

        //public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            //Property = SomeProperty.WithOtherStuff;
          
        }
        /*
        public string Property
        {
            get { return _property; }
            set
            {
                _property = value;
                OnPropertyChanged(nameof(Property));
            }
        }
        /*        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}