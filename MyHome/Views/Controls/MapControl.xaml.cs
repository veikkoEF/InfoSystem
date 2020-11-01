using MyHome.Settings;
using MyHome.ViewModels;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class MapControl : UserControl
    {
        public MapControlViewModel ViewModel { get; set; }
        public MapControl()
        {
            ViewModel = new MapControlViewModel();
            this.InitializeComponent();
            MyMapControl.MapServiceToken = ProgrammSettings.MapsAPIKey;
        }
    }
}
