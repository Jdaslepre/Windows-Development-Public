// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;
using CustomShellTesting.ShellUtilities;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using System;
using System.IO;
using System.Windows;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.UserDataTasks.DataProvider;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using Windows.Storage.FileProperties;
using System.Windows.Input;
using Microsoft.UI.Xaml.Media;





// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomShellTesting.Shell.Taskbar
{
    public class TaskbarItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ImageSource Icon { get; set; }
        public ICommand LaunchCommand { get; set; }
    }

    public sealed partial class TaskbarControl : Microsoft.UI.Xaml.Controls.UserControl
    {
        public ViewModel viewmodel => (ViewModel)DataContext;
        ObservableCollection<TaskbarItem> taskbarItems = new ObservableCollection<TaskbarItem>();


        public TaskbarControl()
        {
            this.InitializeComponent();
            DataContext = new ViewModel();

            var stackLayout = new StackLayout();
            stackLayout.Orientation = Microsoft.UI.Xaml.Controls.Orientation.Horizontal;

            TaskList.Layout = stackLayout;

        }

        private void Start_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            //ShellManager.OpenStartMenu();
            ShellManager.HideProgramManager();
        }

        private void TaskView_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ShellManager.ShowProgramManager();
        }

        private void QuickSettings_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            //ShellManager.ShowTaskbar();

        }

        private void ShowDesktop_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            //ShellManager.HideTaskbar();
        }
    }
}
