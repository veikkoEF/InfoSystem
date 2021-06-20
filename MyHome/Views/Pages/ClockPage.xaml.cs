using MyHome.Settings;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ClockPage : Page
    {
        public ClockPage()
        {
            this.InitializeComponent();
            ViewBoxClockControlAnalog.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ViewBoxClockControlDigital.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            switch (ProgrammSettings.ClockModus)
            {
                case ClockMode.Analog:
                    ViewBoxClockControlAnalog.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;

                case ClockMode.Digital:
                default:
                    ViewBoxClockControlDigital.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    break;
            }
        }
    }
}