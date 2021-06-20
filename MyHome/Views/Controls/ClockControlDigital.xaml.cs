using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class ClockControlDigital : UserControl
    {
        public ClockControlDigitalViewModel ViewModel { get; set; }

        public ClockControlDigital()
        {
            ViewModel = new ClockControlDigitalViewModel();
            this.InitializeComponent();
        }
    }
}