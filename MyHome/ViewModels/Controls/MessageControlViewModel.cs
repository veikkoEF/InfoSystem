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

        


        private async void GetMessagagesFromBackendAsync()
        {
            // Testdaten
            MessageData item1 = new MessageData() { Message = "Test 1", Date = DateTime.Now };
            MessageData item2 = new MessageData() { Message = "Test 2", Date = DateTime.Now };
            Messages.Add(item1);
            Messages.Add(item2);

            // Commands


            BaaSCommunication baas = new BaaSCommunication();
            var newMessages  = await baas.GetMessagesAsync();
            foreach (var item in newMessages)
            {
                Messages.Add(item);
            }
        }
    }
}
