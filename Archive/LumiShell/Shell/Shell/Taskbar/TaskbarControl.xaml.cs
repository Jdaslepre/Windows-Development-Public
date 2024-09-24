using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;
using ShellApp.ShellUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace ShellApp.Shell.Taskbar
{
    public sealed partial class TaskbarControl : UserControl
    {
        public static ToggleButton startbutton { get; set; }
        public static MenuFlyoutItem closelumi { get; set; }
        public static ToggleButton actioncenterbutton { get; set; }

        public TaskbarControl()
        {
            this.InitializeComponent();
            //DataContext = new ViewModel();

            startbutton = StartButton;
            closelumi = CloseLumi;
            actioncenterbutton = ActionCenterFlyoutButton;

            var stackLayout = new StackLayout();
            stackLayout.Orientation = Orientation.Horizontal;

            //TaskList.ItemsSource = LoadPinnedItems();
            TaskList.Layout = stackLayout;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ShellManager.OpenStartMenu();
        }

        private void TaskView_Click(object sender, RoutedEventArgs e)
        {
            ShellManager.OpenTaskView();
        }

        private void ShowTaskbarContextMenu(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            taskbarflyout.ShowAt((FrameworkElement)(sender as UIElement));
        }
    }
}
