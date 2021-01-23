using MyHome.Data;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyHome
{
    public class BaaSCommunication
    {
        public BaaSCommunication()
        {
            // Parse Initlization
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = Settings.ProgrammSettings.ApplicationId,
                WindowsKey = Settings.ProgrammSettings.NetKey,
                Server = Settings.ProgrammSettings.ServerURL
            });
        }

        public async Task<ObservableCollection<MessageData>> GetMessagesAsync()
        {
            var query = ParseObject.GetQuery("Message").OrderByDescending("createdAt");

            IEnumerable<ParseObject> results = await query.FindAsync();
            ObservableCollection<MessageData> list = new ObservableCollection<MessageData>();
            foreach (var item in results)
            {
                string myMessage = item.Get<string>("Message");
                string myUserName = item.Get<string>("UserName");
                DateTime myDate = (DateTime) item.CreatedAt;

                MessageData myMessageData = new MessageData();
                myMessageData.Message = myMessage;
                myMessageData.Date = myDate;
                myMessageData.UserName = myUserName;

                list.Add(myMessageData);
                
            }
            return list;
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
                //foreach (var item in results)
                //{
                //    await item.DeleteAsync();
                //}
            }
        }



    }
}
