using MyHome.Settings;
using MyHome.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace MyHome.Views
{
    public sealed partial class CanvasControl : UserControl
    {
        public CanvasControl()
        {
            this.InitializeComponent();
            ViewModel = new CanvasControlViewModel();
            myCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch;
            LoadFileAsync(ProgrammSettings.LastFileNameFromCanvas);
        }

        public CanvasControlViewModel ViewModel { get; set; }

        private static string GetFileNameFromButtonName(object sender)
        {
            string myButtonName = ((InkToolbarCustomToolButton)sender).Name;
            string fileName;
            switch (myButtonName)
            {
                case "SaveButtonYellow":
                case "OpenButtonYellow":
                    fileName = "inkfileYellow.gif";
                    break;

                case "SaveButtonGreen":
                case "OpenButtonGreen":
                    fileName = "inkfileGreen.gif";
                    break;

                case "SaveButtonRed":
                case "OpenButtonRed":
                default:
                    fileName = "inkfileRed.gif";
                    break;
            }

            return fileName;
        }

        /// <summary>
        /// Open an image from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string fileName = GetFileNameFromButtonName(sender);
            LoadFileAsync(fileName);
        }

        private async void LoadFileAsync(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await storageFolder.GetFileAsync(fileName);
                if (file != null)
                {
                    // Open a file stream for reading.
                    IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                    // Read from file.
                    using (var inputStream = stream.GetInputStreamAt(0))
                    {
                        await myCanvas.InkPresenter.StrokeContainer.LoadAsync(stream);
                    }
                    stream.Dispose();
                }
            }
            catch (FileNotFoundException)
            {
            }
            
        }

        /// <summary>
        /// Save the image in a File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string fileName = GetFileNameFromButtonName(sender);
            ProgrammSettings.LastFileNameFromCanvas = fileName;
            // Get all strokes on the InkCanvas.
            IReadOnlyList<InkStroke> currentStrokes = myCanvas.InkPresenter.StrokeContainer.GetStrokes();
            // Strokes present on ink canvas.
            if (currentStrokes.Count > 0)
            {
                // aktuellen App-Ordner ermitteln
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                // Datei generieren
                StorageFile file = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                if (file != null)
                {
                    //        // Prevent updates to the file until updates are
                    // finalized with call to CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // Open a file stream for writing.
                    IRandomAccessStream stream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                    // Write the ink strokes to the output stream.
                    using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                    {
                        await myCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                        await outputStream.FlushAsync();
                    }
                    stream.Dispose();

                    // Finalize write so other apps can update file.
                    Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                    {
                        // File saved.
                    }
                    else
                    {
                        // File couldn't be saved.
                    }
                }
            }
        }
    }
}
