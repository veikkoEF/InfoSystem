using MyHome.ViewModels;
using System.Collections.Generic;
using Windows.UI.Input.Inking;
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

        private void InkToolbarCustomToolButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //// Get all strokes on the InkCanvas.
            //IReadOnlyList<InkStroke> currentStrokes = myCanvas.InkPresenter.StrokeContainer.GetStrokes();

            //// Strokes present on ink canvas.
            //if (currentStrokes.Count > 0)
            //{
            //    // Let users choose their ink file using a file picker.
            //    // Initialize the picker.
            //    Windows.Storage.Pickers.FileSavePicker savePicker =
            //        new Windows.Storage.Pickers.FileSavePicker();
            //    savePicker.SuggestedStartLocation =
            //        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            //    savePicker.FileTypeChoices.Add(
            //        "GIF with embedded ISF",
            //        new List<string>() { ".gif" });
            //    savePicker.DefaultFileExtension = ".gif";
            //    savePicker.SuggestedFileName = "InkSample";

            //    // Show the file picker.
            //    Windows.Storage.StorageFile file =
            //        await savePicker.PickSaveFileAsync();
            //    // When chosen, picker returns a reference to the selected file.
            //    if (file != null)
            //    {
            //        // Prevent updates to the file until updates are 
            //        // finalized with call to CompleteUpdatesAsync.
            //        Windows.Storage.CachedFileManager.DeferUpdates(file);
            //        // Open a file stream for writing.
            //        IRandomAccessStream stream =
            //            await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
            //        // Write the ink strokes to the output stream.
            //        using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
            //        {
            //            await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
            //            await outputStream.FlushAsync();
            //        }
            //        stream.Dispose();

            //        // Finalize write so other apps can update file.
            //        Windows.Storage.Provider.FileUpdateStatus status =
            //            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);

            //        if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
            //        {
            //            // File saved.
            //        }
            //        else
            //        {
            //            // File couldn't be saved.
            //        }
            //    }
            //    // User selects Cancel and picker returns null.
            //    else
            //    {
            //        // Operation cancelled.
            //    }
            //}
        }
    }
}
