using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class MessageControl : UserControl
    {
        public MessageControlViewModel ViewModel { get; set; }

        public MessageControl()
        {
            this.InitializeComponent();
            ViewModel = new MessageControlViewModel();
        }
    }
}