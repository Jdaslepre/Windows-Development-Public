using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Radios;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Shell.Shell.ActionCenter.QuickSettings_Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WIFI : Page
    {
        public WIFI()
        {
            this.InitializeComponent();
        }

        private async void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var radios = await Radios.GetRadiosAsync();
            var wifiRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.WiFi);
            if (wifiRadio != null)
            {
                await wifiRadio.SetStateAsync(WIFIToggleSwitch.IsOn ? RadioState.On : RadioState.Off);
            }
        }
    }
}
