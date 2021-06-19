using DropboxAPI;
using MyHome.Settings;
using System.IO;
using System.Linq;
using Windows.UI.Xaml.Controls;
using MyHome.ViewModels;
using System;

namespace MyHome.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPageViewModel ViewModel { get; set; }
        public SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = new SettingsPageViewModel();
            comboboxRSSFeed.SelectedItem = ViewModel.NewsFeed;
        }


        private void ButtonDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.SelectedObjects= GridView.SelectedItems.ToArray();
            ViewModel.OnDeleteFilesCommandExecute();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Nicht verwendete Parameter überprüfen", Justification = "<Ausstehend>")]
        private void myColorPickerForground_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            ViewModel.ForegroundColor = sender.Color;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Nicht verwendete Parameter überprüfen", Justification = "<Ausstehend>")]
        private void myColorPickerBackground_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            ViewModel.BackgroundColor = sender.Color;
        }

       

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string nameOfDir = (e.ClickedItem).ToString();
            ViewModel.SelectedDirName = nameOfDir;
            ViewModel.UpdateFilesNamesAsync(nameOfDir);
        }

        private void comboboxRSSFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.NewsFeed = comboboxRSSFeed.Text;
        }

    }
}
