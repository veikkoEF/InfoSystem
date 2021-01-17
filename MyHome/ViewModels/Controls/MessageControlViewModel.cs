using MyHome.Data;
using MyHome.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.ViewModels
{
    public class MessageControlViewModel : Observable
    {
        public MessageControlViewModel()
        {
            Messages = new ObservableCollection<MessageData>();
            GetMessagagesFromBackendAsync();
        }

        public ObservableCollection<MessageData> Messages { get; set; }



        public string LiveTime => DateTime.Now.ToString("dddd, dd. MMMM yyyy");


        private async void GetMessagagesFromBackendAsync()
        {
            BaaSCommunication baas = new BaaSCommunication();
            var newMessages  = await baas.GetMessagesAsync();
            foreach (var item in newMessages)
            {
                Messages.Add(item);
            }
        }
    }
}
