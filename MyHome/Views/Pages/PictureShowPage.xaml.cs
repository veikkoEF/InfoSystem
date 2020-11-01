using MyHome.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class PictureShowPage : Page
    {
        public PictureShowPage()
        {
            this.InitializeComponent();

            PictureShowControlSingleView.Visibility = Visibility.Collapsed;
            PictureShowControlMultiView.Visibility = Visibility.Collapsed;
            switch (ProgrammSettings.ArtOfPicturePresentation)
            {
                case ArtOfPicturePresentation.SinglePicture:
                    {
                        PictureShowControlSingleView.Visibility = Visibility.Visible;
                        break;
                    }
                case ArtOfPicturePresentation.MultiPicture:
                default:
                    {
                        PictureShowControlMultiView.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }
    }
}
