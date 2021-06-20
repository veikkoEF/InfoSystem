using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BaaSCommunication
{
    public class BaaS
    {
        public BaaS(string applicationId, string netKey, string serverURL)
        {
            try
            {
                // Parse Initlization
                ParseClient.Initialize(new ParseClient.Configuration
                {
                    ApplicationId = applicationId,
                    WindowsKey = netKey,
                    Server = serverURL
                });
            }
            catch
            {
            }
        }

        public async Task<ObservableCollection<MessageData>> GetMessagesAsync()
        {
            var query = ParseObject.GetQuery("Message").OrderByDescending("createdAt");
            if (query != null)
            {
                IEnumerable<ParseObject> results = await query.FindAsync();
                ObservableCollection<MessageData> list = new ObservableCollection<MessageData>();
                foreach (var item in results)
                {
                    MessageData myMessageData = new MessageData();
                    string myMessage = item.Get<string>("Message");
                    string myUserName = item.Get<string>("UserName");
                    DateTime myDate = (DateTime)item.CreatedAt;

                    // Opionale Elemente
                    if (item.ContainsKey("imageFile"))
                    {
                        var url = item.Get<ParseFile>("imageFile").Url;
                        //var img  = new HttpClient().GetStreamAsync(url);

                        var httpClient = new HttpClient();
                        Stream st = await httpClient.GetStreamAsync(url);
                        var memoryStream = new MemoryStream();
                        await st.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(memoryStream.AsRandomAccessStream());

                        myMessageData.BitmapImage = bitmapImage;
                    }
                    myMessageData.Message = myMessage;
                    myMessageData.Date = myDate;
                    myMessageData.UserName = myUserName;
                    list.Add(myMessageData);
                }
                return list;
            }
            else
                return null;
        }

        public async void DeleteMessagesAsync()
        {
            // Sicherheitsabfrage
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Löschen aller Nachrichten",
                Content = "Wollen Sie alle Nachrichten löschen? Das kann nicht rückgängig gemacht werden",
                PrimaryButtonText = "Löschen",
                CloseButtonText = "Abbrechen"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var query = ParseObject.GetQuery("Message");
                IEnumerable<ParseObject> results = await query.FindAsync();
                foreach (var item in results)
                {
                    await item.DeleteAsync();
                }
            }
        }
    }
}