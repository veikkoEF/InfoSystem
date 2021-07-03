using MyHome.ViewModels;
using RSSFeed;
using System;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class NewsFeedPage : Page
    {
        public NewsFeedViewModel ViewModel { get; set; }
        public NewsFeedPage()
        {
            this.InitializeComponent();
            ViewModel = new NewsFeedViewModel();
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
