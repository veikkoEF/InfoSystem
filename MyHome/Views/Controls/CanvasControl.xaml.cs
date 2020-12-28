using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class CanvasControl : UserControl
    {
        public CanvasControlViewModel ViewModel { get; set; }
        public CanvasControl()
        {
            this.InitializeComponent();
            ViewModel = new CanvasControlViewModel();
            myCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch;
        }
    }
}
