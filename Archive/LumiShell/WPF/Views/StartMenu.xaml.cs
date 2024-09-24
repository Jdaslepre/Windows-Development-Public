using System;
using System.Windows;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
            WindowSetup();
        }

        private void Window_LocationChanged(object sender, EventArgs e) { WindowSetup(); }

        private void WindowSetup()
        {
            Left = 0;
            Top = Taskbar.taskbar.Top - Height;
        }

    }
}
