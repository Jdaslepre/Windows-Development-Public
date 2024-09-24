using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Shapes;
using Windows.UI.Xaml.Media.Imaging;
using WPF.ViewModels.Global.Taskbar;
using WPF.ViewModels.Tablet;

namespace WPF.Views.TabletUI
{
    /// <summary>
    /// Interaction logic for TabletUIHost.xaml
    /// </summary>
    public partial class TabletUIHost : Window
    {
        public TabletUIHost()
        {
            InitializeComponent();
            DataContext = new TabletShellViewModel();
        }

        public class TaskbarItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public BitmapImage Icon { get; set; }
        }

        public class PinnedItems : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ObservableCollection<TaskbarItem> TaskbarItems { get; } = new ObservableCollection<TaskbarItem>();

            public async Task LoadPinnedItemsAsync()
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var fullPath = System.IO.Path.Combine(appData, "Microsoft\\Internet Explorer\\Quick Launch\\User Pinned\\TaskBar");

                var shortcuts = await GetShortcutsAsync(fullPath);

                foreach (var shortcut in shortcuts)
                {
                    var taskbarItem = new TaskbarItem
                    {
                        Name = System.IO.Path.GetFileNameWithoutExtension(shortcut),
                        Path = shortcut
                    };

                    var icon = await ExtractIconAsync(shortcut);
                    if (icon != null)
                    {
                        taskbarItem.Icon = icon;
                    }

                    TaskbarItems.Add(taskbarItem);
                }
            }

            private async Task<List<string>> GetShortcutsAsync(string directory)
            {
                var shortcuts = new List<string>();

                if (Directory.Exists(directory))
                {
                    var files = await Task.Run(() => Directory.GetFiles(directory));
                    shortcuts.AddRange(files.Where(f => System.IO.Path.GetExtension(f) == ".lnk"));
                }

                return shortcuts;
            }

            private async Task<BitmapImage> ExtractIconAsync(string shortcutPath)
            {
                /*
                var icon = await Task.Run(() =>
                {
                    return Icon.ExtractAssociatedIcon(shortcutPath);
                });
                */
                
                //if (icon != null)
                //{
                    var bitmap = new BitmapImage();
                    using (var memoryStream = new MemoryStream())
                    {
                        //await Task.Run(() => icon.Save(memoryStream, ImageFormat.Png));
                        memoryStream.Position = 0;
                        await bitmap.SetSourceAsync(memoryStream.AsRandomAccessStream());
                    }
                    return bitmap;
                //}

                return null;
            }
        }
    }
}
