using MyHome.Settings;
using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public MapViewModel ViewModel { get; set; }
        public MapPage()
        {
            this.InitializeComponent();
            ViewModel = new MapViewModel();
            MyMap.MapServiceToken = ProgrammSettings.MapsAPIKey;
        }
    }
}
