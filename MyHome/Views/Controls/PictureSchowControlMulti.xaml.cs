using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class PictureSchowControlMulti : UserControl
    {
        public MultiViewPictureShowControlViewModel ViewModel { get; set; }

        public PictureSchowControlMulti()
        {
            this.InitializeComponent();
            ViewModel = new MultiViewPictureShowControlViewModel();
        }

        private void userControl_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StopUITimer();
        }

        private void userControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.StartUITimer();
        }
    }
}