using MyHome.ViewModels;
using RSSFeed;
using System;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class RSSFeedControl : UserControl
    {
        public RSSFeedControlViewModel ViewModel { get; set; }
        public RSSFeedControl()
        {
            this.InitializeComponent();
            ViewModel = new RSSFeedControlViewModel();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                FeedItem item = (FeedItem)ListView.SelectedItem;
                WebView1.Source = new Uri(item.Link);
            }
        }
    }
}
