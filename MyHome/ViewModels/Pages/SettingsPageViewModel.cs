#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using DropboxAPI;
using Hassie.NET.API.NewsAPI.API.v2;
using MyHome.Helpers;
using MyHome.Settings;
using OpenWeatherForecast;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.System;
using Windows.UI.Popups;

namespace MyHome.ViewModels
{
    public class SettingsPageViewModel : Observable
    {
        private bool breakUpload;
        private int numberOfFiles;
        private int progress;
        private object[] selectedObjects;
        private string selectedDirName;

        public string SelectedDirName
        {
            get
            {
                return selectedDirName;
            }
            set
            {

                selectedDirName = value;
                OnPropertyChanged(nameof(SelectedDirName));
            }
        }


        /// <summary>
        /// Generiert ein neues Objekte der Klasse
        /// </summary>
        public SettingsPageViewModel()
        {
            CheckLocationCommand = new RelayCommand(OnCheckLocationCommandExecuteAsync);
            DownloadFromDropBoxCommand = new RelayCommand(OnDownloadFromDropBoxCommandExecuteAsync);
            UploadFileInDropboxCommand = new RelayCommand(OnUploadFileInDropboxCommandExecuteAsync);
            CreateFolderAndUploadFilesInDropboxCommand = new RelayCommand(OnCreateFolderAndUploadFilesInDropboxCommandAsync);
            GetLocationCommand = new RelayCommand(OnGetLocationCommandExecuteAsync);
            RestartAppCommmand = new RelayCommand(OnRestartCommandExecuteAsync);
            ShutDownCommand = new RelayCommand(OnShutDownCommandExecute);
            SaveKeysCommand = new RelayCommand(OnSaveKeysCommandExecute);
            LoadKeysCommand = new RelayCommand(OnLoadKeysCommandExecute);
            BreakUploadCommand = new RelayCommand(OnBreakUploadCommandExecute);
            DeleteFilesCommand = new RelayCommand(OnDeleteFilesCommandExecute);
            DeleteDirectoryCommand = new RelayCommand(OnDeleteDirectoryCommandExecute);
            DeleteAllMessageFromBaaS = new RelayCommand(OnDeleteAllMessageFromBaaSExecute);
            FileNames = new ObservableCollection<string>();
            DirNames = new ObservableCollection<string>();

            UpdateDirNamesAsync();
        }

        private  void OnDeleteAllMessageFromBaaSExecute()
        {
            BaaSCommunication baas = new BaaSCommunication();
            baas.DeleteMessagesAsync();
        }

        private async void OnDeleteDirectoryCommandExecute()
        {
            // Rückfrage
            // https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/dialogs-and-flyouts/dialogs
            Windows.UI.Xaml.Controls.ContentDialog locationPromptDialog = new Windows.UI.Xaml.Controls.ContentDialog
            {
                Title = "Bestätigung",
                Content = "Möchten Sie den gesamten Ordner löschen?",
                CloseButtonText = "Abbrechen",
                PrimaryButtonText = "Löschen"
            };

            Windows.UI.Xaml.Controls.ContentDialogResult result = await locationPromptDialog.ShowAsync();
            // wenn löschen bestätigt
            if (result == Windows.UI.Xaml.Controls.ContentDialogResult.Primary)
            {
                // Ordner lokal löschen
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFolder currentFolder = await storageFolder.GetFolderAsync(SelectedDirName);
                await currentFolder.DeleteAsync();
                // Ordner in Dropbox löschen
                DropboxCommunication dropbox = new DropboxCommunication(ProgrammSettings.DropBoxAppToken);
                dropbox.DeleteFolderAsync(SelectedDirName);
                UpdateDirNamesAsync();
            }

        }

        internal void OnDeleteFilesCommandExecute()
        {
            if (SelectedObjects.Length > 0)
            {
                Progress = 0;
                NumberOfFiles = SelectedObjects.Count();
                foreach (var item in SelectedObjects)
                {
                    Progress++;
                    // Datei lokal löschen
                    string fileNameLocal = item.ToString();
                     File.Delete(fileNameLocal);
                    var fileNameDropBox = Path.GetFileName(item.ToString());
                    // Datei in der Dropbox löschen
                    DropboxCommunication dropbox = new DropboxCommunication(ProgrammSettings.DropBoxAppToken);
                    dropbox.DeleteFileAsync(ProgrammSettings.NameOfCurrentDir, fileNameDropBox);
                }
                Progress = 0;
                UpdateDirNamesAsync();
            }
        }

