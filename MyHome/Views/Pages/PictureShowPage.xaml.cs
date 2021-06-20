using MyHome.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            PictureShowControlCarousel.Visibility = Visibility.Collapsed;

            switch (ProgrammSettings.ArtOfPicturePresentation)
            {
                case ArtOfPicturePresentation.SinglePicture:
                    {
                        PictureShowControlSingleView.Visibility = Visibility.Visible;
                        break;
                    }
                case ArtOfPicturePresentation.CarouselPicture:
                    {
                        PictureShowControlCarousel.Visibility = Visibility.Visible;
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