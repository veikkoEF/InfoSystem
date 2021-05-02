using BaaSCommunication;
using MyHome.Helpers;
using System;
using System.Collections.ObjectModel;

namespace MyHome.ViewModels
{
    public class MessageControlViewModel : Observable
    {
        public MessageControlViewModel()
        {
            Messages = new ObservableCollection<MessageData>();
            DeleteAllMessages = new RelayCommand(DeleteAllMessagesExecuteAsync);
            GetMessagagesFromBackendAsync();
        }

        private void DeleteAllMessagesExecuteAsync()
        {
            BaaS baas = new BaaS(Settings.ProgrammSettings.ApplicationId, Settings.ProgrammSettings.NetKey, Settings.ProgrammSettings.ServerURL);
            baas.DeleteMessagesAsync();
        }

        public RelayCommand DeleteAllMessages { get; set; }

        public ObservableCollection<MessageData> Messages { get; set; }



        public string LiveTime => DateTime.Now.ToString("dddd, dd. MMMM yyyy");


        private async void GetMessagagesFromBackendAsync()
        {
            BaaS baas = new BaaS(Settings.ProgrammSettings.ApplicationId, Settings.ProgrammSettings.NetKey, Settings.ProgrammSettings.ServerURL);
            var newMessages = await baas.GetMessagesAsync();
            if (newMessages != null)
            {
                foreach (var item in newMessages)
                {
                    Messages.Add(item);
                }
            }
        }
    }
}