        /// <summary>
        /// Abruf der aktuellen Version der App
        /// </summary>
        public string AppVersion => "Version: " + Information.GetAppVersion();

        public int ArtOfPicturePresentation
        {
            get
            {
                return (int)ProgrammSettings.ArtOfPicturePresentation;
            }
            set
            {
                ProgrammSettings.ArtOfPicturePresentation = (ArtOfPicturePresentation)value;
                OnPropertyChanged(nameof(ArtOfPicturePresentation));
            }
        }

        public bool AutoChangeContentSections
        {
            get
            {
                return ProgrammSettings.AutoChangeContentSections;
            }
            set
            {
                ProgrammSettings.AutoChangeContentSections = value;
                OnPropertyChanged(nameof(AutoChangeContentSections));
            }
        }

        public bool AutomaticColor
        {
            get
            {
                return ProgrammSettings.AutomaticColor;
            }
            set
            {
                ProgrammSettings.AutomaticColor = value;
                OnPropertyChanged(nameof(AutomaticColor));
            }
        }

        public Windows.UI.Color BackgroundColor
        {
            get
            {
                return ProgrammSettings.BackgroundColor;
            }
            set
            {
                ProgrammSettings.BackgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public bool BreakOperation
        {
            get
            {
                return breakUpload;
            }
            set
            {
                breakUpload = value;
                OnPropertyChanged(nameof(BreakOperation));
            }
        }

        public RelayCommand BreakUploadCommand { get; set; }

        public RelayCommand ChangeActivDir { get; set; }

        public RelayCommand CheckLocationCommand { get; set; }

        public RelayCommand DeleteDirectoryCommand { get; set; }

        public RelayCommand DeleteAllMessageFromBaaS { get; set; }

        public bool ClockIsActiv
        {
            get { return ProgrammSettings.ClockIsActiv; }
            set
            {
                ProgrammSettings.ClockIsActiv = value;
                OnPropertyChanged(nameof(ClockIsActiv));
            }
        }

        public int ClockModus
        {
            get
            {
                return (int)ProgrammSettings.ClockModus;
            }
            set
            {
                ProgrammSettings.ClockModus = (ClockMode)value;
                OnPropertyChanged(nameof(ClockModus));
            }
        }

        public RelayCommand CreateFolderAndUploadFilesInDropboxCommand { get; set; }

        public RelayCommand DeleteFilesCommand { get; set; }

        /// <summary>
        /// Liste der aktuellen Verzeichnisnamen der Bilder
        /// </summary>
        public ObservableCollection<string> DirNames { get; }

        public RelayCommand DownloadFromDropBoxCommand { get; set; }

        public string DropBoxAppToken
        {
            get
            {
                return ProgrammSettings.DropBoxAppToken;
            }
            set
            {
                ProgrammSettings.DropBoxAppToken = value;
                OnPropertyChanged(nameof(DropBoxAppToken));
            }
        }

        /// <summary>
        /// Liste der aktuellen Dateinamen der Bilder
        /// </summary>
        public ObservableCollection<string> FileNames { get; }

        public Windows.UI.Color ForegroundColor
        {
            get
            {
                return ProgrammSettings.ForegroundColor;
            }
            set
            {
                ProgrammSettings.ForegroundColor = value;
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }

        public RelayCommand GetLocationCommand { get; set; }

        

        public bool HomeIsActiv
        {
            get { return ProgrammSettings.HomeIsActiv; }
            set
            {
                ProgrammSettings.HomeIsActiv = value;
                OnPropertyChanged(nameof(HomeIsActiv));
            }
        }

        public RelayCommand LoadKeysCommand { get; set; }

        public string Location
        {
            get
            {
                return ProgrammSettings.Location;
            }
            set
            {
                ProgrammSettings.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }


        public bool MapIsActiv
        {
            get { return ProgrammSettings.MapIsActiv; }
            set
            {
                ProgrammSettings.MapIsActiv = value;
                OnPropertyChanged(nameof(MapIsActiv));
            }
        }

        public bool CanvasIsActiv
        {
            get
            {
                return ProgrammSettings.CanvasIsActiv;
            }
            set
            {
                ProgrammSettings.CanvasIsActiv = value;
                OnPropertyChanged(nameof(CanvasIsActiv));
            }
        }

        public bool MessageIsActiv
        {
            get
            {
                return ProgrammSettings.MessageIsActiv;
            }
            set
            {
                ProgrammSettings.MessageIsActiv = value;
                OnPropertyChanged(nameof(MessageIsActiv));
            }
        }

        public string MapsAPIKey
        {
            get
            {
                return ProgrammSettings.MapsAPIKey;
            }
            set
            {
                ProgrammSettings.MapsAPIKey = value;
                OnPropertyChanged(nameof(MapsAPIKey));
            }
        }

        public string NETKey
        {
            get
            {
                return ProgrammSettings.NetKey;
            }
            set
            {
                ProgrammSettings.NetKey = value;
                OnPropertyChanged(nameof(MapsAPIKey));
            }
        }

        public string ApplicationID
        {
            get
            {
                return ProgrammSettings.ApplicationId;
            }
            set
            {
                ProgrammSettings.ApplicationId = value;
                OnPropertyChanged(nameof(ApplicationId));
            }
        }


        public string Message
        {
            get => ProgrammSettings.Message;
            set
            {
                ProgrammSettings.Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string NewsAPIKey
        {
            get
            {
                return ProgrammSettings.NewsAPIKey;
            }
            set
            {
                ProgrammSettings.NewsAPIKey = value;
                OnPropertyChanged(nameof(NewsAPIKey));
            }
        }

        public int NewsCategoryIndex
        {
            get
            {
                return (int)ProgrammSettings.NewsCategory;
            }
            set
            {
                ProgrammSettings.NewsCategory = (Category)value;
                OnPropertyChanged("NewsCategory");
            }
        }

        public bool NewsIsActiv
        {
            get { return ProgrammSettings.NewsIsActiv; }
            set
            {
                ProgrammSettings.NewsIsActiv = value;
                OnPropertyChanged(nameof(NewsIsActiv));
            }
        }

        public int NumberOfFiles
        {
            get
            {
                return numberOfFiles;
            }
            set
            {
                numberOfFiles = value;
                OnPropertyChanged(nameof(NumberOfFiles));
            }
        }

        public string OpenWeatherMapKey
        {
            get
            {
                return ProgrammSettings.OpenWeatherMapKey;
            }
            set
            {
                ProgrammSettings.OpenWeatherMapKey = value;
                OnPropertyChanged(nameof(OpenWeatherMapKey));
            }
        }

        public bool PictureIsActiv
        {
            get { return ProgrammSettings.PictureIsActiv; }
            set
            {
                ProgrammSettings.PictureIsActiv = value;
                OnPropertyChanged(nameof(PictureIsActiv));
            }
        }

        public int PictureSelectionIndex
        {
            get => (int)ProgrammSettings.PictureSeletionMode;
            set
            {
                ProgrammSettings.PictureSeletionMode = (PictureSeletionMode)value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public RelayCommand RestartAppCommmand { get; set; }

        public RelayCommand SaveKeysCommand { get; set; }

        public Object[] SelectedObjects
        {
            get
            {
                return selectedObjects;
            }
            set
            {
                selectedObjects = value;
                OnPropertyChanged(nameof(SelectedObjects));
            }
        }
        public int ShowDurationClock
        {
            get
            {
                return ProgrammSettings.ShowDurationClock;
            }
            set
            {
                ProgrammSettings.ShowDurationClock = value;
                OnPropertyChanged(nameof(ShowDurationClock));
            }
        }

        public int ShowDurationHome
        {
            get
            {
                return ProgrammSettings.ShowDurationHome;
            }
            set
            {
                ProgrammSettings.ShowDurationHome = value;
                OnPropertyChanged(nameof(ShowDurationHome));
            }
        }

        public int ShowDurationMap
        {
            get
            {
                return ProgrammSettings.ShowDurationMap;
            }
            set
            {
                ProgrammSettings.ShowDurationMap = value;
                OnPropertyChanged(nameof(ShowDurationMap));
            }
        }


        public int ShowDurationCanvas
        {
            get
            {
                return ProgrammSettings.ShowDurationCanvas;
            }
            set
            {
                ProgrammSettings.ShowDurationCanvas = value;
                OnPropertyChanged(nameof(ShowDurationCanvas));
            }
        }

        public int ShowDurationMessage
        {
            get
            {
                return ProgrammSettings.ShowDurationMessage;
            }
            set
            {
                ProgrammSettings.ShowDurationMessage = value;
                OnPropertyChanged(nameof(ShowDurationMessage));
            }
        }

        public int ShowDurationNews
        {
            get
            {
                return ProgrammSettings.ShowDurationNews;
            }
            set
            {
                ProgrammSettings.ShowDurationNews = value;
                OnPropertyChanged(nameof(ShowDurationNews));
            }
        }

        public int ShowDurationPictures
        {
            get
            {
                return ProgrammSettings.ShowDurationPictures;
            }
            set
            {
                ProgrammSettings.ShowDurationPictures = value;
                OnPropertyChanged(nameof(ShowDurationPictures));
            }
        }

        public int ShowDurationWeather
        {
            get
            {
                return ProgrammSettings.ShowDurationWeather;
            }
            set
            {
                ProgrammSettings.ShowDurationWeather = value;
                OnPropertyChanged(nameof(ShowDurationWeather));
            }
        }

        public bool ShowSeconds
        {
            get
            {
                return ProgrammSettings.ShowSeconds;
            }
            set
            {
                ProgrammSettings.ShowSeconds = value;
                OnPropertyChanged(nameof(ShowSeconds));
            }
        }

        public bool ShowDate
        {
            get
            {
                return ProgrammSettings.ShowDate;
            }
            set
            {
                ProgrammSettings.ShowDate = value;
                OnPropertyChanged(nameof(ShowDate));
            }
        }
            

        public RelayCommand ShutDownCommand { get; set; }

        public int TimeInSecondToChangeNewsMessage
        {
            get
            {
                return ProgrammSettings.TimeInSecondToChangeNewsMessage;
            }
            set
            {
                ProgrammSettings.TimeInSecondToChangeNewsMessage = value;
                OnPropertyChanged(nameof(TimeInSecondToChangeNewsMessage));
            }
        }

        public int TimeInSecondToChangePicture
        {
            get
            {
                return ProgrammSettings.TimeInSecondToChangePicture;
            }
            set
            {
                ProgrammSettings.TimeInSecondToChangePicture = value;
                OnPropertyChanged(nameof(TimeInSecondToChangePicture));
            }
        }

        public RelayCommand UploadFileInDropboxCommand { get; set; }

        public bool WeatherIsActiv
        {
            get { return ProgrammSettings.WeatherIsActiv; }
            set
            {
                ProgrammSettings.WeatherIsActiv = value;
                OnPropertyChanged(nameof(WeatherIsActiv));
            }
        }

        public int WeatherModus
        {
            get
            {
                return (int)ProgrammSettings.WeatherModus;
            }
            set
            {
                ProgrammSettings.WeatherModus = (WeatherMode)value;
                OnPropertyChanged(nameof(WeatherModus));
            }
        }

        internal async void UpdateFilesNamesAsync(string dirName)
        {
            ProgrammSettings.NameOfCurrentDir = dirName;
            StorageFolder homeStorageFolder = ApplicationData.Current.LocalFolder;
            var currentFolder = await homeStorageFolder.GetFolderAsync(dirName);
            IReadOnlyList<StorageFile> files = await currentFolder.GetFilesAsync();
            FileNames.Clear();
            foreach (var item in files)
            {
                FileNames.Add(item.Path);
            }
        }

        /// <summary>
        /// Prüfen, ob eine Datei (string Filenname) im Ordner (StorgeFile folder) existiert.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static async Task<bool> FileExistsInStorageFolderAsync(StorageFolder folder, string fileName)
        {
            try
            {
                var item = await folder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<StorageFile> LoadFileResizeAndSaveAsync(StorageFile file, StorageFolder storageFolder)
        {
            var newFile = await file.CopyAsync(storageFolder);
            using (Image image = Image.Load(newFile.Path))
            {
                // Größe Berechnen
                const double widthFullHD = 1920;
                const double heighFullHD = 1080;
                double width = image.Width;
                double heigh = image.Height;
                if ((width > widthFullHD) && (heigh > heighFullHD))
                {
                    double ratioWidth = width / widthFullHD;
                    double ratioHeight = heigh / heighFullHD;

                    double newHeight;
                    double newWidth;

                    if (ratioWidth > ratioHeight)
                    {
                        newHeight = heighFullHD;
                        newWidth = newHeight / heigh * width;
                    }
                    else
                    {
                        newWidth = widthFullHD;
                        newHeight = newWidth / width * heigh;
                    }

                    image.Mutate(x => x
                        .Resize((int)newWidth, (int)newHeight).AutoOrient());
                    image.Save(newFile.Path);
                }
            }

            return newFile;
        }

        /// <summary>
        /// Erstellt die Ordner (lokal) und läd alle Bilddaeien herunter.
        /// </summary>
        private async void GetFilesFromDropBoxAsync()
        {
            // Pfad für Speicherung der Datei
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            // alle Ordner im aktuellen Verzeichnis löschen
            var folders = await storageFolder.GetFoldersAsync();
            foreach (var item in folders)
            {
                await item.DeleteAsync();

            }

            DropboxCommunication dropboxCommunication = new DropboxCommunication(ProgrammSettings.DropBoxAppToken);
            var list = await dropboxCommunication.GetListOfFilesAsync();
            NumberOfFiles = list.Entries.Count;
            Progress = 0;
            for (int i = 0; i < list.Entries.Count; i++)
            {
                if (BreakOperation)
                {
                    Progress = 0;
                    break;
                }
                Progress = i + 1;
                if (list.Entries[i].IsFolder)
                {
                    await storageFolder.CreateFolderAsync(list.Entries[i].Name, CreationCollisionOption.ReplaceExisting);
                }
                else
                {
                    string localFileName = storageFolder.Path + "/" + list.Entries[i].PathLower;
                    if (File.Exists(localFileName) == false)
                    {
                        // Datei herunterladen
                        IDownloadResponse<FileMetadata> file = await dropboxCommunication.DownloadFileAsync(list.Entries[i].PathLower);
                        // Datenstream in lokale Datei schreiben
                        using (var fileStream = File.Create(localFileName))
                        {
                            (await file.GetContentAsStreamAsync()).CopyTo(fileStream);
                        }

                    }
                }
            }
            Progress = 0;
            UpdateDirNamesAsync();
            if (DirNames.Count > 0)
                UpdateFilesNamesAsync(DirNames[0]);

        }

        /// <summary>
        /// Command zum Abbrechen eines laufenden Uploads/ Downloads (Bilder)
        /// </summary>
        private void OnBreakUploadCommandExecute()
        {
            BreakOperation = true;
        }
        private async void OnCheckLocationCommandExecuteAsync()
        {
            var location = Location;
            OpenWeatherMapProxyForecast openWeatherMapProxyForecast = new OpenWeatherMapProxyForecast(ProgrammSettings.OpenWeatherMapKey);
            RootObject myWeather = await openWeatherMapProxyForecast.GetWeather(location);
            if (myWeather == null)
            {
                MessageDialog dialog = new MessageDialog("Ortsname nicht korrekt", "Fehler");
                await dialog.ShowAsync();
                Location = "Erfurt";
            }
        }

        private async void OnCreateFolderAndUploadFilesInDropboxCommandAsync()
        {
            // lokaler Speicherort
            StorageFolder homeStorageFolder = ApplicationData.Current.LocalFolder;
            // Auswahl des Ordners => Aufruf des Dialoges
            FolderPicker folderPicker = new FolderPicker()
            {
                ViewMode = PickerViewMode.List
            };
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder mySourceFolder = await folderPicker.PickSingleFolderAsync();
            if (mySourceFolder != null)
            {
                var options = new QueryOptions(); // Abfragefilter definieren
                options.FileTypeFilter.Add(".jpg"); // alle *.jpg-Dateien
                StorageFileQueryResult query = mySourceFolder.CreateFileQueryWithOptions(options);
                IReadOnlyList<StorageFile> fileList = await query.GetFilesAsync();
                if (fileList.Count > 0)
                {
                    // Ordner enthält Bildateien
                    string folderName = mySourceFolder.Name;

                    // prüfen, ob der Ordner bereits exitiert (Dropbox)

                    // Ordner lokal erstellen
                    StorageFolder storageFolder = await homeStorageFolder.CreateFolderAsync(folderName, CreationCollisionOption.GenerateUniqueName);

                    // Ordner in Dropbox erstellen
                    DropboxCommunication myDropboxCommunication = new DropboxCommunication(ProgrammSettings.DropBoxAppToken);
                    await myDropboxCommunication.CreateFolderAsync("/" + mySourceFolder.Name);

                    Progress = 0;
                    NumberOfFiles = fileList.Count;
                    BreakOperation = false;

                    foreach (var file in fileList)
                    {
                        if (BreakOperation)
                        {
                            Progress = 0;
                            break;
                        }
                        Progress++;
                        // Datei lokal kopieren
                        StorageFile newFile = await LoadFileResizeAndSaveAsync(file, storageFolder);
                        // Datei in die Dropbox kopieren
                        await myDropboxCommunication.UploadFileAsync(mySourceFolder.Name, newFile.Name, newFile.Path);
                    }
                    Progress = 0;
                }
                else
                {
                    // Ordner enthält keine Bilddateien
                    MessageDialog messageDialog = new MessageDialog("Keine Bilddateien vorhanden", "Hinweis");
                    await messageDialog.ShowAsync();
                }
            }
            UpdateDirNamesAsync();
        }

        private void OnDownloadFromDropBoxCommandExecuteAsync()
        {
            FileNames.Clear();
            GetFilesFromDropBoxAsync();
        }

        private async void OnGetLocationCommandExecuteAsync()
        {
            Geoposition position = await LocationService.GetPositionAsync();
            if (position != null)
            {
                OpenWeatherMapProxyForecast openWeatherMapProxyForecast = new OpenWeatherMapProxyForecast(ProgrammSettings.OpenWeatherMapKey);
                RootObject myWeather = await openWeatherMapProxyForecast.GetWeather(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);
                if (myWeather != null)
                {
                    if (myWeather.city.name != "")
                        Location = myWeather.city.name;
                }
            }
        }

        private void OnLoadKeysCommandExecute()
        {
            ProgrammSettings.LoadKeysAsync();
        }

        private async void OnRestartCommandExecuteAsync()
        {
            var result = await CoreApplication.RequestRestartAsync("Neustart der App");
            if (result == AppRestartFailureReason.NotInForeground ||
              result == AppRestartFailureReason.RestartPending ||
              result == AppRestartFailureReason.Other)
            {
                var msgBox = new MessageDialog("Neustart fehlgeschlagen", result.ToString());
                await msgBox.ShowAsync();
            }
        }

        private void OnSaveKeysCommandExecute()
        {
            ProgrammSettings.SaveKeysAsync();
        }
        private void OnShutDownCommandExecute()
        {
            ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, TimeSpan.FromSeconds(1));
        }

        private async void OnUploadFileInDropboxCommandExecuteAsync()
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Datei lokal speichern, d.h. kopieren
                StorageFolder homeStorageFolder = ApplicationData.Current.LocalFolder;
                var storageFolder = await homeStorageFolder.GetFolderAsync(ProgrammSettings.NameOfCurrentDir);
                // Prüfen, ob Datei schon existiert
                if (await FileExistsInStorageFolderAsync(storageFolder, Path.GetFileName(file.Path)).ConfigureAwait(true) == false)
                {
                    // wenn die Datei noch nicht existiert
                    StorageFile newFile = await LoadFileResizeAndSaveAsync(file, storageFolder);

                    // Datei in die Dropbox hochladen
                    DropboxCommunication dropbox = new DropboxCommunication(ProgrammSettings.DropBoxAppToken);
                    await dropbox.UploadFileAsync(ProgrammSettings.NameOfCurrentDir, newFile.Name, newFile.Path);
                    // Dateiname der Liste hinzufügen
                    FileNames.Add(newFile.Path);
                }
            }
        }

        private async void UpdateDirNamesAsync()
        {
            DirNames.Clear();
            StorageFolder homeStorageFolder = ApplicationData.Current.LocalFolder;
            var folders = await homeStorageFolder.GetFoldersAsync();
            if (folders.Count > 0)
            {
                foreach (var item in folders)
                {
                    DirNames.Add(item.Name);
                }     
            }

            if (DirNames.Count > 0)
            {
                var selectDirName = DirNames[0];
                foreach (var item in DirNames)
                {
                    if (item == ProgrammSettings.NameOfCurrentDir)
                        selectDirName = ProgrammSettings.NameOfCurrentDir;
                }
                UpdateFilesNamesAsync(selectDirName);
            }
        }
    }
}
