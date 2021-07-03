using MyHome.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace MyHome.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CanvasPage : Page
    {
        public CanvasPage()
        {
            this.InitializeComponent();
            myCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch;
            LoadFileAsync(ProgrammSettings.LastFileNameFromCanvas);
        }



        /// <summary>
        /// Open an image from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string fileName = ((InkToolbarCustomToolButton)sender).Tag.ToString();
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
            string fileName = ((InkToolbarCustomToolButton)sender).Tag.ToString();
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
                }
            }
        }


    }
}
