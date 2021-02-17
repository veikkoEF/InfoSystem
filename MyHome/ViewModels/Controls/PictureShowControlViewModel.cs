#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using DropboxAPI;
using MyHome.Helpers;
using MyHome.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace MyHome.ViewModels
{
    public abstract class PictureShowControlViewModel : Observable
    {
        private protected readonly DispatcherTimer timer;

        public ObservableCollection<string> ItemsList { get; } = new ObservableCollection<string>();

        protected PictureShowControlViewModel()
        {

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(ProgrammSettings.TimeInSecondToChangePicture)
            };
            timer.Stop();
            timer.Tick += TimerTickAsync;

            GetItemsFromCurrentDirAsync();
           
        }

        // <summary>
        /// Wird ausgelöst, wenn ein Bildwechsel stattfinden soll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void TimerTickAsync(object sender, object e);

        private async void GetItemsFromCurrentDirAsync()
        {
            StorageFolder homeStorageFolder = ApplicationData.Current.LocalFolder;
            var currentFolder = await homeStorageFolder.GetFolderAsync(ProgrammSettings.NameOfCurrentDir);
            IReadOnlyList<StorageFile> files = await currentFolder.GetFilesAsync();
            if ((files != null) && (files.Count > 0))
            {
                ItemsList.Clear();
                foreach (var item in files)
                {
                    ItemsList.Add(item.Path);
                }
                timer.Start();
            }
        }


    }
}
