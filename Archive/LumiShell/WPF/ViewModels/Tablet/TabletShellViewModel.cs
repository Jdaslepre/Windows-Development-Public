using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using WPF.ViewModels.Global.Taskbar;

namespace WPF.ViewModels.Tablet
{
    public class TabletShellViewModel : INotifyPropertyChanged
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SystemParametersInfo(uint uAction, uint uParam, StringBuilder lpvParam, uint init);

        private const uint SPI_GETDESKWALLPAPER = 0x0073;

        private BitmapImage _wallpaper;

        public BitmapImage Wallpaper
        {
            get => _wallpaper;
            set
            {
                _wallpaper = value;
                OnPropertyChanged();
            }
        }

        public TabletShellViewModel()
        {
            GetWallpaper();
        }

        public async void GetWallpaper()
        {
            // Get the current wallpaper path
            StringBuilder wallPaperPath = new StringBuilder(200);
            if (SystemParametersInfo(SPI_GETDESKWALLPAPER, 200, wallPaperPath, 0))
            {
                Uri imageUri = new Uri(wallPaperPath.ToString());
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                Wallpaper = imageBitmap;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
