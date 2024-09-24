using ManagedShell;
using ManagedShell.AppBar;
using ShellApp.Shell.Start;
using ShellApp.Shell.Taskbar;
using System;
using System.Windows;
using WPF.ShellManager;
using WPF.ViewModels;
using WPF.ViewModels.Global;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for Taskbar.xaml
    /// </summary>
    public partial class Taskbar : AppBarWindow
    {
        public static Window start { get; set; }
        public static Window taskbar { get; set; }
        public static Window actioncenter { get; set; }
        public Taskbar(ManagedShell.ShellManager shellManager, AppBarScreen screen, AppBarEdge edge, double desiredHeight) : base(shellManager.AppBarManager, shellManager.ExplorerHelper, shellManager.FullScreenHelper, screen, edge, desiredHeight)
        {
            InitializeComponent();
            DataContext = new Global();
            _explorerHelper.HideExplorerTaskbar = true;
            this.LocationChanged += Window_LocationChanged;
            taskbar = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            start = new StartMenu();
            start.Show();
            start.Visibility = Visibility.Hidden;
            actioncenter = new ActionCenterComposer();
            actioncenter.Show();
            actioncenter.Visibility = Visibility.Hidden;

            TaskbarControl.startbutton.Checked += Startbutton_sometypeofinteraction;
            TaskbarControl.startbutton.Unchecked += Startbutton_sometypeofinteraction;
            TaskbarControl.actioncenterbutton.Click += LaunchActionCenterWindow;
            TaskbarControl.closelumi.Click += closelumi_Clicked;
        }

        private void Startbutton_sometypeofinteraction(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (TaskbarControl.startbutton.IsChecked == true)
            {
                start.Left = 0;
                start.Top = Top - start.Height;
                start.Visibility = Visibility.Visible;
                StartPlaceholder.openstartanimation.Begin();
            }
            if (TaskbarControl.startbutton.IsChecked == false)
            {

                StartPlaceholder.closestartanimation.Begin();
                StartPlaceholder.closestartanimation.Completed += (s, _) =>
                {
                    start.Visibility = Visibility.Hidden;
                };
            }
        }

        protected override void CustomClosing()
        {
            if (AllowClose)
            {
                _explorerHelper.HideExplorerTaskbar = false;
            }
        }

        private void Close()
        {
            this.AllowClose = true;
            App.Current.Shutdown();
        }

        public override void SetPosition()
        {
            base.SetPosition();
        }

        private void Window_LocationChanged(object? sender, EventArgs e)
        {
            double desiredTop = Screen.Bounds.Bottom / DpiScale - Height;

            if (Top != Screen.Bounds.Bottom / DpiScale - Height)
            {
                Top = Screen.Bounds.Bottom / DpiScale - Height;
            }
        }

        private void LaunchActionCenterWindow(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (TaskbarControl.actioncenterbutton.IsChecked == true)
            {

                actioncenter.Left = Screen.Bounds.Right - Width;
                actioncenter.Top = Top - start.Height;
                actioncenter.Visibility = Visibility.Visible;
                //StartPlaceholder.openstartanimation.Begin();
            }
            if (TaskbarControl.actioncenterbutton.IsChecked == false)
            {

                //StartPlaceholder.closestartanimation.Begin();
                //StartPlaceholder.closestartanimation.Completed += (s, _) =>
                {
                    actioncenter.Visibility = Visibility.Hidden;
                };
            }
        }

        private void closelumi_Clicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Close();
        }
    }
}
