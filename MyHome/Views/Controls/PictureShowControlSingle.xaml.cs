using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class PictureShowControlSingle : UserControl
    {
        public SingleViewPictureShowControlViewModel ViewModel { get; set; }
        public PictureShowControlSingle()
        {
            this.InitializeComponent();
            ViewModel = new SingleViewPictureShowControlViewModel();
        }

        private void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StartUITimer();
        }

        private void UserControl_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StopUITimer();
        }
    }
}
