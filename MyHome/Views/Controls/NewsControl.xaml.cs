using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class NewsControl : UserControl
    {
        public NewsControlViewModel ViewModel { get; set; }
        public NewsControl()
        {
            this.InitializeComponent();
            ViewModel = new NewsControlViewModel();
        }

        private void userControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StartUITimer();
        }

        private void userControl_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StopUITimer();
        }
    }
}
