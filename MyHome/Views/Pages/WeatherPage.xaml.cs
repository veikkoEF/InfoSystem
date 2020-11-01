using MyHome.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        public WeatherPage()
        {
            this.InitializeComponent();

            WeatherControl.Visibility = Visibility.Collapsed;
            WeatherControl2.Visibility = Visibility.Collapsed;
            switch (ProgrammSettings.WeatherModus)
            {
                case WeatherMode.TreeDaysSimple:
                    {
                        WeatherControl.Visibility = Visibility.Visible;
                        break;
                    }
                case WeatherMode.TreeDaysComfort:
                default:
                    {
                        WeatherControl2.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }
    }
}
