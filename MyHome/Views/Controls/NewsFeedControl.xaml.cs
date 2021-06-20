using MyHome.ViewModels;
using RSSFeed;
using System;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class NewsFeedControl : UserControl
    {
        public NewsFeedControlViewModel ViewModel { get; set; }
        public NewsFeedControl()
        {
            this.InitializeComponent();
            ViewModel = new NewsFeedControlViewModel();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                FeedItem item = (FeedItem)ListView.SelectedItem;
                WebView1.Navigate(new Uri(item.Link));
            }
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
