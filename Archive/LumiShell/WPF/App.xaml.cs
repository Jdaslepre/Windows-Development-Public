using ManagedShell;
using ManagedShell.AppBar;
using System.Windows;
using WPF.Views;
using WPF.Views.TabletUI;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected void dosomething(object sender, StartupEventArgs e)
        {
            // Initialize the ShellManager
            ShellConfig config = ManagedShell.ShellManager.DefaultShellConfig;
            ManagedShell.ShellManager _shellManager = new ManagedShell.ShellManager(config);

            // Create and show the Taskbar
            Taskbar appBar = new Taskbar(_shellManager, AppBarScreen.FromPrimaryScreen(), AppBarEdge.Bottom, 40);
            appBar.Show();

            //var window = new TabletUIHost();
            //window.Show();

            //var window = new FunctionTestingWindow();
            //window.Show();
        }
    }
}
