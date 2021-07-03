using MyHome.Settings;
using MyHome.ViewModels;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MessagePage : Page
    {
        public MessageViewModel ViewModel { get; set; }
        public MessagePage()
        {
            this.InitializeComponent();
            ViewModel = new MessageViewModel();
        }
    }
}
